using SimpleERP.Models.Abstract;
using SimpleERP.Models.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{
    public class EmployeOrder: IEntity<int>
    {
        public string EmployeId { get; set; }
        public Employe Employe { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
