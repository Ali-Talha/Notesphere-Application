using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Notesphere.Entities.ProductivityModels;
using TaskPriorityEnum = Notesphere.Entities.ProductivityModels.TaskPriority;
using TaskStatusEnum = Notesphere.Entities.ProductivityModels.TaskStatus;


namespace Notesphere.Operations.Models.Productivity
{
    // Row in the Productivity list page
    public class ProductivityTaskListItemVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public TaskPriorityEnum Priority { get; set; }
        public TaskStatusEnum Status { get; set; }
        public DateTime? DueDate { get; set; }
        public int ProgressPercent { get; set; }
    }


    // Form model for creating or editing a task
    public class ProductivityTaskFormVM
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Display(Name = "Due date")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Priority")]
        public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;

        [Display(Name = "Status")]
        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.NotStarted;

        // Checklist items edited on the Edit page
        public List<TaskChecklistItemVM> ChecklistItems { get; set; } = new();
    }


    // Checklist item inside a task
    public class TaskChecklistItemVM
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
