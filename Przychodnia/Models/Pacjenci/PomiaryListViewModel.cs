using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Pacjenci
{
    public class PomiaryListViewModel
    {
        public List<Pomiar> Pomiary { get; set; } = new List<Pomiar>();

        public PomiaryListViewModel() { }

        public PomiaryListViewModel(List<Pomiar> pomiary)
        {
            Pomiary = pomiary;
        }
    }
}