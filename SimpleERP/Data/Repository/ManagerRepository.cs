using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;

namespace SimpleERP.Data.Repository
{
    public class ManagerRepository : CommonRepository<Manager, string>, IManagerRepository
    {
        public ManagerRepository(ContextEF context) : base(context)
        {
        }
    }
}
