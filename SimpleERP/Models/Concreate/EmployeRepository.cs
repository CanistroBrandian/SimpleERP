using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Concreate
{
    public class EmployeRepository : IEmployeRepository
    {
        private readonly IEmployeOrders _context;
        public EmployeRepository(IEmployeOrders context)
        {
            _context = context;
        }

        public Employe AddEmployee(Employe employe)
        {
            _context.Employees.Add(employe);
            _context.SaveChanges();
            return employe;
        }

        public List<Employe> GetEmployes()
        {
            return _context.Employees.ToList();
        }

    }
}
