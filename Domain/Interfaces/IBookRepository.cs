using Domain.Models;

namespace Domain.Interfaces;

public interface IBookRepository
{
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    Task<List<Book>> SearchByAuthorAsync(string fullname);
    Task<List<Book>> SearchByGenreAsync(int genreId);
    Task<Book> GetBookAsync(int id);
    Task<List<Book>> GetBooksAsync();
    Task<int> GetBookAmountAsync();
    Task<int> GetGenresAmountAsync();
    Task<int?> GetMinPublishedYearByGenreAsync(int genreId);
    Task<int?> GetMaxPublishedYearByGenreAsync(int genreId);
    Task<int> GetAmountByPublishedYearAsync(int year);
    Task<int> GetAmountByGenreAsync(int genreId);
}
