using BookMarketApi.Common.Entities.Domain.CartEntities;
using BookMarketApi.Common.Entities.Domain.OrderEntities;

namespace BookMarketApi.BLL.Contracts.CartContracts;

public interface ICartContract
{
    Task<Cart> GetCartAsync(Guid userId);
    Task<CartItem> AddToCartAsync(Guid userId, Guid bookId, int quantity);
    Task<bool> RemoveFromCartAsync(Guid userId, Guid bookId);
    Task<bool> UpdateQuantityAsync(Guid userId, Guid bookId, int quantity);
    Task<bool> ClearCartAsync(Guid userId);
    Task<Order> CheckoutAsync(Guid userId);
}