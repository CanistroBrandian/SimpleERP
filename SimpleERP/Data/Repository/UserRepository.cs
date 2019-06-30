using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;

namespace SimpleERP.Data.Repository
{
    public class UserRepository : CommonRepository<User, string>, IUserRepository
    {
        public UserRepository(ContextEF context) : base(context)
        {
        }

    }
}
