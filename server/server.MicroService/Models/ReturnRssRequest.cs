namespace server.MicroService.Models
{
    public class ReturnRssRequest
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public int WebSiteId { get; set; }
    }
}
