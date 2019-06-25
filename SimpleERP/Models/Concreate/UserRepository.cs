using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Concreate
{
    public class UserRepository:IUserRepository
    {
        private readonly IEmployeOrders _context;
        public UserRepository(IEmployeOrders context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
