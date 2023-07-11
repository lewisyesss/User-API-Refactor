using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NuGet.Frameworks;
using Tests.User.Api.Controllers;
using Tests.User.Api.Interfaces;
using Tests.User.Api.Models;
using Tests.User.Api.POCO;
using Xunit;

namespace Tests.User.Api.Test
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly UserController _userController;
        private DatabaseContext _databaseContext;
        public UserControllerTests()
        {
            _userService= new Mock<IUserService>();
            _userController = new UserController(_userService.Object);
            _databaseContext = new DatabaseContext();
        }
        [Fact]
        public async Task Should_Return_User_When_Valid_Id_Passed()
        {
            UserPOCO testObject = new UserPOCO()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Age = 20
            };
            _databaseContext.Users.Add(testObject.ToModel());
            _databaseContext.SaveChanges();

            _userService.Setup(u => u.GetUser(testObject.Id)).Returns(testObject.ToModel());
            var result = (OkObjectResult)_userController.Get(testObject.Id);

            Assert.True(result is OkObjectResult);
            Assert.NotNull(result);
            Assert.IsType<UserModel>(result.Value);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Created()
        {
            UserPOCO testObject = new UserPOCO()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Age = 20
            };
            _userService.Setup(b => b.CreateUser(testObject)).Returns(true);
            var result = (OkObjectResult)_userController.Create(testObject);

            Assert.True(result is OkObjectResult);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Updated()
        {
            UserPOCO testObject = new UserPOCO()
            {
                Id = 1,
                FirstName = "Testtest",
                LastName = "Usertest",
                Age = 25
            };

            _userService.Setup(u => u.UpdateUser(testObject)).Returns(true);

            testObject.FirstName = "Test";
            var result = (OkObjectResult)_userController.Update(testObject);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Removed()
        {
            UserPOCO testObject = new UserPOCO()
            {
                Id = 4,
                FirstName = "Test",
                LastName = "User",
                Age = 2
            };
            _databaseContext.Users.Add(testObject.ToModel());
            _databaseContext.SaveChanges();

            _userService.Setup(u => u.DeleteUser(testObject.Id)).Returns(true);
            var result = (OkObjectResult)_userController.Delete(testObject.Id);

            Assert.True(result is OkObjectResult);
            Assert.NotNull(result);
        }
    }
}