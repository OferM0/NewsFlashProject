using Microsoft.EntityFrameworkCore;
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
    public class NewsItemService : BaseEntity
    {
        public NewsItemService(Logger log) : base(log)
        {
        }

        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ClearList function in NewsItems Entity." });
                MainManager.Instance.newsItemsList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute ClearList function in NewsItems Entity, {ex.Message}." });
                throw;
            }
        }

        public List<NewsItem> GetAllNewsItems()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllNewsItems function in NewsItems Entity." });

                MainManager.Instance.newsItemsList = MainManager.Instance.db.NewsItems.ToList();
                return MainManager.Instance.newsItemsList;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetAllNewsItems function in NewsItems Entity, {ex.Message}." });

                throw;
            }
        }

        public List<NewsItem> GetAllNewsItemsByTopic(string topic)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllNewsItemsByTopic({topic}) function in NewsItems Entity." });

                MainManager.Instance.newsItemsList = MainManager.Instance.db.NewsItems.ToList();
                return MainManager.Instance.newsItemsList.Where(n => n.Category.Topic == topic).ToList();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetAllNewsItemsByTopic({topic}) function in NewsItems Entity, {ex.Message}." });

                throw;
            }
        }

        public List<NewsItem> GetTrendingNewsItems(string userId)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetTrendingNewsItems function in NewsItems Entity." });

                User user = MainManager.Instance.db.Users.Include(u => u.Interests).FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    return null;
                }

                // Get the user's interests as a list of category IDs
                List<int> categoryIds = user.Interests.Select(i => i.Id).ToList();

                MainManager.Instance.newsItemsList = MainManager.Instance.db.NewsItems.ToList();

                // Retrieve the news items that match the user's interests and meet the other criteria
                List<NewsItem> newsItems = MainManager.Instance.db.NewsItems
                    .Where(n => categoryIds.Contains(n.CategoryId) && n.Category.Topic != "BreakingNews" && n.ClickCount > 0)
                    .OrderByDescending(n => n.ClickCount)
                    .Take(10)
                    .ToList();

                return newsItems;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetTrendingNewsItems function in NewsItems Entity, {ex.Message}." });

                throw;
            }
        }

        public List<NewsItem> GetCuriousNewsItems(string userId)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetCuriousNewsItems function in NewsItems Entity." });

                User user = MainManager.Instance.db.Users.Include(u => u.Interests).FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    return null;
                }

                // Get the user's interests as a list of category IDs
                List<int> categoryIds = user.Interests.Select(i => i.Id).ToList();

                MainManager.Instance.newsItemsList = MainManager.Instance.db.NewsItems.ToList();

                // Retrieve the news items that match the user's interests and meet the other criteria
                List<NewsItem> newsItems = MainManager.Instance.db.NewsItems
                    .Where(n => categoryIds.Contains(n.CategoryId) && n.Category.Topic != "BreakingNews" && n.ClickCount == 0)
                    .Take(10)
                    .ToList();

                return newsItems;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetCuriousNewsItems function in NewsItems Entity, {ex.Message}." });

                throw;
            }
        }

        public NewsItem GetNewsItemById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetNewsItemById(id:{id}) function in NewsItems Entity." });

                NewsItem newsItem = MainManager.Instance.db.NewsItems.Find(id);
                return newsItem;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute GetNewsItemById(id:{id}) function in NewsItems Entity, {ex.Message}." });

                throw;
            }
        }

        public void AddNewNewsItem(string itemId, string title, string description, string link, string imageUrl, DateTime publishDate, int categoryId, int websiteId)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewNewsItem function in NewsItems Entity." });

                NewsItem newsItem = new NewsItem
                {
                    ItemId = itemId,
                    Title = title,
                    Description = description,
                    Link = link,
                    ImageUrl = imageUrl,
                    ClickCount = 0,
                    PublishDate = publishDate,
                    CategoryId = categoryId,
                    WebSiteId = websiteId
                };
                MainManager.Instance.newsItemsList.Add(newsItem);
                MainManager.Instance.db.NewsItems.Add(newsItem);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute AddNewNewsItem function in NewsItems Entity, {ex.Message}." });

                throw;
            }
        }

        public void UpdateNewsItemById(string id, /*string title, string description, string link, string imageUrl, DateTime publishDate,*/ int clickCount)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateNewsItemById(id:{id}) function in NewsItems Entity." });

                NewsItem newsItem = MainManager.Instance.db.NewsItems.FirstOrDefault(i => i.ItemId == id);
                if (newsItem != null)
                {
                    newsItem.ClickCount = clickCount;
                    MainManager.Instance.newsItemsList[MainManager.Instance.newsItemsList.FindIndex(u => u.ItemId == id)] = newsItem;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute UpdateNewsItemById(id:{id}) function in NewsItems Entity, {ex.Message}." });

                throw;
            }
        }

        public void DeleteNewsItemById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteNewsItemById(id:{id}) function in NewsItems Entity." });

                NewsItem newsItem = MainManager.Instance.db.NewsItems.Find(id);
                if (newsItem != null)
                {
                    MainManager.Instance.newsItemsList.Remove(newsItem);
                    MainManager.Instance.db.NewsItems.Remove(newsItem);
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to Execute DeleteNewsItemById(id:{id}) function in NewsItems Entity, {ex.Message}." });

                throw;
            }
        }
    }
}
