using NpgsqlTypes;
using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class KierownikRepository : RepositoryBase
    {
        public KierownikRepository(PrzychodniaContext context) : base(context)
        {
        }

        public void Insert(int lekarzID, int przychodniaID, DateTime poczatek, DateTime? koniec)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_ins_kierownicy(:lekarzid, :poczatek, :koniec, :przychodniaid)");

                cmd.Add("lekarzid", lekarzID);
                cmd.Add("przychodniaid", przychodniaID);
                cmd.Add("poczatek", poczatek);
                cmd.Add("koniec", koniec, NpgsqlDbType.Date);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateKierownik(int lekarzID, string settings)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_upd_many_kierownicy(:lekarzid, :settings)");

                cmd.Add("lekarzID", lekarzID);
                cmd.Add("settings", settings);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Kierownik> GetForLekarz(int lekarzID)
        {
            using (DisposableConnection)
            {
                return GetForLekarzInternal(lekarzID, DisposableConnection.Connection);
            }
        }

        public List<Kierownik> GetForLekarz(int lekarzID, DbConnection connection)
        {
            return GetForLekarzInternal(lekarzID, connection);
        }


        internal List<Kierownik> GetForLekarzInternal(int lekarzID, DbConnection connection)
        {
            List<Kierownik> kierwonicy = new List<Kierownik>();
            var cmd = connection.CreateCommand("select * from sp_sel_kierownicy(:lekarzid)");
            cmd.Add("lekarzid", lekarzID);

            var reader = cmd.ExecuteReader();
            if(reader.HasRows)
            while (reader.Read())
            {
                kierwonicy.Add(new Kierownik()
                {
                    LekarzID = reader.ToInt("lekarzid"),
                    PrzychodniaID = reader.ToInt("przychodniaid"),
                    PoczatekPrzydzialu = reader.ToDate("poczatek").Value,
                    KoniecPrzydzialu = reader.ToDate("koniec")
                });
            }

            reader.Close();
            return kierwonicy;
        }
    }

    
}
