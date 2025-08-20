namespace BookMarketApi.Common.Entities.Domain.BookEntities;

public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}