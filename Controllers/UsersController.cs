using apisApp.Data;
using apisApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(MyDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public MyDbContext DbContext { get; }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(DbContext.Users.ToList());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = DbContext.Users.Find(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //[HttpPost]
        //public IActionResult AddUser(AddUserDto addUserDto)
        //{
        //    var userEntry = new User()
        //    {
        //        Name = addUserDto.Name,
        //        Email = addUserDto.Email,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };

        //    DbContext.Users.Add(userEntry);
        //    DbContext.SaveChanges();

        //    return Ok(userEntry);
        //}

        //[HttpPut]
        //[Route("{id:Guid}")]
        //public IActionResult UpdateUser(Guid id, UpdateUserDto updateUserDto)
        //{
        //    var user = DbContext.Users.Find(id);
        //    if (user is null)
        //    {
        //        return NotFound();
        //    }

        //    user.Name = updateUserDto.Name;
        //    user.Email = updateUserDto.Email;
        //    user.UpdatedAt = DateTime.UtcNow;

        //    DbContext.SaveChanges();

        //    return Ok(user);
        //}

        [HttpPost]
        public IActionResult UpsertUser(UpsertUserDto upsertDto)
        {
            User user;

            if (!upsertDto.Id.HasValue || upsertDto.Id == Guid.Empty) 
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = upsertDto.Name,
                    Email = upsertDto.Email,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                DbContext.Users.Add(user);
            }
            else
            {
                user = DbContext.Users.Find(upsertDto.Id);

                if (user is null)
                {
                    return NotFound();
                }
                user.Name = upsertDto.Name;
                user.Email = upsertDto.Email;
                user.UpdatedAt = DateTime.UtcNow;
            }
            DbContext.SaveChanges();
            return Ok(user);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = DbContext.Users.Find(id);
            if (user is null)
            {
                return NotFound();
            }

            DbContext.Remove(user);
            DbContext.SaveChanges();

            return Ok();
        }
    }
}
