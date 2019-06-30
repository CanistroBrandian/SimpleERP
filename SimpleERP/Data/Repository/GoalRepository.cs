using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.GoalEntity;
using SimpleERP.Data.Repository;

namespace SimpleERP.Abstract
{
    public class GoalRepository : CommonRepository<Goal, int>, IGoalRepository
    {
        public GoalRepository(ContextEF context) : base(context)
        {
        }
    }
}
