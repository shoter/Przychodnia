using Data.Objects;
using PrzychodniaData.Objects;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Helpers
{
    public class SessionHelper
    {
        static private string UzytkownikSession { get { return "UZYTKOWNIK_SESSION"; } }


        public static Uzytkownik Uzytkownik
        {
            get
            {
                return Sesja?.Uzytkownik;
            }
        }

        public static Sesja Sesja
        {
            get
            {
                Sesja session = null;
                HttpContext.Current.Session[UzytkownikSession] = null;
                var sessionRepository = DependencyResolver.Current.GetService<SesjaRepository>();
                if (HttpContext.Current.Session[UzytkownikSession] != null)
                {
                    session = HttpContext.Current.Session[UzytkownikSession] as Sesja;
                    //session = sessionRepository.FirstOrDefault(s => s.ID == session.ID);
                }
                else if (HttpContext.Current.Request.Cookies[UzytkownikSession] != null)
                {
                    var cookie = HttpContext.Current.Request.Cookies[UzytkownikSession].Value;


                    session = sessionRepository
                        .Get(cookie);

                    if (session != null)
                    {
                        if (session.IP != ClientIP && ClientIP != "::1" && ClientIP != "127.0.0.1")
                            session = null;
                        else if (session.DataWygasniecia.CompareTo(DateTime.Now) < 0)
                        {
                            sessionRepository.Remove(session.ID);
                            session = null;
                        }
                    }
                }

                if(session != null)
                {
                    sessionRepository.Update(session.ID, DateTime.Now.AddHours(2));
                }
                return session;
            }
            set
            {
                HttpContext.Current.Session[UzytkownikSession] = value;
                if (value != null)
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(UzytkownikSession, value.Ciasteczko));
                else
                    HttpContext.Current.Response.Cookies.Remove(UzytkownikSession);
            }
        }

        public static string ClientIP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }
    }
}