using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_GVEncheva22.Models;
using project_GVEncheva22.Services;
using System.Security.Claims;

namespace project_GVEncheva22.Controllers;

[Authorize]
public class BoardController : Controller
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    // List all boards for the current user.
    public async Task<IActionResult> Index()
    {
        var boards = await _boardService.GetAllBoardsAsync();
        return View(boards);
    }

    // Show details of a single board by id.
    public async Task<IActionResult> Details(int id)
    {
        var board = await _boardService.GetBoardByIdAsync(id);
        if (board == null)
        {
            return NotFound();
        }
        return View(board);
    }

    // Display the form to create a new board.
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Board board)
    {
        // Save a new board with the signed-in user's ID.
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }
        board.UserId = userId;
        ModelState.Remove(nameof(Board.UserId));
        ModelState.Remove(nameof(Board.User));
        if (ModelState.IsValid)
        {
            await _boardService.CreateBoardAsync(board);
            return RedirectToAction("Index");
        }
        return View(board);
    }

    // Show the edit form for an existing board.
    public async Task<IActionResult> Edit(int id)
    {
        var board = await _boardService.GetBoardByIdAsync(id);
        if (board == null)
        {
            return NotFound();
        }
        return View(board);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Board board)
    {
        // Update selected board if IDs match and input is valid.
        if (id != board.Id)
        {
            return BadRequest();
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }
        board.UserId = userId;
        ModelState.Remove(nameof(Board.UserId));
        ModelState.Remove(nameof(Board.User));
        if (ModelState.IsValid)
        {
            await _boardService.UpdateBoardAsync(board);
            return RedirectToAction("Index");
        }
        return View(board);
    }

    // Confirm deletion of a board.
    public async Task<IActionResult> Delete(int id)
    {
        var board = await _boardService.GetBoardByIdAsync(id);
        if (board == null)
        {
            return NotFound();
        }
        return View(board);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _boardService.DeleteBoardAsync(id);
        return RedirectToAction("Index");
    }
}
