using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Pacjenci
{
    public class AddPacjentViewModel
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public DateTime? DataZgonu { get; set; }
        public string Notes { get; set; }
        public bool CzyMezczyzna { get; set; }
        public string Pesel { get; set; }
    }
}