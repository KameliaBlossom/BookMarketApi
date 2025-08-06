using BookMarketApi.Common.Entities.Domain.BookEntities;

namespace BookMarketApi.DAL.Contracts.OnlineBookContracts;

public interface IOnlineBookRepository
{
    Task<OnlineBook> AddAsync(OnlineBook book);
    Task<OnlineBook?> GetByIdAsync(Guid bookId);
    Task<IEnumerable<OnlineBook>> GetAllAsync();
    Task UpdateAsync(OnlineBook book);
    Task DeleteByIdAsync(Guid bookId);
}