using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Models.Przychodnie
{
    public class AddPrzydzialViewModel
    {
        public List<SelectListItem> Lekarze { get; set; } = new List<SelectListItem>();
        public int PrzychodniaID { get; set; }
        public string Nazwa { get; set; }
        public int LekarzID { get; set; }

        public AddPrzydzialViewModel() { }

        public AddPrzydzialViewModel(List<Lekarz> lekarze, PrzychodniaData.Objects.Przychodnia przychodnia)
        {
            PrzychodniaID = przychodnia.ID;
            Nazwa = przychodnia.Nazwa;

            foreach (var lekarz in lekarze.OrderBy(l => l.Nazwisko))
                Lekarze.Add(new SelectListItem()
                {
                    Text = string.Format("{0} {1}({2})", lekarz.Nazwisko, lekarz.Imie, lekarz.NazwaUzytkownika),
                    Value = lekarz.ID.ToString()
                });
        }
    }
}