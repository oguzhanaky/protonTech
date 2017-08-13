using ProtonDb.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProtonDb.DataUpload
{
    public class DataUploadRepository
    {
        private static readonly string CONN = DbHelper.GetConnectionString("ProtonTeknik");

        public bool saveProjectPhoto(ProjectPhotoModel projectPhotoModel)
        {
            int affectedRows = 0;
            using (SqlConnection connection = new SqlConnection(CONN))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO ProjectPhotos (ProjectId, FileName, IsMainPhoto) VALUES (@ProjectId, @FileName, @IsMainPhoto)";

                connection.Open();

                command.Parameters.AddWithValue("@ProjectId", projectPhotoModel.ProjectId);
                command.Parameters.AddWithValue("@FileName", projectPhotoModel.FileName);
                command.Parameters.AddWithValue("@IsMainPhoto", projectPhotoModel.IsMainPhoto);

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
    }
}
