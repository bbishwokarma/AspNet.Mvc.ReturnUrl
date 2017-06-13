using System;
using System.Web.Mvc;

namespace AspNet.Mvc.RedirectAssist
{
    public abstract class ParameterizedRedirectionBase : FilterAttribute, IActionFilter, IResultFilter
    {
        public string ReturnUrlParameterName { get; set; }

        public ParameterizedRedirectionBase(string _returnUrlParameterName)
        {
            this.ReturnUrlParameterName = _returnUrlParameterName;
        }

        protected string ReadReturnpath(ControllerContext filterContext)
        {
            return filterContext.HttpContext.Request.QueryString.Get(ReturnUrlParameterName);
        }

        protected string ReadReturnpath(Uri uri)
        {
            var queries = System.Web.HttpUtility.ParseQueryString(uri.Query);
            return queries.Get(ReturnUrlParameterName);
        }

        protected bool IsValidUrl(object url)
        {
            return url != null && url.ToString().Trim().Length > 0;
        }

        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public virtual void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public virtual void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }
    }
}
