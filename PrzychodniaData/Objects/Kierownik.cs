using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Kierownik
    {
        public int LekarzID { get; set; }
        public int PrzychodniaID { get; set; }
        public DateTime PoczatekPrzydzialu { get; set; }
        public DateTime? KoniecPrzydzialu { get; set; }
    }
}
