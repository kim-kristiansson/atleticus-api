using Microsoft.AspNetCore.Http.HttpResults;

namespace AtleticusAPI.Models
{
    public class ActivityGroup
    {
        public string? Name { get; set; }
        public List<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
        public ApplicationUser Creator { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ActivityGroup(ApplicationUser creator)
        {
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            Members.Add(creator); 
        }
    }
}
