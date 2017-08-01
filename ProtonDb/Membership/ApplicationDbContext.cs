using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonDb.Membership
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Baglanilacak VeriTabani ismi buraya yazilacak.
        /// </summary>
        public ApplicationDbContext()
            : base("ProtonTeknik", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public static  GetUsers(string searchKey, string roleName)
        //{
        //    UserRepository repo = new UserRepository();
        //    return repo.GetUsers(searchKey, roleName);
        //}
    }
}
