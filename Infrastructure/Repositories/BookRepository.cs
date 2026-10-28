using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

using Dapper;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    public void AddBook(Book book)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"insert into books(title, publishedyear, totalcopies) values
        (@title, @publishedyear, @totalcopies)";
        connection.Execute(query, book);
        
    }
    

    public void DeleteBook(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = $"delete from books where id=@id";
       conn.Execute(query, new {id});
    }

    public Book GetBook(int id)
    {
         using var connection = Database.Connection();
        connection.Open();
        var query = "select * from books where id = @id";
        var book = connection.QuerySingle<Book>(query,new{id});
        return book;
    }

    public List<Book> GetBooks()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = $"select id, title, publishedyear, totalcopies from books" ;
        return conn.Query<Book>(query).ToList();
    }

    public void UpdateBook(Book book)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "update books set title = @title, publishedyear = @publishedyear, totalcopies = @totalcopies where id = @id";
        connection.Execute(query,book);
    }
    public List<Book> SearchByAuthor(string fullname)
    {
        using var conn = Database.Connection();
        conn.Open();
        const string query = "select b.id, b.title, b.publishedyear, b.totalcopies from books b join book_authors ba on b.id = ba.book_id join authors a on ba.author_id = a.id where a.fullname = @fullname";
        return conn.Query<Book>(query, new { fullname }).ToList();
    }

    public List<Book> SearchByGenre(int genreId)
    {
        using var conn = Database.Connection();
        conn.Open();
        const string query = "select b.id, b.title, b.publishedyear, b.totalcopies from books b join book_genres bg on b.id = bg.book_id where bg.genre_id = @genreId";
        return conn.Query<Book>(query, new { genreId }).ToList();
    }

    public int GetBookAmount()
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "select count(*) from books";
        return connection.QuerySingle<int>(query);
    }

    public int GetGenresAmount()
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "select count(*) from genres";
        return connection.QuerySingle<int>(query);
    }

    public int? GetMinPublishedYearByGenre(int genreId)
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = @"select min(b.publishedyear)
                               from books b
                               join book_genres bg on b.id = bg.book_id
                               where bg.genre_id = @genreId";
        return connection.QuerySingle<int?>(query, new { genreId });
    }

    public int? GetMaxPublishedYearByGenre(int genreId)
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = @"select max(b.publishedyear)
                               from books b
                               join book_genres bg on b.id = bg.book_id
                               where bg.genre_id = @genreId";
        return connection.QuerySingle<int?>(query, new { genreId });
    }

    public int GetAmountByPublishedYear(int year)
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "select count(*) from books where publishedyear = @year";
        return connection.QuerySingle<int>(query, new { year });
    }

    public int GetAmountByGenre(int genreId)
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "select count(distinct book_id) from book_genres where genre_id = @genreId";
        return connection.QuerySingle<int>(query, new { genreId });
    }
}

    
    
