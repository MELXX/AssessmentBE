using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Data.Models;
using DAL.Data.Context;
using Backend.DTO.Request;
using Backend.DTO.Response;
using Backend.Interfaces;
using Backend.Interfaces.Services;
using Backend.Services;

namespace Backend.Controllers
{
    public class GroupsController : AppControllerBase, IControllerCRUDBase<GroupRequestDTO, GroupResponseDTO>
    {
        private ICRUDServiceBase<Group> _groupService;
        public GroupsController(ICRUDServiceBase<Group> GroupService,ILogger<GroupsController> logger) : base(logger)
        {
            _groupService = GroupService;
        }

        [HttpPost]
        [Produces(typeof(GroupResponseDTO))]
        public async Task<IActionResult> Create(GroupRequestDTO request)
        {
            await _groupService.Create(new Group()
            {
                Created = DateTime.Now,
                Name = request.Name
            });
            return CreatedAtAction(nameof(Create), request);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _groupService.Get(Id);
            if (result != default)
            {
                _groupService.Delete(result);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Produces(typeof(GroupResponseDTO))]
        public async Task<IActionResult> Read(Guid Id)
        {
            var result = await _groupService.Get(Id);
            if (result != default)
            {
                var response = new GroupResponseDTO()
                {
                    Id = result.Id,
                    Name = result.Name,
                };
                return new OkObjectResult(response);
            }
            return NotFound();
        }

        [HttpGet("list/")]
        [Produces(typeof(GroupResponseDTO[]))]
        public async Task<IActionResult> Read()
        {
            var data = await _groupService.GetMany(0);
            var response = data.Select(Group => new GroupResponseDTO()
            {
                Id = Group.Id,
                Name = Group.Name,
            });
            return new OkObjectResult(response);
        }


        [HttpPut]
        [Produces(typeof(GroupResponseDTO))]

        public async Task<IActionResult> Update(GroupRequestDTO request)
        {
            var data = await _groupService.Get(request.Id.Value);
            if (data != default)
            {
                data.Name = request.Name;
                await _groupService.Update(data);
                return new OkObjectResult(request);
            }
            return NotFound();
        }
    }
}
