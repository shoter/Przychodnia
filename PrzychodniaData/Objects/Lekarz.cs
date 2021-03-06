﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Objects
{
    public class Lekarz
    {
        public Lekarz() { }

        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NazwaUzytkownika { get; set; }

        public List<Przydzial> Przydzialy { get; set; } = new List<Przydzial>();
        public List<Kierownik> Kierownicy { get; set; } = new List<Kierownik>();
    }
}
