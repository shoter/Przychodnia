using Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Navigation
{
    public class TopViewModel
    {
        public string Username { get; set; }

        public TopViewModel(Uzytkownik uzytkownik)
        {
            Username = uzytkownik.nazwaUzytkownika;
        }
    }
}