using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
    }
}
