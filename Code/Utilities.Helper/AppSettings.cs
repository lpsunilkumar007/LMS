using System.Configuration;

namespace Utilities
{
    public static class AppSettings
    {
        /// <summary>
        /// returns the string value
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetStringValue(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
        /// <summary>
        /// returns the Int Value
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static int GetIntValue(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName].ToType<int>();
        }
        /// <summary>
        /// returns the bool(trur false) value
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static bool GetBoolValue(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName].ToType<bool>();
        }
        /// <summary>
        /// Used to get any type of data back
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static object GetValue(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
    }
}
