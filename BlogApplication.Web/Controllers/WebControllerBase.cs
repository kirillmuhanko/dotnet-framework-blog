using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.ViewModels.Base;
using BlogApplication.Shared.ActionResults;

namespace BlogApplication.Web.Controllers
{
    public abstract class WebControllerBase : Controller
    {
        protected bool ModelHasErrors => !ModelState.IsValid;

        protected string UrlScheme => $"{Request?.Url?.Scheme}";

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            var actionResult = RedirectToAction(ActionNames.Index, ControllerNames.Home);
            return actionResult;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new JsonNetResult
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Data = data
            };
        }

        protected override JsonResult Json(
            object data,
            string contentType,
            Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Data = data,
                JsonRequestBehavior = behavior
            };
        }

        protected string BuildActionUrl(string actionName, string controllerName)
        {
            var str = Url.Action(actionName, controllerName);
            return str;
        }

        protected void AddModelError(ViewModelBase model, string message)
        {
            model.AddError(message);
            model.ResetValidation();
            ModelState.AddModelError(string.Empty, message);
        }

        protected void AddModelErrors(ViewModelBase model, IEnumerable<string> messages)
        {
            var list = messages.ToList();
            model.AddErrors(list);
            model.ResetValidation();

            foreach (var message in list)
                ModelState.AddModelError(string.Empty, message);
        }

        protected void SetResponseStatusCode(HttpStatusCode statusCode)
        {
            HttpContext.Response.StatusCode = (int) statusCode;
        }
    }
}