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
        public JsonResult SaveFiles(string description)
        {
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;
 
                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), fileName));
 
                    Message = "File uploaded successfully";
                    flag = true;

                    /*UploadedFile f = new UploadedFile
                    {
                        FileName = actualFileName,
                        FilePath = fileName,
                        Description = description,
                        FileSize = size
                    };
                    using (MyDatabaseEntities dc = new MyDatabaseEntities())
                    {
                        dc.UploadedFiles.Add(f);
                        dc.SaveChanges();
                        Message = "File uploaded successfully";
                        flag = true;
                    }*/
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