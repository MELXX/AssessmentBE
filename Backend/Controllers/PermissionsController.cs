using Backend.DTO.Request;
using Backend.DTO.Response;
using Backend.Interfaces.Services;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DAL.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{

    public class PermissionsController : AppControllerBase, IControllerCRUDBase<PermissionRequestDTO, PermissionResponseDTO>
    {
        private ICRUDServiceBase<Permission> _permissionService;
        public PermissionsController(ICRUDServiceBase<Permission> PermissionService, ILogger<PermissionsController> logger) : base(logger)
        {
            _permissionService = PermissionService;
        }

        [HttpPost]
        [Produces(typeof(PermissionResponseDTO))]

        public async Task<IActionResult> Create(PermissionRequestDTO request)
        {
            await _permissionService.Create(new Permission()
            {
                Created = DateTime.Now,
                Name = request.Name

            });
            return CreatedAtAction(nameof(Create), request);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _permissionService.Get(Id);
            if (result != default)
            {
                _permissionService.Delete(result);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Produces(typeof(PermissionResponseDTO))]

        public async Task<IActionResult> Read(Guid Id)
        {
            var result = await _permissionService.Get(Id);
            if (result != default)
            {
                var response = new PermissionResponseDTO()
                {
                    Id = result.Id,
                    Name = result.Name,
                };
                return new OkObjectResult(response);
            }
            return NotFound();
        }

        [HttpGet("list/")]
        [Produces(typeof(PermissionResponseDTO[]))]

        public async Task<IActionResult> Read()
        {
            var data = await _permissionService.GetMany(0);
            var response = data.Select(Permission => new PermissionResponseDTO()
            {
                Id = Permission.Id,
                Name = Permission.Name,
            });
            return new OkObjectResult(response);
        }


        [HttpPut]
        [Produces(typeof(PermissionResponseDTO))]
        public async Task<IActionResult> Update(PermissionRequestDTO request)
        {
            var data = await _permissionService.Get(request.Id.Value);
            if (data != default)
            {
                data.Name = request.Name;
                await _permissionService.Update(data);
                return new OkObjectResult(request);
            }
            return NotFound();
        }
    }
}
