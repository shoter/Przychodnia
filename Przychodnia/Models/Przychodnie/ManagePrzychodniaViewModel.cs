using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Przychodnie
{
    public class ManagePrzychodniaViewModel
    {
        public int PrzychodniaID { get; set; }
        public string Nazwa { get; set; }
        public string KierownikNazwisko { get; set; } = "Kierownik";
        public string KierownikImie { get; set; } = "Imie";


        public List<Lekarz> NotWorkingDoctors { get; set; } = new List<Lekarz>();
        public List<Lekarz> WorkingDoctors { get; set; } = new List<Lekarz>();
        public List<Pomiar> Pomiary { get; set; } = new List<Pomiar>();
        public List<StatusChoroby> Wizyty { get; set; } = new List<StatusChoroby>();

        public ManagePrzychodniaViewModel(PrzychodniaData.Objects.Przychodnia przychodnia, List<Lekarz> lekarze, List<Pomiar> pomiary, List<StatusChoroby> wizyty)
        {
            PrzychodniaID = przychodnia.ID;
            Nazwa = przychodnia.Nazwa;
            KierownikNazwisko = przychodnia.KierownikNazwisko;
            KierownikImie = przychodnia.KierownikImie;

            WorkingDoctors = lekarze.Where(l => l.Przydzialy.Any(p => p.KoniecPrzydzialu.HasValue == false) == true).ToList();
            NotWorkingDoctors = lekarze.Where(l => l.Przydzialy.Any(p => p.KoniecPrzydzialu.HasValue == false) == false).ToList();
            Pomiary = pomiary;
            Wizyty = wizyty;
        }
    }
}