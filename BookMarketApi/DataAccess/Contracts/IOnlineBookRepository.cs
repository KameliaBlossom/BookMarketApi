using BookMarketApi.Model;

namespace BookMarketApi.DataAccess.Contracts;

public interface IOnlineBookRepository
{
    Task<OnlineBook> AddAsync(OnlineBook book);
    Task<OnlineBook?> GetByIdAsync(Guid bookId);
    Task<IEnumerable<OnlineBook>> GetAllAsync();
    Task UpdateAsync(OnlineBook book);
    Task DeleteByIdAsync(Guid bookId);
}