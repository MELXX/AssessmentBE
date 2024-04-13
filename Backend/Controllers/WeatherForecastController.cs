using Backend.Interfaces.Services;
using Backend.Services;
using DAL.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ICRUDServiceBase<User> u, ICRUDServiceBase<Permission> p,ILogger<WeatherForecastController> logger)
        {
            p.GetMany(0);
            u.Create(new DAL.Data.Models.User() { Created = DateTime.Now,Email="",Groups = null ,Name ="test",Surname= "tested",PasswordHash = Guid.NewGuid().ToString()});
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
