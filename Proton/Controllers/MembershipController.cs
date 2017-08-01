using Proton.Models;
using ProtonDb.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using System.Security.Claims;

namespace Proton.Controllers
{
    public class MembershipController : Controller
    {
        public UserManager<ApplicationUser> userManager;
       // public RoleManager<ApplicationRole> roleManager;

        public MembershipController()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            //roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Name = model.Name;
                user.LastName = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;
                IdentityResult iResult = userManager.Create(user, model.Password);
                if (iResult.Succeeded)
                {
                    // User isminde bir Role ataması yapacağız. Bu rolü ilerleyen kısımda oluşturacağız
                    userManager.AddToRole(user.Id, "User");
                    return RedirectToAction("Login","Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUser", "Kullanıcı ekleme işleminde hata!");
                }
            }
            return View(model);
        }

        [HttpPost]
        public object Logon(Proton.Models.UserLogonModel model)
        {
            if (ModelState.IsValid)
            {
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                ApplicationUser user = userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;//No owin.Environment item was found in the context.
                    ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    AuthenticationProperties authProps = new AuthenticationProperties();
                    authManager.SignIn(authProps, identity);
                    //var requestContext = System.Web.HttpContext.Current.Request.RequestContext;
                    //return new UrlHelper(requestContext).Action("AboutUs", "Home");      
                    return oSerializer.Serialize(new { Success = true });
                    //return RedirectToAction("AboutUs", "Home");
                }
                else
                {
                    return oSerializer.Serialize(new { Success = false });
                }
            } 
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}