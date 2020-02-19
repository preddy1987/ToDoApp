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
        #region UserItemTests
        [TestMethod]
        public void AddUserItemDALTest()
        {

            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
            }

            ToDoEFDAL testDAL = new ToDoEFDAL(options);

            UserItem testUserItem = new UserItem()
            {
                FirstName = "TestFN",
                LastName = "TestLN",
                Username = "TestUser",
                Email = "test@test.com",
                RoleId = "Customer",
            };

            //Act
            testDAL.AddUserItem(testUserItem);

            // Assert  
            using (var context = new ToDoAppContext(options))
            {
                              
                Assert.AreEqual("TestFN", context.UserItem.First().FirstName);
                Assert.AreEqual("TestLN", context.UserItem.First().LastName);
                Assert.AreEqual("TestUser", context.UserItem.First().Username);
                Assert.AreEqual("test@test.com", context.UserItem.First().Email);
                Assert.AreEqual("Customer", context.UserItem.First().RoleId);
            };
        }
        [TestMethod]
        public void AddUserItemEFTest()
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
                    RoleId = "Customer",
                };

                // Act
                context.UserItem.Add(testUserItem);
                context.SaveChanges();

                // Assert                
                Assert.AreEqual(testUserItem.FirstName, context.UserItem.First().FirstName);
                Assert.AreEqual(testUserItem.LastName, context.UserItem.First().LastName);
                Assert.AreEqual(testUserItem.Username, context.UserItem.First().Username);
                Assert.AreEqual(testUserItem.Email, context.UserItem.First().Email);
                Assert.AreEqual(testUserItem.RoleId, context.UserItem.First().RoleId);
            };
        }
        [TestMethod]
        public void GetUserItemDALTest()
        {

            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
            }

            ToDoEFDAL testDAL = new ToDoEFDAL(options);

            UserItem testUserItem = new UserItem()
            {
                FirstName = "TestFN",
                LastName = "TestLN",
                Username = "TestUser",
                Email = "test@test.com",
                RoleId = "Customer",
            };

            testUserItem.Id = testDAL.AddUserItem(testUserItem);

            //Act
            UserItem returnedTestUser = testDAL.GetUserItem(testUserItem.Id);

            // Assert  

            Assert.AreEqual(testUserItem.FirstName, returnedTestUser.FirstName);
            Assert.AreEqual(testUserItem.LastName, returnedTestUser.LastName);
            Assert.AreEqual(testUserItem.Username, returnedTestUser.Username);
            Assert.AreEqual(testUserItem.Email, returnedTestUser.Email);
            Assert.AreEqual(testUserItem.RoleId, returnedTestUser.RoleId);
            Assert.AreEqual(testUserItem.Id, returnedTestUser.Id);

        }
        [TestMethod]
        public void GetUserItemEFTest()
        {
            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            UserItem testUserItem = new UserItem()
            {
                FirstName = "TestFN",
                LastName = "TestLN",
                Username = "TestUser",
                Email = "test@test.com",
                RoleId = "Customer",
            };

            // Act
            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();               
                context.UserItem.Add(testUserItem);
                context.SaveChanges();
                testUserItem.Id = context.UserItem.Single(u => u.Username == testUserItem.Username).Id;
            };

            // Assert
            using (var context = new ToDoAppContext(options))
            {                               
                Assert.AreEqual(testUserItem.FirstName, context.UserItem.Find(testUserItem.Id).FirstName);
                Assert.AreEqual(testUserItem.LastName, context.UserItem.Find(testUserItem.Id).LastName);
                Assert.AreEqual(testUserItem.Username, context.UserItem.Find(testUserItem.Id).Username);
                Assert.AreEqual(testUserItem.Email, context.UserItem.Find(testUserItem.Id).Email);
                Assert.AreEqual(testUserItem.RoleId, context.UserItem.Find(testUserItem.Id).RoleId);
            };
        }
        [TestMethod]
        public void UpdateUserItemDALTest()
        {

            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
            }

            ToDoEFDAL testDAL = new ToDoEFDAL(options);

            UserItem testUserItem = new UserItem()
            {
                FirstName = "TestFN",
                LastName = "TestLN",
                Username = "TestUser",
                Email = "test@test.com",
                RoleId = "Customer",
            };

            testUserItem.Id = testDAL.AddUserItem(testUserItem);

            UserItem updatedTestUserItem = new UserItem()
            {
                FirstName = "FNTest",
                LastName = "LNTest",
                Username = "UserTest",
                Email = "123@test.com",
                RoleId = "Administrator",
                Id = testUserItem.Id
            };

            testDAL.UpdateUserItem(updatedTestUserItem);

            //Act
            UserItem returnedTestUser = testDAL.GetUserItem(updatedTestUserItem.Id);

            // Assert             
            Assert.AreEqual(updatedTestUserItem.FirstName, returnedTestUser.FirstName);
            Assert.AreEqual(updatedTestUserItem.LastName, returnedTestUser.LastName);
            Assert.AreEqual(updatedTestUserItem.Username, returnedTestUser.Username);
            Assert.AreEqual(updatedTestUserItem.Email, returnedTestUser.Email);
            Assert.AreEqual(updatedTestUserItem.RoleId, returnedTestUser.RoleId);
            Assert.AreEqual(updatedTestUserItem.Id, returnedTestUser.Id);           
        }
        [TestMethod]
        public void UpdateUserItemEFTest()
        {
            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            UserItem testUserItem = new UserItem()
            {
                FirstName = "TestFN",
                LastName = "TestLN",
                Username = "TestUser",
                Email = "test@test.com",
                RoleId = "Customer",
            };

            // Act
            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
                context.UserItem.Add(testUserItem);
                context.SaveChanges();
                testUserItem.Id = context.UserItem.Single(u => u.Username == testUserItem.Username).Id;
            };

            UserItem updatedTestUserItem = new UserItem()
            {
                FirstName = "FNTest",
                LastName = "LNTest",
                Username = "UserTest",
                Email = "123@test.com",
                RoleId = "Administrator",
                Id = testUserItem.Id
            };


            UserItem returnedTestUser;
            using (var context = new ToDoAppContext(options))
            {
                returnedTestUser = context.UserItem.Single(u => u.Id == updatedTestUserItem.Id);
                returnedTestUser.FirstName = updatedTestUserItem.FirstName;
                returnedTestUser.LastName = updatedTestUserItem.LastName;
                returnedTestUser.Username = updatedTestUserItem.Username;
                returnedTestUser.RoleId = updatedTestUserItem.RoleId;
                context.SaveChanges();
            }

                // Assert
                using (var context = new ToDoAppContext(options))
            {
                Assert.AreEqual(returnedTestUser.FirstName, context.UserItem.Find(testUserItem.Id).FirstName);
                Assert.AreEqual(returnedTestUser.LastName, context.UserItem.Find(testUserItem.Id).LastName);
                Assert.AreEqual(returnedTestUser.Username, context.UserItem.Find(testUserItem.Id).Username);
                Assert.AreEqual(returnedTestUser.Email, context.UserItem.Find(testUserItem.Id).Email);
                Assert.AreEqual(returnedTestUser.RoleId, context.UserItem.Find(testUserItem.Id).RoleId);
                Assert.AreEqual(updatedTestUserItem.Id, returnedTestUser.Id);
            };
        }
        [TestMethod]
        public void DeleteUserItemDALTest()
        {

            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
            }

            ToDoEFDAL testDAL = new ToDoEFDAL(options);

            UserItem testUserItem = new UserItem()
            {
                FirstName = "TestFN",
                LastName = "TestLN",
                Username = "TestUser",
                Email = "test@test.com",
                RoleId = "Customer",
            };

            testUserItem.Id = testDAL.AddUserItem(testUserItem);

            testDAL.DeleteUserItem(testUserItem.Id);

            //Act
            UserItem returnedTestUser = testDAL.GetUserItem(testUserItem.Id);

            // Assert  
            Assert.IsNull(returnedTestUser);
        }
        [TestMethod]
        public void DeleteUserItemEFTest()
        {
            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            UserItem testUserItem = new UserItem()
            {
                FirstName = "TestFN",
                LastName = "TestLN",
                Username = "TestUser",
                Email = "test@test.com",
                RoleId = "Customer",
            };

            // Act
            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
                context.UserItem.Add(testUserItem);
                context.SaveChanges();
                testUserItem.Id = context.UserItem.Single(u => u.Username == testUserItem.Username).Id;
            };

            using (var context = new ToDoAppContext(options))
            {
                context.UserItem.Remove(testUserItem);
                context.SaveChanges();
            };
           
            UserItem returnedTestUser;
            using (var context = new ToDoAppContext(options))
            {
                returnedTestUser = context.UserItem.Find(testUserItem.Id);                
            }

            // Assert
            Assert.IsNull(returnedTestUser);
        }
        #endregion

        #region ToDoListItemTests
        [TestMethod]
        public void AddToDoListItemDALTest()
        {

            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
            }

            ToDoEFDAL testDAL = new ToDoEFDAL(options);

            ToDoListItem testToDoListItem = new ToDoListItem()
            {
                Name = "TestName",
                Description = "TestDescription",
                Category = "TestCategory",
                UserItemId = 1,
                TimeCreated = DateTime.Now
            };

            //Act
            testDAL.AddToDoList(testToDoListItem,testToDoListItem.UserItemId);

            // Assert  
            using (var context = new ToDoAppContext(options))
            {

                Assert.AreEqual(testToDoListItem.Name, context.ToDoListItem.First().Name);
                Assert.AreEqual(testToDoListItem.Description, context.ToDoListItem.First().Description);
                Assert.AreEqual(testToDoListItem.Category, context.ToDoListItem.First().Category);
                Assert.AreEqual(testToDoListItem.TimeCreated.ToString(), context.ToDoListItem.First().TimeCreated.ToString());
                Assert.AreEqual(testToDoListItem.UserItemId, context.ToDoListItem.First().UserItemId);
            };
        }
        //[TestMethod]
        //public void AddUserItemEFTest()
        //{
        //    // Arrange
        //    DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .EnableSensitiveDataLogging()
        //        .Options;

        //    using (var context = new ToDoAppContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        UserItem testUserItem = new UserItem()
        //        {
        //            FirstName = "TestFN",
        //            LastName = "TestLN",
        //            Username = "TestUser",
        //            Email = "test@test.com",
        //            RoleId = "Customer",
        //        };

        //        // Act
        //        context.UserItem.Add(testUserItem);
        //        context.SaveChanges();

        //        // Assert                
        //        Assert.AreEqual(testUserItem.FirstName, context.UserItem.First().FirstName);
        //        Assert.AreEqual(testUserItem.LastName, context.UserItem.First().LastName);
        //        Assert.AreEqual(testUserItem.Username, context.UserItem.First().Username);
        //        Assert.AreEqual(testUserItem.Email, context.UserItem.First().Email);
        //        Assert.AreEqual(testUserItem.RoleId, context.UserItem.First().RoleId);
        //    };
        //}
        //[TestMethod]
        //public void GetUserItemDALTest()
        //{

        //    // Arrange
        //    DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .EnableSensitiveDataLogging()
        //        .Options;

        //    using (var context = new ToDoAppContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //    }

        //    ToDoEFDAL testDAL = new ToDoEFDAL(options);

        //    UserItem testUserItem = new UserItem()
        //    {
        //        FirstName = "TestFN",
        //        LastName = "TestLN",
        //        Username = "TestUser",
        //        Email = "test@test.com",
        //        RoleId = "Customer",
        //    };

        //    testUserItem.Id = testDAL.AddUserItem(testUserItem);

        //    //Act
        //    UserItem returnedTestUser = testDAL.GetUserItem(testUserItem.Id);

        //    // Assert  

        //    Assert.AreEqual(testUserItem.FirstName, returnedTestUser.FirstName);
        //    Assert.AreEqual(testUserItem.LastName, returnedTestUser.LastName);
        //    Assert.AreEqual(testUserItem.Username, returnedTestUser.Username);
        //    Assert.AreEqual(testUserItem.Email, returnedTestUser.Email);
        //    Assert.AreEqual(testUserItem.RoleId, returnedTestUser.RoleId);
        //    Assert.AreEqual(testUserItem.Id, returnedTestUser.Id);

        //}
        //[TestMethod]
        //public void GetUserItemEFTest()
        //{
        //    // Arrange
        //    DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .EnableSensitiveDataLogging()
        //        .Options;

        //    UserItem testUserItem = new UserItem()
        //    {
        //        FirstName = "TestFN",
        //        LastName = "TestLN",
        //        Username = "TestUser",
        //        Email = "test@test.com",
        //        RoleId = "Customer",
        //    };

        //    // Act
        //    using (var context = new ToDoAppContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.UserItem.Add(testUserItem);
        //        context.SaveChanges();
        //        testUserItem.Id = context.UserItem.Single(u => u.Username == testUserItem.Username).Id;
        //    };

        //    // Assert
        //    using (var context = new ToDoAppContext(options))
        //    {
        //        Assert.AreEqual(testUserItem.FirstName, context.UserItem.Find(testUserItem.Id).FirstName);
        //        Assert.AreEqual(testUserItem.LastName, context.UserItem.Find(testUserItem.Id).LastName);
        //        Assert.AreEqual(testUserItem.Username, context.UserItem.Find(testUserItem.Id).Username);
        //        Assert.AreEqual(testUserItem.Email, context.UserItem.Find(testUserItem.Id).Email);
        //        Assert.AreEqual(testUserItem.RoleId, context.UserItem.Find(testUserItem.Id).RoleId);
        //    };
        //}
        //[TestMethod]
        //public void UpdateUserItemDALTest()
        //{

        //    // Arrange
        //    DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .EnableSensitiveDataLogging()
        //        .Options;

        //    using (var context = new ToDoAppContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //    }

        //    ToDoEFDAL testDAL = new ToDoEFDAL(options);

        //    UserItem testUserItem = new UserItem()
        //    {
        //        FirstName = "TestFN",
        //        LastName = "TestLN",
        //        Username = "TestUser",
        //        Email = "test@test.com",
        //        RoleId = "Customer",
        //    };

        //    testUserItem.Id = testDAL.AddUserItem(testUserItem);

        //    UserItem updatedTestUserItem = new UserItem()
        //    {
        //        FirstName = "FNTest",
        //        LastName = "LNTest",
        //        Username = "UserTest",
        //        Email = "123@test.com",
        //        RoleId = "Administrator",
        //        Id = testUserItem.Id
        //    };

        //    testDAL.UpdateUserItem(updatedTestUserItem);

        //    //Act
        //    UserItem returnedTestUser = testDAL.GetUserItem(updatedTestUserItem.Id);

        //    // Assert             
        //    Assert.AreEqual(updatedTestUserItem.FirstName, returnedTestUser.FirstName);
        //    Assert.AreEqual(updatedTestUserItem.LastName, returnedTestUser.LastName);
        //    Assert.AreEqual(updatedTestUserItem.Username, returnedTestUser.Username);
        //    Assert.AreEqual(updatedTestUserItem.Email, returnedTestUser.Email);
        //    Assert.AreEqual(updatedTestUserItem.RoleId, returnedTestUser.RoleId);
        //    Assert.AreEqual(updatedTestUserItem.Id, returnedTestUser.Id);
        //}
        //[TestMethod]
        //public void UpdateUserItemEFTest()
        //{
        //    // Arrange
        //    DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .EnableSensitiveDataLogging()
        //        .Options;

        //    UserItem testUserItem = new UserItem()
        //    {
        //        FirstName = "TestFN",
        //        LastName = "TestLN",
        //        Username = "TestUser",
        //        Email = "test@test.com",
        //        RoleId = "Customer",
        //    };

        //    // Act
        //    using (var context = new ToDoAppContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.UserItem.Add(testUserItem);
        //        context.SaveChanges();
        //        testUserItem.Id = context.UserItem.Single(u => u.Username == testUserItem.Username).Id;
        //    };

        //    UserItem updatedTestUserItem = new UserItem()
        //    {
        //        FirstName = "FNTest",
        //        LastName = "LNTest",
        //        Username = "UserTest",
        //        Email = "123@test.com",
        //        RoleId = "Administrator",
        //        Id = testUserItem.Id
        //    };


        //    UserItem returnedTestUser;
        //    using (var context = new ToDoAppContext(options))
        //    {
        //        returnedTestUser = context.UserItem.Single(u => u.Id == updatedTestUserItem.Id);
        //        returnedTestUser.FirstName = updatedTestUserItem.FirstName;
        //        returnedTestUser.LastName = updatedTestUserItem.LastName;
        //        returnedTestUser.Username = updatedTestUserItem.Username;
        //        returnedTestUser.RoleId = updatedTestUserItem.RoleId;
        //        context.SaveChanges();
        //    }

        //    // Assert
        //    using (var context = new ToDoAppContext(options))
        //    {
        //        Assert.AreEqual(returnedTestUser.FirstName, context.UserItem.Find(testUserItem.Id).FirstName);
        //        Assert.AreEqual(returnedTestUser.LastName, context.UserItem.Find(testUserItem.Id).LastName);
        //        Assert.AreEqual(returnedTestUser.Username, context.UserItem.Find(testUserItem.Id).Username);
        //        Assert.AreEqual(returnedTestUser.Email, context.UserItem.Find(testUserItem.Id).Email);
        //        Assert.AreEqual(returnedTestUser.RoleId, context.UserItem.Find(testUserItem.Id).RoleId);
        //        Assert.AreEqual(updatedTestUserItem.Id, returnedTestUser.Id);
        //    };
        //}
        [TestMethod]
        public void DeleteToDoListItemDALTest()
        {

            // Arrange
            DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ToDoAppContext(options))
            {
                context.Database.EnsureDeleted();
            }

            ToDoEFDAL testDAL = new ToDoEFDAL(options);

            ToDoListItem testToDoListItem = new ToDoListItem()
            {
                Name = "TestName",
                Description = "TestDescription",
                Category = "TestCategory",
                UserItemId = 1,
                TimeCreated = DateTime.Now
            };

            //Act
            testToDoListItem.Id = testDAL.AddToDoList(testToDoListItem, testToDoListItem.UserItemId);

            

            testDAL.DeleteToDoList(testToDoListItem);

            ToDoListItem returnedTestToDoList = testDAL.GetToDoListItem(testToDoListItem.Id);

            // Assert  
            Assert.IsNull(returnedTestToDoList);
        }
        //[TestMethod]
        //public void DeleteUserItemEFTest()
        //{
        //    // Arrange
        //    DbContextOptions<ToDoAppContext> options = new DbContextOptionsBuilder<ToDoAppContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .EnableSensitiveDataLogging()
        //        .Options;

        //    UserItem testUserItem = new UserItem()
        //    {
        //        FirstName = "TestFN",
        //        LastName = "TestLN",
        //        Username = "TestUser",
        //        Email = "test@test.com",
        //        RoleId = "Customer",
        //    };

        //    // Act
        //    using (var context = new ToDoAppContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.UserItem.Add(testUserItem);
        //        context.SaveChanges();
        //        testUserItem.Id = context.UserItem.Single(u => u.Username == testUserItem.Username).Id;
        //    };

        //    using (var context = new ToDoAppContext(options))
        //    {
        //        context.UserItem.Remove(testUserItem);
        //        context.SaveChanges();
        //    };

        //    UserItem returnedTestUser;
        //    using (var context = new ToDoAppContext(options))
        //    {
        //        returnedTestUser = context.UserItem.Find(testUserItem.Id);
        //    }

        //    // Assert
        //    Assert.IsNull(returnedTestUser);
        //}
        #endregion
    }
}
