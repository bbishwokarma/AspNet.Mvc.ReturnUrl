using System.Web.Mvc;

namespace AspNet.Mvc.RedirectAssist
{
    public class ReturnsUsingParameter : ParameterizedRedirectionBase
    {
        /// <summary>
        /// When a redirection to this action is made, then ParameterName is used to recover the filter parameters previously applied on this action.
        /// </summary>
        /// <param name="ParameterName">Distinct name of parameter that would be used for this redirection.</param>
        public ReturnsUsingParameter(string ParameterName) : base(ParameterName)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.UrlReferrer == null)
            {
                return;
            }
            string returnPath = ReadReturnpath(filterContext);
            if (!IsValidUrl(returnPath))
            {
                returnPath = ReadReturnpath(filterContext.HttpContext.Request.UrlReferrer);
            }

            if (!IsValidUrl(returnPath))
            {
                return;
            }

            string returnBase = returnPath.Substring(0, returnPath.IndexOf("?") <= 0 ? returnPath.Length : returnPath.IndexOf("?"));
            string refererBase = filterContext.HttpContext.Request.UrlReferrer.LocalPath;

            if (returnBase.ToUpper().Trim().Equals(refererBase.ToUpper().Trim()))
            {
                return;
            }

            string currentBase = filterContext.HttpContext.Request.Url.LocalPath;
            if (!returnBase.ToUpper().Trim().Equals(currentBase.Trim().ToUpper()))
            {
                return;
            }
            if (returnPath.ToUpper().Trim().Equals(filterContext.HttpContext.Request.Url.PathAndQuery.ToUpper().Trim()))
            {
                return;
            }
            filterContext.Result = new RedirectResult(returnPath.Trim());
        }
    }
}
