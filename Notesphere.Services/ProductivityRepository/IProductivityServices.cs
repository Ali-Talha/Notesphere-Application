using System.Collections.Generic;
using System.Threading.Tasks;
using Notesphere.Entities.ProductivityModels;

namespace Notesphere.Services.ProductivityRepository
{
    public interface IProductivityServices
    {
        // Task operations


        // Get all tasks for a user
        Task<List<ProductivityTask>> GetTasksForUserAsync(int studentUserId);

        // Find task by Id
        Task<ProductivityTask?> GetTaskByIdAsync(int id);

        // Create a new task
        Task<ProductivityTask> AddTaskAsync(ProductivityTask task);

        // Update an existing task
        Task UpdateTaskAsync(ProductivityTask task);

        // Delete a task
        Task DeleteTaskAsync(int id);

        // Checklist operations

        // Load checklist items for task
        Task<List<TaskChecklistItem>> GetChecklistForTaskAsync(int taskId);

        // Add checklist item
        Task<TaskChecklistItem> AddChecklistItemAsync(TaskChecklistItem item);

        // Toggle completion state of checklist item
        Task ToggleChecklistItemAsync(int itemId, bool isDone);

        // Delete a checklist item
        Task DeleteChecklistItemAsync(int itemId);

        // Helpers to recalculate progress based on checklist
        Task RecalculateProgressAsync(int taskId);
    }
}
