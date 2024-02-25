using Microsoft.AspNetCore.Identity;

namespace DaLatBooking.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
