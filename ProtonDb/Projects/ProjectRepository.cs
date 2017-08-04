using ProtonDb.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonDb.Projects
{
    public class ProjectRepository
    {
        private static readonly string CONN = DbHelper.GetConnectionString("ProtonTeknik");

        public List<ProjectModel> GetProjects()
        {
            List<ProjectModel> projectList  = new List<ProjectModel>();
            using (SqlConnection connection = new SqlConnection(CONN))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Proje";

                //command.Parameters.AddWithValue("@name", "aa");
                //command.Parameters.AddWithValue("@userFrNm", "bb");

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    projectList = reader.DataReaderMapToList<ProjectModel>();
                    //users = reader.DataReaderMapToList<GetUserModel>();
                }
                connection.Close();
            }

            return projectList;
        }

        public Boolean SaveProject(ProjectModel project)
        {
            return true;
        }
    }
}
