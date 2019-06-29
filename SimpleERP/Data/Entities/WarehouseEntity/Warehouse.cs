using SimpleERP.Abstract;
using System.Collections.Generic;

namespace SimpleERP.Data.Entities.WarehouseEntity
{
    public class Warehouse : IEntity<int>
    {
        public int Id { get; set; }
        public List<Stock> Products { get; set; }
        public string Name { get; set; }
        public List<Departament> Departaments { get; set; }

    }
}
