﻿using SimpleERP.Models.Abstract;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{
    public class ClientOrder :IEntity<int>
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
