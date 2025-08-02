using BookMarketApi.Data;
using BookMarketApi.DataAccess.Contracts;
using BookMarketApi.Extension;
using BookMarketApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BookMarketApi.DataAccess.Repositories;

public class CartServiceRepository : ICartServiceRepository
{
    private readonly ApplicationDbContext _context;

    public CartServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetCartAsync(Guid userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Book)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<CartItem> AddToCartAsync(Guid userId, Guid bookId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Количество должно быть больше нуля");
        }
        
        var cart = await GetCartAsync(userId);
        var book = await _context.Books.FindAsync(bookId);
        
        if (book == null)
            throw new ArgumentException("Книга не найдена");
        
        if (book.Price == null)
            throw new ArgumentException("У книги не указана цена");
        
        var bookPrice = book.Price.Value;
        
        var existingItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);
        
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            existingItem.Price = bookPrice * existingItem.Quantity;

        }
        else
        {
            existingItem = new CartItem
            {
                BookId = bookId,
                Book = book,
                CartId = cart.Id,
                Quantity = quantity,
                Price = bookPrice * quantity
            };
            cart.Items.Add(existingItem);
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return existingItem;
    }

    public async Task<bool> RemoveFromCartAsync(Guid userId, Guid bookId)
    {
        var cart = await GetCartAsync(userId);
        var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
        
        if (item == null)
            return false;

        cart.Items.Remove(item);
        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateQuantityAsync(Guid userId, Guid bookId, int quantity)
    {
        if (quantity <= 0)
            return await RemoveFromCartAsync(userId, bookId);

        var cart = await GetCartAsync(userId);
        var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
        
        if (item == null)
            return false;

        item.Quantity = quantity;
        item.Price = (item.Book.Price ?? 0m) * quantity;
        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearCartAsync(Guid userId)
    {
        var cart = await GetCartAsync(userId);
        cart.Items.Clear();
        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Order> CheckoutAsync(Guid userId)
    {
        var cart = await GetCartAsync(userId);
        
        if (!cart.Items.Any())
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

        _context.Orders.Add(order);
        await ClearCartAsync(userId);
        await _context.SaveChangesAsync();
        return order;
    }
}
