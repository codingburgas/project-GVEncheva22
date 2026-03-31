using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace project_GVEncheva22.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? DisplayName { get; set; }

        // Navigation property for related Boards
        public ICollection<Board> Boards { get; set; } = new List<Board>();
    }
}
