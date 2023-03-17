using server.Dal;
using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace server.Entities
{
    public class Maariv : INewsWebsite
    {
        public Queue<NewsItem> NewsItems { get; set; }
        private CancellationTokenSource cts;
        private List<Rss> maarivRsses = new List<Rss>();

        public void Init(List<Rss> rssesList)
        {
            try
            {
                maarivRsses = rssesList.FindAll(rss => rss.WebSiteId == 3);
                NewsItems = new Queue<NewsItem>();
                cts = new CancellationTokenSource();

                Task.Run(() => InsertNewsItem(cts.Token), cts.Token);
                Task.Run(() => CreateNewsItems(cts.Token), cts.Token);

                //MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Maariv Init method executed successfully." });
            }
            catch (Exception ex)
            {
                //MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in Maariv Init method: {ex.Message}" });

                throw;
            }
        }

        public async Task InsertNewsItem(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (NewsItems.Count > 0)
                    {
                        using (var db = new DataLayer(MainManager.Instance.dbOptions))
                        {
                            var newsItem = NewsItems.Dequeue();
                            if (!db.NewsItems.Any(i => i.ItemId == newsItem.ItemId))
                            {
                                db.Add(newsItem);
                                await db.SaveChangesAsync(cancellationToken);

                                //MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Maariv News item with id {newsItem.ItemId} inserted successfully." });
                            }
                        }
                    }
                    await Task.Delay(100, cancellationToken);
                }
                catch (Exception ex)
                {
                    //MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"An error occurred while inserting a Maariv news item to the database: {ex.Message}" });

                    throw;
                }
            }
        }

        public async Task CreateNewsItems(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (var db = new DataLayer(MainManager.Instance.dbOptions))
                    {
                        foreach (var rss in maarivRsses)
                        {
                            var webClient = new WebClient();
                            var rssData = await webClient.DownloadStringTaskAsync(rss.Url);
                            var rssXml = XDocument.Parse(rssData);
                            var items = rssXml.Descendants("item");

                            foreach (var item in items)
                            {
                                var itemId = item.Element("itemID")?.Value;
                                if (!db.NewsItems.Any(i => i.ItemId == itemId))
                                {
                                    var descriptionHtml = item.Element("description")?.Value;

                                    var description = Regex.Replace(descriptionHtml, "<.*?>", string.Empty);
                                    var startIndex = description.IndexOf("<br/>") + 5;
                                    var endIndex = description.Length - startIndex;
                                    var hebrewText = description.Substring(startIndex, endIndex).Trim();

                                    var newsItem = new NewsItem
                                    {

                                        ItemId = itemId,
                                        Title = item.Element("title")?.Value,
                                        Description = hebrewText,
                                        Link = item.Element("link")?.Value,
                                        ImageUrl = Regex.Match(descriptionHtml, @"(?<=src=('|""))[^'""]+(?=('|""))")?.Value,
                                        PublishDate = DateTime.Parse(item.Element("pubDate")?.Value),
                                        WebSiteId = 3,
                                        CategoryId = rss.CategoryId,
                                        ClickCount = 0
                                    };

                                    NewsItems.Enqueue(newsItem);

                                    //MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Maariv News item with id {newsItem.ItemId} added to the queue successfully." });
                                }
                            }
                        }
                        await Task.Delay(3600 * 1000, cancellationToken); //every 60 minutes
                    }
                }
                catch (Exception ex)
                {
                    //MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"An error occurred while inserting a Maariv news item to the queue: {ex.Message}" });

                    throw;
                }
            }
        }

        public void Stop()
        {
            cts.Cancel();
        }
    }
}
