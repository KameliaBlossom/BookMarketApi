using BookMarketApi.Common.Entities.Domain.CartEntities;
using BookMarketApi.Common.Entities.Domain.OrderEntities;
using BookMarketApi.Common.Entities.OutputModels.CartOutputModels;

namespace BookMarketApi.BLL.Contracts.CartContracts;

public interface ICartContract
{
    Task<CartOutputModel?> GetCartAsync(Guid userId);
    Task<CartItem> AddToCartAsync(Guid userId, Guid bookId, int quantity);
    Task<bool> RemoveFromCartAsync(Guid userId, Guid bookId);
    Task<bool> UpdateQuantityAsync(Guid userId, Guid bookId, int quantity);
    Task<bool> ClearCartAsync(Guid userId);
    Task<Order> CheckoutAsync(Guid userId);
}