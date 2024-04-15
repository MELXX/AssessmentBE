using Backend.DTO.Request;
using Backend.Validation;
using FluentValidation;

namespace Backend.AppConfiguration
{
    public static class FluentValidationConfiguration
    {
        public static void RegisterValidation(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<UserRequestDTO>, UserValidatior>();
            builder.Services.AddScoped<IValidator<GroupRequestDTO>, GroupValidatior>();
            builder.Services.AddScoped<IValidator<GroupPermissionRequestDTO>, GroupPermissionValidatior>();
            builder.Services.AddScoped<IValidator<GroupUserRequestDTO>, GroupUserValidatior>();
            builder.Services.AddScoped<IValidator<PermissionRequestDTO>, PermissionValidatior>();
        }
    }
}
