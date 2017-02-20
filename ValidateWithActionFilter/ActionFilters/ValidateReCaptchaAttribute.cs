using System.Configuration;
using System.Web.Mvc;
using GoogleReCaptchaV2Validator;

namespace ValidateWithActionFilter.ActionFilters
{
    public class ValidateReCaptchaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var reCaptchaPrivateKey = ConfigurationManager.AppSettings["reCaptcha_privateKey"];
            var reCaptchaResponse = filterContext.HttpContext.Request.Form["g-recaptcha-response"];

            var validator = new ReCaptchaValidator(reCaptchaPrivateKey);
            var validatorResponse = validator.Validate(reCaptchaResponse);

            if (!validatorResponse.Success)
            {
                filterContext.Controller.ViewData.ModelState.AddModelError("ErrorCodes", string.Join(",", validatorResponse.ErrorCodes));
            }            
        }
    }
}
