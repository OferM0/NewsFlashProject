using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;

namespace Utilities
{
    public class LogNone : ILogger
    {
        public Queue<LogItem> eventsQueue { get; set; }
        public Queue<LogItem> errorsQueue { get; set; }

        public void Init()
        {
        }

        public void LogMessage(LogItem log)
        {
        }

        public void LogException(LogItem log)
        {
        }

        public void LogCheckHouseKeeping()
        {

        }
    }
}
