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
    [Route("api/rsses")]
    public class RssesController : ControllerBase
    {
        private Mapper mapper;

        public RssesController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Rss, ReturnRssRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllRsses()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllRsses function in Rsses Controller." });

                List<Rss> rsses = MainManager.Instance.rssService.GetAllRsses();
                List<ReturnRssRequest> returnRsses = mapper.Map<List<ReturnRssRequest>>(rsses);
                return Ok(returnRsses);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failde to execute GetAllRsses function in Rsses Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public IActionResult GetRssById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetRssById(id:{id}) function in Rsses Controller." });

                Rss rss = MainManager.Instance.rssService.GetRssById(id);
                if (rss == null)
                {
                    return NotFound();
                }
                ReturnRssRequest returnRss = mapper.Map<ReturnRssRequest>(rss);
                return Ok(returnRss);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failde to execute GetRssById(id:{id}) function in Rsses Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddRss(AddRssRequest addRssRequest)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddRss function in Rsses Controller." });

                MainManager.Instance.rssService.AddNewRss(addRssRequest.Url, addRssRequest.CategoryId, addRssRequest.WebSiteId);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failde to execute AddRss function in Rsses Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateRss(int id, UpdateRssRequest updateRssRequest)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateRss(id:{id}) function in Rsses Controller." });

                MainManager.Instance.rssService.UpdateRssById(id, updateRssRequest.Url, updateRssRequest.CategoryId, updateRssRequest.WebSiteId);
                return NoContent();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failde to execute UpdateRss(id:{id}) function in Rsses Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{id}")]
        public IActionResult DeleteRss(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteRss(id:{id}) function in Rsses Controller." });

                MainManager.Instance.rssService.DeleteRssById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failde to execute DeleteRss(id:{id}) function in Rsses Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }
    }
}
