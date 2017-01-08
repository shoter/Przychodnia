using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class PrzychodniaRepository : RepositoryBase
    {
        public PrzychodniaRepository(PrzychodniaContext context) : base(context)
        {
        }

        public void Add(string nazwa)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_ins_przychodnie(:nazwa)");
                cmd.Add("nazwa", nazwa);

                cmd.ExecuteNonQuery();
            }
        }

        public Przychodnia Get(int przychodniaID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_przychodnie(:id)");
                cmd.Add("id", przychodniaID);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        return new Przychodnia()
                        {
                            ID = reader.ToInt("id"),
                            Nazwa = reader.ToString("nazwa"),
                            IloscLekarzy = reader.ToInt("lekarzeilosc")
                        };
                    }
                }
            }
            return null;
        }

        public List<Przychodnia> GetAll()
        {
            List<Przychodnia> przychodnie = new List<Przychodnia>();
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_all_przychodnie()");

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var przychodnia = new Przychodnia()
                        {
                            ID = reader.ToInt("id"),
                            Nazwa = reader.ToString("nazwa"),
                            IloscLekarzy = reader.ToInt("lekarzeilosc")
                        };
                        

                        if(reader.Exists("kierowniklekarzid"))
                        {
                            przychodnia.KierownikImie = reader.ToString("kierownikimie");
                            przychodnia.KierownikNazwisko = reader.ToString("kierowniknazwisko");
                        }

                        przychodnie.Add(przychodnia);
                    }
                }
            }
            return przychodnie;
        }
    }
}
