using Backend.Controllers;
using Backend.Interfaces.Services;
using Backend.Services;
using Castle.Core.Logging;
using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace BackendTests
{
    //https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0
    public class Tests
    {
        public Mock<AppDbContext> MockDbContext { get; set; }
        public IUserService MockSvc { get; set; }
        public ILogger<UsersController> mockLogger { get; set; }
        [SetUp]
        public void Setup()
        {

            Guid theId1 = new Guid("00000000-0000-0000-0000-000000000001");
            Guid theId2 = new Guid("00000000-0000-0000-0000-000000000002");


            var myList = new List<User>
            {

                new User { Id = theId1, Name = "Mums 1", Email = "test@gmail.com" },
                new User { Id = theId2, Name = "Mums 1.2", Email = "test@gmail.com" },
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IAsyncEnumerable<User>>()
            .Setup(m => m.GetAsyncEnumerator(default))
            .Returns(new AsyncHelper.TestAsyncEnumerator<User>(myList.GetEnumerator()));

            dbSetMock.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(new AsyncHelper.TestAsyncQueryProvider<User>(myList.Provider));

            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression)
                .Returns(myList.Expression);

            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType)
                .Returns(myList.ElementType);

            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator())
                .Returns(() => myList.GetEnumerator());


            mockLogger = Mock.Of<ILogger<UsersController>>();

            //HERE
            var dbMock = new Mock<AppDbContext>();
            dbMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            MockDbContext = dbMock;

            MockSvc = new UserService(dbMock.Object);



        }

        [Test]
        public async Task TestUserReadAction()
        {
            var controller = new UsersController(MockSvc, mockLogger);
            // Act
            var result =  controller.Read().Result;
            Assert.Pass();
        }

        [Test]
        public async Task TestUserReadByIdAction()
        {
            var controller = new UsersController(MockSvc, mockLogger);
            // Act
            var result = await controller.Read(new Guid("00000000-0000-0000-0000-000000000002"));
            Assert.Pass();
        }

        [Test]
        public async Task TestUserDeleteAction()
        {
            var controller = new UsersController(MockSvc, mockLogger);
            // Act
            var result = await controller.Delete(new Guid("00000000-0000-0000-0000-000000000001"));
            Assert.AreEqual(typeof(OkResult),result.GetType());
        }

        [Test]
        public async Task TestUserNotFoundDelete()
        {
            var controller = new UsersController(MockSvc, mockLogger);
            // Act
            var result = await controller.Delete(new Guid("00000000-0000-0000-0000-000000000009"));
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public async Task TestUserAdd()
        {
            var controller = new UsersController(MockSvc, mockLogger);
            // Act
            var result = await controller.Create(new Backend.DTO.Request.UserRequestDTO()
            {
                Email = "test@gmail.com",
                Name = "test",
                Password= "xxxxx",
                Surname = "test_voornaam",
                Id = Guid.NewGuid(),
            });
            Assert.AreEqual(typeof(CreatedAtActionResult), result.GetType());
        }
    }
}
