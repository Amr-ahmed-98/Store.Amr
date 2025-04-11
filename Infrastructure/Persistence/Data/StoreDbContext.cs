using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    // i don't do onConfiguring here because i want CLR to do it
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // if you want to get the configuration from the the current assembly / project
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly); // if you want to get the configuration from the project / assembly that contains this class 
            base.OnModelCreating(modelBuilder);

            //// when creating the database so the code that in program will see if there's product that have this data so it won't create it and if didn't have this data it will create it
            //modelBuilder.Entity<Product>().HasData(new Product()
            //{

            //});
        }


    }
}
