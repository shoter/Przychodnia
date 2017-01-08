using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Przydzial
    {
        public int PrzychodniaID { get; set; }
        public int LekarzID { get; set; }
        public DateTime PoczatekPrzydzialu { get; set; }
        public DateTime? KoniecPrzydzialu { get; set; }
        public string PrzychodniaNazwa { get; set; }
    }
}
