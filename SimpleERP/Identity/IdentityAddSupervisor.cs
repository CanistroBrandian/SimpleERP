using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Identity
{
    public class IdentityAddSupervisor
    {

        public async Task AddSupervisor(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string SupervisorRole = "Supervisor";

            var roleCheck = await _roleManager.RoleExistsAsync(SupervisorRole);
            if (!roleCheck)
            {
                //create the roles and seed them to the database  
                await _roleManager.CreateAsync(new IdentityRole(SupervisorRole));
            }
            var admin = await _userManager.FindByEmailAsync("admin@mail.ru");
            if (admin == null)
            {
                var newAdmin = new User
                {
                    NameFirst = "admin",
                    NameLast = "admin",
                    Phone = "37529144",
                    Adress = "adress",
                    Email = "admin@mail.ru",
                    IsActive = true,
                    UserName = "admin@mail.ru"
                };
                string pass = "looser";
                var result = await _userManager.CreateAsync(newAdmin, pass);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, SupervisorRole);
                }
            }
        }
    }
}
