using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.GoalEntity;
using SimpleERP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Abstract
{
   public class GoalRepository : CommonRepository<Goal, int>, IGoalRepository
    {
        public GoalRepository(ContextEF context) : base(context)
        {
        }
    }
}
