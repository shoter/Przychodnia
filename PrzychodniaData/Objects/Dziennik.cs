using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Dziennik
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public string NazwaChoroby { get; set; }
        public int PacjentID { get; set; }

        public string PacjentImie { get; set; }
        public string PacjentNazwisko { get; set; }
    }
}
