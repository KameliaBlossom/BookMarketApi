using BookMarketApi.Common.Entities.Domain.BookEntities;
using BookMarketApi.Common.Entities.Domain.CartEntities;
using BookMarketApi.Common.Entities.Domain.OrderEntities;
using BookMarketApi.DAL.Contracts.CartContracts;
using Microsoft.EntityFrameworkCore;

namespace BookMarketApi.DAL.Repositories.CartImplementations;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;
    public CartRepository(ApplicationDbContext context) => _context = context;

    public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
        => await _context.Carts.Include(c => c.Items).ThenInclude(i => i.Book)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    public async Task AddCartAsync(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
    }
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    public async Task<Book?> GetBookByIdAsync(Guid bookId)
        => await _context.Books.FindAsync(bookId);
    public async Task<Order> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }
}
