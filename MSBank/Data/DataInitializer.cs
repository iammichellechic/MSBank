using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MSBank.Models;

namespace MSBank.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(ApplicationDbContext dbcontext, UserManager<IdentityUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }
        public void SeedData()
        {
          //  _context.Database.Migrate();
            SeedRoles();
            SeedUsers();
        }

        private void SeedUsers()
        {
            AddUserIfNotExists("stefan.holmberg@systementor.se", "Hejsan123#", new string[] { "Admin" });
            AddUserIfNotExists("stefan.holmberg@nackademin.se", "Hejsan123#", new string[] { "Cashier" });
        }

        private void SeedRoles()
        {
            AddRoleIfNotExisting("Admin");
            AddRoleIfNotExisting("Cashier");
        }

        private void AddRoleIfNotExisting(string roleName)
        {
            var role = _dbcontext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                _dbcontext.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName });
                _dbcontext.SaveChanges();
            }
        }


        private void AddUserIfNotExists(string userName, string password, string[] roles)
        {
            if (_userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            _userManager.CreateAsync(user, password).Wait();
            _userManager.AddToRolesAsync(user, roles).Wait();
        }

    }
}
