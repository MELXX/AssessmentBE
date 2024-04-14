using Backend.DTO.Response;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControllerBase: ControllerBase
    {
        ILogger _logger;
        public AppControllerBase(ILogger logger)
        {
            _logger = logger;
        }
    }
}
