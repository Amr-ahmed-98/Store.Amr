
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstractions;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]); // use this way if you put another name for the connection string
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDbInitializer , DbInitializer>(); // Allow DI For DbInitializer

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();



            var app = builder.Build();


            // the code here means that if there's no migrations applied to the database, it will apply them from yourself

            #region Seeding

            using var scope = app.Services.CreateScope(); // unmanaged resource indicate to lifetime for the object that run in runtime so when you deal with unmanaged resources so you have to use using keyword
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // ASk CLR To Create Object from DbInitializer
            await dbInitializer.InitializeAsync();
            #endregion




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
