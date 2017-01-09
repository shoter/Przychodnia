using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Pacjenci
{
    public class IndexViewModel
    {
        public List<Pacjent> Pacjenci { get; set; } = new List<Pacjent>();

        public IndexViewModel(List<Pacjent> pacjenci)
        {
            Pacjenci = pacjenci;
        }


    }
}