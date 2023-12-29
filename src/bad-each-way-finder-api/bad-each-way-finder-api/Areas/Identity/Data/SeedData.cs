using bad_each_way_finder_api_domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bad_each_way_finder_api.Areas.Identity.Data
{
    public static class SeedData
    {
        private static RoleManager<IdentityRole> _roleManager;
        private static UserManager<IdentityUser> _userManager;
        private static IEnumerable<IdentityUser> _users;

        public static void Initialize(IServiceProvider serviceProvider)
        {
                _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                SeedRoles();

                _users = SeedUsers();
                AddUsersToUserManager();
        }

        public static void SeedRoles()
        {
            // Create the "Admin" role if it doesn't exist
            if (!_roleManager.RoleExistsAsync(UserRoles.Admin).Result)
            {
                var adminRole = new IdentityRole(UserRoles.Admin);
                var result = _roleManager.CreateAsync(adminRole).Result;
            }

            // Create the "User" role if it doesn't exist
            if (!_roleManager.RoleExistsAsync(UserRoles.User).Result)
            {
                var userRole = new IdentityRole(UserRoles.User);
                var result = _roleManager.CreateAsync(userRole).Result; 
            }
        }

        private static IEnumerable<IdentityUser> SeedUsers()
        {
            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, "Testing123!");

            return new List<IdentityUser>
            {
                new IdentityUser()
                {
                    Email = "admin@email.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin@email.com",
                    PasswordHash = hashedPassword
                },
                new IdentityUser()
                {
                    Email = "user@email.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user@email.com",
                    PasswordHash = hashedPassword
                }
            };
        }

        public static void AddUsersToUserManager()
        {
            foreach (var user in _users)
            {
                if (_userManager.FindByEmailAsync(user.Email).Result == null)
                {
                    var result = _userManager.CreateAsync(user, "Testing123!").Result;

                    if (result.Succeeded)
                    {
                        // User created successfully

                        if (user.UserName == "admin@email.com")
                        {
                            var addRoleResult = _userManager.AddToRolesAsync(user, new[] { UserRoles.Admin }).Result;
                        }
                        if (user.UserName == "user@email.com")
                        {
                            var addRoleResult = _userManager.AddToRolesAsync(user, new[] { UserRoles.User }).Result;
                        }
                        // Add roles to the user
                    }
                    else
                    {
                        // Failed to create the user
                        // Handle the error or log it
                    }
                }
            }
        }
    }
}
