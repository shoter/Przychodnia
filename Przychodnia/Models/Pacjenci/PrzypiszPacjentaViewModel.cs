using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Models.Pacjenci
{
    public class PrzypiszPacjentaViewModel
    {
        public List<SelectListItem> Pacjenci { get; set; } = new List<SelectListItem>();
        public int PacjentID { get; set; }


        public PrzypiszPacjentaViewModel() { }

        public PrzypiszPacjentaViewModel(List<Pacjent> pacjenci)
        {
            foreach(var pacjent in pacjenci)
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