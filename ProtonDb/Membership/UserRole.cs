using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonDb.Membership
{
    public class UserRole : IdentityRole
    {
        public UserRole()
        {

        }

        public UserRole(string roleName, string companyDomainName, string displayName)
        {
            this.Name = roleName;
            this.CompanyDomainName = companyDomainName;
            this.DisplayName = displayName;
        }

        public string CompanyDomainName { get; set; }

        public string DisplayName { get; set; }
    }
}
