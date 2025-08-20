using AutoMapper;
using BookMarketApi.Common.Entities.Domain.BookEntities;
using BookMarketApi.Common.Entities.DTOs.OnlineBookDTOs;
using BookMarketApi.DAL.Contracts.OnlineBookContracts;

namespace BookMarketApi.BLL.Logic.OnlineBookImplementations;
using BookMarketApi.BLL.Contracts.OnlineBookContracts;

public class OnlineBookImplementation : IOnlineBookContract
{
    private readonly IOnlineBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public OnlineBookImplementation(IOnlineBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<OnlineBookDetailDto> CreateBookAsync(CreateOnlineBookDto createDto)
    {
        var bookEntity = _mapper.Map<OnlineBook>(createDto);

        var createdBook = await _bookRepository.AddAsync(bookEntity);

        return _mapper.Map<OnlineBookDetailDto>(createdBook);
    }

    public async Task<IEnumerable<OnlineBookShortDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OnlineBookShortDto>>(books);
    }

    public async Task<OnlineBookDetailDto?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return null;
        }
        return _mapper.Map<OnlineBookDetailDto>(book);
    }

    public async Task<OnlineBookDetailDto> UpdateAsync(Guid id, UpdateOnlineBookDto updateDto)
    {
        var targetBook = await _bookRepository.GetByIdAsync(id);
        if (targetBook == null)
        {
            return null;
        }

        _mapper.Map(updateDto, targetBook);
        await _bookRepository.UpdateAsync(targetBook);
        return _mapper.Map<OnlineBookDetailDto>(targetBook);
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