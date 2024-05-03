
using System.IO;
using System.Net;
using System.Web;
namespace Utilities
{
    public static class DownloadFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="downloadFileName"></param>
        /// <returns></returns>
        public static void DownloadFiles(MemoryStream memoryStream, string downloadFileName)
        {
            byte[] bytesInStream = memoryStream.ToArray(); // simpler way of converting to array
            memoryStream.Close();
            var response = HttpContext.Current.Response;
            response.Clear();
            response.ContentType = "application/force-download";
            response.AddHeader("content-disposition", "attachment;    filename=" + downloadFileName);
            response.BinaryWrite(bytesInStream);
            response.End();
        }

        /// <summary>
        /// File Path should be without Server.MapPath
        /// </summary>
        /// <param name="path"></param>
        /// <param name="downloadFileName">Name with extension that will be displayed to user</param>
        /// <param name="isAbsolutePath">if path is : C:\Windows\calc.exe then true</param>
        public static string DownloadFiles(string path, string downloadFileName = "")
        {
            if (string.IsNullOrEmpty(downloadFileName) || string.IsNullOrWhiteSpace(downloadFileName))
            {
                downloadFileName = FileSystem.GetFileNameWithOutExtension(path);
            }            
            var fi = new FileInfo(path);
            if (fi.Exists)
            {
                var type = "";
                switch (fi.Extension.ToLower())
                {

                    case ".xls":
                    case ".xlsx":
                    case ".csv":
                        type = "application/octet-stream";
                        break;
                    case ".htm":
                    case ".html":
                        type = "text/HTML";
                        break;

                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".doc":
                    case ".rtf":
                        type = "Application/msword";
                        break;

                    case ".xml":
                        type = "text/xml";
                        break;
                }
                downloadFileName = Path.GetFileNameWithoutExtension(downloadFileName);
                var req = new WebClient();
                var response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.AppendHeader("content-disposition", "attachment; filename=" + downloadFileName + fi.Extension);
                response.Buffer = true;
                response.AddHeader("Content-disposition", "attachment; filename=\"" + downloadFileName + fi.Extension + "\"");
                response.ContentType = type != "" ? type : "application/octet-stream";
                var data = req.DownloadData(path);
                response.BinaryWrite(data);
                response.End();

                return "";
            }
            return "File not found";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="downloadFileName"></param>
        /// <param name="isAbsolutePath"></param>
        /// <returns></returns>
        public static string DownloadFiles1(string path, string downloadFileName = "")
        {             
            var fi = new FileInfo(path);
            if (fi.Exists)
            {
                if (!string.IsNullOrEmpty(downloadFileName))
                {
                    downloadFileName = FileSystem.GetFileNameWithOutExtension(downloadFileName);
                }
                else
                {
                    downloadFileName = FileSystem.GetFileNameWithOutExtension(path);
                }
                var req = new WebClient();
                var response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + downloadFileName + fi.Extension + "\"");
                response.ContentType = "application/octet-stream";
                var data = req.DownloadData(path);
                response.BinaryWrite(data);
                response.End();
                return "";
            }
            return "File not found";
        }
    }
}
