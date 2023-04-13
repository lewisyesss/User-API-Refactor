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
            /// Created a try-catch exception to handle InavlidOperationException which is when the id doesn't exist in the database
            try
            {
                Models.User user = database.Users.Where(user => user.Id == id).First();

                return Ok(user);
            }
            
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("User with ID {O} not found in the database.", id);
                return NotFound();
            }
            

           
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

            /// Checking if age string value is a number and if it is then create the User else get an error message
            int testIfNumber;
            bool result = int.TryParse(age, out testIfNumber);

            if(result)
            {
                DatabaseContext Database = new DatabaseContext();
                Database.Users.Add(new Models.User
                {
                    /// Changed the order of the parameters to match the IActionResult Create method structure
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                });
                Database.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Invalid age value. Age must be a number");
            }
            
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
            /// Checking if age string value is a number and if it is then create the User else get an error message
            int testIfNumber;
            bool result = int.TryParse(age, out testIfNumber);

            if (result)
            {

                DatabaseContext Database = new DatabaseContext();
                Database.Users.Update(new Models.User
                {
                    /// Changed the order of the parameters to match the IActionResult Update method 
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                });
                Database.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Invalid age value. Age must be a number");
            }
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
            /// Created a try-catch exception to handle InavlidOperationException which is when the id doesn't exist in the database
            try
            {
                database.Users.Remove(new Models.User
                {
                    Id = id
                });
                database.SaveChanges();
                return Ok(); 
            }

            catch (InvalidOperationException ex)
            {
                Console.WriteLine("User with ID {O} not found in the database.", id);
                return NotFound();
            }
            
            
        }
    }
}
