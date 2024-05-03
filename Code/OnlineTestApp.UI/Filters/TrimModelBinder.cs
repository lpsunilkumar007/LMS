using System.Web.Mvc;

namespace OnlineTestApp.UI.Filters
{
    /// <summary>
    /// used to trim string properties
    /// Add in Global file under Application_Start :   ModelBinders.Binders.Add(typeof(string), new Utilities.MVC.ExtendedControls.TrimModelBinder());
    /// </summary>
    public class TrimModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            // First check if request validation is required
            var shouldPerformRequestValidation = controllerContext.Controller.ValidateRequest && bindingContext.ModelMetadata.RequestValidationEnabled;

            // Get value
            var valueResult = bindingContext.GetValueFromValueProvider(shouldPerformRequestValidation);


            // ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult == null || string.IsNullOrEmpty(valueResult.AttemptedValue)) return null;
            return valueResult.AttemptedValue.Trim();
        }

    }
    public static class test
    {
        public static ValueProviderResult GetValueFromValueProvider(this ModelBindingContext bindingContext, bool performRequestValidation)
        {
            var unvalidatedValueProvider = bindingContext.ValueProvider as IUnvalidatedValueProvider;
            return (unvalidatedValueProvider != null)
                       ? unvalidatedValueProvider.GetValue(bindingContext.ModelName, !performRequestValidation)
                       : bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        }
    }
}