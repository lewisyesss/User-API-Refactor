using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Users.Api.Controllers;
using Tests.Users.Api.DTO;
using Tests.Users.Api.Interfaces;
using Tests.Users.Api.Models;

namespace Tests.Users.Api.Test
{
    public class UserControllerTests
    {
        private readonly Mock<ILogger<UserController>> _logger;
        private readonly Mock<IUserService> _userService;

        public UserControllerTests()
        {
            _logger = new Mock<ILogger<UserController>>();
            _userService = new Mock<IUserService>();
        }

        [Fact]
        public async Task Should_Return_User_When_Valid_Id_Passed()
        {
            _userService.Setup(x => x.Get(It.IsAny<int>())).Returns(new User { Id = 1 });

            UserController controller = new UserController(_logger.Object, _userService.Object);

            IActionResult result = controller.Get(1);
            OkObjectResult ok = result as OkObjectResult;

            Assert.Equal(((User)ok.Value).Id, 1);
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Created()
        {
            _userService.Setup(x => x.Create(It.IsAny<UserDto>())).Returns(new User { Id = 1 });

            UserController controller = new UserController(_logger.Object, _userService.Object);
            IActionResult result = controller.Create(new UserDto { Id = 1, FirstName = "Mamadu", LastName = "Balde", Age = "22" });

            OkObjectResult ok = result as OkObjectResult;

            Assert.NotNull(ok);
            Assert.Equal(((User?)ok.Value).Id, 1);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Updated()
        {
            _userService.Setup(x => x.Update(It.IsAny<UserDto>())).Returns(new User { Id = 1, Age = "30" });

            UserController controller = new UserController(_logger.Object, _userService.Object);

            UserDto user = new UserDto
            {
                Id = 1,
                FirstName = "Mamadu",
                LastName = "Balde",
                Age = "25"
            };

            IActionResult result = controller.Update(user);

            OkObjectResult ok = result as OkObjectResult;

            Assert.NotNull(ok);
            Assert.Equal(((User?)ok.Value).Age, "30");
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Removed()
        {
            _userService.Setup(x => x.Delete(It.IsAny<int>())).Returns(true);

            UserController controller = new UserController(_logger.Object, _userService.Object);

            UserDto user = new UserDto
            {
                Id = 1,
                FirstName = "Mamadu",
                LastName = "Balde",
                Age = "25"
            };

            IActionResult result = controller.Delete(user.Id);

            OkObjectResult ok = result as OkObjectResult;

            Assert.NotNull(ok);
            Assert.Equal((ok.Value), true);
            Assert.Equal(200, ok.StatusCode);
        }
    }
}