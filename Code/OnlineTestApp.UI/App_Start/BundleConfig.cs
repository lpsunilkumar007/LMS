using System.Web;
using System.Web.Optimization;

namespace OnlineTestApp.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string themeFolderName = "CommonTheme";
            #region css
            bundles.Add(new StyleBundle("~/css").Include(

                 //dropdown to auto help
                 "~/Templates/scripts/Plugin/drpAUtoHelp/chosen.css"

                //font-awesome-4.7.0
                , "~/Templates/font-awesome-4.7.0/css/font-awesome.css"

                //dropdown to auto help
                , "~/Templates/scripts/Plugins/drpAUtoHelp/chosen.css"

                //css
                , "~/Templates/Themes/" + themeFolderName + "/css/bootstrap.css"
                , "~/Templates/Themes/" + themeFolderName + "/css/fontawesome.5.0.6.css"
                , "~/Templates/Themes/" + themeFolderName + "/css/StyleSheet.css"
                , "~/Templates/Themes/" + themeFolderName + "/css/TestTabs.css"

                //common for all clients
                , "~/Templates/Themes/commonCss.css"

                //alert message css
                , "~/Templates/scripts/Plugins/AlertMessage/sweetalert.css"

                 //tags
                 , "~/Templates/scripts/Plugins/Tags/css/*.css"

                 //color picker
                 , "~/Templates/Scripts/Plugins/ColorPicker/spectrum.css"

                 //datetime picker
                 , "~/Templates/Scripts/Plugins/DateTimePicker/jquery.datetimepicker.css"

               //custom scroll bar
               , "~/Templates/Themes/CommonTheme/css/jquery.mCustomScrollbar.css"

               ));
            #endregion

            #region javascript Bundel Not logged in User
            bundles.Add(new ScriptBundle("~/Anonymousjs").Include(
                  "~/Templates/scripts/jquery/jquery-3.2.1.js"
                  , "~/Templates/scripts/jquery/jquery-ui.js"

                       //Validation Js Starts
                       , "~/Templates/scripts/validation/jquery.validate.js"
                       , "~/Templates/scripts/validation/jquery.validate.unobtrusive.js"
                       , "~/Templates/scripts/validation/jquery-migrate-1.1.0.js"
              //Validation Js Ends

              //bootstrap js
              , "~/Templates/scripts/bootstrap.js"

                //jquery UI.block
                , "~/Templates/scripts/Plugins/jquery.blockUI.js"

                 //dropdown to auto help
                 , "~/Templates/scripts/Plugins/drpAUtoHelp/chosen.jquery.js"
                 , "~/Templates/scripts/Plugins/drpAUtoHelp/prism.js"

                 //bootstrap js
                 , "~/Templates/scripts/bootstrap/bootstrap.js"

                 //alert message js
                 , "~/Templates/scripts/Plugins/AlertMessage/sweetalert-dev.js"

                 //tags
                 , "~/Templates/scripts/Plugins/Tags/Js/*.js"

                ////jquery UI.block
                //"~/Templates/scripts/Plugins/pace.js"

                , "~/Templates/scripts/commonJs.js"

                , "~/Templates/scripts/Anonymousjs.js"

                //color picker
                , "~/Templates/Scripts/Plugins/ColorPicker/spectrum.js"

                 //datetime picker
                 , "~/Templates/Scripts/Plugins/DateTimePicker/jquery.datetimepicker.js"

                ////custom scroll bar
                //, "~/Templates/Scripts/Plugins/jquery.mCustomScrollbar.concat.min.js"

                ));
            #endregion          

            #region javascript Logged In User Bundle
            bundles.Add(new ScriptBundle("~/Ljs").Include(
                  "~/Templates/scripts/jquery/jquery-3.2.1.js"
                  , "~/Templates/scripts/jquery/jquery-ui.js"

                       //Validation Js Starts
                       , "~/Templates/scripts/validation/jquery.validate.js"
                       , "~/Templates/scripts/validation/jquery.validate.unobtrusive.js"
                       , "~/Templates/scripts/validation/jquery-migrate-1.1.0.js"
              //Validation Js Ends

              //bootstrap js
              , "~/Templates/scripts/bootstrap.js"

                //jquery UI.block
                , "~/Templates/scripts/Plugins/jquery.blockUI.js"

                 //dropdown to auto help
                 , "~/Templates/scripts/Plugins/drpAUtoHelp/chosen.jquery.js"
                 , "~/Templates/scripts/Plugins/drpAUtoHelp/prism.js"

                 //bootstrap js
                 , "~/Templates/scripts/bootstrap/bootstrap.js"

                 //alert message js
                 , "~/Templates/scripts/Plugins/AlertMessage/sweetalert-dev.js"

                 //tags
                 , "~/Templates/scripts/Plugins/Tags/Js/*.js"

                , "~/Templates/scripts/commonJs.js"

                , "~/Templates/scripts/SecureJs.js"

                //color picker
                , "~/Templates/Scripts/Plugins/ColorPicker/spectrum.js"

                 //datetime picker
                 , "~/Templates/Scripts/Plugins/DateTimePicker/jquery.datetimepicker.js"

                 //double scroll
                 , "~/Templates/Scripts/Plugins/jqDoubleScroll/jquery.doubleScroll.js"
                //custom scroll bar
                , "~/Templates/Scripts/Plugins/jquery.mCustomScrollbar.concat.js"
                ));
            #endregion

            #region javascript Candidate Apply Now 
            bundles.Add(new ScriptBundle("~/TestApplyNowJs").Include(
                  "~/Templates/scripts/jquery/jquery-3.2.1.js"
                  , "~/Templates/scripts/jquery/jquery-ui.js"

                       //Validation Js Starts
                       , "~/Templates/scripts/validation/jquery.validate.js"
                       , "~/Templates/scripts/validation/jquery.validate.unobtrusive.js"
                       , "~/Templates/scripts/validation/jquery-migrate-1.1.0.js"
              //Validation Js Ends

              //bootstrap js
              , "~/Templates/scripts/bootstrap.js"

                //jquery UI.block
                , "~/Templates/scripts/Plugins/jquery.blockUI.js"

                 //dropdown to auto help
                 , "~/Templates/scripts/Plugins/drpAUtoHelp/chosen.jquery.js"
                 , "~/Templates/scripts/Plugins/drpAUtoHelp/prism.js"

                 //bootstrap js
                 , "~/Templates/scripts/bootstrap/bootstrap.js"

                 //alert message js
                 , "~/Templates/scripts/Plugins/AlertMessage/sweetalert-dev.js"

                 //tags
                 //, "~/Templates/scripts/Plugins/Tags/Js/*.js"

                ////jquery UI.block
                //"~/Templates/scripts/Plugins/pace.js"

                , "~/Templates/scripts/commonJs.js"

                , "~/Templates/scripts/ApplyNowJs.js"

                //color picker
                //, "~/Templates/Scripts/Plugins/ColorPicker/spectrum.js"

                 //datetime picker
                 //, "~/Templates/Scripts/Plugins/DateTimePicker/jquery.datetimepicker.js"

                ////custom scroll bar
                //, "~/Templates/Scripts/Plugins/jquery.mCustomScrollbar.concat.min.js"

                ));
            #endregion

            #region css Candidate Apply Now 
            bundles.Add(new StyleBundle("~/ApplyNowCss").Include(

                 //dropdown to auto help
                 "~/Templates/scripts/Plugin/drpAUtoHelp/chosen.css"

                //font-awesome-4.7.0
                , "~/Templates/font-awesome-4.7.0/css/font-awesome.css"

                //dropdown to auto help
                , "~/Templates/scripts/Plugins/drpAUtoHelp/chosen.css"

                //css
                , "~/Templates/Themes/" + themeFolderName + "/ApplyNow/css/bootstrap.css"
                , "~/Templates/Themes/" + themeFolderName + "/ApplyNow/css/fontawesome.5.0.7.css"
                , "~/Templates/Themes/" + themeFolderName + "/ApplyNow/css/StyleSheet.css"

                //common for all clients
                , "~/Templates/Themes/commonCss.css"

                //alert message css
                , "~/Templates/scripts/Plugins/AlertMessage/sweetalert.css"

                 //tags
                 //, "~/Templates/scripts/Plugins/Tags/css/*.css"

                 //color picker
                 //, "~/Templates/Scripts/Plugins/ColorPicker/spectrum.css"

                 //datetime picker
                 //, "~/Templates/Scripts/Plugins/DateTimePicker/jquery.datetimepicker.css"

               //custom scroll bar
               //, "~/Templates/Themes/CommonTheme/css/jquery.mCustomScrollbar.css"

               ));
            #endregion

        }
    }
}
