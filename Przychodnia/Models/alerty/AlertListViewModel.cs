using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.alerty
{
    public class AlertListViewModel
    {
        public List<Alert> Alerty { get; set; } = new List<Alert>();

        public AlertListViewModel() { }
        public AlertListViewModel(List<Alert> alerty)
        {
            Alerty = alerty;
        }
    }
}