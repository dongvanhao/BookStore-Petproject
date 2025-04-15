using BookStore.Application.DTOs.Books;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetAll(string? keyword, int page = 1, int pageSize = 10)
    {
        var books = await _bookService.GetAllAsync(keyword, page, pageSize);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _bookService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var result = await _bookService.CreateAsync(dto);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch.");

        var result = await _bookService.UpdateAsync(dto);
        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _bookService.DeleteAsync(id);
        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }
}
