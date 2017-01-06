using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class SessionRepository : RepositoryBase
    {
        public SessionRepository(PrzychodniaContext context) : base(context)
        {
        }

        public Sesja GetSesja(string cookie)
        {
            throw new NotImplementedException();
        }
    }
}
