using Microsoft.AspNetCore.Mvc;
using server.Model;
using server.Entities;
using AutoMapper;
using Utilities;
using server.MicroService.Models;

namespace server.MicroService.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private Mapper mapper;

        public CategoriesController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, ReturnCategoryRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllCategories()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllCategories function in Categories Controller." });

                List<Category> categories = MainManager.Instance.categoryService.GetAllCategories();
                List<ReturnCategoryRequest> returnCategories = mapper.Map<List<ReturnCategoryRequest>>(categories);
                return Ok(returnCategories);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetAllCategories function in Categories Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetCategoryById(id:{id}) function in Categories Controller." });

                Category category = MainManager.Instance.categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }
                ReturnCategoryRequest returnCategory = mapper.Map<ReturnCategoryRequest>(category);
                return Ok(returnCategory);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetCategoryById(id:{id}) function in Categories Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddCategory(string addCategoryRequest)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddCategory function in Categories Controller." });

                MainManager.Instance.categoryService.AddNewCategory(addCategoryRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute AddCategory function in Categories Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateCategory(int id, string categoryUpdate)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateCategory(id:{id}) function in Categories Controller." });

                MainManager.Instance.categoryService.UpdateCategoryById(id, categoryUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateCategory(id:{id}) function in Categories Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteCategory(id:{id}) function in Categories Controller." });

                MainManager.Instance.categoryService.DeleteCategoryById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute DeleteCategory(id:{id}) function in Categories Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }
    }
}
