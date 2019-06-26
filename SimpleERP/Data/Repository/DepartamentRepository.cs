using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Data.Repository
{
   public class DepartamentRepository : CommonRepository<Departament, int>, IDepartamentRepository
    {
        public DepartamentRepository(ContextEF context) : base(context)
        {
        }
    }
}
