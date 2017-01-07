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

            NpgsqlConnection conn = Context.NpgsqlConnection;
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("select * from sp_sel_sesje(:ciastko)", conn);
            cmd.Add(cookieParam);

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
            conn.Close();

            var user = uzytkownikRepository.Get(sesja.UzytkownikID);

            sesja.Uzytkownik = user;

            return sesja;
        }

        public void Update(int id, DateTime data)
        {
            var idParam = Parameter("ID", id);
            var dataParam = Parameter("datawygasniecia", data);

            using (DisposableConnection)
            {
                var conn = DisposableConnection.Connection;
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select sp_upd_sesje(:ID, :datawygasniecia)"
                NpgsqlCommand cmd = new NpgsqlCommand(, conn);
                cmd.Add(idParam);
                cmd.Add(dataParam);

                cmd.ExecuteNonQuery();
            }
        }

        public void Insert(string ip, string cookie, DateTime data, int userID)
        {
            var ipParam = Parameter("IP", ip);
            var ciasteczkoParam = Parameter("ciasteczko", cookie);
            var dataParam = Parameter("datawygasniecia", data);
            var uzytkownikParam = Parameter("uzytkownikid", userID);

            NpgsqlConnection conn = Context.NpgsqlConnection;
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("select sp_ins_sesje(:IP, :ciasteczko, :datawygasniecia, :uzytkownikid)", conn);
            cmd.Add(ipParam);
            cmd.Add(ciasteczkoParam);
            cmd.Add(dataParam);
            cmd.Add(uzytkownikParam);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Remove(int uzytkownikID)
        {
            var uzytkownikParam = Parameter("uzytkownikid", uzytkownikID);

            NpgsqlConnection conn = Context.NpgsqlConnection;
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("select sp__del_sesje(:uzytkownikid)", conn);

            cmd.Add(uzytkownikParam);

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
