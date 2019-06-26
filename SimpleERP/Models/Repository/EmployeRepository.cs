using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Repository
{
   public class EmployeRepository : CommonRepository<Employe, string>, IEmployeRepository
    {
        public EmployeRepository(ContextEF context) : base(context)
        {
        }


    }
}
