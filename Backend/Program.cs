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
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Service registration
            builder.Services.AddScoped<ICRUDServiceBase<Permission>, ServiceBase<Permission>>();
            builder.Services.AddScoped<ICRUDServiceBase<Group>, ServiceBase<Group>>();
            builder.Services.AddScoped<ICRUDServiceBase<UserGroup>, ServiceBase<UserGroup>>();
            builder.Services.AddScoped<ICRUDServiceBase<GroupPermission>, ServiceBase<GroupPermission>>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSwaggerGen();

            //validators
            builder.Services.AddScoped<IValidator<UserRequestDTO>, UserValidatior>();
            builder.Services.AddScoped<IValidator<GroupRequestDTO>, GroupValidatior>();
            builder.Services.AddScoped<IValidator<GroupPermissionRequestDTO>, GroupPermissionValidatior>();
            builder.Services.AddScoped<IValidator<GroupUserRequestDTO>, GroupUserValidatior>();
            builder.Services.AddScoped<IValidator<PermissionRequestDTO>, PermissionValidatior>();

            //Cors config
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins(builder.Configuration
                    .GetSection("AllowedOrigins")
                    .Get<string[]>())
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseCors(options => options.WithOrigins(builder.Configuration
                .GetSection("AllowedOrigins")
                .Get<string[]>())
            .AllowAnyHeader()
            .AllowAnyMethod());


            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
