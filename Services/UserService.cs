using apisApp.Data;
using apisApp.Services.Interfaces;
using apisApp.Models.Entities;
using apisApp.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace apisApp.Services
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _dbContext;
        public UserService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User? GetUserById(Guid id)
        {
            var user = _dbContext.Users.Find(id);
            if (user is null)
            {
                throw new KeyNotFoundException("user does not exist");
            }
            return user;
        }

        public User UpsertUser(Guid id, UpsertUserDto upsertDto)
        {
            User user;
            if (id == Guid.Empty)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = upsertDto.Name,
                    Email = upsertDto.Email,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _dbContext.Users.Add(user);
            }
            else
            {
                user = _dbContext.Users.Find(id);
                if (user is null)
                {
                    throw new KeyNotFoundException("User not found.");
                }
                user.Name = upsertDto.Name;
                user.Email = upsertDto.Email;
                user.UpdatedAt = DateTime.UtcNow;
            }
            _dbContext.SaveChanges();
            return user;
        }

        public bool DeleteUser(Guid id)
        {
            var user = _dbContext.Users.Find(id);
            if (user is null)
            {
                return false;
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
