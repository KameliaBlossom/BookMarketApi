using BookMarketApi.Data;
using BookMarketApi.DataAccess.Contracts;
using BookMarketApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BookMarketApi.DataAccess.Repositories;

public class MarketBookRepository : IMarketBookRepository
{
    public readonly ApplicationDbContext _context;

    public MarketBookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MarketBook book)
    {
        _context.MarketBooks.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task<MarketBook?> GetByIdAsync(Guid bookId)
    {
        return await _context.MarketBooks.FindAsync(bookId);
    }

    public async Task<IEnumerable<MarketBook>> GetAllAsync()
    {
        return await _context.MarketBooks.ToListAsync();
    }

    public async Task UpdateAsync(MarketBook book)
    {
        _context.MarketBooks.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid bookdId)
    {
        var deletedBook = await _context.MarketBooks.FindAsync(bookdId);
        if (deletedBook != null)
        {
            _context.MarketBooks.Remove(deletedBook);
            await _context.SaveChangesAsync();
        }
    
    }
}