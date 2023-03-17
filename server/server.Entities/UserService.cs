using Microsoft.Extensions.Logging;
using server.Dal;
using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities
{
    public class UserService : BaseEntity
    {
        public UserService(Logger log) : base(log)
        {
        }

        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ClearList function in Users Entity." });

                MainManager.Instance.usersList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetAllUsers function in Users Entity, {ex.Message}." });

                throw;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllUsers function in Users Entity." });

                MainManager.Instance.usersList = MainManager.Instance.db.Users.ToList();
                return MainManager.Instance.usersList;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetAllUsers function in Users Entity, {ex.Message}." });
                throw;
            }
        }

        public User GetUserById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetUserById(id:{id}) function in Users Entity." });

                User user = MainManager.Instance.db.Users.Find(id);
                return user;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetUserById(id:{id}) function in Users Entity, {ex.Message}." });

                throw;
            }
        }

        public void AddNewUser(string userId, string name, string email, string phone)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewUser function in Users Entity." });

                User user = new User
                {
                    UserId = userId,
                    Name = name,
                    Email = email,
                    PhoneNumber = phone,
                    Interests = new List<Category> { MainManager.Instance.categoriesList.FirstOrDefault() } //each new user will get "BreakingNews" news even if he's not choosing- by deafult
                };
                MainManager.Instance.usersList.Add(user);
                MainManager.Instance.db.Users.Add(user);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute AddNewUser function in Users Entity, {ex.Message}." });

                throw;
            }
        }

        public void UpdateUserById(string id, string name, string phone)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateUserById(id:{id}) function in Users Entity." });

                User user = MainManager.Instance.db.Users.Find(id);
                if (user != null)
                {
                    user.Name = name;
                    user.PhoneNumber = phone;
                    MainManager.Instance.usersList[MainManager.Instance.usersList.FindIndex(u => u.UserId == id)] = user;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateUserById(id:{id}) function in Users Entity, {ex.Message}." });

                throw;
            }
        }

        public void UpdateUserInterestsById(string id, string[] interests)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateUserInterestsById(id:{id}) function in Users Entity." });

                User user = MainManager.Instance.db.Users.Find(id);
                if (user != null)
                {
                    List<Category> categories = MainManager.Instance.categoriesList.Where(c => interests.Contains(c.Topic)).ToList();
                    user.Interests = categories;
                    MainManager.Instance.usersList[MainManager.Instance.usersList.FindIndex(u => u.UserId == id)] = user;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateUserInterestsById(id:{id}) function in Users Entity, {ex.Message}." });

                throw;
            }
        }

        public void DeleteUserById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteUserById(id:{id}) function in Users Entity." });

                User user = MainManager.Instance.db.Users.Find(id);
                if (user != null)
                {
                    MainManager.Instance.usersList.Remove(user);
                    MainManager.Instance.db.Users.Remove(user);
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute DeleteUserById(id:{id}) function in Users Entity, {ex.Message}." });

                throw;
            }
        }
    }
}
