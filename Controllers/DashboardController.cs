using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_GVEncheva22.Services;
using project_GVEncheva22.Models;

namespace project_GVEncheva22.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITaskService _taskService;

        public DashboardController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            // Load task statistics and prepare values for the dashboard view.
            var tasks = await _taskService.GetAllTasksAsync();
            var totalTasks = tasks.Count();
            var completedTasks = tasks.Count(t => t.Status == Status.Done);
            var pendingTasks = tasks.Count(t => t.Status != Status.Done);
            var completionPercentage = totalTasks > 0 ? (double)completedTasks / totalTasks * 100 : 0;
            var overdueTasks = tasks.Count(t => t.Deadline.HasValue && t.Deadline.Value < DateTime.Now && t.Status != Status.Done);
            var upcomingDeadlines = tasks.Where(t => t.Deadline.HasValue && t.Deadline.Value >= DateTime.Now && t.Deadline.Value <= DateTime.Now.AddDays(7) && t.Status != Status.Done)
                                         .OrderBy(t => t.Deadline)
                                         .Take(5)
                                         .ToList();

            ViewBag.TotalTasks = totalTasks;
            ViewBag.CompletedTasks = completedTasks;
            ViewBag.PendingTasks = pendingTasks;
            ViewBag.CompletionPercentage = completionPercentage;
            ViewBag.OverdueTasks = overdueTasks;
            ViewBag.UpcomingDeadlines = upcomingDeadlines;

            return View();
        }
    }
}