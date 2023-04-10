using Microsoft.AspNetCore.Mvc;
using Tests.User.Api.Controllers;

namespace Tests.User.Api.Test
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Should_Return_User_When_Valid_Id_Passed()
        {
            using (var db = new DatabaseContext())
            {
                var newUser = new Models.User
                {
                    Age = "20",
                    FirstName = "Test",
                    LastName = "User"
                };
                
                db.Users.Add(newUser);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                UserController controller = new UserController();
                IActionResult result = controller.Get(newUser.Id);
                OkObjectResult ok = result as OkObjectResult;

                Assert.NotNull(ok);
                Assert.Equal(200, ok.StatusCode);
            }
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Created()
        {
            UserController controller = new UserController();
            IActionResult result = controller.Create("Test", "User", "20");
            
            // bad request check added but not sure if needed
            if (result is BadRequestObjectResult)
            {
                var badRequest = result as BadRequestObjectResult;
                Assert.NotNull(badRequest);
                Assert.Equal(400, badRequest.StatusCode);
            }

            OkResult ok = result as OkResult;

            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Updated()
        {
            using (var db = new DatabaseContext()) 
            { 
                Models.User user = new Models.User {
                    FirstName = "Test",
                    LastName = "User",
                    Age = "20"
                };
                db.Users.Add(user);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                UserController controller = new UserController();
                IActionResult result = controller.Update(user.Id, "Updated", "User", "21");

                // bad request check added but not sure if needed
                if (result is BadRequestObjectResult)
                {
                    var badRequest = result as BadRequestObjectResult;
                    Assert.NotNull(badRequest);
                    Assert.Equal(400, badRequest.StatusCode);
                }
                
                OkResult ok = result as OkResult;

                Assert.NotNull(ok);
                Assert.Equal(200, ok.StatusCode);
            }
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Removed()
        {
            using (var db = new DatabaseContext())
            {
                Models.User user = new Models.User
                {
                    FirstName = "Test",
                    LastName = "User",
                    Age = "20"
                };
                db.Users.Add(user);
                await db.SaveChangesAsync();

                UserController controller = new UserController();
                IActionResult result = controller.Delete(user.Id);

                // bad request check added but not sure if needed
                if (result is BadRequestObjectResult)
                {
                    var badRequest = result as BadRequestObjectResult;
                    Assert.NotNull(badRequest);
                    Assert.Equal(400, badRequest.StatusCode);
                }
                
                OkResult ok = result as OkResult;

                Assert.NotNull(ok);
                Assert.Equal(200, ok.StatusCode);
            }
        }
    }
}