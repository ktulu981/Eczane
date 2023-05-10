using Eczane.Core.Entities;
using Eczane.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane.Core.Repositories
{
    public interface IILAC_AMBALAJRepository:IRepository<ILAC_AMBALAJ>
    {
        ICollection<Ilac> Search(string keyword);

        IlacDetay Detay(long id);
    }
}
