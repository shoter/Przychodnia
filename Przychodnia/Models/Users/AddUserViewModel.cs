using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Models.Users
{
    public class AddUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string RepeatPassword { get; set; }
        public int? OsobaID { get; set; }
        public bool CreateNewOsoba { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public bool Mezczyzna { get; set; }
        public DateTime? BirthDate { get; set; }

        public List<SelectListItem> Osoby { get; set; } = new List<SelectListItem>();

        public void SetLekarze(List<Osoba> osoby)
        {
            foreach(var osoba in osoby.OrderBy(l => l.Nazwisko))
            {
                Osoby.Add(new SelectListItem()
                {
                    Text = string.Format("{0} {1}", osoba.Nazwisko, osoba.Imie),
                    Value = osoba.ID.ToString()
                });
            }
        }

    }
}