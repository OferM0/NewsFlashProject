using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class Rss : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int WebSiteId { get; set; }
        public virtual WebSite WebSite { get; set; }
    }
}