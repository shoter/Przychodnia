using PrzychodniaData.Enums;
using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Objects
{
    [Table("uzytkownicy", Schema = "public")]
    public class Uzytkownik
    {
        public Uzytkownik()
        {
        }

        public int ID { get; set; }
        public string nazwaUzytkownika { get; set; }
        public int? LekarzID { get; set; }
        public Lekarz Lekarz { get; set; }
        public List<PrawoUzytkownikaEnum> Prawa { get; set; } = new List<PrawoUzytkownikaEnum>();
        public List<Kierownik> Kierownicy { get; set; } = new List<Kierownik>();


    }

    public static class UzytkownikExtensions
    {
        public static bool Is(this Uzytkownik user, PrawoUzytkownikaEnum prawo)
        {
            return user.Prawa.Contains(prawo);
        }
    }
}
