namespace BookMarketApi.Common.Entities.DTOs.OnlineBookDTOs;

public class OnlineBookDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public int PublicationYear { get; set; }
    public string CoverImageUrl { get; set; }
    public decimal? Price { get; set; }
}