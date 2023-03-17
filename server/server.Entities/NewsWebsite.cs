using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static System.Reflection.Metadata.BlobBuilder;

namespace server.Entities
{
    public interface INewsWebsite
    {
        Queue<NewsItem> NewsItems { get; set; }
        void Init(List<Rss> rssesList);
        Task CreateNewsItems(CancellationToken cancellationToken);
        Task InsertNewsItem(CancellationToken cancellationToken);
        void Stop();
    }
    public class NewsWebsite
    {
        public INewsWebsite newsWebsite;

        public NewsWebsite(string provider)
        {
            switch (provider)
            {
                case "Ynet":
                    newsWebsite = new Ynet();
                    break;

                case "Walla":
                    newsWebsite = new Walla();
                    break;

                case "Maariv":
                    newsWebsite = new Maariv();
                    break;

                case "Globes":
                    newsWebsite = new Globes();
                    break;

                default:
                    newsWebsite = null;
                    break;
            }
        }
    }
}