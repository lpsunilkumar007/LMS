using System.IO;
using System;

namespace HelperClasses
{
    internal static class FileSystem
    {
        public static bool fileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// To get application install directory.
        /// Returns '\' at the end
        /// </summary>
        public static string ApplicationDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        /// <summary>
        /// check directory exists or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool directoryExists(string path)
        {
            return Directory.Exists(path);
        }
        /// <summary>
        /// create directory
        /// </summary>
        /// <param name="path"></param>
        public static void createDirectory(string path)
        {
            path = Path.GetDirectoryName(path);
            if (!directoryExists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// get file Extension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string getExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }
        /// <summary>
        /// get file Name without extension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string getFileNameWithoutExtension(string fileName)
        {
            if (fileName == "") return "";
            return Path.GetFileNameWithoutExtension(fileName);
        }
        public static void deleteFile(string path)
        {
            if (path == "") return;
            if (!fileExists(path)) return;
            File.Delete(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string getFileNameWithExtension(string fileName)
        {
            if (fileName == "") return "";
            return Path.GetFileName(fileName);
        }
        /// <summary>
        /// FileName with extension and filePath without File Name
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string getFileName(string filePath, string fileName)
        {
            int count = 1;
            string newfileName = getFileNameWithoutExtension(fileName);
            string ext = getExtension(fileName);
            string prefix = newfileName;
            while (fileExists(filePath + fileName))
            {
                newfileName = prefix + "_" + count.ToString();
                count++;
                fileName = newfileName + ext;
            }
            return newfileName + ext;
        }
    }
}
