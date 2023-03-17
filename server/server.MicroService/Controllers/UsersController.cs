using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Dal;
using server.Entities;
using server.MicroService.Models;
using server.Model;
using System.Security.Cryptography;
using Utilities;

namespace server.MicroService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private Mapper mapper;

        public UsersController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, ReturnUserRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllUsers()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllUsers function in Users Controller." });

                List<User> users = MainManager.Instance.userService.GetAllUsers();
                List<ReturnUserRequest> returnUsers = mapper.Map<List<ReturnUserRequest>>(users);
                return Ok(returnUsers);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetAllUsers function in Users Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public IActionResult GetUserById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetUserById(id:{id}) function in Users Controller." });

                User user = MainManager.Instance.userService.GetUserById(id);
                if (user == null)
                {
                    return NoContent();
                }
                ReturnUserRequest returnUser = mapper.Map<ReturnUserRequest>(user);
                returnUser.Interests = user.Interests.Select(i => i.Topic).ToArray();
                return Ok(returnUser);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetUserById(id:{id}) function in Users Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddUser(AddUserRequest addUserRequest)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddUser function in Users Controller." });

                MainManager.Instance.userService.AddNewUser(addUserRequest.Id, addUserRequest.Name, addUserRequest.Email, addUserRequest.PhoneNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute AddUser function in Users Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(string id, UpdateUserRequest userUpdate)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateUser(id:{id}) function in Users Controller." });

                MainManager.Instance.userService.UpdateUserById(id, userUpdate.Name, userUpdate.PhoneNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateUser(id:{id}) function in Users Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateInterests/{id}")]
        public IActionResult UpdateUserInterests(string id, string[] interests)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateUserInterests(id:{id}) function in Users Controller." });

                MainManager.Instance.userService.UpdateUserInterestsById(id, interests);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateUserInterests(id:{id}) function in Users Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{id}")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteUser(id:{id}) function in Users Controller." });

                MainManager.Instance.userService.DeleteUserById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute DeleteUser(id:{id}) function in Users Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }
    }
}
