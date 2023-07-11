using Tests.User.Api.Models;
using Tests.User.Api.POCO;

namespace Tests.User.Api.Interfaces
{
    public interface IUserService
    {
        UserModel GetUser(int userId);
        bool CreateUser(UserPOCO user);
        bool UpdateUser(UserPOCO user);
        bool DeleteUser(int userId);
    }
}
