using Data.Objects;
using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Models.Choroby
{
    public class AddChorobaViewModel
    {
        public int PacjentID { get; set; }

        public List<SelectListItem> Dzienniki { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Przychodnie { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Choroby { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Pacjenci { get; set; } = new List<SelectListItem>();
        public int? DziennikID { get; set; }
        public int PrzychodnieID { get; set; }
        public string Notatka { get; set; }

        public bool NowyDziennik { get; set; } = false;
        public string NazwaDziennika { get; set; }
        public int? ChorobaID { get; set; }

        public AddChorobaViewModel() { }

        public AddChorobaViewModel(List<Dziennik> dzienniki, List<Przydzial> przydzialy, List<TypChoroby> choroby, int pacjentID, List<Pacjent> pacjenci)
        {
            Init(dzienniki, przydzialy, choroby, pacjenci);
            this.PacjentID = pacjentID;
        }

        public void Init(List<Dziennik> dzienniki, List<Przydzial> przydzialy, List<TypChoroby> choroby, List<Pacjent> pacjenci)
        {
            foreach (var dziennik in dzienniki)
            {
                Dzienniki.Add(new SelectListItem()
                {
                    Text = dziennik.Nazwa,
                    Value = dziennik.ID.ToString()
                });
            }

            foreach (var przydzial in przydzialy)
            {
                Przychodnie.Add(new SelectListItem()
                {
                    Text = przydzial.PrzychodniaNazwa,
                    Value = przydzial.PrzychodniaID.ToString()
                });
            }
            Choroby.Add(new SelectListItem()
            {
                Value = "",
                Text = "Nieokreślona"
            });
            foreach(var choroba in choroby)
            {
                Choroby.Add(new SelectListItem()
                {
                    Value = choroba.ID.ToString(),
                    Text = choroba.Nazwa
                });
                
            }

            foreach (var pacjent in pacjenci)
            {
                Pacjenci.Add(new SelectListItem()
                {
                    Value = pacjent.ID.ToString(),
                    Text = string.Format("{0} {1}", pacjent.Nazwisko, pacjent.Imie)
                });

            }
        }
    }
}