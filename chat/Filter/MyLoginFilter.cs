using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chat.Filter
{
    public class MyLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UrlHelper uu = new UrlHelper(filterContext.HttpContext.Request.RequestContext);

            if (filterContext.HttpContext.Session["LoginName"] == null
                || string.IsNullOrEmpty(filterContext.HttpContext.Session["LoginName"].ToString()))
            {
                //抓出目前原本要瀏覽的網頁
                string ww = filterContext.HttpContext.Request.RawUrl;
                //filterContext.HttpContext.Session["ReUrl"] = ww;
                HttpCookie cc = new HttpCookie("ReUrl");
                cc.Value = ww;
                filterContext.HttpContext.Response.Cookies.Add(cc);

                filterContext.HttpContext.Response.Redirect(uu.Action("Index", "Login"));
            }
        }
    }
}