using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities
{
    public class BaseEntity : BaseAppSystem
    {
        public BaseEntity(Logger log) : base(log) { }
    }
}
