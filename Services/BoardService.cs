using Microsoft.EntityFrameworkCore;
using project_GVEncheva22.Data;
using project_GVEncheva22.Models;

namespace project_GVEncheva22.Services
{
    public class BoardService : IBoardService
    {
        private readonly AppDbContext _dbContext;

        public BoardService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Board>> GetAllBoardsAsync()
        {
            return await _dbContext.Boards.Include(b => b.TaskItems).ToListAsync();
        }

        public async Task<Board?> GetBoardByIdAsync(int boardId)
        {
            return await _dbContext.Boards.Include(b => b.TaskItems)
                .FirstOrDefaultAsync(b => b.Id == boardId);
        }

        public async Task<Board> CreateBoardAsync(Board board)
        {
            board.CreatedAt = DateTime.UtcNow;
            _dbContext.Boards.Add(board);
            await _dbContext.SaveChangesAsync();
            return board;
        }

        public async Task<Board?> UpdateBoardAsync(Board board)
        {
            var existing = await _dbContext.Boards.FindAsync(board.Id);
            if (existing == null)
                return null;

            existing.Title = board.Title;
            existing.UserId = board.UserId;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteBoardAsync(int boardId)
        {
            var existing = await _dbContext.Boards.FindAsync(boardId);
            if (existing == null)
                return false;

            _dbContext.Boards.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Board>> GetBoardsByUserAsync(string userId)
        {
            return await _dbContext.Boards
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}