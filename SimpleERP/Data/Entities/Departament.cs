using SimpleERP.Abstract;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Data.Entities.WarehouseEntity;
using System.Collections.Generic;

namespace SimpleERP.Data.Entities
{
    public class Departament : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<Employe> Employees { get; set; }



    }
}
