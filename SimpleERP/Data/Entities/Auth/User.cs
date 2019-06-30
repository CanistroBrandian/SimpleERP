using Microsoft.AspNetCore.Identity;
using SimpleERP.Abstract;

namespace SimpleERP.Data.Entities.Auth
{
    public class User : IdentityUser, IEntity<string>
    {
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
