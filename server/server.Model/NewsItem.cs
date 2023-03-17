using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class NewsItem : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public int WebSiteId { get; set; }
        public virtual WebSite WebSite { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int ClickCount { get; set; }
    }
}