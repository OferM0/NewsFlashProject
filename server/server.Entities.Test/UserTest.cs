using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using server.Entities;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;
using server.Model;
using Utilities;

namespace server.Entities.Test
{
    [TestFixture]
    internal class UserTest
    {
        private UserService userService;
        private Logger log;

        [SetUp]
        public void Init()
        {
            log = new Logger("Console");
            userService = new UserService(log);
        }

        [Test, Category("TestClearList")]
        public void TestClearList()
        {
            try
            {
                int expectedCount = 0;

                List<User> actualUsers = userService.GetAllUsers();
                int actualCount = actualUsers.Count;

                Assert.AreEqual(expectedCount, actualCount);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestClearList failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetAllUsers")]
        public void TestGetAllUsers()
        {
            try
            {
                string userId = Guid.NewGuid().ToString();
                string name = "John Doe";
                string email = "johndoe@example.com";
                string phone = "555-1234";

                userService.AddNewUser(userId, name, email, phone);
                int expectedCount = 1;

                List<User> actualUsers = userService.GetAllUsers();
                int actualCount = actualUsers.Count;

                Assert.AreEqual(expectedCount, actualCount);

                userService.DeleteUserById(userId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetAllUsers failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetUserById")]
        public void TestGetUserById()
        {
            try
            {
                string userId = Guid.NewGuid().ToString();
                string name = "John Doe";
                string email = "johndoe@example.com";
                string phone = "555-1234";

                userService.AddNewUser(userId, name, email, phone);
                string expectedName = "John Doe";

                User actualUser = userService.GetUserById(userId);
                string actualName = actualUser.Name;

                Assert.AreEqual(expectedName, actualName);

                userService.DeleteUserById(userId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetUserById failed: {ex.Message}");
            }
        }

        [Test, Category("TestAddNewUser")]
        public void TestAddNewUser()
        {
            try
            {
                string userId = Guid.NewGuid().ToString();
                string name = "John Doe";
                string email = "johndoe@example.com";
                string phone = "555-1234";

                userService.AddNewUser(userId, name, email, phone);
                int expectedCount = 1;

                List<User> actualUsers = userService.GetAllUsers();
                int actualCount = actualUsers.Count;

                Assert.AreEqual(expectedCount, actualCount);

                userService.DeleteUserById(userId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestAddNewUser failed: {ex.Message}");
            }
        }

        [Test, Category("TestUpdateUserById")]
        public void TestUpdateUserById()
        {
            try
            {
                string userId = Guid.NewGuid().ToString();
                string name = "John Doe";
                string email = "johndoe@example.com";
                string phone = "555-1234";
                userService.AddNewUser(userId, name, email, phone);

                string updatedName = "Jane Doe";
                string updatedPhone = "555-5678";
                userService.UpdateUserById(userId, updatedName, updatedPhone);

                User updatedUser = userService.GetUserById(userId);

                Assert.AreEqual(updatedName, updatedUser.Name);
                Assert.AreEqual(updatedPhone, updatedUser.PhoneNumber);

                userService.DeleteUserById(userId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestUpdateUserById failed: {ex.Message}");
            }
        }
        [Test, Category("TestUpdateUserInterestsById")]
        public void TestUpdateUserInterestsById()
        {
            try
            {
                string userId = Guid.NewGuid().ToString();
                string name = "John Doe";
                string email = "johndoe@example.com";
                string phone = "555-1234";
                userService.AddNewUser(userId, name, email, phone);

                string[] updatedInterests = { "Politics", "Technology" };
                userService.UpdateUserInterestsById(userId, updatedInterests);

                User updatedUser = userService.GetUserById(userId);

                Assert.AreEqual(updatedInterests.Length, updatedUser.Interests.Count);
                foreach (string interest in updatedInterests)
                {
                    Assert.IsTrue(updatedUser.Interests.Any(c => c.Topic == interest));
                }

                userService.DeleteUserById(userId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestUpdateUserInterestsById failed: {ex.Message}");
            }
        }

        [Test, Category("TestDeleteUserById")]
        public void TestDeleteUserById()
        {
            try
            {
                string userId = Guid.NewGuid().ToString();
                string name = "John Doe";
                string email = "johndoe@example.com";
                string phone = "555-1234";
                userService.AddNewUser(userId, name, email, phone);

                userService.DeleteUserById(userId);

                User deletedUser = userService.GetUserById(userId);

                Assert.IsNull(deletedUser);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestDeleteUserById failed: {ex.Message}");
            }
        }
    }
}
