using BookMarketApi.Model;

namespace BookMarketApi.DataAccess.Contracts;

public interface ICartServiceRepository
{
    Task<Cart> GetCartAsync(Guid userId);
    Task<CartItem> AddToCartAsync(Guid userId, Guid bookId, int quantity);
    Task<bool> RemoveFromCartAsync(Guid userId, Guid bookId);
    Task<bool> UpdateQuantityAsync(Guid userId, Guid bookId, int quantity);
    Task<bool> ClearCartAsync(Guid userId);
    Task<Order> CheckoutAsync(Guid userId);
}
