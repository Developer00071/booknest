using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository(ApplicationDbContext dbContext) : IBookRepository
{
    public async Task<bool> AddBookAsync(Book book)
    {
        await dbContext.Books.AddAsync(book);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await dbContext.Books.FindAsync(id);
        if (book is null)
        {
            return false;
        }

        dbContext.Books.Remove(book);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public Task<Book?> GetBookAsync(int id)
    {
        return dbContext.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(book => book.Id == id);
    }

    public Task<List<Book>> GetBooksAsync()
    {
        return dbContext.Books.AsNoTracking().ToListAsync();
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        var existingBook = await dbContext.Books.FindAsync(book.Id);
        if (existingBook is null)
        {
            return false;
        }

        dbContext.Entry(existingBook).CurrentValues.SetValues(book);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public Task<List<Book>> SearchByAuthorAsync(string fullname)
    {
        return dbContext.Books
            .AsNoTracking()
            .Where(book => dbContext.Authors.Any(author =>
                author.Id == book.AuthorId && author.Fullname == fullname))
            .ToListAsync();
    }

    public Task<List<Book>> SearchByGenreAsync(int genreId)
    {
        return dbContext.Books
            .AsNoTracking()
            .Where(book => book.GenreId == genreId)
            .ToListAsync();
    }

    public Task<int> GetBookAmountAsync()
    {
        return dbContext.Books.CountAsync();
    }

    public Task<int> GetGenresAmountAsync()
    {
        return dbContext.Genres.CountAsync();
    }

    public Task<int?> GetMinPublishedYearByGenreAsync(int genreId)
    {
        return dbContext.Books
            .Where(book => book.GenreId == genreId)
            .Select(book => (int?)book.PublishedYear)
            .MinAsync();
    }

    public Task<int?> GetMaxPublishedYearByGenreAsync(int genreId)
    {
        return dbContext.Books
            .Where(book => book.GenreId == genreId)
            .Select(book => (int?)book.PublishedYear)
            .MaxAsync();
    }

    public Task<int> GetAmountByPublishedYearAsync(int year)
    {
        return dbContext.Books.CountAsync(book => book.PublishedYear == year);
    }

    public Task<int> GetAmountByGenreAsync(int genreId)
    {
        return dbContext.Books.CountAsync(book => book.GenreId == genreId);
    }
}
