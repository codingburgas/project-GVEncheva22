using Microsoft.EntityFrameworkCore;
using project_GVEncheva22.Data;
using project_GVEncheva22.Models;

namespace project_GVEncheva22.Services
{
    // Service responsible for handling all business logic related to Tasks
    // Communicates with the database through AppDbContext
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _dbContext;

    // Constructor injection of the database context
        public TaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _dbContext.TaskItems.Include(t => t.Board).ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int taskId)
        {
            return await _dbContext.TaskItems.Include(t => t.Board)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }
        // Creates a new task and saves it to the database
        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.UtcNow;
            _dbContext.TaskItems.Add(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        // Updates an existing task
        public async Task<TaskItem?> UpdateTaskAsync(TaskItem task)
        {
            var existing = await _dbContext.TaskItems.FindAsync(task.Id);
            if (existing == null)
                return null;

            // Update task properties
            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Priority = task.Priority;
            existing.Status = task.Status;
            existing.Deadline = task.Deadline;
            existing.BoardId = task.BoardId;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        // Deletes a task by Id
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var existing = await _dbContext.TaskItems.FindAsync(taskId);
            if (existing == null)
                return false;

            _dbContext.TaskItems.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByBoardAsync(int boardId)
        {
            return await _dbContext.TaskItems
                .Where(t => t.BoardId == boardId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(Status status)
        {
            return await _dbContext.TaskItems
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByPriorityAsync(Priority priority)
        {
            return await _dbContext.TaskItems
                .Where(t => t.Priority == priority)
                .ToListAsync();
        }

        public async Task<Dictionary<Status, IEnumerable<TaskItem>>> GetTasksGroupedByStatusFilteredByPriorityAsync(Priority priority)
        {
            var tasks = await _dbContext.TaskItems
                .Include(t => t.Board)
                .Where(t => t.Priority == priority)
                .ToListAsync();

            return tasks
                .GroupBy(t => t.Status)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
        }
    }
}
