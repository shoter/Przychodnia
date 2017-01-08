using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class OsobaRepository : RepositoryBase
    {
        public OsobaRepository(PrzychodniaContext context) : base(context)
        {
        }

        public List<Osoba> GetAll()
        {
            List<Osoba> lekarze = new List<Osoba>();
            using (DisposableConnection)
            {

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_all_osoby()");

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var Osoba = new Osoba()
                    {
                        ID = reader.ToInt("id"),
                        Imie = reader.ToString("imie"),
                        Nazwisko = reader.ToString("nazwisko")
                    };

                    lekarze.Add(Osoba);
                }

                return lekarze;
            }
        }
    }
}
