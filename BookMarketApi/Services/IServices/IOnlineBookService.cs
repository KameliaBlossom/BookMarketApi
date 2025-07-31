namespace BookMarketApi.Services.IServices;
using BookMarketApi.DTOs;

public interface IOnlineBookService
{
    Task<IEnumerable<OnlineBookShortDTO>> GetAllBooksAsync();
    Task<OnlineBookDetailDTO?> GetBookByIdAsync(Guid bookId);
    Task<OnlineBookDetailDTO> CreateBookAsync(CreateOnlineBookDTO createDto);
    Task<OnlineBookDetailDTO> UpdateAsync(Guid updateId, UpdateOnlineBookDTO updateDto);
    
    Task<bool> DeleteAsync(Guid deleteId);
}