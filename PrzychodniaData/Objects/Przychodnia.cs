using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Przychodnia
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public List<Lekarz> Lekarze { get; set; }
        public int IloscLekarzy { get; set; }
        public string KierownikImie { get; set; }
        public string KierownikNazwisko { get; set; }

    }
}
