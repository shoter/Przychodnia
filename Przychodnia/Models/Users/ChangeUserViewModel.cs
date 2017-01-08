using Data.Objects;
using PrzychodniaData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrzychodniaData.Objects;

namespace Przychodnia.Models.Users
{
    public class ChangeUserViewModel
    {
        public List<ChangeUserItemViewModel> Prawa { get; set; } = new List<ChangeUserItemViewModel>();
        public List<ChangeUserKierownikViewModel> Kierownictwo { get; set; } = new List<ChangeUserKierownikViewModel>();
        public List<Przydzial> Przydzialy { get; set; } = new List<Przydzial>();
        public string Username { get; set; }
        public int UserID { get; set; }
        public bool CanBeKierownik { get; set; } = false;
        public int? LekarzID { get; set; }
        public ChangeUserViewModel() { }

        public ChangeUserViewModel(Uzytkownik user, List<PrzychodniaData.Objects.Przychodnia> przychodnie)
        {
            UserID = user.ID;
            CanBeKierownik = user.LekarzID.HasValue;
            LekarzID = user.LekarzID;

            Username = user.nazwaUzytkownika;
            {
                foreach (PrawoUzytkownikaEnum prawo in Enum.GetValues(typeof(PrawoUzytkownikaEnum)))
                {
                    Prawa.Add(new ChangeUserItemViewModel()
                    {
                        Prawo = prawo,
                        Value = false,
                        Disabled = (prawo != PrawoUzytkownikaEnum.Administrator)
                    });
                }
            }

            foreach(var prawo in user.Prawa)
            {
                Prawa.Where(p => p.Prawo == prawo).First().Value = true;
            }

            foreach(var przychodnia in przychodnie)
            {
                Kierownictwo.Add(new ChangeUserKierownikViewModel()
                {
                    PrzychodniaID = przychodnia.ID,
                    Value = user.Kierownicy.Any(k => k.PrzychodniaID == przychodnia.ID && k.KoniecPrzydzialu.HasValue == false),
                    Nazwa = przychodnia.Nazwa
                });
            }
        }

        public void MakeAbsentValuesAsFalse()
        {
            foreach (PrawoUzytkownikaEnum prawo in Enum.GetValues(typeof(PrawoUzytkownikaEnum)))
            {
                if (Prawa.Any(p => p.Prawo == prawo) == false)
                    Prawa.Add(new ChangeUserItemViewModel()
                    {
                        Prawo = prawo,
                        Value = false
                    });
            }
        }

        internal void zaladuj(List<Przydzial> zatrudnienia)
        {
            Przydzialy = zatrudnienia;
        }
    }

    public class ChangeUserItemViewModel
    {
        public ChangeUserItemViewModel() { }

        public PrawoUzytkownikaEnum Prawo { get; set; }
        public bool Value { get; set; }
        public bool Disabled { get; set; } = false;
    }

    public class ChangeUserKierownikViewModel
    {
        public int PrzychodniaID { get; set; }
        public string Nazwa { get; set; }
        public bool Value { get; set; }
    }

}