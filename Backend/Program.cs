using Backend.Interfaces.Services;
using Backend.Services;
using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

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

            builder.Services.AddScoped<ICRUDServiceBase<Permission>, ServiceBase<Permission>>();
            builder.Services.AddScoped<ICRUDServiceBase<Group>, ServiceBase<Group>>();
            builder.Services.AddScoped<ICRUDServiceBase<UserGroup>, ServiceBase<UserGroup>>();
            builder.Services.AddScoped<ICRUDServiceBase<GroupPermission>, ServiceBase<GroupPermission>>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
