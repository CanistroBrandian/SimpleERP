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
        private readonly IEmployeOrders _context;
        public ManagerRepository(IEmployeOrders context)
        {
            _context = context;
        }

        public Manager AddManager(Manager manager)
        {
            _context.Add(manager);
            _context.SaveChanges();
            return manager;
        }

        public List<Manager> GetManagers()
        {
            return _context.Managers.ToList();
        }

    }
}
