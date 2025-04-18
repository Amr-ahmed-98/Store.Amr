using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Shared.ErrorsModels;
using Store.Api.Middlewares;

namespace Store.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddBuiltInServices();

            services.AddSwaggerServices();


            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices();

            services.ConfigureServices();



            return services;
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(m => m.Value.Errors.Any())
                                            .Select(m => new ValidationError()
                                            {
                                                Field = m.Key,
                                                Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                            });
                    var response = new ValidationErrorWithResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }


        public static async Task<WebApplication> ConfigurMiddlewares(this WebApplication app)
        {
            // the code here means that if there's no migrations applied to the database, it will apply them from yourself

            await app.InitializeDatabaseAsync();

            app.UseGlobalErrorHandling();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;

        }

        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            #region Seeding

            using var scope = app.Services.CreateScope(); // unmanaged resource indicate to lifetime for the object that run in runtime so when you deal with unmanaged resources so you have to use using keyword
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // ASk CLR To Create Object from DbInitializer
            await dbInitializer.InitializeAsync();
            #endregion

            return app;
        } 
        private static  WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();


            return app;
        }

    }
}
