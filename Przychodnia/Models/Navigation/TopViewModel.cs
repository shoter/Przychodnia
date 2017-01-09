using Data.Objects;
using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Navigation
{
    public class TopViewModel
    {
        public string Username { get; set; }
        public List<Alert> Alerty { get; set; } = new List<Alert>();

        public TopViewModel(Uzytkownik uzytkownik)
        {
            Username = uzytkownik.nazwaUzytkownika;
        }
    }
}