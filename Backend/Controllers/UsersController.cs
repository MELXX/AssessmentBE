using Backend.DTO.Request;
using Backend.DTO.Response;
using Backend.Interfaces;
using Backend.Interfaces.Services;
using DAL.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{

    public class UsersController : AppControllerBase, IControllerCRUDBase<UserRequestDTO, UserResponseDTO>
    {
        IUserService _userService;
        public UsersController(IUserService userService, ILogger<UsersController> logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpPost]
        [Produces(typeof(UserResponseDTO))]

        public async Task<IActionResult> Create(UserRequestDTO request)
        {
            await _userService.Create(new User()
            {
                Created = DateTime.Now,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Groups = new List<UserGroup>()

            });
            return CreatedAtAction(nameof(Create), request);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _userService.Get(Id);
            if (result != default)
            {
                _userService.Delete(result);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Produces(typeof(UserResponseDTO))]

        public async Task<IActionResult> Read(Guid Id)
        {
            var result = await _userService.Get(Id);
            if (result != default)
            {
                var response = new UserResponseDTO()
                {
                    Email = result.Email,
                    Id = result.Id,
                    Name = result.Name,
                    Surname = result.Surname,
                };
                return new OkObjectResult(response);
            }
            return NotFound();
        }

        [HttpGet("list")]
        [Produces(typeof(UserResponseDTO[]))]
        public async Task<IActionResult> Read()
        {
            var data = await _userService.GetMany(0);
            var response = data.Select(user => new UserResponseDTO()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
            });
            return new OkObjectResult(response);
        }

        [HttpGet("/count")]
        [Produces(typeof(int))]

        public async Task<IActionResult> GetUserCount()
        {
            return new OkObjectResult(await _userService.Count());
        }

        [HttpPut]
        [Produces(typeof(UserResponseDTO))]
        public async Task<IActionResult> Update(UserRequestDTO request)
        {
            var data = await _userService.Get(request.Id.Value);
            if (data != default)
            {
                data.Surname = request.Surname;
                data.Name = request.Name;
                data.Email = request.Email;
                await _userService.Update(data);
                return new OkObjectResult(request);
            }
            return NotFound();
        }
    }
}
