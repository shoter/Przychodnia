using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Models.Choroby
{
    public class DziennikViewModel
    {
        public Dziennik Dziennik { get; set; } = new Dziennik();
        public List<StatusChoroby> Statusy { get; set; } = new List<StatusChoroby>();
        public string Notatka { get; set; }
        public List<SelectListItem> Przychodnie { get; set; } = new List<SelectListItem>();
        public int PrzychodnieID { get; set; }

        public DziennikViewModel(Dziennik dziennik, List<StatusChoroby> statusy, List<Przydzial> przydzialy)
        {
            Dziennik = dziennik;
            Statusy = statusy;

            foreach(var przydzial in przydzialy)
            {
                Przychodnie.Add(new SelectListItem()
                {
                    Text = przydzial.PrzychodniaNazwa,
                    Value = przydzial.PrzychodniaID.ToString()
                });
            }
        }

        public DziennikViewModel() { }
    }
}