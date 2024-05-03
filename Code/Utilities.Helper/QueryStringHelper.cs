using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    public static class QueryStringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetQueryStringVaue(string keyName)
        {
            if (HttpContext.Current == null) return string.Empty;
            if (HttpContext.Current.Request == null) return string.Empty;
            return HttpContext.Current.Request.QueryString[keyName];
        }        
        /// <summary>
        /// used to get Interger Value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>If no integer value or query string not exists then returns False</returns>
        public static bool GetIntValue(string key, out int value)
        {
            value = 0;
            string data = GetQueryStringVaue(key);
            if (data == "") return false;
            if (!Validations.IsNumeric(data)) return false;
            value = Convert.ToInt32(data); return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetIntValue(string key)
        {
            return Convert.ToInt32(GetQueryStringVaue(key));
        }
        /// <summary>
        /// used to get Interger Value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>If no integer value or query string not exists then returns False</returns>
        public static int? GetNullIntValue(string key)
        {
            string data = GetQueryStringVaue(key);
            if (data == "") return null;
            if (Validations.IsNumeric(data)) { return Convert.ToInt32(data); }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetStringValue(string key, out string value)
        {
            value = string.Empty;
            string data = GetQueryStringVaue(key);
            if (data == "") return false;
            if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) return false;
            value = data; return true;
        }
        /// <summary>
        /// get query string value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValue(string key)
        {
            return GetQueryStringVaue(key);
        }

    }
}
