using Microsoft.EntityFrameworkCore;
using project_GVEncheva22.Data;
using project_GVEncheva22.Models;

namespace project_GVEncheva22.Services
{
    // Service responsible for handling all business logic related to Boards
    // Uses dependency injection to access the database context
    public class BoardService : IBoardService
    {
        private readonly AppDbContext _dbContext;

        // Constructor injection of AppDbContext
        public BoardService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Returns all boards including their related tasks
        public async Task<IEnumerable<Board>> GetAllBoardsAsync()
        {
            return await _dbContext.Boards.Include(b => b.TaskItems).ToListAsync();
        }

        // Returns a single board by Id, including its tasks
        public async Task<Board?> GetBoardByIdAsync(int boardId)
        {
            return await _dbContext.Boards.Include(b => b.TaskItems)
                .FirstOrDefaultAsync(b => b.Id == boardId);
        }
        // Creates a new board and saves it to the database
        public async Task<Board> CreateBoardAsync(Board board)
        {
            // Set creation date
            board.CreatedAt = DateTime.UtcNow;
            _dbContext.Boards.Add(board);
            await _dbContext.SaveChangesAsync();
            return board;
        }

        // Updates an existing board
        public async Task<Board?> UpdateBoardAsync(Board board)
        {
            var existing = await _dbContext.Boards.FindAsync(board.Id);
            if (existing == null)
                return null;

            // Update only necessary fields
            existing.Title = board.Title;
            existing.UserId = board.UserId;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        // Deletes a board by Id
        public async Task<bool> DeleteBoardAsync(int boardId)
        {
            var existing = await _dbContext.Boards.FindAsync(boardId);
            if (existing == null)
                return false;

            _dbContext.Boards.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Returns all boards belonging to a specific user
        public async Task<IEnumerable<Board>> GetBoardsByUserAsync(string userId)
        {
            return await _dbContext.Boards
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}