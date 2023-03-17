using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class WebSite
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Rss> RssLinks { get; set; }
        public virtual List<NewsItem> NewsItems { get; set; }
    }
}
