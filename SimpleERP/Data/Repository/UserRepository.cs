using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Data.Repository
{
    public class UserRepository : CommonRepository<User, string>, IUserRepository
    {
        public UserRepository(ContextEF context) : base(context)
        {
        }

    }
}
