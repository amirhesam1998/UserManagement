using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Users;
using Application.Commands.Permissions;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { Error = result.Error });

            return Ok(new { UserId = result.Value });
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetByUsername([FromBody] GetUserCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new { user });
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUserCommand command)
        {
            var users = await _mediator.Send(command);
            return Ok(new { users });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess && result.Value)
                return Ok("User deleted.");

            return NotFound(result.Error);
        }
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditPermission(Guid id ,[FromBody] EditUserCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { Error = result.Error });
            return Ok(new { PermissionId = result.Value });
        }

        // GET: UserController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: UserController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: UserController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: UserController/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //}
    }
}
