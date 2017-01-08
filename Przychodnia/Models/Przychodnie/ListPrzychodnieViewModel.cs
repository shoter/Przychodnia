using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Przychodnie
{
    public class ListPrzychodnieViewModel
    {
        public List<PrzychodniaData.Objects.Przychodnia> Przychodnie { get; set; } = new List<PrzychodniaData.Objects.Przychodnia>();

        public ListPrzychodnieViewModel(List<PrzychodniaData.Objects.Przychodnia> przychodnie)
        {
            Przychodnie = przychodnie;
        }
    }

 
}