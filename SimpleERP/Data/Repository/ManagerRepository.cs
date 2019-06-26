using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Data.Repository
{
   public class ManagerRepository : CommonRepository<Manager, string>, IManagerRepository
    {
        public ManagerRepository(ContextEF context) : base(context)
        {
        }
    }
}
