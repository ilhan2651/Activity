namespace App.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }  
        public int EventId { get; set; }
        public string? EventImage { get; set; }
        public AppUser User { get; set; }
        public Event Event { get; set; }
    }
}
