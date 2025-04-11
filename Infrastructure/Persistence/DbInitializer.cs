using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
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
    }
}
