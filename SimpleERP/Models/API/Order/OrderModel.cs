using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.API.Order
{
    public class OrderModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Information { get; set; }
    }
}
