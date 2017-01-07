using Przychodnia.Helpers;
using PrzychodniaData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Przychodnia.Attributes
{
    public class PrzychodniaAuthorize : AuthorizeAttribute
    {
        public PrawoUzytkownikaEnum? Authorized { get; set; }

        public PrzychodniaAuthorize(PrawoUzytkownikaEnum prawo)
        {
            Authorized = prawo;
        }

        public PrzychodniaAuthorize()
        {
            Authorized = null;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var uzytkownik = SessionHelper.Uzytkownik;

            if (uzytkownik == null)
                return false;

            /*var playerType = player.GetPlayerType();

            if (!isAuthorized(playerType))
            {
                return false;
            }
            */
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Account",
                        action = "Login"
                    })
                );
        }

        private bool isAuthorized(List<PrawoUzytkownikaEnum> rights)
        {
            if (Authorized == null)
                return true;

            return true;
            /*if ((int)prawo >= (int)Authorized)
                return true;
            return false;*/
        }
    }
}