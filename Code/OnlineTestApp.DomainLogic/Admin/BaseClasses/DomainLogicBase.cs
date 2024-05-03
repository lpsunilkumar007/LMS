using System;
using System.Web;

namespace OnlineTestApp.DomainLogic.Admin.BaseClasses
{
    public abstract class DomainLogicBase// : IDisposable
    {           

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="saltValue"></param>
        /// <returns></returns>
        protected string EncryptText(string value, string saltValue)
        {
            return Utilities.Encryption.EncryptText(
                 text: value,
                 saltValue: saltValue,
                 passPhrase: SystemSettings.PasswordPassPhrase,
                 passwordIterations: SystemSettings.PasswordIterations,
                 initVector: SystemSettings.PasswordInitVector
                 );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="saltValue"></param>
        /// <returns></returns>
        protected string DecryptText(string value, string saltValue)
        {
            return Utilities.Encryption.DecryptText(
                 text: value,
                 saltValue: saltValue,
                 passPhrase: SystemSettings.PasswordPassPhrase,
                 passwordIterations: SystemSettings.PasswordIterations,
                 initVector: SystemSettings.PasswordInitVector
                 );
        }

        protected string GetQueryStringsForSorting()
        {
            var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove("sortby");

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path) + "?";

            return newQueryString.Count > 0
                ? String.Format("{0}{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }

    }
}
