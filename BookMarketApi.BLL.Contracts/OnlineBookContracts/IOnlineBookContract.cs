using BookMarketApi.Common.Entities.DTOs.OnlineBookDTOs;

namespace BookMarketApi.BLL.Contracts.OnlineBookContracts;

public interface IOnlineBookContract
{
    Task<IEnumerable<OnlineBookShortDto>> GetAllBooksAsync();
    Task<OnlineBookDetailDto?> GetBookByIdAsync(Guid bookId);
    Task<OnlineBookDetailDto> CreateBookAsync(CreateOnlineBookDto createDto);
    Task<OnlineBookDetailDto> UpdateAsync(Guid updateId, UpdateOnlineBookDto updateDto);
    
    Task<bool> DeleteAsync(Guid deleteId);
}