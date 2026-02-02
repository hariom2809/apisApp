using apisApp.Models.DTOs;
using apisApp.Models.Entities;

namespace apisApp.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(Guid id);
        User UpsertUser(Guid id, UpsertUserDto upsertDto);
        bool DeleteUser(Guid id);
    }
}
