using SimpleERP.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.WarehouseEntity
{
    public class Warehouse : IEntity<int>
    {
        public int Id { get; set; }
        public List<Stock> Products { get; set; }
        public string Name { get; set; }
        public List<Departament> Departaments { get; set; }
 
    }
}
