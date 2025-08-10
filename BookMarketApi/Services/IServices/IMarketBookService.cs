using BookMarketApi.DTOs;

namespace BookMarketApi.Services.IServices;

public interface IMarketBookService
{
    Task<IEnumerable<MarketBookShortDTO>> GetAllBooksAsync();
    Task<MarketBookDetailDTO?> GetBookByIdAsync(Guid bookId);
    Task<MarketBookDetailDTO> CreateBookAsync(CreateMarketBookDTO createDto);
    Task<MarketBookDetailDTO> UpdateAsync(Guid updateId, UpdateMarketBookDTO updateDto);
    Task<bool> DeleteAsync(Guid deleteId);
}