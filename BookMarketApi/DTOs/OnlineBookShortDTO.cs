namespace BookMarketApi.DTOs;

public class OnlineBookShortDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string CoverImageUrl { get; set; }
    public decimal? Price { get; set; }
}