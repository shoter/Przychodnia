using System;
using System.Collections.Generic;
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

        public void Login(string username, string password)
        {

        }

        //public void CreateAccount(string username, string password)
    }
}
