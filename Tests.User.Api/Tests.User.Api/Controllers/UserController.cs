using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tests.User.Api.Controllers;

public class UserController : Controller
{
    private readonly DatabaseContext database;

    public UserController(DatabaseContext dbContext)
    {
        database = dbContext;
    }

    private List<string> MissingParameters(string firstName, string lastName, string age)
    {
        List<string> missingValues = new List<string>(3);

        if (firstName is null)
            missingValues.Add(nameof(firstName));

        if (lastName is null)
            missingValues.Add(nameof(lastName));

        if (age is null)
            missingValues.Add(nameof(age));

        return missingValues;
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
        Models.User? user = database.Users.Where(user => user.Id == id).FirstOrDefault();

        if(user is not null)
        {
            return Ok(user);
        }
        else 
        { 
            return BadRequest("User does not exist."); 
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
    [Route("api/create_user")]
    public IActionResult Create(string firstName, string lastName, string age)
    {
        List<string> missingValues = MissingParameters(firstName, lastName, age);

        if(missingValues.Count > 0)
        {
            string missingVals = String.Join(", ", missingValues);

            return BadRequest($"The following fields are required: {missingVals}");
        }

        Models.User user = new Models.User() 
        {
            Age = age,
            FirstName = firstName,
            LastName = lastName
        };

        database.Users.Add(user);

        database.SaveChanges();

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
    [Route("api/update_user/{id}")]
    public IActionResult Update(int id, string firstName, string lastName, string age)
    {
        List<string> missingValues = MissingParameters(firstName, lastName, age);

        if (missingValues.Count > 0)
        {
            string missingVals = String.Join(", ", missingValues);

            return BadRequest($"The following fields are required: {missingVals}");
        }

        Models.User? user = database.Users.Where(x => x.Id == id).FirstOrDefault();

        if(user is not null) 
        {
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Age = age;

            database.Users.Update(user);

            database.SaveChanges();
            return Ok();
        }
        else
        {
            return BadRequest("User does not exist.");
        }
    }

    /// <summary>
    ///     Delets a user
    /// </summary>
    /// <param name="id">ID of the user</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("api/delete_user/{id}")]
    public IActionResult Delete(int id)
    {
        Models.User? user = database.Users.Where(x => x.Id == id).FirstOrDefault();

        if(user is not null)
        {
            database.Users.Remove(user);
            database.SaveChanges();
            return Ok();
        }
        else
        {
            return BadRequest("User does not exist.");
        }

    }
}



//Ideas that didn't work.

//private bool AuthenticateUser(Models.User user, string firstName, string lastName, string age)
//{
//    bool authenticated = false;

//    if(user.FirstName == firstName && user.LastName == lastName && user.Age == age) 
//    {
//        authenticated = true;
//    }

//    return authenticated;
//}