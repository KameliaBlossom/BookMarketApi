namespace BookMarketApi.Model;

public class Review
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid BookId { get; set; }
    public virtual Book Book { get; set; }
    
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

}