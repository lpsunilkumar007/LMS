using System;

namespace OnlineTestApp.DomainLogic.Admin.Settings
{
    public static class FileSystemDomainLogic
    {
        #region Private 
        /// <summary>
        /// 
        /// </summary>
        static string AbsolutePath
        {
            get
            {
                if (UserVariables.IsAuthenticated)
                {
                    return RootPath + "/Upload/" + UserVariables.UserCompanyId + "/";
                }
                return RootPath + "/Upload/Anonymous/";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        static string RootPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        static void CreateDirectory(string path)
        {
            Utilities.FileSystem.CreateDirectory(path);
        }
        #endregion

        /// <summary>
        /// returns relative path
        /// </summary>
        public static string GetEditorImageBodyPath
        {
            get
            {
                string path =  "/EmailResources/";
                CreateDirectory(RootPath + path);
                return path;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public static string GetCandidateEmailLogBodyPath(Guid candidateId)
        {
            string path = AbsolutePath + "/Candidate/" + candidateId + "/EmailLog/EmailBody/";
            CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string TempFolderPath
        {
            get
            {
                string path = "";
                if(UserVariables.IsAuthenticated)
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + UserVariables.UserCompanyId + "/";
                }
                else
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "/Temp/Anonymous/";
                }
                CreateDirectory(path);
                return path;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string TempFolderRelativePath
        {
            get
            {
                string path = "";
                if (UserVariables.IsAuthenticated)
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + UserVariables.UserCompanyId + "/";
                }
                else
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "/Temp/Anonymous/";
                }
                CreateDirectory(path);
                return path;
            }
        }
    }
}
