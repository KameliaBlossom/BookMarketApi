using AutoMapper;
using BookMarketApi.DataAccess.Contracts;

using BookMarketApi.Services.IServices;
using BookMarketApi.DTOs;
using BookMarketApi.Model;

namespace BookMarketApi.Services;

public class MarketBookService : IMarketBookService
{
    private readonly IMarketBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public MarketBookService(IMarketBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<MarketBookShortDTO>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        
        return _mapper.Map<IEnumerable<MarketBookShortDTO>>(books);
    }

    public async Task<MarketBookDetailDTO?> GetBookByIdAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        
        if (book == null)
        {
            return null;
        }
        
        return _mapper.Map<MarketBookDetailDTO>(book);
    }

    public async Task<MarketBookDetailDTO> CreateBookAsync(CreateMarketBookDTO createDto)
    {
        var bookEntity = _mapper.Map<MarketBook>(createDto);
        
        var createdBook = await _bookRepository.AddAsync(bookEntity);
        
        return _mapper.Map<MarketBookDetailDTO>(createdBook);
    }

    public async Task<MarketBookDetailDTO> UpdateAsync(Guid bookId, UpdateMarketBookDTO updateDto)
    {
        var targetBook = await _bookRepository.GetByIdAsync(bookId);
        if (targetBook == null)
        {
            return null;
        }
        
        _mapper.Map(updateDto, targetBook);
        await _bookRepository.UpdateAsync(targetBook);
        return _mapper.Map<MarketBookDetailDTO>(targetBook);
    }

    public async Task<bool> DeleteAsync(Guid bookId)
    {
        var deletedBook = await _bookRepository.GetByIdAsync(bookId);
        if (deletedBook == null)
        {
            return false;
        }
        await _bookRepository.DeleteByIdAsync(bookId);
        return true;
    }
}