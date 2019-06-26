using SimpleERP.Models.Abstract;
using SimpleERP.Models.Entities.GoalEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleERP.Models.Entities.Auth
{

    public class Employe : User, IEntity<string>
    {
        public List<EmployeClient> EmployeClients { get; set; }
        public List<Goal> Goals { get; set; }
        public int DepartamentId { get; set; }
        public Departament Departament { get; set; }
    }
}
