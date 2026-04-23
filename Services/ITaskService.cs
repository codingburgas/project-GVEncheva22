using project_GVEncheva22.Models;

namespace project_GVEncheva22.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int taskId);
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem?> UpdateTaskAsync(TaskItem task);
        Task<TaskItem?> MarkTaskAsDoneAsync(int taskId);
        Task<bool> DeleteTaskAsync(int taskId);

        Task<IEnumerable<TaskItem>> GetTasksByBoardAsync(int boardId);
        Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(Status status);
        Task<IEnumerable<TaskItem>> GetTasksByPriorityAsync(Priority priority);

        Task<Dictionary<Status, IEnumerable<TaskItem>>> GetTasksGroupedByStatusFilteredByPriorityAsync(Priority priority);
    }
}
