using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class RepositoryBase : IRepository
    {
        public PrzychodniaContext Context { get; set; }
        
        public RepositoryBase(PrzychodniaContext context)
        {
            Context = context;
        }
    }
}
