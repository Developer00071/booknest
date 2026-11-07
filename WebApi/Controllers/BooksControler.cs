using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    private readonly IBookService service = bookService;

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        return ToActionResult(await service.GetBooksAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBook(int id)
    {
        return ToActionResult(await service.GetBookAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        return ToActionResult(await service.AddBookAsync(book));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        return ToActionResult(await service.DeleteBookAsync(id));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBook([FromBody] Book book)
    {
        return ToActionResult(await service.UpdateBookAsync(book));
    }

    private IActionResult ToActionResult<T>(ApiResponse<T> response) =>
        StatusCode((int)response.StatusCode, response);
}
