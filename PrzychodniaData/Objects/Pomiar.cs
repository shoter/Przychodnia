using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Pomiar
    {
        public string PacjentImie { get; set; } = "X";
        public string PacjentNazwisko { get; set; } = "PACJENT";
        public DateTime Data { get; set; }
        public string Przychodnia { get; set; }
        public string LekarzImie { get; set; }
        public string LekarzNazwisko { get; set; }
        public int Skurczowe { get; set; }
        public int Rozkurczowe { get; set; }
        public int Tetno { get; set; }
    }
}
