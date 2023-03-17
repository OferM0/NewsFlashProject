using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using server.Model;

namespace Utilities
{
    public interface ILogger
    {
        Queue<LogItem> eventsQueue { get; set; }
        Queue<LogItem> errorsQueue { get; set; }
        void Init();
        void LogMessage(LogItem log);
        void LogException(LogItem log);
        void LogCheckHouseKeeping();
    }
    public class Logger
    {
        private static ILogger logger;

        public Logger(string provider)
        {
            switch (provider)
            {
                case "File":
                    logger = new LogFile();
                    break;

                case "Database":
                    logger = new LogDB();
                    break;

                case "Console":
                    logger = new LogConsole();
                    break;

                default:
                    logger = new LogNone();
                    break;
            }

            logger.Init();
        }

        public void LogEvent(LogItem log)
        {
            logger.eventsQueue.Enqueue(log);
        }
        public void LogError(LogItem log)
        {
            logger.errorsQueue.Enqueue(log);
        }

    }
}
