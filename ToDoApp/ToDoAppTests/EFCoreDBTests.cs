using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ToDoApp.Models;
using ToDoEFDB.Context;
using ToDoDAL;

namespace ToDoAppTests
{
    [TestClass]
    public class EFCoreDBTests
    {
        [TestMethod]
        public void AddUserItemTest()
        {

            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
                UserItem testUserItem = new UserItem()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN",
                    Username = "TestUser",
                    Email = "test@test.com",
                    RoleId = 2,
                    Password = "TestPass",
                    ConfirmPassword = "TestConfirmTest"
                };

                // Act
                context.UserItem.Add(testUserItem);
                context.SaveChanges();

                // Assert                
                Assert.AreEqual("TestFN", context.UserItem.First().FirstName);
                Assert.AreEqual("TestLN", context.UserItem.First().LastName);
                Assert.AreEqual("TestUser", context.UserItem.First().Username);
                Assert.AreEqual("test@test.com", context.UserItem.First().Email);
                Assert.AreEqual(2, context.UserItem.First().RoleId);
                Assert.AreEqual("TestPass", context.UserItem.First().Password);
                Assert.AreEqual("TestConfirmTest", context.UserItem.First().ConfirmPassword);
            };

            //UserItem testUserItem = new UserItem()
            //{
            //    FirstName = "TestFN",
            //    LastName = "TestLN",
            //    Username = "TestUser",
            //    Email = "test@test.com",
            //    RoleId = 2,
            //    Password = "TestPass",
            //    ConfirmPassword = "TestConfirmTest"
            //};

            //ToDoEFDAL.AddUserItem(testUserItem);

            //using (var context = new ToDoAppContext(options))
            //{
            //    Assert.AreEqual("TestFN", context.UserItem.First().FirstName);
            //    Assert.AreEqual("TestLN", context.UserItem.First().LastName);
            //    Assert.AreEqual("TestUser", context.UserItem.First().Username);
            //    Assert.AreEqual("test@test.com", context.UserItem.First().Email);
            //    Assert.AreEqual(2, context.UserItem.First().RoleId);
            //    Assert.AreEqual("TestPass", context.UserItem.First().Password);
            //    Assert.AreEqual("TestConfirmTest", context.UserItem.First().ConfirmPassword);
            //}
        }
        [TestMethod]
        public void UpdateUserItem()
        {
            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
                UserItem testUserItem = new UserItem()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN",
                    Username = "TestUser",
                    Email = "test@test.com",
                    RoleId = 2,
                    Password = "TestPass",
                    ConfirmPassword = "TestConfirmTest"
                };

                // Act
                context.UserItem.Add(testUserItem);
                context.SaveChanges();


                // Assert                
                Assert.AreEqual("TestFN", context.UserItem.First().FirstName);
                Assert.AreEqual("TestLN", context.UserItem.First().LastName);
                Assert.AreEqual("TestUser", context.UserItem.First().Username);
                Assert.AreEqual("test@test.com", context.UserItem.First().Email);
                Assert.AreEqual(2, context.UserItem.First().RoleId);
                Assert.AreEqual("TestPass", context.UserItem.First().Password);
                Assert.AreEqual("TestConfirmTest", context.UserItem.First().ConfirmPassword);
            };
        }
    }
}
