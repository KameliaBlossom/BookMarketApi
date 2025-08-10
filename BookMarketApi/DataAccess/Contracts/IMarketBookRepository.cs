using BookMarketApi.Model;

namespace BookMarketApi.DataAccess.Contracts;

public interface IMarketBookRepository
{
    Task<MarketBook> AddAsync(MarketBook book);
    Task<MarketBook?> GetByIdAsync(Guid bookId);
    Task<IEnumerable<MarketBook>> GetAllAsync();
    Task UpdateAsync(MarketBook book);
    Task DeleteByIdAsync(Guid bookId);
}