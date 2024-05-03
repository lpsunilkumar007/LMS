namespace OnlineTestApp
{
    public static class SystemSettings
    {


        /// <summary>
        /// 
        /// </summary>
        public static short DefaultTestValidForDays
        {
            get
            {
                return 6;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Error404PageUrl
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("Error404PageUrl");
            }
        }

        public static string UnauthorizedPageUrl
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("UnauthorizedPageUrl");
            }
        }

        public static string LoginPageUrl
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("LoginPageUrl");
            }
        }




        /// <summary>
        /// 
        /// </summary>
        public static string PageHeadingPrefix
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("PageHeadingPrefix");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static int PageSize
        {
            get
            {
                return Utilities.AppSettings.GetIntValue("MaximumPageSize");
            }
        }

        public static string ChkEditorVersion
        {
            get
            {
                return "1.1";
            }
        }

        public static string ApplyForTestUrl
        {
            get
            {
                return CurrentDomain + "applynow/applyfortest/@@TestReferenceNumber@@";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string EmailFromName
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("EmailFromName");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string EmailFromEmailAddress
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("EmailFromEmailAddress");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string PasswordPassPhrase
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("PasswordPassPhrase");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static int PasswordIterations
        {
            get
            {
                return Utilities.AppSettings.GetIntValue("PasswordIterations");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string PasswordInitVector
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("PasswordInitVector");
            }
        }

        public static string CurrentDomain
        {
            get
            {
                return Utilities.AppSettings.GetStringValue("CurrentDomain");
            }

        }
    }
}
