using System;
using Moq;
using DTO;

using Xunit;
using Domain;
using Core31API.Controllers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using DataAccess.Repositories;
using Microsoft.CodeAnalysis.Options;
using Common;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core31APITest
{
    public class UserControllerTest
    {
       
        private readonly UserController _controllerTest;
        private readonly Mock<IUserRepository> _mockUserRepo;

        private readonly IOptions<MyDBSetting> _options;

        public UserControllerTest()
        {
            _options = Options.Create(new MyDBSetting() { ConnectionString = "My connection string" });
          
            var logger = Mock.Of<ILogger<UserController>>();
            _mockUserRepo = new Mock<IUserRepository>();


            _controllerTest = new UserController(logger, _options, _mockUserRepo.Object);

        }


       
        [Fact]
        public async Task TESTOK()
        {
            Assert.Equal("Framework Working", "Framework Working");

        }
        [Fact]
        public void Test_Options_Injected()
        {
            var connectionstringAvailable = _options.Value.ConnectionString;
            Assert.True(connectionstringAvailable == _options.Value.ConnectionString);
        }
        private IEnumerable<User> GetFakeUserLists()
        {
            return new List<User>
            {
                new User()
               {
                    UserID = 1, FirstName = "Praveen", LastName = "Parashar", JobID = 10001, Address =new Address {Country="India",City="Delhi"}},
                new User()
                {
                    UserID = 2, FirstName = "Soniya", LastName = "Sharma", JobID = 10002,  Address =new Address {Country="India",City="Bangalore"} }
            };
        }
        // Arrange   
       private  IEnumerable<UserDTO> GetFakeUserListsDTO()
        {
            return new List<UserDTO>
            {
                new UserDTO()
               {
                    UserID = 1, FirstName = "Praveen", LastName = "Parashar", JobID = 10001, Address = "India Delhi" },
                new UserDTO()
                {
                    UserID = 2, FirstName = "Soniya", LastName = "Sharma", JobID = 10002,  Address ="India Bangalore" }
            };
        }
        [Fact]
        public async Task GET_All_RETURNS_OK()
        {

            // Arrange
            _mockUserRepo.Setup(users => users.GetAllUser()).Returns(GetFakeUserLists());
           

            var userslist = from u in GetFakeUserLists()
                            select new UserDTO()
                            {
                                UserID = u.UserID,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                JobID = u.JobID,
                                Address = String.Format(@"City :{0} Country :{1} ", u.Address.City, u.Address.Country)
                            };

            // Act
            var result = _controllerTest.Get();


            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());



            //Assert.NotNull(result);
            //var objectResult = Assert.IsType<OkObjectResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<UserDTO>>(objectResult.Value);
            //var modelCount = model.Count();
            //Assert.Equal(userslist.Count(), modelCount);
           

            //var users = Assert.IsType<List<UserDTO>>(GetFakeUserLists());
            //Assert.Equal(2, users.Count);
        }
        [Fact]
        public async Task GET_ById_RETURNS_OK()
        {
            int id = 1;

            _mockUserRepo.Setup(user => user.GetUserById(id))
               .Returns(GetFakeUserLists().Single(p => p.UserID.Equals(id)));

            var user =  _controllerTest.GetByID(id);
           // Assert.Throws<NotImplementedException>(()=> _controllerTest.GetByID(id));
            Assert.IsType<string>(user);
           Assert.Equal("your Id is " + id, user.ToString());

        }

        //[Fact]
        //public void Get_WhenCalled_ReturnsOkResult()
        //{
        //    // Act
        //    var okResult = _controllerTest.Get();
        //    var objectResult = Assert.IsType<OkObjectResult>(okResult);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(objectResult);
        //}

       
        //[Fact]
        //public async Task POST_Create_RETURNS_BADREQUEST()
        //{
        //    _controllerTest.ModelState.AddModelError("DateOfBirth", "Required");

        //    var apiException = await Assert.ThrowsAsync<ApiProblemDetailsException>(() => _controller.Post(FakeCreateRequestObjectWithMissingAttribute()));
        //    Assert.Equal(422, apiException.StatusCode);
        //}

        //[Fact]
        //public async Task POST_Create_RETURNS_OK()
        //{

        //    _mockDataManager.Setup(manager => manager.CreateAsync(It.IsAny<Person>()))
        //        .ReturnsAsync(It.IsAny<long>());

        //    var person = await _controller.Post(FakeCreateRequestObject());

        //    var response = Assert.IsType<ApiResponse>(person);
        //    Assert.Equal(201, response.StatusCode);
        //}

        //[Fact]
        //public async Task POST_Create_RETURNS_SERVERERROR()
        //{
        //    _mockDataManager.Setup(manager => manager.CreateAsync(It.IsAny<Person>()))
        //        .Throws(new Exception());

        //    await Assert.ThrowsAsync<Exception>(() => _controller.Post(FakeCreateRequestObject()));
        //}


    }
}
