using BookMarketApi.Extension;

namespace BookMarketApi.DTOs;

public class MarketBookDetailDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public int PublicationYear { get; set; }
    public string CoverImageUrl { get; set; }
    public decimal? Price { get; set; }

    public BookCondition Condition { get; set; }
    public ListingStatus ListingStatus { get; set; }
    
    public DateTime ListedDate { get; set; }
    public List<string> PhotoUrls { get; set; }
    public string Location { get; set; }
    
    public Guid SellerId { get; set; }
    public Guid? OnlineBookId { get; set; }
}