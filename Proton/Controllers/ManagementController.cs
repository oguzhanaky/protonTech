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


        [HttpPost]
        public object GetProjectPhotos(int projectId)
        {
            List<ProjectPhotoModel> projectPhotosList = ProjectService.GetProjectPhotos(projectId);
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return oSerializer.Serialize(projectPhotosList);
        }
        
	}
}