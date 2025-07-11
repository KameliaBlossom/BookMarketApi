namespace BookMarketApi.Model;

public class OnlineBook : Book
{
    public bool IsSubscribtionable { get; set; }
    public string ContentUrl { get; set; }
    
}