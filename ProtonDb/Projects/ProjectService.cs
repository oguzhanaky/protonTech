using ProtonDb.Common;
using ProtonDb.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonDb.Projects
{
    public static class ProjectService
    {
        public static List<ProjectModel> GetProjects()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            //MetueceUserManager userManager = new MetueceUserManager(userStore);
            ProjectRepository repo = new ProjectRepository();
            List<ProjectModel> projectList = new List<ProjectModel>();
            projectList = repo.GetProjects();
            return projectList;
        }

        public static Boolean SaveProject(ProjectModel project)
        {
            ProjectRepository repo = new ProjectRepository();
            return repo.SaveProject(project); 
        }
    }
}
