using SimpleERP.Models.Entities.GoalEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{

    public class Manager : Employe
    {
        public List<Goal> CreatedGoals { get; set; }
    }
}
