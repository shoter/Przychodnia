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


        public List<Lekarz> NotWorkingDoctors { get; set; } = new List<Lekarz>();
        public List<Lekarz> WorkingDoctors { get; set; } = new List<Lekarz>();

        public ManagePrzychodniaViewModel(PrzychodniaData.Objects.Przychodnia przychodnia, List<Lekarz> lekarze)
        {
            PrzychodniaID = przychodnia.ID;
            Nazwa = przychodnia.Nazwa;

            WorkingDoctors = lekarze.Where(l => l.Przydzialy.Any(p => p.KoniecPrzydzialu.HasValue == false) == true).ToList();
            NotWorkingDoctors = lekarze.Where(l => l.Przydzialy.Any(p => p.KoniecPrzydzialu.HasValue == false) == false).ToList();
        }
    }
}