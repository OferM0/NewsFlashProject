using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using server.Dal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using server.Model;

namespace Utilities
{
    public class LogDB : ILogger
    {
        public Queue<LogItem> eventsQueue { get; set; }
        public Queue<LogItem> errorsQueue { get; set; }
        Task task;
        Task task2;
        public bool stop = false;

        private static IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        private static DbContextOptions<DataLayer> dbOptions = new DbContextOptionsBuilder<DataLayer>()
                .UseSqlServer(config.GetConnectionString("NewsFlash"))
                .Options;
        //private static DataLayer db = new DataLayer(dbOptions);

        public void Init()
        {
            eventsQueue = new Queue<LogItem>();
            errorsQueue = new Queue<LogItem>();

            LogMessage(new LogItem { Type = "Event", Message = "Logs Started", LogTime = DateTime.Now });

            task = Task.Run(() =>
            {
                while (!stop)
                {
                    if (eventsQueue.Count > 0)
                    {
                        LogMessage(eventsQueue.Dequeue());
                    }
                    if (errorsQueue.Count > 0)
                    {
                        LogException(errorsQueue.Dequeue());
                    }
                    Thread.Sleep(100);
                }
            });

            task2 = Task.Run(() =>
            {
                while (!stop)
                {
                    LogCheckHouseKeeping();
                    Thread.Sleep(3600000);
                }
            });
        }

        public void LogMessage(LogItem log)
        {
            try
            {
                using (var db = new DataLayer(dbOptions))
                {
                    db.Logs.Add(new LogItem { Message = log.Message, Type = log.Type, LogTime = log.LogTime });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error inserting message into logs: " + ex.Message);
                Console.ResetColor();
            }
        }

        public void LogException(LogItem log)
        {
            try
            {
                using (var db = new DataLayer(dbOptions))
                {
                    db.Logs.Add(new LogItem { Message = log.Message, Type = log.Type, LogTime = log.LogTime });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error inserting exception into logs: " + ex.Message);
                Console.ResetColor();
            }
        }

        public void LogCheckHouseKeeping()
        {
            try
            {
                using (var db = new DataLayer(dbOptions))
                {
                    var oldLogs = db.Logs.Where(l => l.LogTime < DateTime.Now.AddMonths(-3));
                    db.Logs.RemoveRange(oldLogs);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error cleaning up logs: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}
