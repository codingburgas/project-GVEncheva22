using Microsoft.AspNetCore.Identity;

namespace project_GVEncheva22.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom properties here if needed (e.g. DisplayName, FullName, etc.)
        public string? DisplayName { get; set; }
    }
}
