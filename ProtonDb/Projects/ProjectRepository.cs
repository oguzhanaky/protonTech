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
            int affectedRows = 0;
            using (SqlConnection connection = new SqlConnection(CONN))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO Proje (ProjeAdi, ProjeSehir, ProjeIlce, ProjeDurum, ProjeAciklama) VALUES (@ProjeAdi, @ProjeSehir, @ProjeIlce, @ProjeDurum, @ProjeAciklama)";

                connection.Open();
                command.Parameters.AddWithValue("@ProjeAdi", project.ProjeAdi);
                command.Parameters.AddWithValue("@ProjeSehir", project.ProjeSehir);
                command.Parameters.AddWithValue("@ProjeIlce", project.ProjeIlce);
                command.Parameters.AddWithValue("@ProjeDurum", project.ProjeDurum);
                command.Parameters.AddWithValue("@ProjeAciklama", project.ProjeAciklama);

                affectedRows = command.ExecuteNonQuery();
                connection.Close();
            }

            if (affectedRows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ProjectPhotoModel> GetProjectPhotos(int projectId)
        {
            List<ProjectPhotoModel> ProjectPhotosList = new List<ProjectPhotoModel>();
            using (SqlConnection connection = new SqlConnection(CONN))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM ProjectPhotos WHERE ProjectId = " + projectId;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProjectPhotoModel projectPhotoModel = new ProjectPhotoModel();
                        projectPhotoModel.Id = (int)reader["Id"];
                        projectPhotoModel.FileName = (string)reader["FileName"];
                        projectPhotoModel.ProjectId = (int)reader["ProjectId"];
                        projectPhotoModel.IsMainPhoto = (bool)reader["IsMainPhoto"];
                        ProjectPhotosList.Add(projectPhotoModel);
                    }         
                }
                connection.Close();
            }
            return ProjectPhotosList;
        }

        public List<ProjectModel> GetOnGoingTasks()
        {
            List<ProjectModel> projectList = new List<ProjectModel>();
            using (SqlConnection connection = new SqlConnection(CONN))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Proje WHERE ProjeDurum = 0";

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

        public List<ProjectModel> GetCompletedProjects()
        {
            List<ProjectModel> projectList = new List<ProjectModel>();
            using (SqlConnection connection = new SqlConnection(CONN))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Proje WHERE ProjeDurum = 1";

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
    }
}
