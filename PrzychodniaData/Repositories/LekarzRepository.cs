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
    public class LekarzRepository : RepositoryBase
    {
        public LekarzRepository(PrzychodniaContext context) : base(context)
        {
        }

        public void AddPrzydzial(int lekarzID, int przychodniaID, DateTime poczatekPrzydzialu, DateTime? koniecPrzydzialu)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_ins_przydzialy(:id, :lekarzid, :poczatek, :koniec)");
                cmd.Add("id", przychodniaID);
                cmd.Add("lekarzid", lekarzID);
                cmd.Add("poczatek", poczatekPrzydzialu);
                cmd.Add("koniec", koniecPrzydzialu);

                cmd.ExecuteNonQuery();
            }
        }

        public List<NajlepszyLekarz> GetNajlepsiLekarze()
        {
            using (DisposableConnection)
            {
                List<NajlepszyLekarz> lekarze = new List<NajlepszyLekarz>();

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_best_lekarze()");

                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var lekarz = new NajlepszyLekarz()
                        {
                            Imie = reader.ToString("imie"),
                            Nazwisko = reader.ToString("nazwisko"),
                            IloscPacjentow = reader.ToInt("iloscPacjentow")
                        };


                        lekarze.Add(lekarz);

                    }
                }

                return lekarze;
            }
        }

        public void Fire(int przychodniaID, int lekarzID)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select sp_upd_przydzialy_zwolnij(:lekarzID, :przychodniaID)");
                cmd.Add("lekarzid", lekarzID);
                cmd.Add("przychodniaid", przychodniaID);
                cmd.ExecuteNonQuery();
            }
        }

        public Lekarz Get(int lekarzID)
        {
           
            using (DisposableConnection)
            {

                Lekarz lekarz = null;

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_lekarz(:id)");
                cmd.Add("id", lekarzID);

                var reader = cmd.ExecuteReader();
   
                while (reader.Read())
                {
                    lekarz = new Lekarz()
                    {
                        ID = reader.ToInt("id"),
                        Imie = reader.ToString("imie"),
                        Nazwisko = reader.ToString("nazwisko")
                    };

                }

                lekarz.Przydzialy = GetPrzydzialy(lekarzID, DisposableConnection.Connection);
                return lekarz;
            }
        }

        public List<Lekarz> GetAll()
        {
            List<Lekarz> lekarze = new List<Lekarz>();
            using (DisposableConnection)
            {

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_all_lekarze()");

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var Lekarz = new Lekarz()
                    {
                        ID = reader.ToInt("id"),
                        Imie = reader.ToString("imie"),
                        Nazwisko = reader.ToString("nazwisko"),
                        NazwaUzytkownika = reader.ToString("nazwa")
                    };

                    lekarze.Add(Lekarz);
                }

                return lekarze;
            }
        }

        public List<Lekarz> GetMany(int przychodniaID)
        {
            List<Lekarz> lekarze = new List<Lekarz>();
            using (DisposableConnection)
            {

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_many_przydzialy(:przychodniaID)");
                cmd.Add("przychodniaid", przychodniaID);


                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ID = reader.ToInt("lekarzid");

                    Lekarz lekarz = lekarze.FirstOrDefault(l => l.ID == ID);

                    if (lekarz == null)
                    {
                        lekarz = new Lekarz()
                        {
                            ID = reader.ToInt("lekarzid"),
                            Imie = reader.ToString("lekarzimie"),
                            Nazwisko = reader.ToString("lekarznazwisko"),
                            NazwaUzytkownika = reader.ToString("uzytkowniknazwa")
                        };

                        lekarze.Add(lekarz);
                    }

                    Przydzial przydzial = new Przydzial()
                    {
                        LekarzID = reader.ToInt("lekarzid"),
                        PrzychodniaID = przychodniaID,
                        PoczatekPrzydzialu = reader.ToDate("poczatekdata").Value,
                        KoniecPrzydzialu = reader.ToDate("koniecdata")
                    };
                    lekarz.Przydzialy.Add(przydzial);



                    
                }
                foreach(var lekarz in lekarze)
                {
                    lekarz.Przydzialy = lekarz.Przydzialy.OrderBy(p => p.PoczatekPrzydzialu).ToList();
                }
                return lekarze;
            }
        }

        public List<Przydzial> GetPrzydzialy(int lekarzID)
        {
            List<Przydzial> przydzialy = new List<Przydzial>();
            using (DisposableConnection)
            {

                return GetPrzydzialyInternal(lekarzID, DisposableConnection.Connection);
            }
        }

        public List<Przydzial> GetPrzydzialy(int lekarzID, DbConnection connection)
        {
            List<Przydzial> przydzialy = new List<Przydzial>();
            using (DisposableConnection)
            {

                return GetPrzydzialyInternal(lekarzID, connection);
            }
        }



        protected List<Przydzial> GetPrzydzialyInternal(int lekarzID, DbConnection connection)
        {
            List<Przydzial> przydzialy = new List<Przydzial>();

            var cmd = connection.CreateCommand("select * from sp_sel_all_przydzialy(:id)");
            cmd.Add("id", lekarzID);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    var przydzial = new Przydzial()
                    {
                        PrzychodniaID = reader.ToInt("przychodniaid"),
                        LekarzID = lekarzID,
                        PoczatekPrzydzialu = reader.ToDate("poczatekdata").Value,
                        KoniecPrzydzialu = reader.ToDate("koniecdata"),
                        PrzychodniaNazwa = reader.ToString("przychodnianazwa")
                    };

                    przydzialy.Add(przydzial);
                }

            return przydzialy;
        }

        public List<Przydzial> GetActivePrzydzialy(int lekarzID )
        {
            using (DisposableConnection)
            {

                List<Przydzial> przydzialy = new List<Przydzial>();

                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_active_przydzialy(:id)");
                cmd.Add("id", lekarzID);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var przydzial = new Przydzial()
                        {
                            PrzychodniaID = reader.ToInt("przychodniaid"),
                            LekarzID = lekarzID,
                            PoczatekPrzydzialu = reader.ToDate("poczatekdata").Value,
                            KoniecPrzydzialu = reader.ToDate("koniecdata"),
                            PrzychodniaNazwa = reader.ToString("przychodnianazwa")
                        };

                        przydzialy.Add(przydzial);
                    }

                return przydzialy;
            }
        }
    }
    
}
