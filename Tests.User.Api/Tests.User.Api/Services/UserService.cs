using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;
using Tests.User.Api.Interfaces;
using Tests.User.Api.Models;
using Tests.User.Api.POCO;

namespace Tests.User.Api.Services
{
    public class UserService : IUserService
    {
        private DatabaseContext _databaseContext;
        public UserService()
        {
            this._databaseContext = new DatabaseContext();
        }
        public UserModel GetUser(int userId)
        {
            UserModel user = _databaseContext.Users.Where(user => user.Id == userId).FirstOrDefault();
            return user;
            
        }
        public bool CreateUser(UserPOCO newUser)
        {
            try
            {
                _databaseContext.Users.Add(newUser.ToModel());
                return _databaseContext.SaveChangesAsync().Result > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(UserPOCO user)
        {
            try
            { 
                _databaseContext.Users.Update(user.ToModel());
                return _databaseContext.SaveChangesAsync().Result > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                UserModel userToDelete = _databaseContext.Users.Where(a => a.Id == userId).FirstOrDefault();
                if (userToDelete != null)
                {
                    _databaseContext.Users.Remove(userToDelete);
                }
                return _databaseContext.SaveChangesAsync().Result > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
