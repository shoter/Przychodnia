using Data.Objects;
using Npgsql;
using PrzychodniaData.Enums;
using PrzychodniaData.Objects;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class UzytkownikRepository :RepositoryBase
    {
        private readonly KierownikRepository kierownikRepository;
        public UzytkownikRepository(PrzychodniaContext context, KierownikRepository kierownikRepository) : base(context)
        {
            this.kierownikRepository = kierownikRepository;
        }


        public List<LoginInfo> GetLoginInfo()
        {
            using (DisposableConnection)
            {

                List<LoginInfo> loginy = new List<LoginInfo>();

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_all_logininfo()");

                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                    while(reader.Read())
                    {
                        loginy.Add(new LoginInfo()
                        {
                            Login = reader.ToString("login"),
                            Password = reader.ToString("password")
                        });
                    }

                return loginy;
            }
        }
        public Uzytkownik Get(string username, string password)
        {
            using (DisposableConnection)
            {
                var nameParam = Parameter("nazwa", username);
                var passParam = Parameter("haslo", password);

                var conn = DisposableConnection.Connection;
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from sp_sel_uzytkownicy(:nazwa,:haslo)";


                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(passParam);

                return getUser(conn, ref cmd);
            }
        }

        public Uzytkownik Get(int id)
        {
            using (DisposableConnection)
            {
                var idParam = Parameter("id", id);

                var conn = DisposableConnection.Connection;
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from sp_sel_uzytkownicy(:id)";
                cmd.Parameters.Add(idParam);

                return getUser(conn, ref cmd);
            }
        }

        public void UpdatePrawa(int userID, string settings)
        {
            using (DisposableConnection)
            {
                var idParam = Parameter("id", userID);
                var settingsParam = Parameter("settings", settings);

                var cmd = DisposableConnection.CreateCommand("select sp_upd_many_prawa(:id, :settings)");
                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(settingsParam);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Uzytkownik> GetAll(int? prawo)
        {
            List<Uzytkownik> uzytkownicy = new List<Uzytkownik>();
            using (DisposableConnection)
            {
                var prawoParam = Parameter("prawo", prawo);

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_all_uzytkownicy(:prawo)");

                cmd.Parameters.Add(prawoParam);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Uzytkownik user = new Uzytkownik()
                        {
                            ID = int.Parse(reader[0].ToString()),
                            nazwaUzytkownika = reader[1].ToString()
                        };

                        if (string.IsNullOrWhiteSpace(reader[2].ToString()) == false)
                        {
                            var lekarz = new Lekarz()
                            {
                                ID = int.Parse(reader[2].ToString()),
                                Imie = reader[3].ToString(),
                                Nazwisko = reader[4].ToString()
                            };

                            user.Lekarz = lekarz;
                            user.LekarzID = lekarz.ID;
                        }

                        uzytkownicy.Add(user);
                    }
                }

                return uzytkownicy;
            }
        }

        private Uzytkownik getUser(DbConnection conn, ref DbCommand cmd)
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
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from sp_sel_prawa(:id)";
            cmd.Parameters.Add(idParam);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = int.Parse(reader[0].ToString());

                user.Prawa.Add((PrawoUzytkownikaEnum)id);
            }

            reader.Close();

            
            if(user.Is(PrawoUzytkownikaEnum.Kierownik) && user.LekarzID.HasValue)
            {
                user.Kierownicy = kierownikRepository.GetForLekarz(user.LekarzID.Value);
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

                var conn = DisposableConnection.Connection;
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select sp_ins_uzytkownicy(:nazwa,:haslo, :lekarzid)";

                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(passParam);
                cmd.Parameters.Add(lekarzParam);

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

                var conn = DisposableConnection.Connection;
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select sp_ins_uzytkownicy(:nazwa,:haslo, :imie, :nazwisko, :pesel, :data, :mezczyzna)";

                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(passParam);
                cmd.Parameters.Add(lekarzNameParam);
                cmd.Parameters.Add(lekarzSurnameParam);
                cmd.Parameters.Add(lekarzPeselParam);
                cmd.Parameters.Add(lekarzDateParam);
                cmd.Parameters.Add(mezczyznaParam);


                var result = cmd.ExecuteNonQuery();

            }
        }
    }
}
