using System.ComponentModel.DataAnnotations;

namespace project_GVEncheva22.Models
{
    public class TaskItem : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime? Deadline { get; set; }

        // Relations
        [Required]
        public int BoardId { get; set; }

        public Board Board { get; set; }
    }
}