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
    public class RssService : BaseEntity
    {
        public RssService(Logger log) : base(log)
        {
        }

        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute  ClearList function in Rsses Entity." });

                MainManager.Instance.rssesList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute ClearList function in Rsses Entity, {ex.Message}." });

                throw;
            }
        }

        public List<Rss> GetAllRsses()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllRsses function in Rsses Entity." });

                MainManager.Instance.rssesList = MainManager.Instance.db.Rsses.ToList();
                return MainManager.Instance.rssesList;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetAllRsses function in Rsses Entity, {ex.Message}." });

                throw;
            }
        }

        public Rss GetRssById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetRssById(id:{id}) function in Rsses Entity." });

                Rss rss = MainManager.Instance.db.Rsses.Find(id);
                return rss;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetRssById(id:{id}) function in Rsses Entity, {ex.Message}." });

                throw;
            }
        }

        public void AddNewRss(string url, int categoryId, int websiteId)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewRss function in Rsses Entity." });

                Rss rss = new Rss
                {
                    Url = url,
                    CategoryId = categoryId,
                    WebSiteId = websiteId
                };
                MainManager.Instance.rssesList.Add(rss);
                MainManager.Instance.db.Rsses.Add(rss);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute AddNewRss function in Rsses Entity, {ex.Message}." });

                throw;
            }
        }

        public void UpdateRssById(int id, string url, int categoryId, int websiteId)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateRssById(id:{id}) function in Rsses Entity." });

                Rss rss = MainManager.Instance.db.Rsses.Find(id);
                if (rss != null)
                {
                    rss.Url = url;
                    rss.CategoryId = categoryId;
                    rss.WebSiteId = websiteId;
                    MainManager.Instance.rssesList[id] = rss;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateRssById(id:{id}) function in Rsses Entity, {ex.Message}." });

                throw;
            }
        }

        public void DeleteRssById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteRssById(id:{id}) function in Rsses Entity." });

                Rss rss = MainManager.Instance.db.Rsses.Find(id);
                if (rss != null)
                {
                    MainManager.Instance.rssesList.Remove(rss);
                    MainManager.Instance.db.Rsses.Remove(rss);
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute DeleteRssById(id:{id}) function in Rsses Entity, {ex.Message}." });

                throw;
            }
        }
    }
}
