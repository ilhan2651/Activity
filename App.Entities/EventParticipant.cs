namespace App.Entities
{
    public class EventParticipant :IEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public Event Event { get; set; }
        public AppUser User { get; set; }
    }
}
