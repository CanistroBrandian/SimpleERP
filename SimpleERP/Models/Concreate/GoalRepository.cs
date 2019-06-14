using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.GoalEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Concreate
{
    public class GoalRepository : IGoalRepository
    {
        private readonly ContextEF _context;
        public GoalRepository(ContextEF context)
        {
            _context = context;
        }

        public List<Goal> GetGoals()
        {
            return _context.Goals.ToList();
        }
    }
}
