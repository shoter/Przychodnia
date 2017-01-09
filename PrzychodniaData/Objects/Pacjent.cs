using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Pacjent
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public DateTime? DataZgonu { get; set; }
        public string Notatki { get; set; }
        public string Pesel { get; set; }
        public bool mezczyzna { get; set; }
    }
}
