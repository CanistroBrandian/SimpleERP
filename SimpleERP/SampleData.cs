using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP
{
    public static class SampleData
    {
        public static void Init(ContextEF context)
        {
            Employe employe = new Employe
            {
                NameFirst = "Bob",
                NameLast = "Niohert",
                Phone = "375214885",
                Adress = "ul.malinia",
                DepartamentId = 5
            };
            context.Employees.Add(employe);
        }
    }
}
