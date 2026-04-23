using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace project_GVEncheva22.Models
{
    public class Board : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public required string Title { get; set; }

        [Required]
        public required string UserId { get; set; }

        [ValidateNever]
        public required ApplicationUser User { get; set; }

        // Navigation property for related TaskItems
        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
