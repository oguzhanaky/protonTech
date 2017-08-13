using ProtonDb.DataUpload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Proton.Controllers
{
    public class DataController : Controller
    {

        [HttpPost]
        public JsonResult SaveFiles(int projectId)
        {
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = projectId+"_0_"+Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;
 
                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), fileName));
                    //file.SaveAs(Path.Combine("http://protondb.somee.com/UploadedFiles/", fileName));
                    ProjectPhotoModel projectPhotoModel = new ProjectPhotoModel();
                    projectPhotoModel.FileName = fileName;
                    projectPhotoModel.ProjectId = projectId;
                    projectPhotoModel.IsMainPhoto = false;
                    DataUploadService dataUploadService = new DataUploadService();
                    bool savedFileRow = dataUploadService.saveProjectPhoto(projectPhotoModel);

                    if (savedFileRow == false)
                    {
                        var uri = new Uri(Path.Combine(Server.MapPath("~/UploadedFiles"), fileName), UriKind.Absolute);
                        System.IO.File.Delete(uri.LocalPath);
                    }
                    else
                    {
                        Message = "File uploaded successfully" + fileName + "server path : " + Server.MapPath("~/UploadedFiles").ToString();
                        flag = true;
                    }
                }
                catch (Exception)
                {
                    Message = "File upload failed! Please try again";
                }
 
            }
            return new JsonResult { Data = new { Message = Message, Status = flag } };
        }
    }
}