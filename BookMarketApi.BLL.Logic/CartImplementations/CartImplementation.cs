using BookMarketApi.BLL.Contracts.CartContracts;
using BookMarketApi.Common.Entities.Domain.CartEntities;
using BookMarketApi.Common.Entities.Domain.OrderEntities;
using BookMarketApi.DAL.Contracts.CartContracts;
using BookMarketApi.Extension;

namespace BookMarketApi.BLL.Logic.CartImplementations;

public class CartImplementation : ICartContract
{
    private readonly ICartRepository _repository;
    public CartImplementation(ICartRepository repository) => _repository = repository;

    public async Task<Cart> GetCartAsync(Guid userId)
    {
        var cart = await _repository.GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _repository.AddCartAsync(cart);
            cart = await _repository.GetCartByUserIdAsync(userId);
        }
        return cart;
    }

    public async Task<Cart> GetOrCreateCartAsync(Guid userId)
    {
        var cart = await _repository.GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _repository.AddCartAsync(cart);
        }
        return cart;
    }

    public async Task<CartItem> AddToCartAsync(Guid userId, Guid bookId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть больше нуля");

        var cart = await GetOrCreateCartAsync(userId);
        var book = await _repository.GetBookByIdAsync(bookId);

        if (book == null)
            throw new ArgumentException("Книга не найдена");

        if (book.Price == null)
            throw new ArgumentException("У книги не указана цена");

        var existingItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            existingItem.Price = book.Price.Value * existingItem.Quantity;
        }
        else
        {
            var item = new CartItem
            {
                BookId = bookId,
                Book = book,
                CartId = cart.Id,
                Quantity = quantity,
                Price = book.Price.Value * quantity
            };
            cart.Items.Add(item);
            existingItem = item;
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return existingItem;
    }

    public async Task<bool> RemoveFromCartAsync(Guid userId, Guid bookId)
    {
        var cart = await GetOrCreateCartAsync(userId);
        var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);

        if (item == null)
            return false;

        cart.Items.Remove(item);
        cart.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateQuantityAsync(Guid userId, Guid bookId, int quantity)
    {
        if (quantity <= 0)
            return await RemoveFromCartAsync(userId, bookId);

        var cart = await GetOrCreateCartAsync(userId);
        var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);

        if (item == null)
            return false;

        item.Quantity = quantity;
        item.Price = (item.Book.Price ?? 0m) * quantity;
        cart.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearCartAsync(Guid userId)
    {
        var cart = await GetOrCreateCartAsync(userId);
        if (!cart.Items.Any())
            return false;
        cart.Items.Clear();
        cart.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<Order> CheckoutAsync(Guid userId)
    {
        var cart = await GetOrCreateCartAsync(userId);

        if (cart.Items == null || !cart.Items.Any())
            throw new InvalidOperationException("Корзина пуста");

        var order = new Order
        {
            UserId = userId,
            Status = OrderStatus.Created,
            TotalAmount = cart.Items.Sum(i => i.Price),
            OrderItems = cart.Items.Select(i => new OrderItem
            {
                BookId = i.BookId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };

        await _repository.AddOrderAsync(order);
        cart.Items.Clear();
        cart.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();

        return order;
    }
}
