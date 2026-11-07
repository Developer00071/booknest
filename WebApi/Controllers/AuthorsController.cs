using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IAuthorService authorService) : ControllerBase
{
    private readonly IAuthorService service = authorService;

    [HttpGet]
    public async Task<IActionResult> GetAuthors() =>
        ToActionResult(await service.GetAuthorsAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthor(int id) =>
        ToActionResult(await service.GetAuthorAsync(id));

    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] Author author) =>
        ToActionResult(await service.AddAuthorAsync(author));

    [HttpPut]
    public async Task<IActionResult> UpdateAuthor([FromBody] Author author) =>
        ToActionResult(await service.UpdateAuthorAsync(author));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id) =>
        ToActionResult(await service.DeleteAuthorAsync(id));

    private IActionResult ToActionResult<T>(ApiResponse<T> response) =>
        StatusCode((int)response.StatusCode, response);
}
