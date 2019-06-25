using Microsoft.AspNetCore.Identity;
using SimpleERP.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{
    public class User : IdentityUser, IEntity<string>
    {
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
    }
}
