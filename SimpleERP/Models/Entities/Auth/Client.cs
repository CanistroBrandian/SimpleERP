﻿using SimpleERP.Models.Abstract;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.OrderEntity;
using System.Collections.Generic;

namespace SimpleERP.Models.Entities.Auth
{
    public class Client : User, IEntity<string>
    {
        public List<EmployeClient> EmployeClients { get; set; }
        public List<ClientOrder> ClientOrders { get; set; }

    }
}
