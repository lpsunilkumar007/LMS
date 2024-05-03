using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Company.OnlineTestApp.UI.Controllers.Base
{
    public class BaseController : Controller
    {
        public ActionResult RedirectTo404()
        {
            return RedirectToAction("Error404", "Error");
        }
        public ActionResult RedirectAccessDenied()
        {
            return RedirectToAction("AccessDenied", "Error");
        }

        protected void ErrorBlock(string msg)
        {
            ViewBag.ErrorBlock = msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected ActionResult RedirectToLogin()
        {
            return RedirectToAction("login", "Account");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
          #region Ajax Messages
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected JsonResult ReturnAjaxModelError()
        {
            return Json(new
            {
                Success = false,
                Message = string.Join("\n", ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                                .Select(m => m.ErrorMessage).ToArray())
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected JsonResult ReturnJsonResult(object value)
        {
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="successMessage"></param>
        /// <returns></returns>
        protected JsonResult ReturnAjaxSuccessMessage(string successMessage)
        {
            return Json(new
            {
                Success = true,
                Message = successMessage
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="successMessage"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        protected JsonResult ReturnAjaxSuccessMessage(string successMessage, string redirectUrl)
        {
            return Json(new
            {
                Success = true,
                RedirectUrl = redirectUrl,
                Message = successMessage
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="successMessage"></param>
        /// <returns></returns>
        protected JsonResult ReturnAjaxWithSuccessOutPut(string successMessage)
        {
            return Json(new
            {
                Success = true,
                OutPut = successMessage
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected JsonResult ReturnAjaxSuccessMessage(Dictionary<string, string> message, string mssage = "", string redirectUrl = "")
        {
            message.Add("Success", "true");
            message.Add("Message", mssage);
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                message.Add("RedirectUrl", redirectUrl);
            }
            return Json(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        protected JsonResult ReturnAjaxErrorMessage(string errorMessage)
        {
            return Json(new
            {
                Success = false,
                Message = errorMessage
            });
        }

        public JsonResult Json_GetValue(object data)
        {
            return Json(new
            {
                data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult Json_AllowGet_IgnoreReferenceLoop(object data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            JavaScriptSerializer j = new JavaScriptSerializer();
            return Json(j.Deserialize(json, typeof(object)), JsonRequestBehavior.AllowGet);
        }


        public JsonResult NotAjaxRequest()
        {
            return Json(new
            {
                Message = "Not valid request"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="successMessage"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        protected JsonResult ReturnAjaxErrorMessage(string successMessage, string redirectUrl)
        {
            return Json(new
            {
                Success = false,
                RedirectUrl = redirectUrl,
                Message = successMessage
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mssage"></param>
        /// <returns></returns>
        protected JsonResult ReturnAjaxErrorMessage(Dictionary<string, string> message, string mssage = "")
        {
            message.Add("Success", "false");
            message.Add("Message", mssage);
            return Json(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mssage"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        protected JsonResult ReturnAjaxErrorMessage(Dictionary<string, string> message, string mssage = "", string redirectUrl = "")
        {
            message.Add("Success", "false");
            message.Add("Message", mssage);
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                message.Add("RedirectUrl", redirectUrl);
            }
            return Json(message);
        }
        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, string> GetDictionaryForAjaxResult
        {
            get
            {
                return new Dictionary<string, string>();
            }
        }
        #endregion
    }
}