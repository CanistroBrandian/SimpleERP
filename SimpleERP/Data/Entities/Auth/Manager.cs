using SimpleERP.Abstract;
using SimpleERP.Data.Entities.GoalEntity;
using System.Collections.Generic;

namespace SimpleERP.Data.Entities.Auth
{

    public class Manager : Employe, IEntity<string>
    {
        public List<Goal> CreatedGoals { get; set; }
    }
}
