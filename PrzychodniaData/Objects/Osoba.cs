using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Osoba
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public DateTime? DataZgonu { get; set; }
        public bool CzyMezczyzna { get; set; }
        public string Pesel { get; set; }
    }
}
