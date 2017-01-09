using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Pacjenci
{
    public class InfoViewModel
    {
        public List<Pomiar> Pomiary { get; set; } = new List<Pomiar>();
        public List<StatusChoroby> Choroby { get; set; } = new List<StatusChoroby>();

        public InfoViewModel(List<Pomiar> pomiary, List<StatusChoroby> choroby)
        {
            Pomiary = pomiary;
            Choroby = choroby;
        }
    }
}