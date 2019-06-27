using SimpleERP.Abstract;
using System.Collections.Generic;

namespace SimpleERP.Data.Entities.Auth
{
    public class Client : User, IEntity<string>
    {
        public List<EmployeClient> EmployeClients { get; set; }
        public List<ClientOrder> ClientOrders { get; set; }

    }
}
