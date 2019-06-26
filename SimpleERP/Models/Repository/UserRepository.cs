using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Repository
{
    public class UserRepository : CommonRepository<User, string>, IUserRepository
    {
        public UserRepository(ContextEF context) : base(context)
        {
        }

    }
}
