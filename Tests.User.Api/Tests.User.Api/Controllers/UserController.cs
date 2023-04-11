using Microsoft.AspNetCore.Mvc;
using Tests.Users.Api.DTO;
using Tests.Users.Api.Interfaces;

namespace Tests.Users.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {            
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        ///     Gets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users")]
        public IActionResult Get(int id)
        {

             var user  = _userService.Get(id);
            if(user != null) return Ok(user);

            return NotFound();

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
        public IActionResult Create([FromBody] UserDto user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createUser = _userService.Create(user);

                return Ok(createUser);
            }
            catch (Exception ex)
            {
                // Log the error and return an error response
                _logger.LogError(ex, "Failed to create User");
                return StatusCode(500, "An error occurred while creating your user.");
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
        public IActionResult Update(UserDto user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedUser = _userService.Update(user);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                // Log the error and return an error response
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while updating user.");
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
            var isDeleted = _userService.Delete(id);
            
            return Ok(isDeleted);
        }
    }
}
