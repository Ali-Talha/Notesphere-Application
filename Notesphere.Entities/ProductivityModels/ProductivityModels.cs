using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Notesphere.Entities.NotesModels; // for StudentUser

namespace Notesphere.Entities.ProductivityModels
{
    // Priority levels for a task
    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    // Status values for a task
    public enum TaskStatus
    {
        NotStarted = 1,
        InProgress = 2,
        Blocked = 3,
        Completed = 4
    }

    // Main entity for productivity tasks created by users
    public class ProductivityTask
    {
        [Key]
        public int Id { get; set; }

        // Owner of the task
        public int StudentUserId { get; set; }
        public StudentUser StudentUser { get; set; } = null!;

        [Required, MaxLength(150)]
        
        // Title of task
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        // Priority level
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        // Current status
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;

        // 0 progress value based on checklist items
        public int ProgressPercent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // List of checklist items that are linked to this task
        public ICollection<TaskChecklistItem> ChecklistItems { get; set; } =
            new List<TaskChecklistItem>();
    }


    // Individual checklist items inside a task
    public class TaskChecklistItem
    {
        [Key]

        // Primary Key
        public int Id { get; set; }

        // FK to ProductivityTask
        public int ProductivityTaskId { get; set; }
        public ProductivityTask ProductivityTask { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Label { get; set; } = string.Empty;

        public bool IsDone { get; set; }

        public int Order { get; set; }
    }
}
