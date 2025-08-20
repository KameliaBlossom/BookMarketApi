using BookMarketApi.Common.Entities.Domain.UserEntities;

namespace BookMarketApi.Common.Entities.Domain.CartEntities;

public class Cart
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
