using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project_GVEncheva22.Models;
using project_GVEncheva22.Services;

namespace project_GVEncheva22.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITaskService _taskService;

    public HomeController(ILogger<HomeController> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    // Build summary statistics for the home page from all tasks.
    public async Task<IActionResult> Index()
    {
        var allTasks = await _taskService.GetAllTasksAsync();
        var totalTasks = allTasks.Count();
        var completedTasks = allTasks.Count(t => t.Status == Status.Done);
        var completionPercentage = totalTasks > 0 ? (double)completedTasks / totalTasks * 100 : 0;

        var now = DateTime.Now;
        var overdueTasks = allTasks.Count(t => t.Deadline.HasValue && t.Deadline < now && t.Status != Status.Done);
        var upcomingDeadlines = allTasks.Where(t => t.Deadline.HasValue && t.Deadline >= now && t.Status != Status.Done)
                                       .OrderBy(t => t.Deadline)
                                       .Take(5)
                                       .ToList();

        ViewBag.TotalTasks = totalTasks;
        ViewBag.CompletedTasks = completedTasks;
        ViewBag.CompletionPercentage = completionPercentage;
        ViewBag.OverdueTasks = overdueTasks;
        ViewBag.UpcomingDeadlines = upcomingDeadlines;

        return View();
    }

    // Show the privacy policy page.
    public async Task<IActionResult> Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // Render an error view with the current request ID.
    public async Task<IActionResult> Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
