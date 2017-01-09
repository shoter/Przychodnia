using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Osoby
{
    public class UsmiercViewModel
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int OsobaID { get; set; }
        public DateTime? Date { get; set; }
        public DateTime DataNarodzin { get; set; }
        public UsmiercViewModel() { }
        public UsmiercViewModel(Osoba osoba)
        {
            Imie = osoba.Imie;
            Nazwisko = osoba.Nazwisko;
            OsobaID = osoba.ID;
            DataNarodzin = osoba.DataUrodzenia;
            Date = null;
        }
    }
}