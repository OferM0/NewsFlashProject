namespace server.MicroService.Models
{
    public class ReturnUserRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string[] Interests { get; set; }
    }
}
