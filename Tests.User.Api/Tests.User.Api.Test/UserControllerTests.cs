using Microsoft.AspNetCore.Mvc;
using Tests.User.Api.Controllers;

namespace Tests.User.Api.Test
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Should_Return_User_When_Valid_Id_Passed()
        {
            using (var database = new DatabaseContext())
            {
                Models.User user = new Models.User
                {
                    FirstName = "Test",
                    LastName = "User",
                    Age = "20"
                };
                database.Users.Add(user);
                database.SaveChanges();

                UserController controller = new UserController(database);
                IActionResult result = await controller.Get(user.Id);
                OkObjectResult ok = result as OkObjectResult;

                Assert.NotNull(ok);
                Assert.Equal(200, ok.StatusCode);
            }
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Created()
        {
            using (var database = new DatabaseContext())
            {
                UserController controller = new UserController(database);
                IActionResult result = await controller.Create("Test", "User", "20");

                OkResult ok = result as OkResult;

                Assert.NotNull(ok);
                Assert.Equal(200, ok.StatusCode);
            }
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Updated()
        {
            using (var database = new DatabaseContext())
            {
                Models.User user = new Models.User
                {
                    FirstName = "Test",
                    LastName = "User",
                    Age = "20"
                };
                database.Users.Add(user);
                database.SaveChanges();

                UserController controller = new UserController(database);
                IActionResult result = await controller.Update(user.Id, "Updated", "User", "21");

                OkResult ok = result as OkResult;

                Assert.NotNull(ok);
                Assert.Equal(200, ok.StatusCode);
            }
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Removed()
        {
            using (var database = new DatabaseContext())
            {
                Models.User user = new Models.User
                {
                    FirstName = "Test",
                    LastName = "User",
                    Age = "20"
                };
                database.Users.Add(user);
                database.SaveChanges();

                UserController controller = new UserController(database);
                IActionResult result = await controller.Delete(user.Id);

                OkResult ok = result as OkResult;

                Assert.NotNull(ok);
                Assert.Equal(200, ok.StatusCode);
            }
        }
    }
}