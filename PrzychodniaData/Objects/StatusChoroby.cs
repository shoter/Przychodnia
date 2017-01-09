using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class StatusChoroby
    {
        public int ID { get; set; }
        public string Notatka { get; set; }
        public string LekarzImie { get; set; }
        public string LekarzNazwisko { get; set; }
        public DateTime Data { get; set; }
        public string NazwaPrzychodni { get; set; }
        public string NazwaDziennika { get; set; }
        public string NazwaChoroby { get; set; }
        public int DziennikID { get; set; }
        public string PacjentImie { get; set; }
        public string PacjentNazwisko { get; set; }
    }
}
