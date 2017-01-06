using Data.Objects;
using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Helpers
{
    public class SessionHelper
    {
        static private string UzytkownikSession { get { return "UZYTKOWNIK_SESSION"; } }


        /*public static Uzytkownik Uzytkownik
        {
            get
            {
                if(Sessio)
            }
        }*/

        public static Sesja Sesja
        {
            get
            {
                /*Session session = null;
                HttpContext.Current.Session[CitizenSession] = null;
                var sessionRepository = DependencyResolver.Current.GetService<ISessionRepository>();
                if (HttpContext.Current.Session[CitizenSession] != null)
                {
                    session = HttpContext.Current.Session[CitizenSession] as Session;
                    //session = sessionRepository.FirstOrDefault(s => s.ID == session.ID);
                }
                else if (HttpContext.Current.Request.Cookies[CitizenSession] != null)
                {
                    var cookie = HttpContext.Current.Request.Cookies[CitizenSession].Value;


                    session = sessionRepository
                        .FirstOrDefault(s => s.Cookie == cookie);
                    if (session != null)
                    {
                        if (session.IP != ClientIP && ClientIP != "::1" && ClientIP != "127.0.0.1")
                            session = null;
                        else if (session.ExpirationDate.CompareTo(DateTime.Now) < 0)
                        {
                            sessionRepository.Remove(session.ID);
                            session = null;
                            sessionRepository.SaveChanges();
                        }
                    }*/
                //}

                return new Sesja();
            }
            set
            {
             //   HttpContext.Current.Session[CitizenSession] = value;
              //  HttpContext.Current.Response.Cookies.Add(new HttpCookie(CitizenSession, value.Cookie));
            }
        }
    }
}