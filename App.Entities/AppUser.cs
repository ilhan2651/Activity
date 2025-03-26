using Microsoft.AspNetCore.Identity;

namespace App.Entities
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        public string UserFullName { get; set; } = default!;
        public string UserProfilePictureUrl { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public ICollection<Event> Events { get; set; } 
        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
