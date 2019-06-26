using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.GoalEntity;
using SimpleERP.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Abstract
{
   public class GoalRepository : CommonRepository<Goal, int>, IGoalRepository
    {
        public GoalRepository(ContextEF context) : base(context)
        {
        }
    }
}
