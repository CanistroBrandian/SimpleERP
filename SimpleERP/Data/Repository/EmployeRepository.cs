using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;

namespace SimpleERP.Data.Repository
{
    public class EmployeRepository : CommonRepository<Employe, string>, IEmployeRepository
    {
        public EmployeRepository(ContextEF context) : base(context)
        {
        }

        public async Task AddClientToEmploye(EmployeClient employeClient)
        {
            _context.Set<EmployeClient>().Add(employeClient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeClient>> GetAllEmployeClients()
        {
            return await _context.Set<EmployeClient>().AsNoTracking().ToListAsync();
        }

        public async Task AddOrdersToEmploye(EmployeOrder employeOrder)
        {
            _context.Set<EmployeOrder>().Add(employeOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeOrder>> GetAllOrdersOfEmploye()
        {
            return await _context.Set<EmployeOrder>().AsNoTracking().ToListAsync();
        }
    }
}
