using Microsoft.AspNetCore.Authorization;
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
        [Route("api/get/user{id}")]
        public IActionResult Get(int id)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(user => user.Id == id);

                if (user == null)
                {
                    // maybe do some form of logging
                    
                    return NotFound();
                }

                return Ok(user);
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
        [Route("api/create/user")]
        public IActionResult Create(string firstName, string lastName, string age)
        {
            using (var db = new DatabaseContext())
            {
                db.Users.Add(new Models.User
                {
                    Age = age,
                    FirstName = firstName,
                    LastName = lastName
                });

                try
                {
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // maybe do some form of logging
                    return BadRequest();
                }
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
        [Route("api/update/user{id}")]
        public IActionResult Update(int id, string firstName, string lastName, string age)
        {
            if (id != null)
            {
                using (var db = new DatabaseContext())
                {
                    // check if user exists
                    var user = db.Users.FirstOrDefault(user => user.Id == id);
                
                    //if user doesn't exist, return 404
                    if (user == null)
                    {
                        // maybe do some form of logging
                        return NotFound();
                    }
                    else
                    {
                        user.Age = age;
                        user.FirstName = firstName;
                        user.LastName = lastName;
                        
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error: In updating user with user id {id} as we got exception of {e.Message}");
                            // maybe also log the above message too in some form of logger
                            return BadRequest();
                        }
                        return Ok();
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: No user id provided");
                // maybe also log the above message too in some form of logger
                return NotFound();
            }
        }

        /// <summary>
        ///     Delets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/delete/user/{id}")]
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                using (var db = new DatabaseContext())
                {
                    var user = db.Users.FirstOrDefault(user => user.Id == id);

                    if (user == null)
                    {
                        // maybe do some form of logging
                        return NotFound();
                    }
                    else
                    {
                        db.Users.Remove(user);

                        try
                        {
                            db.SaveChanges();
                            return Ok();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error: In deleting user with user id {id} as we got exception of {e.Message}");
                            return BadRequest();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: No user id provided");
                // maybe also log the above message too in some form of logger
                return NotFound();
            }
        }
    }
}
