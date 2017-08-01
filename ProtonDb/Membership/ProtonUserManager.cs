using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ProtonDb.Membership
{
   public class ProtonUserManager : UserManager<ApplicationUser>
    {
       public ProtonUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            this.UserValidator = new UserValidator<ApplicationUser>(this) { AllowOnlyAlphanumericUserNames = false };
        }
    }

    public class ProtonRoleManager : RoleManager<UserRole>
    {
        public ProtonRoleManager(IRoleStore<UserRole> store)
            : base(store)
        {
        }
    }
}
