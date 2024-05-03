using System.IO;
using System.Web.Mvc;

namespace OnlineTestApp.UI
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// this.RenderView("ViewName", model);
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RenderView(this Controller controller, string viewName, object model)
        {
            return RenderView(controller, viewName, new ViewDataDictionary(model));
        }

        /// <summary>
        /// this.RenderView("ViewName", model);
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="viewData"></param>
        /// <returns></returns>
        public static string RenderView(this Controller controller, string viewName, ViewDataDictionary viewData)
        {
            var controllerContext = controller.ControllerContext;

            var viewResult = ViewEngines.Engines.FindView(controllerContext, viewName, null);

            StringWriter stringWriter;

            using (stringWriter = new StringWriter())
            {
                var viewContext = new ViewContext(
                    controllerContext,
                    viewResult.View,
                    viewData,
                    controllerContext.Controller.TempData,
                    stringWriter);

                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RenderPartialView(this Controller controller, string viewName, object model)
        {
            return RenderPartialView(controller, viewName, new ViewDataDictionary(model));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="viewData"></param>
        /// <returns></returns>
        public static string RenderPartialView(this Controller controller, string viewName, ViewDataDictionary viewData)
        {
            var controllerContext = controller.ControllerContext;

            var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);

            StringWriter stringWriter;

            using (stringWriter = new StringWriter())
            {
                var viewContext = new ViewContext(
                    controllerContext,
                    viewResult.View,
                    viewData,
                    controllerContext.Controller.TempData,
                    stringWriter);

                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
            }

            return stringWriter.ToString();
        }
    }
}