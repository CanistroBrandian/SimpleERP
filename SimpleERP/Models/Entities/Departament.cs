using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<Employe> Employees { get; set; }



    }
}
