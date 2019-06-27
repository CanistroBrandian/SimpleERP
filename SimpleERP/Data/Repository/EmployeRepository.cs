using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Data.Repository
{
   public class EmployeRepository : CommonRepository<Employe, string>, IEmployeRepository
    {
        public EmployeRepository(ContextEF context) : base(context)
        {
        }


    }
}
