using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Dapper;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public async Task AddAuthorAsync(Author author)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "insert into authors(fullname, country, birthyear) values(@fullname, @country, @birthyear)";
        await connection.ExecuteAsync(query, author);
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "update authors set fullname = @fullname, country = @country, birthyear = @birthyear where id = @id";
        await connection.ExecuteAsync(query, author);
        System.Console.WriteLine("Updated!");
    }

    public async Task DeleteAuthorAsync(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "delete from authors where id = @id";
        await connection.ExecuteAsync(query, new { id });
    }

    public async Task<Author> GetAuthorAsync(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "select id, fullname, country, birthyear from authors where id = @id";
        var author = await connection.QuerySingleAsync<Author>(query, new { id });
        return author;
    }

    public async Task<List<Author>> GetAuthorsAsync()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "select id, fullname, country, birthyear from authors";
        var authors = await connection.QueryAsync<Author>(query);
        return authors.ToList();
    }

    public async Task<int> GetAuthorsAmountAsync()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "select count(*) from authors";
        return await connection.QuerySingleAsync<int>(query);
    }

    public async Task<int?> GetMinPublishedYearByAuthorAsync(int authorId)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = @"select min(b.publishedyear)
                               from books b
                               join book_authors ba on b.id = ba.book_id
                               where ba.author_id = @authorId";
        return await connection.QuerySingleAsync<int?>(query, new { authorId });
    }

    public async Task<int?> GetMaxPublishedYearByAuthorAsync(int authorId)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = @"select max(b.publishedyear)
                               from books b
                               join book_authors ba on b.id = ba.book_id
                               where ba.author_id = @authorId";
        return await connection.QuerySingleAsync<int?>(query, new { authorId });
    }
}

     
