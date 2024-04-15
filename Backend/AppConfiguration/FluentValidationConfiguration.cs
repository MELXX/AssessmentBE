using Backend.DTO.Request;
using Backend.Validation;
using FluentValidation;

namespace Backend.AppConfiguration
{
    /// <summary>
    /// Registration of model validation services
    /// </summary>
    public static class FluentValidationConfiguration
    {
        public static void RegisterValidation(IServiceCollection services)
        {
            services.AddScoped<IValidator<UserRequestDTO>, UserValidatior>();
            services.AddScoped<IValidator<GroupRequestDTO>, GroupValidatior>();
            services.AddScoped<IValidator<GroupPermissionRequestDTO>, GroupPermissionValidatior>();
            services.AddScoped<IValidator<GroupUserRequestDTO>, GroupUserValidatior>();
            services.AddScoped<IValidator<PermissionRequestDTO>, PermissionValidatior>();
        }
    }
}
