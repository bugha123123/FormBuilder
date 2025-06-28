using Microsoft.AspNetCore.Identity;

namespace FormBuilder.Models
{
    public class User : IdentityUser
    {
        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }
}
