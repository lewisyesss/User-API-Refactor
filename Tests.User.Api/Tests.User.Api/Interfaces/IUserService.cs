using Tests.Users.Api.DTO;
using Tests.Users.Api.Models;

namespace Tests.Users.Api.Interfaces
{
    public interface IUserService
    {
        User Create(UserDto user);
        User? Update(UserDto user);
        bool Delete(int id);
        User? Get(int id);
    }
}