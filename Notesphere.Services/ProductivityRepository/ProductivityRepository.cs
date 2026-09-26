using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notesphere.Entities.ProductivityModels;
using Notesphere.Services.NotesphereDataAccessLayer;

namespace Notesphere.Services.ProductivityRepository
{
    //Handles data access for productivity tasks and checklist items
    public class ProductivityRepository : IProductivityServices
    {
        private readonly NotesphereDbContext _dbContext;

        public ProductivityRepository(NotesphereDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductivityTask>> GetTasksForUserAsync(int studentUserId)
        {

            // Fetch all tasks for a user with checklist items included

            return await _dbContext.ProductivityTasks
                .Where(t => t.StudentUserId == studentUserId)
                .Include(t => t.ChecklistItems)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<ProductivityTask?> GetTaskByIdAsync(int id)
        {
            // Find a task and load its checklist items

            return await _dbContext.ProductivityTasks
                .Include(t => t.ChecklistItems)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<ProductivityTask> AddTaskAsync(ProductivityTask task)
        {
            // Create a new task

            _dbContext.ProductivityTasks.Add(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task UpdateTaskAsync(ProductivityTask task)
        {
            // Update an existing task

            _dbContext.ProductivityTasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            // Soft check before deleting a task

            var existing = await _dbContext.ProductivityTasks.FindAsync(id);
            if (existing != null)
            {
                _dbContext.ProductivityTasks.Remove(existing);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TaskChecklistItem>> GetChecklistForTaskAsync(int taskId)
        {
            // Return checklist items sorted by order

            return await _dbContext.TaskChecklistItems
                .Where(c => c.ProductivityTaskId == taskId)
                .OrderBy(c => c.Order)
                .ToListAsync();
        }

        public async Task<TaskChecklistItem> AddChecklistItemAsync(TaskChecklistItem item)
        {
            // Add checklist row and update task progress

            _dbContext.TaskChecklistItems.Add(item);
            await _dbContext.SaveChangesAsync();
            await RecalculateProgressAsync(item.ProductivityTaskId);
            return item;
        }

        public async Task ToggleChecklistItemAsync(int itemId, bool isDone)
        {
            // Marks checklist item as done/undone
            var item = await _dbContext.TaskChecklistItems.FindAsync(itemId);
            if (item == null) return;

            item.IsDone = isDone;
            await _dbContext.SaveChangesAsync();
            await RecalculateProgressAsync(item.ProductivityTaskId);
        }

        public async Task DeleteChecklistItemAsync(int itemId)
        {
            // Delete checklist item

            var item = await _dbContext.TaskChecklistItems.FindAsync(itemId);
            if (item == null) return;

            // Recalculates progress

            var taskId = item.ProductivityTaskId;
            _dbContext.TaskChecklistItems.Remove(item);
            await _dbContext.SaveChangesAsync();
            await RecalculateProgressAsync(taskId);
        }

        public async Task RecalculateProgressAsync(int taskId)
        {
            // Recalculate progress based on checklist state

            var task = await _dbContext.ProductivityTasks
                .Include(t => t.ChecklistItems)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null) return;

            if (!task.ChecklistItems.Any())
            {
                task.ProgressPercent = 0;
            }
            else
            {
                var total = task.ChecklistItems.Count;
                var done = task.ChecklistItems.Count(c => c.IsDone);
                task.ProgressPercent = (int)((done / (double)total) * 100);
            }

            _dbContext.ProductivityTasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}
