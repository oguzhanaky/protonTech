using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonDb.DataUpload
{
    public class DataUploadService
    {
        public Boolean saveProjectPhoto(ProjectPhotoModel projectPhotoModel)
        {
            DataUploadRepository datarepo = new DataUploadRepository();
            return datarepo.saveProjectPhoto(projectPhotoModel);
        }
    }
}
