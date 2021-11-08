using KTU_API.Autherization.Model;
using KTU_API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data
{
    public class DatabaseSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DatabaseSeeder(UserManager<User> user, RoleManager<IdentityRole> role)
        {
            _roleManager = role;
            _userManager = user;
        }

        public async Task Seed()
        {
            foreach (var role in UserRoles.all)
            {
                var roleExsists = await _roleManager.RoleExistsAsync(role);
                if (!roleExsists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var Admin = new User
            {
                UserName = "admin",
            };

            var exsisting = await _userManager.FindByNameAsync(Admin.UserName);

            if (exsisting == null)
            {
                var newAdmin = await _userManager.CreateAsync(Admin, "Password123!");
                if (newAdmin.Succeeded)
                {
                    await _userManager.AddToRolesAsync(Admin, UserRoles.all);
                }

            }
        }

    }
}
