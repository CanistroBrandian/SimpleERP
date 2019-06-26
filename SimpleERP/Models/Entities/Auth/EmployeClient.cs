using SimpleERP.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{
    public class EmployeClient: IEntity<int>
    {
        public string EmployeId { get; set; }
        public Employe Employe { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
