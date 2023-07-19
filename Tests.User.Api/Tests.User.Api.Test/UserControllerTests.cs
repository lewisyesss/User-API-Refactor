using Microsoft.AspNetCore.Mvc;
using Tests.User.Api.Controllers;

namespace Tests.User.Api.Test
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Should_Return_User_When_Valid_Id_Passed()
        {
            DatabaseContext database = new DatabaseContext();
            Models.User user = new Models.User
            {
                FirstName = "Test",
                LastName = "User",
                Age = 20
            };
            database.Users.Add(user);
            database.SaveChanges();

            await Task.Delay(3000);

            UserController controller = new UserController();
            IActionResult result = controller.Get(user.Id);
            OkObjectResult? ok = result as OkObjectResult;           

            Assert.NotNull(ok);
            Assert.Equal(200, actual: ok.StatusCode);            
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Created()
        {
            UserController controller = new UserController();
            IActionResult result = controller.Create("Test", "User", 20);

            await Task.Delay(3000);

            OkResult? ok = result as OkResult;

            Assert.NotNull(ok);
            Assert.Equal(200, actual: ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Updated()
        {
            DatabaseContext database = new DatabaseContext();
            Models.User user = new Models.User
            {
                FirstName = "Test",
                LastName = "User",
                Age = 20
            };
            database.Users.Add(user);
            database.SaveChanges();

            await Task.Delay(3000);

            UserController controller = new UserController();
            IActionResult result = controller.Update(user.Id, "Updated", "User", 21);

            OkResult? ok = result as OkResult;

            Assert.NotNull(ok);
            Assert.Equal(200, actual: ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Removed()
        {
            DatabaseContext database = new DatabaseContext();
            Models.User user = new Models.User
            {
                FirstName = "Test",
                LastName = "User",
                Age = 20
            };
            database.Users.Add(user);
            database.SaveChanges();

            await Task.Delay(3000);

            UserController controller = new UserController();
            IActionResult result = controller.Delete(user.Id);

            OkResult? ok = result as OkResult;

            Assert.NotNull(ok);
            Assert.Equal(200, actual: ok.StatusCode);
        }
    }
}