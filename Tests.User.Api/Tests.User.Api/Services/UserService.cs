using Tests.Users.Api.DTO;
using Tests.Users.Api.Interfaces;
using Tests.Users.Api.Models;

namespace Tests.Users.Api.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _dbContext = new DatabaseContext();
            _logger = logger;
        }

        public User Create(UserDto user)
        {
            // Map the DTO data to model data
            var newUser = new User { FirstName = user.FirstName, LastName = user.LastName, Age = user.Age };


            // Save the user to the database
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return newUser;
        }

        public bool Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user != null) {
                // Save the user to the database
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();

                return true; 
            }

            return false;

        }

        public User? Get(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);

        }

        public User? Update(UserDto user)
        {
            // Fetch user for update
            var updateUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);

            if (updateUser != null)
            {
                updateUser.FirstName = user.FirstName;
                updateUser.LastName = user.LastName;
                updateUser.Age = user.Age;

                _dbContext.Users.Update(updateUser);
                _dbContext.SaveChanges();
            }

            return updateUser;
        }
    }
}
