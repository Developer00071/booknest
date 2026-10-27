using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Dapper;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public void AddAuthor(Author author)
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "insert into authors(fullname, country, birthyear) values(@fullname, @country, @birthyear)";
        connection.Execute(query, author);
    }

    public void UpdateAuthor(Author author)
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "update authors set fullname = @fullname, country = @country, birthyear = @birthyear where id = @id";
        connection.Execute(query, author);
        System.Console.WriteLine("Updated!");
    }

    public void DeleteAuthor(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "delete from authors where id = @id";
        connection.Execute(query, new { id });
    }

    public Author GetAuthor(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "select id, fullname, country, birthyear from authors where id = @id";
        var author = connection.QuerySingle<Author>(query, new { id });
        return author;
    }

    public List<Author> GetAuthors()
    {
        using var connection = Database.Connection();
        connection.Open();
        const string query = "select id, fullname, country, birthyear from authors";
        return connection.Query<Author>(query).ToList();
    }
}

     
