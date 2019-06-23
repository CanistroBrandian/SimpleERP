using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.Auth
{
    public class User : IdentityUser
    {
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
    }
}
