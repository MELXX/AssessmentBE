using Backend.Interfaces.Services;
using Backend.Services;
using DAL.Data.Models;

namespace Backend.AppConfiguration
{
    /// <summary>
    /// Register custom services for DI
    /// </summary>
    public static class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            services.AddScoped<ICRUDServiceBase<Permission>, ServiceBase<Permission>>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ICRUDServiceBase<UserGroup>, ServiceBase<UserGroup>>();
            services.AddScoped<ICRUDServiceBase<GroupPermission>, ServiceBase<GroupPermission>>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
