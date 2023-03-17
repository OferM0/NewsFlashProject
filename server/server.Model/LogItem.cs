using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class LogItem
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
    }
}
