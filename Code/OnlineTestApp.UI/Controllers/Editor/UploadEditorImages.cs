using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.Editor
{
    public partial class EditorController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="CKEditorFuncNum"></param>
        /// <param name="CKEditor"></param>
        /// <param name="langCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadEmailTemplateImages(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string message = "", url = ""; // message to display (optional)
            if (Utilities.Validations.IsValidImageExtension(upload.FileName))
            {
                //Change this path
                string fileName = Path.GetFileNameWithoutExtension(upload.FileName)
                    + "_" + Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                string folderName = DomainLogic.Admin.Settings.FileSystemDomainLogic.GetEditorImageBodyPath;
                using (Stream file = System.IO.File.Create(Server.MapPath(folderName) + fileName))
                {
                    upload.InputStream.CopyTo(file);
                }
                url = Url.Content(SystemSettings.CurrentDomain + folderName + fileName);
            }
            else
            {
                message = "Please select valid image";
            }


            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" +
          url + "\", \"" + message + "\");</script></body></html>";
            return Content(output);
        }
    }
}