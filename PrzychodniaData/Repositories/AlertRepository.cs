using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class AlertRepository : RepositoryBase
    {
        public AlertRepository(PrzychodniaContext context) : base(context)
        {
        }

        public List<Alert> GetAlerts(int lekarzID, int limit = 1000)
        {
            using (DisposableConnection)
            {
                var cmd = DisposableConnection.CreateCommand("select * from sp_sel_lekarz_alerty(:id, :limit)");
                cmd.Add("id", lekarzID);
                cmd.Add("limit", limit);

                List<Alert> alerty = new List<Alert>();

                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                    while(reader.Read())
                    {
                        var alert = new Alert()
                        {
                            Imie = reader.ToString("imie"),
                            Nazwisko = reader.ToString("nazwisko"),
                            Message = reader.ToString("message"),
                            Date = reader.ToDate("Data").Value
                        };

                        alerty.Add(alert);
                    }
                    
                return alerty;
            }
        }
    }
}
