using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Home
{
    public class HomeViewModel
    {
        public List<NajlepszyLekarz> NajlepsiLekarze { get; set; } = new List<NajlepszyLekarz>();

        public HomeViewModel(List<NajlepszyLekarz> lekarze)
        {
            NajlepsiLekarze = lekarze;
        }
    }
}