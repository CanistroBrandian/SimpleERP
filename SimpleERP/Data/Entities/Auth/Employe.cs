using SimpleERP.Abstract;
using SimpleERP.Data.Entities.GoalEntity;
using System.Collections.Generic;

namespace SimpleERP.Data.Entities.Auth
{

    public class Employe : User, IEntity<string>
    {
        public List<EmployeClient> EmployeClients { get; set; }
        public List<Goal> Goals { get; set; }
        public int DepartamentId { get; set; }
        public Departament Departament { get; set; }
    }
}
