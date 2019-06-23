using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Abstract
{
   public interface IManagerRepository
    {
        List<Manager> GetManagers();
        Manager AddManager(Manager manager);
    }
}
