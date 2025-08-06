using BookMarketApi.Common.Entities.Domain.BookEntities;

namespace BookMarketApi.Common.Entities.Domain.CartEntities;

public class CartItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookId { get; set; }
    public virtual Book Book { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; }
}
