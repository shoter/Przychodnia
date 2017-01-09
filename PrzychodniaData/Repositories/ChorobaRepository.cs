using Data.Objects;
using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class ChorobaRepository : RepositoryBase
    {
        public ChorobaRepository(PrzychodniaContext context) : base(context)
        {
        }

        public void InsertChoroba(string nazwa)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("insert into typychorob(nazwa) values(:nazwa)");
                cmd.Add("nazwa", nazwa);

                cmd.ExecuteNonQuery();
            }
        }

        public Dziennik GetDziennik(int dziennikID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_dziennikchorobowy(:dziennikID)");
                cmd.Add("dziennikID", dziennikID);

                var reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {

                    var dziennik = new Dziennik()
                    {
                        ID = reader.ToInt("ID"),
                        Nazwa = reader.ToString("nazwa"),
                        NazwaChoroby = reader.ToString("nazwachoroby"),
                        PacjentID = reader.ToInt("PacjentID")
                    };

                    return dziennik;
                }
                return null;
            }
        }


        public List<StatusChoroby> GetStatusyForPrzychodnia(int przychodniaID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_przychodnia_statusychorob(:przychodniaID)");
                cmd.Add("przychodniaID", przychodniaID);

                var reader = cmd.ExecuteReader();

                var choroby = new List<StatusChoroby>();

                if (reader.HasRows)
                    while (reader.Read())
                        choroby.Add(new StatusChoroby()
                        {
                            ID = reader.ToInt("ID"),
                            Data = reader.ToDate("Data").Value,
                            LekarzImie = reader.ToString("LekarzImie"),
                            LekarzNazwisko = reader.ToString("LekarzNazwisko"),
                            NazwaChoroby = reader.ToString("NazwaChoroby"),
                            NazwaDziennika = reader.ToString("NazwaDziennika"),
                            NazwaPrzychodni = reader.ToString("NazwaPrzychodni"),
                            Notatka = reader.ToString("Notatka"),
                            DziennikID = reader.ToInt("DziennikID"),
                            PacjentImie = reader.ToString("pacjentimie"),
                            PacjentNazwisko = reader.ToString("pacjentnazwisko")
                        });


                return choroby;
            }
        }

        public List<StatusChoroby> GetStatusyForDziennik(int dziennikID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_dziennik_statusychorob(:dziennikID)");
                cmd.Add("dziennikID", dziennikID);

                var reader = cmd.ExecuteReader();

                var choroby = new List<StatusChoroby>();

                if (reader.HasRows)
                    while (reader.Read())
                        choroby.Add(new StatusChoroby()
                        {
                            ID = reader.ToInt("ID"),
                            Data = reader.ToDate("Data").Value,
                            LekarzImie = reader.ToString("LekarzImie"),
                            LekarzNazwisko = reader.ToString("LekarzNazwisko"),
                            NazwaChoroby = reader.ToString("NazwaChoroby"),
                            NazwaDziennika = reader.ToString("NazwaDziennika"),
                            NazwaPrzychodni = reader.ToString("NazwaPrzychodni"),
                            Notatka = reader.ToString("Notatka"),
                            DziennikID = reader.ToInt("DziennikID")
                        });


                return choroby;
            }
        }
        public List<StatusChoroby> GetStatusyForPacjent(int pacjentID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_pacjent_statusychorob(:pacjentID)");
                cmd.Add("pacjentID", pacjentID);

                var reader = cmd.ExecuteReader();

                var choroby = new List<StatusChoroby>();

                if (reader.HasRows)
                    while (reader.Read())
                        choroby.Add(new StatusChoroby()
                        {
                            ID = reader.ToInt("ID"),
                            Data = reader.ToDate("Data").Value,
                            LekarzImie = reader.ToString("LekarzImie"),
                            LekarzNazwisko = reader.ToString("LekarzNazwisko"),
                            NazwaChoroby = reader.ToString("NazwaChoroby"),
                            NazwaDziennika = reader.ToString("NazwaDziennika"),
                            NazwaPrzychodni = reader.ToString("NazwaPrzychodni"),
                            Notatka = reader.ToString("Notatka"),
                            DziennikID = reader.ToInt("DziennikID")
                        });


                return choroby;
            }
        }

        public List<TypChoroby> GetChoroby()
        {
            using (DisposableConnection)
            {
                List<TypChoroby> choroby = new List<TypChoroby>();

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_all_choroby()");

                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                    while(reader.Read())
                    {
                        choroby.Add(new TypChoroby()
                        {
                            ID = reader.ToInt("id"),
                            Nazwa = reader.ToString("nazwa")
                        });
                    }


                return choroby;
            }
        }

        public List<Dziennik> GetDziennikiForLekarz(int lekarzID, int pacjentID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_lekarz_dziennikichorob(:lekarzID, :pacjentID)");
                cmd.Add("lekarzID", lekarzID);
                cmd.Add("pacjentID", pacjentID);

                var reader = cmd.ExecuteReader();

                List<Dziennik> dzienniki = new List<Dziennik>();

                if (reader.HasRows)
                    while (reader.Read())
                        dzienniki.Add(new Dziennik()
                        {
                            ID = reader.ToInt("id"),
                            Nazwa = reader.ToString("nazwa"),
                            NazwaChoroby = reader.ToString("typchoroby"),
                            
                        });

                return dzienniki;
            }
        }

        public List<Dziennik> GetDziennikiForLekarz(int lekarzID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_lekarz_dziennikichorob(:lekarzID)");
                cmd.Add("lekarzID", lekarzID);

                var reader = cmd.ExecuteReader();

                List<Dziennik> dzienniki = new List<Dziennik>();

                if (reader.HasRows)
                    while (reader.Read())
                        dzienniki.Add(new Dziennik()
                        {
                            ID = reader.ToInt("id"),
                            Nazwa = reader.ToString("nazwa"),
                            NazwaChoroby = reader.ToString("typchoroby"),
                            PacjentImie = reader.ToString("imie"),
                            PacjentNazwisko = reader.ToString("nazwisko")

                        });

                return dzienniki;
            }
        }



        public int DodajDziennik(int pacjentID, int? typChorobyID, string nazwa)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_ins_dziennikchoroby(:pacjentID, :typchorobyID, :nazwa)");
                cmd.Add("pacjentID", pacjentID);
                cmd.Add("typchorobyID", typChorobyID);
                cmd.Add("nazwa", nazwa);

                var reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                    return reader.ToInt(0);
            }
            throw new Exception("Dziennik chorób nie zwrócił swojego ID");
        }

        public void DodajWpis(int dziennikChorobyID, DateTime data, string notatka, int lekarzID, int przychodniaID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_ins_statuschoroby(:dziennikID, :data, :notatka, :lekarzid, :przychodniaid)");
                cmd.Add("dziennikID", dziennikChorobyID);
                cmd.Add("data", data);
                cmd.Add("notatka", notatka);
                cmd.Add("lekarzid", lekarzID);
                cmd.Add("przychodniaid", przychodniaID);

                cmd.ExecuteNonQuery();
       
            }
        }
    }
}
