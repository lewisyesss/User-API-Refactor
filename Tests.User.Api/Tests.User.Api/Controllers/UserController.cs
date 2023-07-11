using Microsoft.AspNetCore.Mvc;
using Tests.User.Api.Interfaces;
using Tests.User.Api.Models;
using Tests.User.Api.POCO;

namespace Tests.User.Api.Controllers
{
    public class UserController : Controller
    {
        IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        /// <summary>
        ///     Gets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users/get")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetUser(id);
            if(user == null)
            {
                return BadRequest($"User with ID {id} could not be found");
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
        [Route("api/users/create")]
        public IActionResult Create(UserPOCO user)
        {
            if (!_userService.CreateUser(user))
            {
                return BadRequest("User could not be added");
            }
            return Ok("User has been created");
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
        [Route("api/users/update")]
        public IActionResult Update(UserPOCO user)
        {
            if (!_userService.UpdateUser(user))
            {
                return BadRequest("User could not be updated");
            }
            return Ok($"User has been updated");
        }

        /// <summary>
        ///     Delets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/users/delete")]
        public IActionResult Delete(int id)
        {
            if (!_userService.DeleteUser(id))
            {
                return BadRequest($"User with Id {id} could not be deleted");
            }
            return Ok($"User with Id {id} has been deleted");
        }
    }
}
