using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ProtonDb.Common
{
    public class DbHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }

        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
