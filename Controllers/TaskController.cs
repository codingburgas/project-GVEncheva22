using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_GVEncheva22.Models;
using project_GVEncheva22.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace project_GVEncheva22.Controllers;

[Authorize]
public class TaskController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IBoardService _boardService;

    public TaskController(ITaskService taskService, IBoardService boardService)
    {
        _taskService = taskService;
        _boardService = boardService;
    }

    // Show the list of all tasks.
    public async Task<IActionResult> Index()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return View(tasks);
    }

    // Display details for a specific task.
    public async Task<IActionResult> Details(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    // Show the form for creating a new task.
    public async Task<IActionResult> Create()
    {
        ViewBag.BoardsSelectList = new SelectList(await _boardService.GetAllBoardsAsync(), "Id", "Title");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "AdminOnly")]
    // Create a new task when the form is submitted.
    public async Task<IActionResult> Create(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            await _taskService.CreateTaskAsync(task);
            return RedirectToAction("Index");
        }
        ViewBag.BoardsSelectList = new SelectList(await _boardService.GetAllBoardsAsync(), "Id", "Title");
        return View(task);
    }

    // Show the form to edit an existing task.
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        ViewBag.BoardsSelectList = new SelectList(await _boardService.GetAllBoardsAsync(), "Id", "Title");
        return View(task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "AdminOnly")]
    // Save changes to an existing task.
    public async Task<IActionResult> Edit(int id, TaskItem task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }
        if (ModelState.IsValid)
        {
            await _taskService.UpdateTaskAsync(task);
            return RedirectToAction("Index");
        }
        ViewBag.BoardsSelectList = new SelectList(await _boardService.GetAllBoardsAsync(), "Id", "Title");
        return View(task);
    }

    // Confirm deletion of a task.
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "AdminOnly")]
    // Delete the task after confirmation.
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _taskService.DeleteTaskAsync(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    // Show tasks grouped by status and filtered by priority.
    public async Task<IActionResult> GroupedByStatus(Priority priority = Priority.Medium)
    {
        var groupedTasks = await _taskService.GetTasksGroupedByStatusFilteredByPriorityAsync(priority);
        ViewBag.SelectedPriority = priority;
        return View(groupedTasks);
    }
}