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
    [Route("api/websites")]
    public class WebSitesController : ControllerBase
    {
        private Mapper mapper;

        public WebSitesController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WebSite, ReturnWebSiteRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllWebSite()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllWebSite function in WebSites Controller." });

                List<WebSite> websites = MainManager.Instance.websiteService.GetAllWebSites();
                List<ReturnWebSiteRequest> returnWebsites = mapper.Map<List<ReturnWebSiteRequest>>(websites);
                return Ok(returnWebsites);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetAllWebSite function in WebSites Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public IActionResult GetWebSiteById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetWebSiteById(id:{id}) function in WebSites Controller." });

                WebSite webSite = MainManager.Instance.websiteService.GetWebSiteById(id);
                if (webSite == null)
                {
                    return NotFound();
                }
                ReturnWebSiteRequest returnWebsite = mapper.Map<ReturnWebSiteRequest>(webSite);
                return Ok(returnWebsite);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetWebSiteById(id:{id}) function in WebSites Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddWebSite(string name)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddWebSite function in WebSites Controller." });

                MainManager.Instance.websiteService.AddNewWebSite(name);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute AddWebSite function in WebSites Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateWebSite(int id, string name)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateWebSite(id:{id}) function in WebSites Controller." });

                MainManager.Instance.websiteService.UpdateWebSiteById(id, name);
                return NoContent();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateWebSite(id:{id}) function in WebSites Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{id}")]
        public IActionResult DeleteWebSite(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteWebSite(id:{id}) function in WebSites Controller." });

                MainManager.Instance.websiteService.DeleteWebSiteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute DeleteWebSite(id:{id}) function in WebSites Controller, {ex.Message}." });

                return BadRequest(ex.Message);
            }
        }
    }
}
