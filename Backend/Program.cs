using Backend.Interfaces.Services;
using Backend.Middleware;
using Backend.Services;
using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using FluentValidation;
using System;
using Backend.DTO.Request;
using Backend.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Backend.AppConfiguration;
namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //https://dev.to/m4rri4nne/nunit-and-c-tutorial-to-automate-your-api-tests-from-scratch-24nf
            // Add services to the container.

            builder.Services.AddControllers();

            var ConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ?? builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(ConnectionString));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Service registration
            ServiceRegistration.RegisterServices(builder.Services);
            //validators
            FluentValidationConfiguration.RegisterValidation(builder.Services);
            //Cors config
            CorsConfiguration.Register(builder.Services,builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            CorsConfiguration.ActivateCors(app, builder.Configuration);


            //app.UseAuthorization();


            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AppDbContext>().Database;
                if (db.GetPendingMigrations().Count() > 0)
                {
                    db.Migrate();
                }
            }
            app.Run();
        }
    }
}
