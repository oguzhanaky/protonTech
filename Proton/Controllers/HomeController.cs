using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtonDb.Projects;

namespace Proton.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
 
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult CompletedTasks()
        {
            return View();
        }

        public ActionResult OngoingTasks()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Logon()
        {
            return View();
        }

        public ActionResult Manager()
        {
            return View();
        }

        [HttpGet]
        public object GetProjects()
        {
            List<ProjectModel> projectList = ProjectService.GetProjects();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return oSerializer.Serialize(projectList);
        }
	}
}