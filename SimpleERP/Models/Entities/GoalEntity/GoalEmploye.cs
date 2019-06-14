using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.GoalEntity
{
    public class GoalEmploye
    {
        public int GoalId { get; set; }
        public Goal Goal { get; set; }
        public int EmployeId { get; set; }
        public Employe Employe { get; set; }
    }
}
