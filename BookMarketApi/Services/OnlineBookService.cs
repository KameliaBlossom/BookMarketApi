using AutoMapper;
using BookMarketApi.DataAccess.Contracts;
using BookMarketApi.DTOs;
using BookMarketApi.Model;
using BookMarketApi.Services.IServices;


namespace BookMarketApi.Services;

public class OnlineBookService : IOnlineBookService
{
    private readonly IOnlineBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public OnlineBookService(IOnlineBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<OnlineBookDetailDTO> CreateBookAsync(CreateOnlineBookDTO createDto)
    {
        var bookEntity = _mapper.Map<OnlineBook>(createDto);

        var createdBook = await _bookRepository.AddAsync(bookEntity);

        return _mapper.Map<OnlineBookDetailDTO>(createdBook);
    }

    public async Task<IEnumerable<OnlineBookShortDTO>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OnlineBookShortDTO>>(books);
    }

    public async Task<OnlineBookDetailDTO?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return null;
        }
        return _mapper.Map<OnlineBookDetailDTO>(book);
    }

    public async Task<OnlineBookDetailDTO> UpdateAsync(Guid id, UpdateOnlineBookDTO updateDto)
    {
        var targetBook = await _bookRepository.GetByIdAsync(id);
        if (targetBook == null)
        {
            return null;
        }

        _mapper.Map(updateDto, targetBook);
        await _bookRepository.UpdateAsync(targetBook);
        return _mapper.Map<OnlineBookDetailDTO>(targetBook);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleteBook = await _bookRepository.GetByIdAsync(id);
        if (deleteBook == null)
        {
            return false;
        }
        await _bookRepository.DeleteByIdAsync(id);
        return true;
    }
}