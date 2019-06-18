using SimpleERP.Models.Entities.GoalEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleERP.Models.Entities.Auth
{

    public class Employe : User
    {
        public List<EmployeClient> EmployeClients { get; set; }
        public int? GoalId { get; set; }
        public List<Goal> Goals { get; set; }
        public int DepartamentId { get; set; }
        public Departament Departament { get; set; }
    }
}
