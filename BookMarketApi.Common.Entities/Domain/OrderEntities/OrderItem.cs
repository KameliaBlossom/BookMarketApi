using BookMarketApi.Common.Entities.Domain.BookEntities;

namespace BookMarketApi.Common.Entities.Domain.OrderEntities;

public class OrderItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
    public Guid BookId { get; set; }
    public virtual Book Book { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
