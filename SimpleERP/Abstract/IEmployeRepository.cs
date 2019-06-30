using SimpleERP.Data.Entities.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleERP.Abstract
{
    public interface IEmployeRepository : ICommonRepository<Employe, string>
    {
        Task AddClientToEmploye(EmployeClient employeClient);
        Task<List<EmployeClient>> GetAllEmployeClients();
        Task AddOrdersToEmploye(EmployeOrder employeOrder);
        Task<List<EmployeOrder>> GetAllOrdersOfEmploye();

    }
}
