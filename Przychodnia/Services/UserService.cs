using Data.Objects;
using Przychodnia.Code;
using Przychodnia.Helpers;
using PrzychodniaData.Objects;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Services
{
    public class UserService
    {
        private readonly UzytkownikRepository uzytkownikRepository;
        private readonly SesjaRepository sesjaRepository;

        public UserService(UzytkownikRepository uzytkownikRepository, SesjaRepository sesjaRepository)
        {
            this.uzytkownikRepository = uzytkownikRepository;
            this.sesjaRepository = sesjaRepository;
        }


        public Uzytkownik Login(string username, string password)
        {
            var user = uzytkownikRepository.Get(username, password);

            if(user != null)
            {
                SessionHelper.Sesja = createSession(user);
                return user;
            }
            return null;
        }

        private Sesja createSession(Uzytkownik uzytkownik)
        {
            var IP = SessionHelper.ClientIP;

            sesjaRepository.Remove(uzytkownik.ID);

            var cookie = Common.Base64Encode(uzytkownik.nazwaUzytkownika + IP);

            sesjaRepository.Insert(IP, cookie , DateTime.Now.ToUniversalTime().AddHours(1), uzytkownik.ID);

            var sesja = sesjaRepository.Get(cookie);

            return sesja;

        }

    }
}