using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{
    public class EmployeClient
    {
        public int EmployeId { get; set; }
        public Employe Employe { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
