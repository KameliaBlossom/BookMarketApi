namespace BookMarketApi.Common.Entities.Domain.BookEntities;

public class OnlineBook : Book
{
    public bool IsSubscribtionable { get; set; }
    public string ContentUrl { get; set; }
    
}