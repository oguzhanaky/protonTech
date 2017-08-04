using ProtonDb.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proton.Controllers
{
    public class ManagementController : Controller
    {
        [HttpPost]
        public Boolean SaveProject(ProjectModel project)
        {
            return ProjectService.SaveProject(project);
        }
	}
}