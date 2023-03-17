using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities
{
    public class BaseAppSystem
    {
        public Logger _log;
        public BaseAppSystem(Logger log) { _log = log; }
    }
}
