using KEShop_Api_N_Tier_Art.DAL.Data;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplictionDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(
            ApplictionDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task DataSeedingAsync()
        {
            // Apply pending migrations if there are any
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }

            // Seed Categories if empty
            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Category { Name = "Electronics" },
                    new Category { Name = "Books" },
                    new Category { Name = "Clothing" }
                );
            }

           
            if (!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Brand { Name = "BrandA", MainImage = "path_or_default_image_for_BrandA.jpg" },
                    new Brand { Name = "BrandB", MainImage = "path_or_default_image_for_BrandB.jpg" },
                    new Brand { Name = "BrandC", MainImage = "path_or_default_image_for_BrandC.jpg" }
                );
            }

            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            // Seed Roles if empty
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            // Seed Users if empty
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "user1@gmail.com",
                    UserName = "user1@gmail.com",
                    FullName = "User One",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                };
                var user2 = new ApplicationUser()
                {
                    Email = "user2@gmail.com",
                    UserName = "user2@gmail.com",
                    FullName = "User Two",
                    PhoneNumber = "1234567830",
                    EmailConfirmed = true,
                };
                var user3 = new ApplicationUser()
                {
                    Email = "user3@gmail.com",
                    UserName = "user3@gmail.com",
                    FullName = "User Three",
                    PhoneNumber = "1234567840",
                    EmailConfirmed = true,
                };

                await _userManager.CreateAsync(user1, "Pass@1212");
                await _userManager.CreateAsync(user2, "Pass@1212");
                await _userManager.CreateAsync(user3, "Pass@1212");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");
            }

            await _context.SaveChangesAsync();
        }
    }
}
