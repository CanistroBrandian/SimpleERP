using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Abstract
{
   public interface IEmployeClientsRepository: ICommonRepository<EmployeClient, int>
    {
    }
}
