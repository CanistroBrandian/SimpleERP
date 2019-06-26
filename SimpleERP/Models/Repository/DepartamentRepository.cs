using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Repository
{
   public class DepartamentRepository : CommonRepository<Departament, int>, IDepartamentRepository
    {
        public DepartamentRepository(ContextEF context) : base(context)
        {
        }
    }
}
