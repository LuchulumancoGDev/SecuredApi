using Microsoft.AspNetCore.Identity;
using SecureApiWithJwtToken.Constants;
using SecureApiWithJwtToken.Models;

namespace SecureApiWithJwtToken.Contexts
{
    public class ApplicationDbContextSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextSeed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedEssentialsAsync()
        {
            //Seed Roles
            await _roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await _roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
            await _roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

            //Seed Default User
            var defaultUser = new ApplicationUser { FirstName = "Jimmy", LastName = "Joe", UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

            if (await _userManager.FindByNameAsync(defaultUser.UserName) == null)
            {
                var result = await _userManager.CreateAsync(defaultUser, Authorization.default_password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
                }
                else
                {
                    throw new Exception($"Failed to create default user: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }
}
