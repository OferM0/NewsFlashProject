using server.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Utilities;
using server.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace server.Entities
{
    public class MainManager
    {
        public DataLayer db;
        public DbContextOptions<DataLayer> dbOptions;

        public Logger log;
        //private ConfigurationKeys config;

        public WebSiteService websiteService;
        public CategoryService categoryService;
        public RssService rssService;
        public UserService userService;
        public NewsItemService newsItemService;

        public NewsWebsite websiteYnet;
        public NewsWebsite websiteWalla;
        public NewsWebsite websiteMaariv;
        public NewsWebsite websiteGlobes;

        public List<WebSite> webSitesList = new List<WebSite>();
        public List<Category> categoriesList = new List<Category>();
        public List<Rss> rssesList = new List<Rss>();
        public List<User> usersList = new List<User>();
        public List<NewsItem> newsItemsList = new List<NewsItem>();

        private static readonly MainManager _instance = new MainManager();
        public static MainManager Instance { get { return _instance; } }

        private MainManager()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            dbOptions = new DbContextOptionsBuilder<DataLayer>()
                .UseSqlServer(config.GetConnectionString("NewsFlash"))
                .Options;

            //for the tests
            //dbOptions = new DbContextOptionsBuilder<DataLayer>()
            //    .UseSqlServer("Server=localhost\\sqlexpress;Database=NewsFlashTestsDB;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True")
            //    .Options;

            db = new DataLayer(dbOptions);

            InitManager();
        }

        public void InitManager()
        {
            log = new Logger("Console");

            websiteService = new WebSiteService(log);
            categoryService = new CategoryService(log);
            rssService = new RssService(log);
            userService = new UserService(log);
            newsItemService = new NewsItemService(log);

            webSitesList = db.WebSites.ToList();
            categoriesList = db.Categories.Include(c => c.Users).ToList();
            rssesList = db.Rsses.ToList();
            usersList = db.Users.Include(u => u.Interests).ToList();
            newsItemsList = db.NewsItems.ToList();

            websiteYnet = new NewsWebsite("Ynet");
            websiteYnet.newsWebsite.Init(rssesList);
            websiteWalla = new NewsWebsite("Walla");
            websiteWalla.newsWebsite.Init(rssesList);
            websiteMaariv = new NewsWebsite("Maariv");
            websiteMaariv.newsWebsite.Init(rssesList);
            websiteGlobes = new NewsWebsite("Globes");
            websiteGlobes.newsWebsite.Init(rssesList);
        }

        public void InitWebsites()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize websites List." });

                websiteService.GetAllWebSites();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InitWebsites function in MainManager." });

                throw;
            }
        }

        public void InitCategories()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize categories List." });

                categoryService.GetAllCategories();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InitCategories function in MainManager." });

                throw;
            }
        }

        public void InitRsses()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize rsses List." });

                rssService.GetAllRsses();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InitRsses function in MainManager." });

                throw;
            }
        }

        public void InitUsers()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize users List." });

                userService.GetAllUsers();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InitUsers function in MainManager." });

                throw;
            }
        }

        public void InitNewsItems()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize newsItems List." });

                newsItemService.GetAllNewsItems();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InitNewsItems function in MainManager." });

                throw;
            }
        }
    }
}
