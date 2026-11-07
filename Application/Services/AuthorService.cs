using System.Net;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public sealed class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    public async Task<ApiResponse<bool>> AddAuthorAsync(Author author)
    {
        var added = await authorRepository.AddAuthorAsync(author);
        return added
            ? new ApiResponse<bool>(HttpStatusCode.Created, "Author created.", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Author was not created.", false);
    }

    public async Task<ApiResponse<bool>> UpdateAuthorAsync(Author author)
    {
        var updated = await authorRepository.UpdateAuthorAsync(author);
        return updated
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Author updated.", true)
            : new ApiResponse<bool>(HttpStatusCode.NotFound, "Author was not found.", false);
    }

    public async Task<ApiResponse<bool>> DeleteAuthorAsync(int id)
    {
        var deleted = await authorRepository.DeleteAuthorAsync(id);
        return deleted
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Author deleted.", true)
            : new ApiResponse<bool>(HttpStatusCode.NotFound, "Author was not found.", false);
    }

    public async Task<ApiResponse<Author>> GetAuthorAsync(int id)
    {
        var author = await authorRepository.GetAuthorAsync(id);
        return author is null
            ? new ApiResponse<Author>(HttpStatusCode.NotFound, "Author was not found.")
            : new ApiResponse<Author>(HttpStatusCode.OK, "Author retrieved.", author);
    }

    public async Task<ApiResponse<List<Author>>> GetAuthorsAsync()
    {
        var authors = await authorRepository.GetAuthorsAsync();
        return new ApiResponse<List<Author>>(HttpStatusCode.OK, "Authors retrieved.", authors);
    }
}
