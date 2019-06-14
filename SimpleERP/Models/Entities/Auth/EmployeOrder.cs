using SimpleERP.Models.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{
    public class EmployeOrder
    {
        public int EmployeId { get; set; }
        public Employe Employe { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
