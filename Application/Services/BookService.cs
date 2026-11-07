using System.Net;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public sealed class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<ApiResponse<bool>> AddBookAsync(Book book)
    {
        var added = await bookRepository.AddBookAsync(book);
        return added
            ? new ApiResponse<bool>(HttpStatusCode.Created, "Book created.", true)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Book was not created.", false);
    }

    public async Task<ApiResponse<bool>> UpdateBookAsync(Book book)
    {
        var updated = await bookRepository.UpdateBookAsync(book);
        return updated
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Book updated.", true)
            : new ApiResponse<bool>(HttpStatusCode.NotFound, "Book was not found.", false);
    }

    public async Task<ApiResponse<bool>> DeleteBookAsync(int id)
    {
        var deleted = await bookRepository.DeleteBookAsync(id);
        return deleted
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Book deleted.", true)
            : new ApiResponse<bool>(HttpStatusCode.NotFound, "Book was not found.", false);
    }

    public async Task<ApiResponse<Book>> GetBookAsync(int id)
    {
        var book = await bookRepository.GetBookAsync(id);
        return book is null
            ? new ApiResponse<Book>(HttpStatusCode.NotFound, "Book was not found.")
            : new ApiResponse<Book>(HttpStatusCode.OK, "Book retrieved.", book);
    }

    public async Task<ApiResponse<List<Book>>> GetBooksAsync()
    {
        var books = await bookRepository.GetBooksAsync();
        return new ApiResponse<List<Book>>(HttpStatusCode.OK, "Books retrieved.", books);
    }
}
