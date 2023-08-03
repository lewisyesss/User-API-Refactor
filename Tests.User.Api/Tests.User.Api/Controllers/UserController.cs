using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tests.User.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _database;

        public UserController(DatabaseContext database)
        {
            _database = database;
        }

        /// <summary>
        ///     Gets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users")]
        public async Task<IActionResult> Get(int id)
        {
            Models.User user = await _database.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null) { return NotFound(); }

            return Ok(user);
        }

        /// <summary>
        ///     Create a new user
        /// </summary>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="age">Age of the user (must be a number)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users")]
        public async Task<IActionResult> Create(string firstName, string lastName, string age)
        {
            _database.Users.Add(new Models.User
            {
                Age = age,
                FirstName = firstName,
                LastName = lastName
            });
            await _database.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        ///     Updates a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="age">Age of the user (must be a number)</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/users")]
        public async Task<IActionResult> Update(int id, string firstName, string lastName, string age)
        {
            Models.User user = await _database.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null) { return NotFound(); }

            user.Age = age;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Id = id;

            await _database.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        ///     Delets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/users")]
        public async Task<IActionResult> Delete(int id)
        {
            Models.User user = await _database.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null) { return NotFound(); }

            _database.Users.Remove(user);
            await _database.SaveChangesAsync();
            return Ok();
        }
    }
}
