using Library.Application.DTOs;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book is null) return NotFound();
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto dto)
    {
        var created = await _bookService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [HttpPut("{id:int}")]
public async Task<ActionResult<BookDto>> Update(int id, [FromBody] CreateBookDto dto)
{
    try
    {
        var updated = await _bookService.UpdateAsync(id, dto);
        return Ok(updated);
    }
    catch (InvalidOperationException ex)
    {
        return NotFound(new { message = ex.Message });
    }
}

[HttpDelete("{id:int}")]
public async Task<IActionResult> Delete(int id)
{
    try
    {
        await _bookService.DeleteAsync(id);
        return NoContent();
    }
    catch (InvalidOperationException ex)
    {
        return NotFound(new { message = ex.Message });
    }
}

}
