
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstractions;
using Shared.ErrorsModels;
using Store.Api.Extensions;
using Store.Api.Middlewares;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();



            // Configure the HTTP request pipeline.


            await app.ConfigurMiddlewares();



            app.Run();
        }
    }
}
