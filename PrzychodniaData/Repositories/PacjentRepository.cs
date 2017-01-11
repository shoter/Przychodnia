using NpgsqlTypes;
using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class PacjentRepository : RepositoryBase
    {
        public PacjentRepository(PrzychodniaContext context) : base(context)
        {
        }
        public string GetNotatki(int pacjentID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_pacjent_notatki(:pacjentID)");
                cmd.Add("pacjentID", pacjentID);

                var reader = cmd.ExecuteReader();

                if(reader.HasRows && reader.Read())
                {
                    var notatki = reader.ToString("notatki");
                    return notatki;
                }
            }
            throw new Exception("Nie znaleziono notatek? Ten błąd nie powinien wystąpić.");
        }

        public void UpdateNotatki(int pacjentID, string notatki)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_upd_pacjent_notatki(:pacjentID, :notatki)");
                cmd.Add("pacjentID", pacjentID);
                cmd.Add("notatki", notatki);

                cmd.ExecuteNonQuery();
            }
        }
        public void Insert(string imie, string nazwisko, string pesel, bool czymezczyzna, DateTime dataurodzenia, DateTime? datazgonu, string notes)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_ins_pacjent(:imie, :nazwisko, :pesel, :urodzenia, :zgonu, :mezczyzna, :notatki)");
                cmd.Add("imie", imie);
                cmd.Add("nazwisko", nazwisko);
                cmd.Add("pesel", pesel);
                cmd.Add("urodzenia", dataurodzenia, NpgsqlDbType.Date);
                cmd.Add("zgonu", datazgonu, NpgsqlDbType.Date);
                cmd.Add("mezczyzna", czymezczyzna);
                cmd.Add("notatki", notes);

                cmd.ExecuteNonQuery();

            }
        }

        public void AddPomiar(int pacjentID, int skurczowe, int rozkurczowe, int tetno, int lekarzID, int przychodniaID, DateTime date)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_ins_pomiary(:pacjentid, :rozkurczowe, :skurczowe, :tetno, :lekarzid, :przychodniaid, :data)");
                cmd.Add("pacjentid", pacjentID);
                cmd.Add("rozkurczowe", rozkurczowe);
                cmd.Add("skurczowe", skurczowe);
                cmd.Add("tetno", tetno);
                cmd.Add("lekarzid", lekarzID);
                cmd.Add("przychodniaid", przychodniaID);
                cmd.Add("data", date);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Pacjent> GetAll()
        {
            List<Pacjent> pacjenci = new List<Pacjent>();
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_all_pacjenci()");

                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                    while(reader.Read())
                    {
                        var pacjent = new Pacjent()
                        {
                            DataUrodzenia = reader.ToDate("dataurodzenia").Value,
                            DataZgonu = reader.ToDate("datazgonu"),
                            ID = reader.ToInt("id"),
                            Imie = reader.ToString("imie"),
                            Nazwisko = reader.ToString("nazwisko"),
                            Notatki = reader.ToString("notatki"),
                            Pesel = reader.ToString("pesel"),
                            mezczyzna = reader.toBool("mezczyzna")
                        };

                        pacjenci.Add(pacjent);


                    }
            }

            return pacjenci;
        }

        public List<Pacjent> GetForLekarz(int lekarzID)
        {
            List<Pacjent> pacjenci = new List<Pacjent>();
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_lekarz_pacjenci(:id)");
                cmd.Add("id", lekarzID);

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var pacjent = new Pacjent()
                        {
                            DataUrodzenia = reader.ToDate("dataurodzenia").Value,
                            DataZgonu = reader.ToDate("datazgonu"),
                            ID = reader.ToInt("id"),
                            Imie = reader.ToString("imie"),
                            Nazwisko = reader.ToString("nazwisko"),
                            Notatki = reader.ToString("notatki"),
                            Pesel = reader.ToString("pesel"),
                            mezczyzna = reader.toBool("mezczyzna")
                        };

                        pacjenci.Add(pacjent);


                    }
            }

            return pacjenci;
        }

        public void Przypisz(int pacjentID, int lekarzID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_ins_przypis(:pacjentID, :lekarzID, :data)");
                cmd.Add("pacjentid", pacjentID);
                cmd.Add("lekarzid", lekarzID);
                cmd.Add("data", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Pomiar> GetPomiary(int pacjentID)
        {
            using (DisposableConnection)
            {
                List<Pomiar> pomiary = new List<Pomiar>();

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_many_pomiary(:pacjentid)");
                cmd.Add("pacjentid", pacjentID);

                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                    while(reader.Read())
                    {
                        var pomiar = new Pomiar()
                        {
                            Data = reader.ToDate("data").Value,
                            LekarzImie = reader.ToString("imie"),
                            LekarzNazwisko = reader.ToString("nazwisko"),
                            Przychodnia = reader.ToString("przychodnia"),
                            Rozkurczowe = reader.ToInt("rozkurczowe"),
                            Skurczowe = reader.ToInt("skurczowe"),
                            Tetno = reader.ToInt("tetno")
                        };

                        pomiary.Add(pomiar);
                    }

                return pomiary;
            }
        }

        public List<Pomiar> GetPomiaryForPrzychodnia(int przychodniaID)
        {
            using (DisposableConnection)
            {
                List<Pomiar> pomiary = new List<Pomiar>();

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_przychodnia_pomiary(:przychodniaID)");
                cmd.Add("przychodniaID", przychodniaID);

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var pomiar = new Pomiar()
                        {
                            Data = reader.ToDate("data").Value,
                            LekarzImie = reader.ToString("imie"),
                            LekarzNazwisko = reader.ToString("nazwisko"),
                            Przychodnia = reader.ToString("przychodnia"),
                            Rozkurczowe = reader.ToInt("rozkurczowe"),
                            Skurczowe = reader.ToInt("skurczowe"),
                            Tetno = reader.ToInt("tetno"),
                            PacjentImie = reader.ToString("pacjentimie"),
                            PacjentNazwisko = reader.ToString("pacjentnazwisko")
                        };

                        pomiary.Add(pomiar);
                    }

                return pomiary;
            }
        }

        public Pomiar GetNajstarszyPomiar(int pacjentID)
        {
            using (DisposableConnection)
            {


                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_oldest_pomiary(:pacjentid)");
                cmd.Add("pacjentid", pacjentID);

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var pomiar = new Pomiar()
                        {
                            Data = reader.ToDate("data").Value,
                            LekarzImie = reader.ToString("imie"),
                            LekarzNazwisko = reader.ToString("nazwisko"),
                            Przychodnia = reader.ToString("przychodnia"),
                            Rozkurczowe = reader.ToInt("rozkurczowe"),
                            Skurczowe = reader.ToInt("skurczowe"),
                            Tetno = reader.ToInt("tetno")
                        };

                        return pomiar;
                    }
                return null;
            }
        }
    }
}
