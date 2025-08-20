using BookMarketApi.Common.Entities.Domain.BookEntities;

namespace BookMarketApi.DAL.Contracts.MarketBookContracts;

public interface IMarketBookRepository
{
    Task AddAsync(MarketBook book);
    Task<MarketBook?> GetByIdAsync(Guid bookId);
    Task<IEnumerable<MarketBook>> GetAllAsync();
    Task UpdateAsync(MarketBook book);
    Task DeleteByIdAsync(Guid bookId);
}