using SimpleERP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Abstract
{
   public interface IDepartamentRepository
    {
        List<Departament> GetDepartaments();
    }
}
