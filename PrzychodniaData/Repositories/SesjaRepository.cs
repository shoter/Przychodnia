using Npgsql;
using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class SesjaRepository : RepositoryBase
    {
        private readonly UzytkownikRepository uzytkownikRepository;

        public SesjaRepository(PrzychodniaContext context, UzytkownikRepository uzytkownikRepository) : base(context)
        {
            this.uzytkownikRepository = uzytkownikRepository;
        }

        public Sesja Get(string cookie)
        {
            var cookieParam = Parameter("ciastko", cookie);
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_sesje(:ciastko)");
                cmd.Parameters.Add(cookieParam);

                var reader = cmd.ExecuteReader();

                if (reader.Read() == false)
                    return null;


                Sesja sesja = new Sesja()
                {
                    ID = int.Parse(reader[0].ToString()),
                    IP = reader[1].ToString(),
                    Ciasteczko = reader[2].ToString(),
                    DataWygasniecia = DateTime.Parse(reader[3].ToString()),
                    UzytkownikID = int.Parse(reader[4].ToString())
                };
                reader.Close();

                var user = uzytkownikRepository.Get(sesja.UzytkownikID);

                sesja.Uzytkownik = user;

                return sesja;
            }
        }

        public void Update(int id, DateTime data)
        {
            var idParam = Parameter("ID", id);
            var dataParam = Parameter("datawygasniecia", data);

            using (DisposableConnection)
            {
                var conn = DisposableConnection.Connection;

                var cmd = conn.CreateCommand();
                cmd.CommandText = "select sp_upd_sesje(:ID, :datawygasniecia)";
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(dataParam);

                cmd.ExecuteNonQuery();
            }
        }

        public void Insert(string ip, string cookie, DateTime data, int userID)
        {
            var ipParam = Parameter("IP", ip);
            var ciasteczkoParam = Parameter("ciasteczko", cookie);
            var dataParam = Parameter("datawygasniecia", data);
            var uzytkownikParam = Parameter("uzytkownikid", userID);

            var conn = Context.Database.Connection;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "select sp_ins_sesje(:IP, :ciasteczko, :datawygasniecia, :uzytkownikid)";
            cmd.Parameters.Add(ipParam);
            cmd.Parameters.Add(ciasteczkoParam);
            cmd.Parameters.Add(dataParam);
            cmd.Parameters.Add(uzytkownikParam);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Remove(int uzytkownikID)
        {
            var uzytkownikParam = Parameter("uzytkownikid", uzytkownikID);

            var conn = Context.Database.Connection;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "select sp__del_sesje(:uzytkownikid)";

            cmd.Parameters.Add(uzytkownikParam);

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
