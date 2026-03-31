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

    public async Task<IActionResult> Index()
    {
        var boards = await _boardService.GetAllBoardsAsync();
        return View(boards);
    }

    public async Task<IActionResult> Details(int id)
    {
        var board = await _boardService.GetBoardByIdAsync(id);
        if (board == null)
        {
            return NotFound();
        }
        return View(board);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create(Board board)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }
        board.UserId = userId;
        if (ModelState.IsValid)
        {
            await _boardService.CreateBoardAsync(board);
            return RedirectToAction("Index");
        }
        return View(board);
    }

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
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Edit(int id, Board board)
    {
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
        if (ModelState.IsValid)
        {
            await _boardService.UpdateBoardAsync(board);
            return RedirectToAction("Index");
        }
        return View(board);
    }

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
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _boardService.DeleteBoardAsync(id);
        return RedirectToAction("Index");
    }
}