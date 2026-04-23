using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace project_GVEncheva22.Models
{
    // Represents a single task in the system
    // Inherits common properties (Id, CreatedAt) from BaseEntity
    public class TaskItem : BaseEntity
    {
        // Title of the task (required, max 200 characters)
        [Required]
        [StringLength(200)]
        public required string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        // Priority level of the task (Low, Medium, High)
        [Required]
        public Priority Priority { get; set; }

        // Current status of the task (e.g. To Do, In Progress, Done)
        [Required]
        public Status Status { get; set; }

        // Optional deadline for the task
        [DataType(DataType.DateTime)]
        public DateTime? Deadline { get; set; }

        // Foreign key linking the task to a specific board
        [Required]
        public int BoardId { get; set; }

        // Navigation property to access the related Board entity
        [ValidateNever]
        public required Board Board { get; set; }
    }
}
