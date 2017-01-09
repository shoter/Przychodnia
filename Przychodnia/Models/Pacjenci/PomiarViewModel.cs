using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Models.Pacjenci
{
    public class PomiarViewModel
    {
        [Required]
        public int? PacjentID { get; set; }
        public List<SelectListItem> Pacjenci { get; set; } = new List<SelectListItem>();
        [Required]
        public int? Rozkurczowe { get; set; }
        [Required]
        public int? Skurczowe { get; set; }
        [Required]
        public int? Tetno { get; set; }
        [Required]
        public int? PrzychodniaID { get; set; }
        public List<SelectListItem> Przychodnie { get; set; } = new List<SelectListItem>();
        public PomiarViewModel() { }

        public PomiarViewModel(int? pacjentID, List<Pacjent> pacjenci, List<Przydzial> przydzialy)
        {
            PacjentID = pacjentID;
            foreach (var pacjent in pacjenci)
                Pacjenci.Add(new SelectListItem()
                {
                    Text = string.Format("{0} {1}", pacjent.Nazwisko, pacjent.Imie),
                    Value = pacjent.ID.ToString(),
                    Selected = pacjent.ID == pacjentID
                });

            foreach (var przydzial in przydzialy)
                Przychodnie.Add(new SelectListItem()
                {
                    Text = przydzial.PrzychodniaNazwa,
                    Value = przydzial.PrzychodniaID.ToString()
                });

        }

    }
}