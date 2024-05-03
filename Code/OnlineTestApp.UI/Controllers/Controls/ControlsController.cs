using OnlineTestApp.Domain.Control;
using System;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.Controls
{
    public class ControlsController : BaseClasses.ControllerBase
    {
        #region Paging Control
        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalRecords"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Paging(int totalRecords, int pageSize = -100, bool isTopPaging = false)
        {
            int currentPage;
            ViewBag.IsTopPaging = isTopPaging;
            if (!Utilities.QueryStringHelper.GetIntValue("page", out currentPage))
            {
                currentPage = 1;
            }
            if (totalRecords <= 0 || totalRecords <= pageSize) { return PartialView(); }

            PagerControl obj = new PagerControl
            {
                LastPage = totalRecords = PageCount(totalRecords, pageSize)
            };

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            obj.FirstIndex = currentPage < 3 ? 1 : currentPage - 2;

            if (obj.FirstIndex > totalRecords - 5)
                obj.FirstIndex = totalRecords - 4;
            obj.LastIndex = obj.FirstIndex + 5;
            if (obj.FirstIndex < 1)
            {
                obj.FirstIndex = 1;
            }
            obj.CurrentPage = currentPage;
            if (Request.QueryString.Count > 0)
            {
                obj.QueryString = RemoveQueryStringByKey(System.Web.HttpContext.Current.Request.Url.AbsoluteUri, "page");
            }
            else
            {
                obj.QueryString = RemoveQueryStringByKey(System.Web.HttpContext.Current.Request.Url.AbsoluteUri, "page") + "?";
            }
            return PartialView(obj);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        static string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove(key);

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="total"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int PageCount(int total, int pageSize)
        {
            return total % pageSize == 0 ? total / pageSize : total / pageSize + 1;

        }
        #endregion
    }
}