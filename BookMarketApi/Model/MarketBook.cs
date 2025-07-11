using BookMarketApi.Extension;

namespace BookMarketApi.Model;

public class MarketBook : Book
{
    public BookCondition Condition { get; set; }
    public ModerationStatus ModerationStatus { get; set; }
    public ListingStatus ListingStatus { get; set; }
    public string? ModerationComment { get; set; }
    
    public DateTime ListedDate { get; set; }
    public List<string> PhotoUrls { get; set; }
    public string Location { get; set; }
    
    public Guid SellerId { get; set; }
    
    public Guid? OnlineBookId { get; set; }
    public OnlineBook OnlineBook { get; set; }
}