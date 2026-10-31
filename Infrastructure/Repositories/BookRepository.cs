using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

using Dapper;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    public async Task AddBookAsync(Book book)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = @"insert into books(title, publishedyear, totalcopies) values
        (@title, @publishedyear, @totalcopies)";
        await connection.ExecuteAsync(query, book);
        
    }
    

    public async Task DeleteBookAsync(int id)
    {
        await using var conn = Database.Connection();
        await conn.OpenAsync();
        var query = $"delete from books where id=@id";
       await conn.ExecuteAsync(query, new {id});
    }

    public async Task<Book> GetBookAsync(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "select * from books where id = @id";
        var book = await connection.QuerySingleAsync<Book>(query,new{id});
        return book;
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        await using var conn = Database.Connection();
        await conn.OpenAsync();
        var query = $"select id, title, publishedyear, totalcopies from books" ;
        var books = await conn.QueryAsync<Book>(query);
        return books.ToList();
    }

    public async Task UpdateBookAsync(Book book)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "update books set title = @title, publishedyear = @publishedyear, totalcopies = @totalcopies where id = @id";
        await connection.ExecuteAsync(query,book);
    }
    public async Task<List<Book>> SearchByAuthorAsync(string fullname)
    {
        await using var conn = Database.Connection();
        await conn.OpenAsync();
        const string query = "select b.id, b.title, b.publishedyear, b.totalcopies from books b join book_authors ba on b.id = ba.book_id join authors a on ba.author_id = a.id where a.fullname = @fullname";
        var books = await conn.QueryAsync<Book>(query, new { fullname });
        return books.ToList();
    }

    public async Task<List<Book>> SearchByGenreAsync(int genreId)
    {
        await using var conn = Database.Connection();
        await conn.OpenAsync();
        const string query = "select b.id, b.title, b.publishedyear, b.totalcopies from books b join book_genres bg on b.id = bg.book_id where bg.genre_id = @genreId";
        var books = await conn.QueryAsync<Book>(query, new { genreId });
        return books.ToList();
    }

    public async Task<int> GetBookAmountAsync()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "select count(*) from books";
        return await connection.QuerySingleAsync<int>(query);
    }

    public async Task<int> GetGenresAmountAsync()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "select count(*) from genres";
        return await connection.QuerySingleAsync<int>(query);
    }

    public async Task<int?> GetMinPublishedYearByGenreAsync(int genreId)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = @"select min(b.publishedyear)
                               from books b
                               join book_genres bg on b.id = bg.book_id
                               where bg.genre_id = @genreId";
        return await connection.QuerySingleAsync<int?>(query, new { genreId });
    }

    public async Task<int?> GetMaxPublishedYearByGenreAsync(int genreId)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = @"select max(b.publishedyear)
                               from books b
                               join book_genres bg on b.id = bg.book_id
                               where bg.genre_id = @genreId";
        return await connection.QuerySingleAsync<int?>(query, new { genreId });
    }

    public async Task<int> GetAmountByPublishedYearAsync(int year)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "select count(*) from books where publishedyear = @year";
        return await connection.QuerySingleAsync<int>(query, new { year });
    }

    public async Task<int> GetAmountByGenreAsync(int genreId)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();
        const string query = "select count(distinct book_id) from book_genres where genre_id = @genreId";
        return await connection.QuerySingleAsync<int>(query, new { genreId });
    }
}

    
    
