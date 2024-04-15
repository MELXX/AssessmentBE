namespace Backend.AppConfiguration
{
    public static class CorsConfiguration
    {
        public static void Register(IServiceCollection services,IConfiguration configuration) 
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins(configuration
                    .GetSection("AllowedOrigins")
                    .Get<string[]>())
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }

        public static void ActivateCors(WebApplication app,IConfiguration configuration) 
        {
            app.UseCors(options => options.WithOrigins(configuration
                .GetSection("AllowedOrigins")
                .Get<string[]>())
            .AllowAnyHeader()
            .AllowAnyMethod());
        }
    }
}
