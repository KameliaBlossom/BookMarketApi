using BookMarketApi.Common.Entities.Domain.UserEntities;
using BookMarketApi.Extension;

namespace BookMarketApi.Common.Entities.Domain.OrderEntities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
