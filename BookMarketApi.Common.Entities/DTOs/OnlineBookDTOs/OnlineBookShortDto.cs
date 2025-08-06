namespace BookMarketApi.Common.Entities.DTOs.OnlineBookDTOs;

public class OnlineBookShortDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string CoverImageUrl { get; set; }
    public decimal? Price { get; set; }
}