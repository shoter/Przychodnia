using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData
{
    public interface IRepository
    {
        PrzychodniaContext Context { get;  }
    }

}
