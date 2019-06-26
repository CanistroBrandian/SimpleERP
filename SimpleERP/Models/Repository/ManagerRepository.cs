using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Repository
{
   public class ManagerRepository : CommonRepository<Manager, string>, IManagerRepository
    {
        public ManagerRepository(ContextEF context) : base(context)
        {
        }
    }
}
