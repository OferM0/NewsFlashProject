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
    public class WebSiteService : BaseEntity
    {
        public WebSiteService(Logger log) : base(log)
        {
        }

        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ClearList function in WebSites Entity." });
                MainManager.Instance.webSitesList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute ClearList function in WebSites Entity, {ex.Message}." });
                throw;
            }
        }

        public List<WebSite> GetAllWebSites()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllWebSites function in WebSites Entity." });

                MainManager.Instance.webSitesList = MainManager.Instance.db.WebSites.ToList();
                return MainManager.Instance.webSitesList;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetAllWebSites function in WebSites Entity, {ex.Message}." });
                throw;
            }
        }

        public WebSite GetWebSiteById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetWebSiteById(id:{id}) function in WebSites Entity." });

                WebSite webSite = MainManager.Instance.db.WebSites.Find(id);
                return webSite;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetWebSiteById(id:{id}) function in WebSites Entity, {ex.Message}." });

                throw;
            }
        }

        public void AddNewWebSite(string name)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewWebSite function in WebSites Entity." });

                WebSite webSite = new WebSite
                {
                    Name = name
                };
                MainManager.Instance.webSitesList.Add(webSite);
                MainManager.Instance.db.WebSites.Add(webSite);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute AddNewWebSite function in WebSites Entity, {ex.Message}." });

                throw;
            }
        }

        public void UpdateWebSiteById(int id, string name)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateWebSiteById(id:{id}) function in WebSites Entity." });

                WebSite webSite = MainManager.Instance.db.WebSites.Find(id);
                if (webSite != null)
                {
                    webSite.Name = name;
                    MainManager.Instance.webSitesList[id].Name = name;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateWebSiteById(id:{id}) function in WebSites Entity, {ex.Message}." });

                throw;
            }
        }

        public void DeleteWebSiteById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteWebSiteById(id:{id}) function in WebSites Entity." });

                WebSite webSite = MainManager.Instance.db.WebSites.Find(id);
                if (webSite != null)
                {
                    MainManager.Instance.webSitesList.Remove(webSite);
                    MainManager.Instance.db.WebSites.Remove(webSite);
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute DeleteWebSiteById(id:{id}) function in WebSites Entity, {ex.Message}." });

                throw;
            }
        }
    }
}
