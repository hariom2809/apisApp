using apisApp.Data;
using apisApp.Models.DTOs;
using apisApp.Models.Entities;
using apisApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet]
        [Route("id:Guid")]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(_userService.GetUserById(id));
        }

        [HttpPost]
        [Route("id:Guid")]
        public IActionResult UpsertUser(Guid id, UpsertUserDto upsertDto)
        {
            var user = _userService.UpsertUser(id, upsertDto);
            return Ok(user);
        }

        [HttpDelete]
        [Route("id:Guid")]
        public IActionResult DeleteUser(Guid id)
        {
            return Ok(_userService.DeleteUser(id));
        }

    }
}
