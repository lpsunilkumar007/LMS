using System;
using System.Web.Mvc;
using OnlineTestApp.Domain.LookUps;
using OnlineTestApp.DomainLogic.Admin.LookUps;

namespace OnlineTestApp.UI.Controllers.LookUp
{
    public class LookUpController : BaseClasses.SuperAdminUserControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewLookUpValues(Enums.LookUps.LookUpDomainCode lookUpDomainCode)
        {
            ViewBag.LookUpDomainCode = lookUpDomainCode;
            return View();
        }



        [HttpGet]
        public JsonResult GetLookUpValues(Enums.LookUps.LookUpDomainCode lookUpDomainCode)
        {
            if (!Request.IsAjaxRequest()) return NotAjaxRequest();
            LookUpDomainValueDomainLogic obj = new LookUpDomainValueDomainLogic();
            return Json_AllowGet_IgnoreReferenceLoop(obj.GetLookUpDomainValuesByLookUpDomainCode(new ViewModel.LookUps.ViewLookUpsViewModel(), lookUpDomainCode));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddLookUpDomainValue(Guid lookUpDomainId)
        {
            return View(new LookUpDomainValues
            {
                LookUpDomain = GetLookUpDomainById(lookUpDomainId)
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainId"></param>
        /// <returns></returns>
        LookUpDomains GetLookUpDomainById(Guid lookUpDomainId)
        {
            LookUpDomainDomainLogic obj = new LookUpDomainDomainLogic();
            return obj.GetLookUpDomainById(lookUpDomainId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainCode"></param>
        /// <param name="lookUpDomainValues"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddLookUpDomainValue(Guid lookUpDomainId, LookUpDomainValues lookUpDomainValues)
        {
            if (!Request.IsAjaxRequest()) return null;
            if (ModelState.IsValid)
            {
                LookUpDomainValueDomainLogic obj = new LookUpDomainValueDomainLogic();
                lookUpDomainValues.FkLookUpDomainId = lookUpDomainId;
                bool result = obj.AddNewLookUpValue(lookUpDomainValues);

                if (result)
                {
                    return ReturnAjaxSuccessMessage("Record added sucessfully");
                }
                else
                {
                    return ReturnAjaxErrorMessage("Value already exists. Please try again");
                }
            }
            return ReturnAjaxModelError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValueId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditLookUpDomainValue(Guid lookUpDomainValueId)
        {
            LookUpDomainValueDomainLogic obj = new LookUpDomainValueDomainLogic();
            var result = obj.GetLookUpDomainValueById(lookUpDomainValueId);
            result.LookUpDomain = GetLookUpDomainById(result.FkLookUpDomainId);
            return View(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValueId"></param>
        /// <param name="lookUpDomainValues"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult EditLookUpDomainValue(Guid lookUpDomainValueId, LookUpDomainValues lookUpDomainValues)
        {
            if (!Request.IsAjaxRequest()) return null;
            if (ModelState.IsValid)
            {
                LookUpDomainValueDomainLogic obj = new LookUpDomainValueDomainLogic();
                lookUpDomainValues.LookUpDomainValueId = lookUpDomainValueId;
                bool result = obj.UpdateNewLookUpValue(lookUpDomainValues);
                if (result)
                {
                    return ReturnAjaxSuccessMessage("Record updated sucessfully");
                }
                else
                {
                    return ReturnAjaxSuccessMessage("Value already exists. Please try again");
                }
            }
            lookUpDomainValues.LookUpDomain = GetLookUpDomainById(lookUpDomainValues.FkLookUpDomainId);
            return ReturnAjaxModelError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValueId"></param>
        [HttpPost]
        public void DeleteLookUpDomainValue(Guid lookUpDomainValueId)
        {
            LookUpDomainValueDomainLogic obj = new LookUpDomainValueDomainLogic();
            obj.DeleteLookUpValue(lookUpDomainValueId);
        }

    }
}