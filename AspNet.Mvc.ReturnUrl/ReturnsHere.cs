using System.Web.Mvc;

namespace AspNet.Mvc.ReturnUrl
{
    public class ReturnsHere : ReturnPathBase
    {
        public ReturnsHere(string _returnUrlParameterName) : base(_returnUrlParameterName)
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
