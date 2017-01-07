using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Przychodnia.Helpers
{
    public static class RouteDataHelper
    {
        public static RouteValueDictionary RouteValueDictionary
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values;
            }
        }

        public static string ActionName
        {
            get
            {
                var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
                if (routeValues != null)
                {
                    if (routeValues.ContainsKey("action"))
                    {
                        return HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
                    }
                }

                throw new KeyNotFoundException();
            }
        }

        public static string ControllerName
        {
            get
            {
                var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
                if (routeValues != null)
                {
                    if (routeValues.ContainsKey("controller"))
                    {
                        return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                    }
                }

                throw new KeyNotFoundException();
            }
        }
    }
}