using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace ProtonDb.Administrator
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Yeni Sütunlar(Özellikler) buraya eklenecek
        /// </summary>
        public string Name { get; set; }

        public string LastName { get; set; }


        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(MetueceUserManager manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }
}
