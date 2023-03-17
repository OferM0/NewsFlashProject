using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using server.Model;

namespace server.Dal
{
    public class DataLayer : DbContext
    {
        public DataLayer(DbContextOptions options) : base(options)
        {
        }

        //DbSet- פקודה ליצירת טבלאות בדטה בייס
        //טבלת משתמשים
        public DbSet<User> Users { get; set; }
        public DbSet<WebSite> WebSites { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rss> Rsses { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<LogItem> Logs { get; set; }
    }
}

