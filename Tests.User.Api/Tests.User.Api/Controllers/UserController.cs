using Microsoft.AspNetCore.Mvc;

namespace Tests.User.Api.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        ///     Gets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users")]
        public IActionResult Get(int id)
        {
            DatabaseContext database = new DatabaseContext();
            Models.User user = database.Users.Where(user => user.Id == id).First();
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
        public IActionResult Create(string firstName, string lastName, string age)
        {
            DatabaseContext Database = new DatabaseContext();
            Database.Users.Add(new Models.User
            {
                Age = age,
                FirstName = firstName,
                LastName = lastName
            });
            Database.SaveChanges();
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
        public IActionResult Update(int id, string firstName, string lastName, string age)
        {
            DatabaseContext Database = new DatabaseContext();
            Database.Users.Update(new Models.User
            {
                Age = age,
                FirstName = firstName,
                LastName = lastName,
                Id = id
            });
            Database.SaveChanges();
            return Ok();
        }

        /// <summary>
        ///     Delets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/users")]
        public IActionResult Delete(int id)
        {
            DatabaseContext database = new DatabaseContext();
            database.Users.Remove(new Models.User
            {
                Id = id
            });
            database.SaveChanges();
            return Ok();
        }
    }
}
