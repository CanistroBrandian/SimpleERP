using SimpleERP.Models.Abstract;
using SimpleERP.Models.Entities.GoalEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{

    public class Manager : Employe,IEntity<string>
    {
        public List<Goal> CreatedGoals { get; set; }
    }
}
