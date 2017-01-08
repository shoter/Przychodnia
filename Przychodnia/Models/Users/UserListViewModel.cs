using Data.Objects;
using PrzychodniaData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Models.Users
{
    public class UserListViewModel
    {
        public List<Uzytkownik> Uzytkownicy { get; set; } = new List<Uzytkownik>();
        public List<SelectListItem> Prawa { get; set; } = new List<SelectListItem>();
        public int? AktualnePrawoID { get; set; } = null;

        public UserListViewModel() { }
        public UserListViewModel(List<Uzytkownik> users, int? aktualnePrawo) : this(users)
        {
            AktualnePrawoID = aktualnePrawo;
        }

        public UserListViewModel(List<Uzytkownik> users)
        {
            Uzytkownicy = users;
            Prawa.Add(new SelectListItem()
            {
                Text = "-- nic --",
                Value = "",
                Selected = true
            });
            foreach (PrawoUzytkownikaEnum prawo in Enum.GetValues(typeof(PrawoUzytkownikaEnum)))
            {
                Prawa.Add(new SelectListItem()
                {
                    Text = prawo.ToString(),
                    Value = ((int)prawo).ToString()
                });
            }
        }
    }
}