﻿using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Abstract
{
   public interface IEmployeRepository
    {
        List<Employe> GetEmployes();
        Employe AddEmployee(Employe employe);
    }
}
