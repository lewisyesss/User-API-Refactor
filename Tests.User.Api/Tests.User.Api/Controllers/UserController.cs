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
        // Multiple response types are possible with IActionResult, so provide attribute for all possible response types
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Models.User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            DatabaseContext database = new DatabaseContext();
            Models.User user = database.Users.Where(user => user.Id == id).First();

            // Checks if a user exists, if not return http 404 error
            if (user == null) {
                return HTTPNotFound(); 
            }
            
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
        // Specify response types for IActionResult
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/users")]
        public IActionResult Create(string firstName, string lastName, int age)
        {
            DatabaseContext Database = new DatabaseContext();

            // Validate the user model
            if (ModelState.IsValid) {
                Database.Users.Add(new Models.User
                {
                    Age = age,
                    FirstName = firstName,
                    LastName = lastName
                });
                Database.SaveChanges();
            }

            // Return created result
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
        public IActionResult Update(int id, string firstName, string lastName, int age)
        {
            DatabaseContext Database = new DatabaseContext();
            
            // Validate user model
            if (ModelState.IsValid) {
                Database.Users.Update(new Models.User
                {
                    Age = age,
                    FirstName = firstName,
                    LastName = lastName,
                    Id = id
                });
                Database.SaveChanges();
            }
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

            // Validate user model
            if (ModelState.IsValid) {
                database.Users.Remove(new Models.User
                {
                    Id = id
                });
                database.SaveChanges();
            }
            return Ok();
        }
    }
}
