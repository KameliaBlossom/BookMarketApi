using BookMarketApi.DTOs;
using BookMarketApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketApi.Controllers;


[ApiController]
[Route("api/market-books")]
public class MarketBookController : ControllerBase
{
    private readonly IMarketBookService _bookService;

    public MarketBookController(IMarketBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(MarketBookDetailDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] CreateMarketBookDTO createDto)
    {
        var createdBook = await _bookService.CreateBookAsync(createDto);

        return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MarketBookDetailDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookById(Guid bookId)
    {
        var book = await _bookService.GetBookByIdAsync(bookId);
        
        return book == null ? NotFound() : Ok(book);
    }

    [HttpGet]
    [ProducesResponseType(typeof(MarketBookShortDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(MarketBookDetailDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateBook(Guid bookId, [FromBody] UpdateMarketBookDTO updateDto)
    {
        var updatedBook = await _bookService.UpdateAsync(bookId, updateDto);
        
        return updatedBook == null ? NotFound() : Ok(updatedBook);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(MarketBookShortDTO), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        var deletedFlag = await _bookService.DeleteAsync(bookId);

        if (!deletedFlag)
        {
            return NotFound();
        }

        return NoContent();
    }
    
}