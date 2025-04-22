using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            StoreDbContext context,
            StoreIdentityDbContext IdentityDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _identityDbContext = IdentityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitializeAsync()
        {
            try
            {
                // Create Database if doesn't exist && Apply to Any Pending Migrations
                // _context.Database.GetPendingMigrations().Any() this will return sequence for names for migrations that didn't apply
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync(); // it will do save changes that will apply the pending migrations and if database doesn't exist it will create it
                }

                // Data Seeding (Seeding happened if the tables that in database are empty)

                // Seeding ProductTypes From Json Files

                if (!_context.ProductTypes.Any())
                {
                    // 1. Read All Data From types Json File as string
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");


                    // 2. Transform string to C# objects  [List<ProductTypes>]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);


                    // 3. Add  List<ProductTypes> To Database
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }


                // Seeding ProductBrands From Json Files
                if (!_context.ProductBrands.Any())
                {
                    // 1. Read All Data From Brands Json File as string
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");


                    // 2. Transform string to C# objects  [List<ProductBrand>]
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);


                    // 3. Add  List<ProductBrand> To Database
                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }


                // Seeding Products From Json Files

                if (!_context.Products.Any())
                {
                    // 1. Read All Data From Products Json File as string
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");


                    // 2. Transform string to C# objects  [List<Products>]
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);


                    // 3. Add  List<Products> To Database
                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task InitializeIdentityAsync()
        {
            // Create Database if doesn't exist && Apply to Any Pending Migrations
            if (_identityDbContext.Database.GetPendingMigrations().Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }

            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin"
                });
            }



            // Seeding
            if (!_userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "superAdmin@gmail.com",
                    UserName = "superAdmin",
                    PhoneNumber = "01156039815"
                };

                var adminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01156039815"
                };

               await  _userManager.CreateAsync(superAdminUser, "$AmrAhmed98");
               await _userManager.CreateAsync(adminUser, "$AmrAhmed98");


                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin"); 

            }
        }
    }
}
