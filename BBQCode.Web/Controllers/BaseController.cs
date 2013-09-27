using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;

namespace BBQCode.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            SetLocalization(requestContext);
            base.Initialize(requestContext);
        }

        private void SetLocalization(System.Web.Routing.RequestContext requestContext)
        {
            // From http://geekswithblogs.net/shaunxu/archive/2010/05/06/localization-in-asp.net-mvc-ndash-3-days-investigation-1-day.aspx
            if (requestContext.RouteData.Values["culture"] != null &&
           !string.IsNullOrWhiteSpace(requestContext.RouteData.Values["culture"].ToString()))
            {
                // set the culture from the route data (url)
                var lang = requestContext.RouteData.Values["culture"].ToString();
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
            }
            else
            {
                // load the culture info from the cookie
                var cookie = requestContext.HttpContext.Request.Cookies["BBQCode.Culture"];
                var langHeader = string.Empty;
                if (cookie != null)
                {
                    // set the culture by the cookie content
                    langHeader = cookie.Value;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                else
                {
                    // set the culture by the location if not speicified
                    langHeader = requestContext.HttpContext.Request.UserLanguages[0];
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                // set the lang value into route data
                requestContext.RouteData.Values["culture"] = langHeader;
            }

            // save the location into cookie
            HttpCookie _cookie = new HttpCookie("BBQCode.Culture", Thread.CurrentThread.CurrentUICulture.Name);
            _cookie.Expires = DateTime.Now.AddYears(1);
            requestContext.HttpContext.Response.SetCookie(_cookie);
        }
    }
}
