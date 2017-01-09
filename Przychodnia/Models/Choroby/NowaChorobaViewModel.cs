using Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Choroby
{
    public class NowaChorobaViewModel
    {
        public string Nazwa { get; set; }
        public List<TypChoroby> Choroby { get; set; } = new List<TypChoroby>();

        public NowaChorobaViewModel() { }
        public NowaChorobaViewModel(List<TypChoroby> choroby)
        {
            Choroby = choroby;
        }
    }
}