using BookMarketApi.Common.Entities.Domain.BookEntities;
using BookMarketApi.Common.Entities.Domain.CartEntities;
using BookMarketApi.Common.Entities.Domain.OrderEntities;

namespace BookMarketApi.DAL.Contracts.CartContracts;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(Guid userId);
    Task AddCartAsync(Cart cart);
    Task SaveChangesAsync();
    Task<Book?> GetBookByIdAsync(Guid bookId);
    Task<Order> AddOrderAsync(Order order);
}