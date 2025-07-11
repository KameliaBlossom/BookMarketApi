namespace BookMarketApi.Model;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public int PublicationYear { get; set; }
    public string CoverImageUrl { get; set; }
    public decimal? Price { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}