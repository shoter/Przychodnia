using Przychodnia.Helpers;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
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


            if (!isAuthorized(uzytkownik.Prawa))
            {
                return false;
            }
            
            var session = SessionHelper.Sesja;
            if (session != null)
            {
                DependencyResolver.Current.GetService<SesjaRepository>().Update(session.ID, DateTime.Now.ToUniversalTime().AddHours(2));
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var uzytkownik = SessionHelper.Uzytkownik;

            if (uzytkownik != null && !isAuthorized(uzytkownik.Prawa))
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "Index"
                    })
                );

                TempDataHelper.AddMessage(filterContext.Controller.TempData, new Models.PopupMessageViewModel(string.Format("Nie jesteś {0} aby wykonać tą akcje", Authorized), enums.PopupMessageType.Error));
            }
            else
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


           return rights.Contains(Authorized.Value) ;
        }
    }
}