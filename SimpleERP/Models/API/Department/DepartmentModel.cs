using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.API.Department
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WarehouseId { get; set; }
    }
}
