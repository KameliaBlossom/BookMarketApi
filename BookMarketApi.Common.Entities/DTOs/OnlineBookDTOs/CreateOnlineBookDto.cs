using System.ComponentModel.DataAnnotations;

namespace BookMarketApi.Common.Entities.DTOs.OnlineBookDTOs;

public class CreateOnlineBookDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Length must be between 3 and 200")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Author is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 100")]
    public string Author { get; set; }
    
    [StringLength(5000, ErrorMessage = "Length must be less than 5000")]
    public string Description { get; set; }
    
    [Range(1400, 2100, ErrorMessage = "Incorrect year")]
    public int PublicationYear { get; set; }
    
    [Range(0.0, 1000000.0)]
    public decimal? Price { get; set; }
    
    [Required]
    [Url(ErrorMessage = "Некорректный формат URL")]
    public string CoverImageUrl { get; set; }
    
    [Required]//Точно ли?
    [Url(ErrorMessage = "Некорректный формат URL")]
    public string ContentUrl { get; set; }
}