using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Choroby
{
    public class DziennikiListIndexViewModel
    {
        public List<Dziennik> Dzienniki { get; set; } = new List<PrzychodniaData.Objects.Dziennik>();


        public DziennikiListIndexViewModel() { }

        public DziennikiListIndexViewModel(List<Dziennik> dzienniki)
        {
            Dzienniki = dzienniki;
        }
    }
}