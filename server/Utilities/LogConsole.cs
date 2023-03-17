using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using server.Model;

namespace Utilities
{
    public class LogConsole : ILogger
    {
        public Queue<LogItem> eventsQueue { get; set; }
        public Queue<LogItem> errorsQueue { get; set; }
        Task task;
        private bool stop = false;

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
        }

        public void LogMessage(LogItem log)
        {
            Console.WriteLine($"{log.LogTime} : {log.Type} - {log.Message}");
        }

        public void LogException(LogItem log)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{log.LogTime} : {log.Type} - {log.Message}");
            Console.ResetColor();
        }

        public void LogCheckHouseKeeping()
        {

        }
    }
}
