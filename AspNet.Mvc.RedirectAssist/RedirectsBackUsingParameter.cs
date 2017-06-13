using System.Web.Mvc;

namespace AspNet.Mvc.RedirectAssist
{
    public class RedirectsBackUsingParameter : ParameterizedRedirectionBase
    {
        /// <summary>
        /// This attribute is used to remember the ParameterName used in ReturnsUsingParameter attribute in the URL (for stateless). 
        /// Whenever a redirection to ReturnsUsingParameter is made, the parameter values would be recovered from the ParameterName.
        /// </summary>
        /// <param name="ParameterName">Use same value of ParameterName used in ReturnsUsingParameter</param>
        public RedirectsBackUsingParameter(string ParameterName) : base(ParameterName)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.UrlReferrer == null)
            {
                return;
            }
            if (filterContext.HttpContext.Request.HttpMethod != "GET")
            {
                return;
            }
            string returnPath = ReadReturnpath(filterContext);
            if (IsValidUrl(returnPath))
            {
                //set view data so that view can access return url easily, in case the view wants, e.g., in BACK button.
                filterContext.Controller.ViewData[ReturnUrlParameterName] = returnPath;
                //returnpath is already set in url. No need to set it again.
                return;
            }
            string referrer = filterContext.HttpContext.Request.UrlReferrer.PathAndQuery.Trim();

            //if the referrer URL has return path that needs to be passed to current url, read the return path from referrer.
            returnPath = ReadReturnpath(filterContext.HttpContext.Request.UrlReferrer);

            if (!IsValidUrl(returnPath))
            {
                //referrer will be the return path.
                returnPath = referrer;
            }
            string current = filterContext.HttpContext.Request.Url.PathAndQuery;
            if (IsValidUrl(referrer) && !referrer.ToUpper().Trim().Equals(current.Trim().ToUpper()))
            {
                //new URL with return path added as query string.
                string newPath = current + (current.Contains("?") ? "&" : "?") + ReturnUrlParameterName + "=" + System.Web.HttpUtility.UrlEncode(returnPath);

                filterContext.Result = new RedirectResult(newPath);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResult)
            {
                return;
            }
            if (filterContext.HttpContext.Request.HttpMethod == "GET")
            {
                return;
            }
            string returnPath = ReadReturnpath(filterContext);
            if (!IsValidUrl(returnPath))
            {
                returnPath = ReadReturnpath(filterContext.HttpContext.Request.UrlReferrer);
            }
            if (IsValidUrl(returnPath))
            {
                //override the action's redirect URL with new redirect URL which is the actual return path.
                filterContext.Result = new RedirectResult(returnPath);
            }
        }
    }
}
