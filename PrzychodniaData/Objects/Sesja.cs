using Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Sesja
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string Ciasteczko { get; set; }
        public DateTime DataWygasniecia { get; set; }
        public int UzytkownikID { get; set; }

        public Uzytkownik Uzytkownik { get; set; }
    }
}
