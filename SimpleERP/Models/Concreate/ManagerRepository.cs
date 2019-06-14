using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Concreate
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ContextEF _context;
        public ManagerRepository(ContextEF context)
        {
            _context = context;
        }

        public List<Manager> GetManagers()
        {
            return _context.Managers.ToList();
        }
    }
}
