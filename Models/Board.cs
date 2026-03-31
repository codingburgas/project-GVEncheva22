using System.ComponentModel.DataAnnotations;

namespace project_GVEncheva22.Models
{
    public class Board : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        // Navigation property for related TaskItems
        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}