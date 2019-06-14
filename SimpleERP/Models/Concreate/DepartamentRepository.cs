﻿using SimpleERP.Models.Context;
using SimpleERP.Models.Entities;
using SimpleERP.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Concreate
{
    public class DeparmanetRepository : IDepartamentRepository
    {
        private readonly ContextEF _context;
        public DeparmanetRepository(ContextEF context)
        {
            _context = context;
        }

        public List<Departament> GetDepartaments()
        {
            return _context.Departmaentes.ToList();
        }
    }
}
