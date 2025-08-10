using BookMarketApi.Extension;

namespace BookMarketApi.DTOs;

public class MarketBookShortDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string CoverImageUrl { get; set; }
    public decimal? Price { get; set; }
    
    public ListingStatus ListingStatus { get; set; }
    public BookCondition Condition { get; set; }
    public string Location { get; set; }
}