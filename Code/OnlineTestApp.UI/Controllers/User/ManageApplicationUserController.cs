using OnlineTestApp.DomainLogic.Admin.Common;
using OnlineTestApp.DomainLogic.Admin.User;
using OnlineTestApp.ViewModel.User;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineTestApp.UI.Controllers.User
{
    public class ManageApplicationUserController : BaseClasses.SuperAdminUserControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> AddUser()
        {
            return View(await InitializeAddEditUserViewModel(null));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addEditUserViewModel"></param>
        /// <returns></returns>
        async Task<AddEditApplicationUserViewModel> InitializeAddEditUserViewModel(AddEditApplicationUserViewModel addEditUserViewModel)
        {
            if (addEditUserViewModel == null)
            {
                addEditUserViewModel = new AddEditApplicationUserViewModel
                {
                    ApplicationUsers = new Domain.User.ApplicationUsers()
                };
            }
            if (addEditUserViewModel.ApplicationUsers == null)
            {
                addEditUserViewModel.ApplicationUsers = new Domain.User.ApplicationUsers();
            }
            addEditUserViewModel.LstCompanies = await CompanyDomainLogic.GetAllCompanies();
            addEditUserViewModel.LstApplicationUserRoles = await UserDomainLogic.GetApplicationUserRoles();
            return addEditUserViewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddUser(AddEditApplicationUserViewModel addUserViewModel)
        {
            if (!Request.IsAjaxRequest()) return null;
            if (ModelState.IsValid)
            {
                ManageUsersDomainLogic obj = new ManageUsersDomainLogic();
                string result = await obj.AddNewUser(addUserViewModel);

                if (!string.IsNullOrEmpty(result))
                {
                    return ReturnAjaxErrorMessage(result);
                }
                else
                {
                    return ReturnAjaxSuccessMessage("User created successfully");
                }
            }
            return ReturnAjaxModelError();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationuserid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EditUser(Guid applicationuserid)
        {
            ManageUsersDomainLogic obj = new ManageUsersDomainLogic();
            return View(await InitializeAddEditUserViewModel(obj.GetUserDetailsById(applicationuserid)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationuserid"></param>
        /// <param name="editUserViewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult EditUser(Guid applicationuserid, AddEditApplicationUserViewModel editUserViewModel)
        {
            if (!Request.IsAjaxRequest()) return null;
            editUserViewModel.ApplicationUsers.ApplicationUserId = applicationuserid;
            if (ModelState.IsValid)
            {
                ManageUsersDomainLogic obj = new ManageUsersDomainLogic();
                string result = obj.EditUserDetails(editUserViewModel);
                if (!string.IsNullOrEmpty(result))
                {
                    return ReturnAjaxErrorMessage(result);
                }
                else
                {
                    return ReturnAjaxSuccessMessage("User details updated successfully");
                }
            }
            return ReturnAjaxModelError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationUserId"></param>
        [HttpPost]
        public void DeleteUser(Guid applicationUserId)
        {
            if (!Request.IsAjaxRequest()) return;
            ManageUsersDomainLogic obj = new ManageUsersDomainLogic();
            obj.DeleteUser(applicationUserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ViewUsers(ViewApplicationUserViewModel viewApplicationUserViewModel)
        {
            ManageUsersDomainLogic obj = new ManageUsersDomainLogic();
            return View(await obj.ViewApplicationUsers(viewApplicationUserViewModel));
        }


        [HttpGet]
        public ActionResult ViewUsersAngular()
        {
            //ManageUsersDomainLogic obj = new ManageUsersDomainLogic();
            //return View(await obj.ViewApplicationUsers(viewApplicationUserViewModel));
            return View();
        }

        [HttpGet]
        public JsonResult GetUsersDetail(ViewApplicationUserViewModel viewApplicationUserViewModel)
        {
            //ViewApplicationUserViewModel viewApplicationUserViewModel = new ViewApplicationUserViewModel();
            if (!Request.IsAjaxRequest()) return NotAjaxRequest();
            ManageUsersDomainLogic obj = new ManageUsersDomainLogic();
            return Json_AllowGet_IgnoreReferenceLoop(obj.GetUsersDetails( viewApplicationUserViewModel));
        }

    }
}