using System.Collections;
using Domain.Models;
using Infrastructure.Repositories;
using Dapper;

var repo = new BookRepository();
var authorRepo = new AuthorRepository();

while (true)
{
    System.Console.WriteLine("======================================");
    System.Console.WriteLine(@"1. Show all books.
2. Show book by id.
3. Add new book.
4. Update book.
5. Delete book.
6. Show all authors.
7. Show author by id.
8. Add new author.
9. Update author.
10. Delete author.
11. Search books by author.
12. Search books by genre.
0. Exit.");
    var a = Console.ReadLine();
    System.Console.WriteLine("======================================");
    switch (a)
    {
        case "1":
            var books = repo.GetBooks();
            foreach(var b in books)
            {
                System.Console.WriteLine(b.Id+" "+b.Title+" "+b.PublishedYear+" "+b.TotalCopies);
            }
            break;
        case "2":
            System.Console.Write("Enter id: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var bb = repo.GetBook(id);
            System.Console.WriteLine(bb.Id+" "+bb.Title+" "+bb.PublishedYear+" "+bb.TotalCopies);
            break;
        case "3":
            System.Console.Write("Enter title: ");
            var t = Console.ReadLine()!;
            System.Console.Write("Enter published year: ");
            var py = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter total copies: ");
            var tc = Convert.ToInt32(Console.ReadLine());
            var bbb = new Book()
            {
                Title = t,
                PublishedYear = py,
                TotalCopies = tc
            };
            repo.AddBook(bbb);
            break;
        case "4":
            System.Console.Write("Enter id: ");
            var uid = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter title: ");
            var ut = Console.ReadLine()!;
            System.Console.Write("Enter published year: ");
            var upy = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter total copies: ");
            var utc = Convert.ToInt32(Console.ReadLine());
            var ubbb = new Book()
            {
                Id = uid,
                Title = ut,
                PublishedYear = upy,
                TotalCopies = utc
            };
            repo.UpdateBook(ubbb);
            break;
        case "5":
            System.Console.Write("Enter id: ");
            var did = Convert.ToInt32(Console.ReadLine());
            repo.DeleteBook(did);
            break;
        case "6":
            var authors = authorRepo.GetAuthors();
            foreach (var authori in authors)
            {
                System.Console.WriteLine($"{authori.Id} {authori.Fullname} {authori.Country} {authori.BirthYear}");
            }
            break;
        case "7":
            System.Console.Write("Enter id: ");
            var authorId = Convert.ToInt32(Console.ReadLine());
            var selectedAuthor = authorRepo.GetAuthor(authorId);
            System.Console.WriteLine($"{selectedAuthor.Id} {selectedAuthor.Fullname} {selectedAuthor.Country} {selectedAuthor.BirthYear}");
            break;
        case "8":
            System.Console.Write("Enter fullname: ");
            var fullname = Console.ReadLine()!;
            System.Console.Write("Enter country: ");
            var country = Console.ReadLine()!;
            System.Console.Write("Enter birth year: ");
            var birthYear = Convert.ToInt32(Console.ReadLine());
            authorRepo.AddAuthor(new Author
            {
                Fullname = fullname,
                Country = country,
                BirthYear = birthYear
            });
            break;
        case "9":
            System.Console.Write("Enter id: ");
            var updateAuthorId = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter fullname: ");
            var updateFullname = Console.ReadLine()!;
            System.Console.Write("Enter country: ");
            var updateCountry = Console.ReadLine()!;
            System.Console.Write("Enter birth year: ");
            var updateBirthYear = Convert.ToInt32(Console.ReadLine());
            authorRepo.UpdateAuthor(new Author
            {
                Id = updateAuthorId,
                Fullname = updateFullname,
                Country = updateCountry,
                BirthYear = updateBirthYear
            });
            break;
        case "10":
            System.Console.Write("Enter id: ");
            var deleteAuthorId = Convert.ToInt32(Console.ReadLine());
            authorRepo.DeleteAuthor(deleteAuthorId);
            break;
        case "11":
            System.Console.Write("Enter author id: ");
            var searchAuthorId = Convert.ToInt32(Console.ReadLine());
            var author = authorRepo.GetAuthor(searchAuthorId);
            foreach (var book in repo.SearchByAuthor(author.Fullname))
            {
                System.Console.WriteLine($"{book.Id} {book.Title} {book.PublishedYear} {book.TotalCopies}");
            }
            break;
        case "12":
            System.Console.Write("Enter genre id: ");
            var searchGenreId = Convert.ToInt32(Console.ReadLine());
            var booksByGenre = repo.SearchByGenre(searchGenreId);
            foreach (var book in booksByGenre)
            {
                System.Console.WriteLine($"{book.Id} {book.Title} {book.PublishedYear} {book.TotalCopies}");
            }
            break;
        case "0":
            System.Console.WriteLine("Exiting ...");
            return;
    }
    
}
