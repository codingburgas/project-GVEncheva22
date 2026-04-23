using project_GVEncheva22.Models;

namespace project_GVEncheva22.Services
{
    // Interface defining operations for managing Boards
    public interface IBoardService
    {
        Task<IEnumerable<Board>> GetAllBoardsAsync();
        Task<Board?> GetBoardByIdAsync(int boardId);
        Task<Board> CreateBoardAsync(Board board);
        Task<Board?> UpdateBoardAsync(Board board);
        Task<bool> DeleteBoardAsync(int boardId);

        Task<IEnumerable<Board>> GetBoardsByUserAsync(string userId);
    }
}