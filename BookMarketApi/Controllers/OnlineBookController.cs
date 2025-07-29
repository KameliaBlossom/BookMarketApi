using BookMarketApi.DTOs;
using BookMarketApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OnlineBookController : ControllerBase
{
    private readonly IOnlineBookService _bookService;
    
    public OnlineBookController(IOnlineBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(OnlineBookDetailDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] CreateOnlineBookDTO createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdBook = await _bookService.CreateBookAsync(createDto);

        return CreatedAtAction(nameof(createdBook), new { id = createdBook.Id }, createdBook);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OnlineBookDetailDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        return book == null ? NotFound() : Ok(book);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OnlineBookShortDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(OnlineBookDetailDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateOnlineBookDTO updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var updatedBook = await _bookService.UpdateAsync(id, updateDto);
        
        return updatedBook == null ? NotFound() : Ok(updatedBook);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var deletedBook = await _bookService.DeleteAsync(id);

        if (deletedBook == null)
        {
            return NotFound();
        }
        
        return Ok();
    }
}