using System;
using System.IO;
using System.Linq;

namespace Utilities
{
    public static class FileSystem
    {
        /// <summary>
        /// Only copy if File Exists
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="overwrite"></param>
        public static void Copy(string sourceFileName, string destFileName, bool overwrite = false)
        {
            if (File.Exists(sourceFileName))
            {
                File.Copy(sourceFileName, destFileName, overwrite);
            }
        }

        /// <summary>
        /// Only move if File Exists
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="overwrite"></param>
        public static void Move(string sourceFileName, string destFileName)
        {
            if (File.Exists(sourceFileName))
            {
                File.Move(sourceFileName, destFileName);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFullPath(string filePath)
        {
            return AppDomain.CurrentDomain.BaseDirectory + filePath;//HttpContext.Current.Server.MapPath(path);             
        }

        /// <summary>
        /// if file exists then returns full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static string CreateFilePath(string path)
        {
            if (!File.Exists(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + path;//HttpContext.Current.Server.MapPath(path);
            }
            return path;
        }
        /// <summary>
        /// get Extension from the file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// used to return file name with extension
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileNameWithExtension(string path)
        {
            return Path.GetFileName(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileNameWithOutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        /// <summary>
        /// delete the file. Returns true is file is deleted successfully else false
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isAbsolutePath">if path is : C:\Windows\calc.exe then true</param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return false;
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    return true;
                }
                catch { return false; }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isAbsolutePath">if path is : C:\Windows\calc.exe then true</param>
        /// <returns></returns>
        public static string ReadFileData(string path)
        {
            if (!File.Exists(path)) return "";
            return File.ReadAllText(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isAbsolutePath">if path is : C:\Windows\calc.exe then true</param>
        /// <returns>Returns filenName without path</returns>
        public static string CreateDirectory(string folderPath)
        {          
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        public static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_text"></param>
        /// <param name="path">With File Name</param>
        /// <param name="isAbsolutePath">if path is : C:\Windows\calc.exe then true</param>
        /// <returns>Returns filenName without path</returns>
        public static string CreateFile(string _text, string path)
        {
            if (File.Exists(path))
            {
                var name = Path.GetFileName(path);
                path = path + Guid.NewGuid().ToString() + "_" + name;
                File.Create(path).Close();
            }
            else
            {
                File.Create(path).Close();
            }
            using (var w = File.AppendText(path))
            {
                w.WriteLine(_text.Replace("\n\r", Environment.NewLine));
                w.Flush();
                w.Close();
            }
            return Path.GetFileName(path);
        }
        /// <summary>
        /// used to remove Illegal Characters In Path/ file name
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="replaceWith">Used to replace Illegal Characters</param>
        /// <returns></returns>
        public static string RemoveIllegalCharactersInPath(string filename, char replaceWith = '_')
        {
            return Path.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c, replaceWith));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static double GetFileSize(string filePath)
        {
            FileInfo f = new FileInfo(filePath);
            return f.Length;
        }
    }
}
