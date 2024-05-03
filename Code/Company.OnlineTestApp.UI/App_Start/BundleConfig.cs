using System.Web;
using System.Web.Optimization;

namespace Company.OnlineTestApp.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region css company
            bundles.Add(new StyleBundle("~/css").Include(
                   "~/Template/Company/Css/bootstrap.css"
                //"~/Templates/Css/Chosen/bootstrap.css"
                //dropdown to auto help
                , "~/Template/Plugins/Chosen/chosen.css"
                // , "~/Template/Css/custom-style.css"
                //font-awesome-4.7.
                , "~/Template/Company/Css/font-awesome.css"
                , "~/Template/Company/Css/common.css"
               //alert message css
               , "~/Template/Plugins/AlertMessage/sweetalert.css"
               ));


            #endregion
            #region javascript for Company
            bundles.Add(new ScriptBundle("~/CompanyJs").Include(
                 "~/Template/Scripts/jquery/jquery-3.2.1.js"
                  , "~/Template/Scripts/jquery/jquery-ui.js"
                       //Validation Js Starts
                       , "~/Template/Scripts/validation/jquery.validate.js"
                , "~/Template/Scripts/validation/jquery.validate.unobtrusive.js"
                 , "~/Template/Scripts/validation/jquery-migrate-1.1.0.js"
                //Validation Js Ends

                //bootstrap js
                , "~/Template/Scripts/bootstrap.js"
                //jquery UI.block
                , "~/Template/Plugins/jquery.blockUI.js"
                //dropdown to auto help
                , "~/Template/Plugins/Chosen/chosen.jquery.js"
                , "~/Template/Plugins/Chosen/prism.js"
                //alert message js
                , "~/Template/Plugins/AlertMessage/sweetalert-dev.js"
                , "~/Template/Scripts/common.js"
                , "~/Template/Scripts/secure.js"
                ));
            #endregion

            #region css for candidate
            bundles.Add(new StyleBundle("~/CandidateCss").Include(
                   "~/Template/Candidate/Css/bootstrap.css"
                //"~/Templates/Css/Chosen/bootstrap.css"
                //dropdown to auto help
                , "~/Template/Plugins/Chosen/chosen.css"
                // , "~/Template/Css/custom-style.css"
                //font-awesome-4.7.o
                , "~/Template/Candidate/Css/font-awesome.css"
                , "~/Template/Candidate/Css/common.css"
               //alert message css
               , "~/Template/Plugins/AlertMessage/sweetalert.css"
               ));
            #endregion
            #region javascript Bundel  for Candidtae 
            bundles.Add(new ScriptBundle("~/CandidateJs").Include(
                         "~/Template/Scripts/popper.min.js",
                         "~/Template/Scripts/jquery/jquery-3.2.1.js"
                       , "~/Template/Scripts/jquery/jquery-ui.js"

                       //Validation Js Starts
                       , "~/Template/Scripts/validation/jquery.validate.js"
                       , "~/Template/Scripts/validation/jquery.validate.unobtrusive.js"
                       , "~/Template/Scripts/validation/jquery-migrate-1.1.0.js"
                //bootstrap js
                , "~/Template/Candidate/bootstrap.js"
                //jquery UI.block
                , "~/Template/Plugins/jquery.blockUI.js"
                //dropdown to auto help
                , "~/Template/Plugins/Chosen/chosen.jquery.js"
                , "~/Template/Plugins/Chosen/prism.js"
                //alert message js
                , "~/Template/Plugins/AlertMessage/sweetalert-dev.js"
                       , "~/Template/Scripts/common.js"
                       , "~/Template/Candidate/candidate.js"
                //Validation Js Ends
                ));
            #endregion


            #region css for account
            bundles.Add(new StyleBundle("~/AccountCss").Include(
                   "~/Template/Account/Css/bootstrap.css"
                //"~/Templates/Css/Chosen/bootstrap.css"
                //dropdown to auto help
                //, "~/Template/Plugins/Chosen/chosen.css"
                // , "~/Template/Css/custom-style.css"
                //font-awesome-4.7.
                , "~/Template/font-awesome-4.7.0/css/font-awesome.css"
                , "~/Template/Account/Css/common.css"
               //alert message css
               , "~/Template/Plugins/AlertMessage/sweetalert.css"
               ));
            #endregion
            #region javascript Bundel  for account 
            bundles.Add(new ScriptBundle("~/AccountJs").Include(
                         "~/Template/Scripts/jquery/jquery-3.2.1.js"
                       , "~/Template/Scripts/jquery/jquery-ui.js"

                       //Validation Js Starts
                       , "~/Template/Scripts/validation/jquery.validate.js"
                       , "~/Template/Scripts/validation/jquery.validate.unobtrusive.js"
                       , "~/Template/Scripts/validation/jquery-migrate-1.1.0.js"
                  , "~/Template/Plugins/jquery.blockUI.js"
                  , "~/Template/Scripts/common.js"
                   , "~/Template/Account/account.js"

                //Validation Js Ends
                ));
            #endregion

        }
    }
}
