using BookMarketApi.Common.Entities.Domain.BookEntities;
using BookMarketApi.Common.Entities.Domain.CartEntities;
using BookMarketApi.Common.Entities.Domain.OrderEntities;
using BookMarketApi.Common.Entities.OutputModels.CartOutputModels;

namespace BookMarketApi.DAL.Contracts.CartContracts;

public interface ICartRepository
{
    Task<CartOutputModel?> GetCartByUserIdAsync(Guid userId);
    Task AddCartAsync(Cart cart);
    Task SaveChangesAsync();
    Task<Book?> GetBookByIdAsync(Guid bookId);
    Task<Order> AddOrderAsync(Order order);
    Task<Cart?> GetCartEntityByUserIdAsync(Guid userId);

}