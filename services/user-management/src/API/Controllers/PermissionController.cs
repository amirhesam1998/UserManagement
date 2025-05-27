using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Permissions;
using Application.Commands.Users;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddPermission([FromBody] AddPermissionCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { Error = result.Error });
            return Ok(new { PermissionId = result.Value });
        }

        [HttpPost("Get/{id}")]
        public async Task<IActionResult> GetPermission(Guid id)
        {
            var permission = await _mediator.Send(new GetPermissionCommand(id));
            return Ok(new { permission });
        }

        [HttpPost("GetPermissionGroup/{id}")]
        public async Task<IActionResult> GetPermissionGroup(Guid id)
        {
            var permissionGroup = await _mediator.Send(new GetPermissionGroupCommand(id));
            return Ok(new { permissionGroup });
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPermissions([FromQuery] GetAllPermissionCommand command)
        {
            var permissions = await _mediator.Send(command);
            return Ok(new { permissions });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePermission(Guid id)
        {
            var result = await _mediator.Send(new DeletePermissionCommand(id));
            if (result.IsSuccess && !result.Value)
                return Ok("Permission deleted.");

            return NotFound(result.Error);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> EditPermission([FromBody] EditPermissionCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { Error = result.Error });
            return Ok(new { PermissionId = result.Value });
        }

    }
}
