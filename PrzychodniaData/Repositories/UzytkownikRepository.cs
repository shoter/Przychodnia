using Data.Objects;
using Npgsql;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class UzytkownikRepository :RepositoryBase
    {
        public UzytkownikRepository(PrzychodniaContext context) : base(context)
        {
        }

        public Uzytkownik Get(string username, string password)
        {
            using (DisposableConnection)
            {
                var nameParam = Parameter("nazwa", username);
                var passParam = Parameter("haslo", password);

                NpgsqlConnection conn = DisposableConnection.Connection;

                NpgsqlCommand cmd = new NpgsqlCommand("select * from sp_sel_uzytkownicy(:nazwa,:haslo)", conn);
                cmd.Add(nameParam);
                cmd.Add(passParam);

                return getUser(conn, ref cmd);
            }
        }

        public Uzytkownik Get(int id)
        {
            using (DisposableConnection)
            {
                var idParam = Parameter("id", id);

                NpgsqlConnection conn = DisposableConnection.Connection;
                NpgsqlCommand cmd = new NpgsqlCommand("select * from sp_sel_uzytkownicy(:id)", conn);
                cmd.Add(idParam);

                return getUser(conn, ref cmd);
            }
        }

        private Uzytkownik getUser(NpgsqlConnection conn, ref NpgsqlCommand cmd)
        {
            var reader = cmd.ExecuteReader();

            if (reader.Read() == false)
                return null;

            Uzytkownik user = new Uzytkownik()
            {
                ID = int.Parse(reader[0].ToString()),
                nazwaUzytkownika = reader[1].ToString()
            };

            int val;
            if (int.TryParse(reader[2].ToString(), out val))
                user.LekarzID = val;

            reader.Close();

            var idParam = Parameter("id", user.ID);

            cmd = new NpgsqlCommand("select * from sp_sel_prawa(:id)", conn);
            cmd.Add(idParam);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = int.Parse(reader[0].ToString());

                user.Prawa.Add((PrawoUzytkownikaEnum)id);
            }


            return user;
        }

        public void CreateAccount(string username, string password, int? lekarzID)
        {
            using (DisposableConnection)
            {
                var nameParam = Parameter("nazwa", username);
                var passParam = Parameter("haslo", password);
                var lekarzParam = Parameter("lekarzid", lekarzID);

                NpgsqlConnection conn = DisposableConnection.Connection;

                NpgsqlCommand cmd = new NpgsqlCommand("select sp_ins_uzytkownicy(:nazwa,:haslo, :lekarzid)", conn);
                cmd.Add(nameParam);
                cmd.Add(passParam);
                cmd.Add(lekarzParam);

                var result = cmd.ExecuteNonQuery();

            }
        }

        public void CreateAccount(string username, string password, string lekarzName, string lekarzSurname, string lekarzPesel, DateTime lekarzBirthDate, bool mezczyzna)
        {
            using (DisposableConnection)
            {
                var nameParam = Parameter("nazwa", username);
                var passParam = Parameter("haslo", password);
                var lekarzNameParam = Parameter("imie", lekarzName);
                var lekarzSurnameParam = Parameter("nazwisko", lekarzSurname);
                var lekarzPeselParam = Parameter("pesel", lekarzPesel);
                var lekarzDateParam = Parameter("data", lekarzBirthDate, NpgsqlTypes.NpgsqlDbType.Date);
                var mezczyznaParam = Parameter("mezczyzna", mezczyzna);

                NpgsqlConnection conn = DisposableConnection.Connection;

                NpgsqlCommand cmd = new NpgsqlCommand("select sp_ins_uzytkownicy(:nazwa,:haslo, :imie, :nazwisko, :pesel, :data, :mezczyzna)", conn);
                cmd.Add(nameParam);
                cmd.Add(passParam);
                cmd.Add(lekarzNameParam);
                cmd.Add(lekarzSurnameParam);
                cmd.Add(lekarzPeselParam);
                cmd.Add(lekarzDateParam);
                cmd.Add(mezczyznaParam);


                var result = cmd.ExecuteNonQuery();

            }
        }
    }
}
