using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthorRepository(ApplicationDbContext dbContext) : IAuthorRepository
{
    public async Task<bool> AddAuthorAsync(Author author)
    {
        await dbContext.Authors.AddAsync(author);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAuthorAsync(Author author)
    {
        var existingAuthor = await dbContext.Authors.FindAsync(author.Id);
        if (existingAuthor is null)
        {
            return false;
        }

        dbContext.Entry(existingAuthor).CurrentValues.SetValues(author);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await dbContext.Authors.FindAsync(id);
        if (author is null)
        {
            return false;
        }

        dbContext.Authors.Remove(author);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public Task<Author?> GetAuthorAsync(int id)
    {
        return dbContext.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(author => author.Id == id);
    }

    public Task<List<Author>> GetAuthorsAsync()
    {
        return dbContext.Authors.AsNoTracking().ToListAsync();
    }

    public Task<int> GetAuthorsAmountAsync()
    {
        return dbContext.Authors.CountAsync();
    }

    public Task<int?> GetMinPublishedYearByAuthorAsync(int authorId)
    {
        return dbContext.Books
            .Where(book => book.AuthorId == authorId)
            .Select(book => (int?)book.PublishedYear)
            .MinAsync();
    }

    public Task<int?> GetMaxPublishedYearByAuthorAsync(int authorId)
    {
        return dbContext.Books
            .Where(book => book.AuthorId == authorId)
            .Select(book => (int?)book.PublishedYear)
            .MaxAsync();
    }
}
