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

        public void Usmierc(int osobaID, DateTime dataZgonu)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_usmierc(:osobaID, :data)");

                cmd.Add("osobaID", osobaID);
                cmd.Add("data", dataZgonu, NpgsqlTypes.NpgsqlDbType.Date);

                cmd.ExecuteNonQuery();
            }
        }

        public Osoba get(int osobaID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_osoba(:osobaid)");
                cmd.Add("osobaid", osobaID);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    return new Osoba()
                    {
                        ID = reader.ToInt("id"),
                        Imie = reader.ToString("imie"),
                        Nazwisko = reader.ToString("nazwisko"),
                        CzyMezczyzna = reader.toBool("czymezczyzna"),
                        DataUrodzenia = reader.ToDate("dataurodzenia").Value,
                        DataZgonu = reader.ToDate("datazgonu"),
                        Pesel = reader.ToString("pesel")
                    };
                }


            }

            throw new Exception("Nie udało się załadować osoby. Dziiiwne");
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
                        Nazwisko = reader.ToString("nazwisko"),
                        CzyMezczyzna = reader.toBool("czymezczyzna"),
                        DataUrodzenia = reader.ToDate("dataurodzenia").Value,
                        DataZgonu = reader.ToDate("datazgonu"),
                        Pesel = reader.ToString("pesel")
                    };

                    lekarze.Add(Osoba);
                }

                return lekarze;
            }
        }
    }
}
