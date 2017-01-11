using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Pacjenci
{
    public class InfoViewModel
    {
        public int PacjentID { get; set; }
        public List<Pomiar> Pomiary { get; set; } = new List<Pomiar>();
        public List<StatusChoroby> Choroby { get; set; } = new List<StatusChoroby>();
        public string Notatki { get; set; }

        public InfoViewModel(List<Pomiar> pomiary, List<StatusChoroby> choroby, string notatki, int pacjentID)
        {
            Pomiary = pomiary;
            Choroby = choroby;
            Notatki = notatki;
            PacjentID = pacjentID;
        }
    }
}