using BookMarketApi.Extension;
using System.ComponentModel.DataAnnotations;

namespace BookMarketApi.DTOs;

public class UpdateMarketBookDTO
{
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Length must be between 3 and 200")]
    public string? Title { get; set; }
    
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 100")]
    public string? Author { get; set; }
    
    [StringLength(5000, ErrorMessage = "Length must be less than 5000")]
    public string? Description { get; set; }
    
    [Range(1400, 2100, ErrorMessage = "Incorrect year")]
    public int? PublicationYear { get; set; }
    
    [Url(ErrorMessage = "Valid cover image URL is required")]
    public string? CoverImageUrl { get; set; }
    
    [Range(0.0, 1000000.0)]
    public decimal? Price { get; set; }
    
    
    public BookCondition? Condition { get; set; }
    public string Location{ get; set; }
    
    [MinLength(1, ErrorMessage = "At least one photo URL is required")]
    [MaxLength(7, ErrorMessage = "You can upload a maximum of 7 photos.")]
    public List<string> PhotoUrls { get; set; }
}