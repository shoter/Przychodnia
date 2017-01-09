using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Osoby
{
    public class OsobyListViewModel
    {
        public List<Osoba> Osoby { get; set; } = new List<Osoba>();

        public OsobyListViewModel() { }
        public OsobyListViewModel(List<Osoba> osoby)
        {
            Osoby = osoby;
        }
    }
}