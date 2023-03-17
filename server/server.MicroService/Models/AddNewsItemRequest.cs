namespace server.MicroService.Models
{
    public class AddNewsItemRequest
    {
        public string ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public int WebSiteId { get; set; }
        public int CategoryId { get; set; }
        public int ClickCount { get; set; } = 0;
    }
}
