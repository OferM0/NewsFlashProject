using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Dal;
using server.Entities;
using server.MicroService.Models;
using server.Model;
using Utilities;

namespace server.MicroService.Controllers
{
    [ApiController]
    [Route("api/newsitems")]
    public class NewsItemsContoller : ControllerBase
    {
        private Mapper mapper;

        public NewsItemsContoller()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NewsItem, ReturnNewsItemRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllNewsItems()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllNewsItems function in NewsItems Controller." });

                List<NewsItem> newsItems = MainManager.Instance.newsItemService.GetAllNewsItems();
                List<ReturnNewsItemRequest> returnNewsItems = mapper.Map<List<ReturnNewsItemRequest>>(newsItems);
                return Ok(returnNewsItems);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetAllNewsItems function in NewsItems Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getbytopic")]
        public IActionResult GetAllNewsItemsByTopic(string topic)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllNewsItemsByTopic(topic:{topic}) function in NewsItems Controller." });

                if (!string.IsNullOrEmpty(topic))
                {
                    List<NewsItem> newsItems = MainManager.Instance.newsItemService.GetAllNewsItemsByTopic(topic);
                    if (newsItems == null && !newsItems.Any())
                    {
                        return BadRequest("There are not any news from this topic");
                    }
                    List<ReturnNewsItemRequest> returnNewsItems = mapper.Map<List<ReturnNewsItemRequest>>(newsItems.OrderByDescending(n => n.PublishDate).Take(10).ToList());
                    return Ok(returnNewsItems);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetAllNewsItemsByTopic(topic:{topic}) function in NewsItems Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("gettrending")]
        public IActionResult GetTrendingNewsItems(string userId)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetTrendingNewsItems function in NewsItems Controller." });

                List<NewsItem> newsItems = MainManager.Instance.newsItemService.GetTrendingNewsItems(userId);
                List<ReturnNewsItemRequest> returnNewsItems = mapper.Map<List<ReturnNewsItemRequest>>(newsItems);
                return Ok(returnNewsItems);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetTrendingNewsItems function in NewsItems Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getcurious")]
        public IActionResult GetCuriousNewsItems(string userId)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetCuriousNewsItems function in NewsItems Controller." });

                List<NewsItem> newsItems = MainManager.Instance.newsItemService.GetCuriousNewsItems(userId);
                List<ReturnNewsItemRequest> returnNewsItems = mapper.Map<List<ReturnNewsItemRequest>>(newsItems);
                return Ok(returnNewsItems);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetCuriousNewsItems function in NewsItems Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public IActionResult GetNewsItemById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetNewsItemById(id:{id}) function in NewsItems Controller." });

                NewsItem newsItem = MainManager.Instance.newsItemService.GetNewsItemById(id);
                if (newsItem == null)
                {
                    return NotFound();
                }
                ReturnNewsItemRequest returnCategory = mapper.Map<ReturnNewsItemRequest>(newsItem);
                return Ok(returnCategory);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetNewsItemById(id:{id}) function in NewsItems Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddNewsItem(AddNewsItemRequest addNewsItemRequest)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewsItem function in NewsItems Controller." });

                MainManager.Instance.newsItemService.AddNewNewsItem(addNewsItemRequest.ItemId, addNewsItemRequest.Title, addNewsItemRequest.Description, addNewsItemRequest.Link, addNewsItemRequest.ImageUrl, addNewsItemRequest.PublishDate, addNewsItemRequest.CategoryId, addNewsItemRequest.WebSiteId);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute AddNewsItem function in NewsItems Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateNewsItem(string id, int ClickCount)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateNewsItem(id:{id}) function in NewsItems Controller." });

                MainManager.Instance.newsItemService.UpdateNewsItemById(id, ClickCount);
                return NoContent();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute UpdateNewsItem(id:{id}) function in NewsItems Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{id}")]
        public IActionResult DeleteNewsItem(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteNewsItem(id:{id}) function in NewsItems Controller." });

                MainManager.Instance.newsItemService.DeleteNewsItemById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute DeleteNewsItem(id:{id}) function in NewsItems Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }
    }
}
