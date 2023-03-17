using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Topic { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Rss> Rss { get; set; }
    }
}