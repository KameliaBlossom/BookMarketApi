using BookMarketApi.Data;
using BookMarketApi.DataAccess.Contracts;
using BookMarketApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BookMarketApi.DataAccess.Repositories;

public class OnlineBookRepository : IOnlineBookRepository
{
    private readonly ApplicationDbContext _context;
    
    public OnlineBookRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<OnlineBook> AddAsync(OnlineBook book)
    {
        _context.OnlineBooks.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }
    
    public async Task<OnlineBook?> GetByIdAsync(Guid bookId)
    {
        return await _context.OnlineBooks.FindAsync(bookId);
    }

    public async Task<IEnumerable<OnlineBook>> GetAllAsync()
    {
        return await _context.OnlineBooks.ToListAsync();
    }

    public async Task UpdateAsync(OnlineBook book)
    {
        _context.OnlineBooks.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid bookId)
    {
        var deletedBook = await _context.OnlineBooks.FindAsync(bookId);

        if (deletedBook != null)
        {
            _context.OnlineBooks.Remove(deletedBook);
            await _context.SaveChangesAsync();
        }
    }
    
    
}