using Application.Commands.Permissions;
using Application.Commands.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Add([FromBody] AddRoleCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { Error = result.Error });

            return Ok(new { RoleId = result.Value });
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetAllRoleCommand command)
        {
            var Roles = await _mediator.Send(command);

            return Ok(new { Roles });
        }


        [HttpPost("get")]
        public async Task<IActionResult> GetByUsername([FromBody] GetRoleCommand command)
        {
            var role = await _mediator.Send(command);
            return Ok(new { role });
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteRoleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess && result.Value)
                return Ok("Role deleted.");

            return NotFound(result.Error);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> EditPermission([FromBody] EditRoleCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { Error = result.Error });
            return Ok(new { PermissionId = result.Value });
        }
        // GET: RoleController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: RoleController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: RoleController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: RoleController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: RoleController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: RoleController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: RoleController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: RoleController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
