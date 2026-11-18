using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Set the ConnectionStrings__DefaultConnection environment variable before running the console app.");
}

var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseNpgsql(connectionString)
    .Options;

await using var dbContext = new ApplicationDbContext(options);
var repo = new BookRepository(dbContext);
var authorRepo = new AuthorRepository(dbContext);

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
            var books = await repo.GetBooksAsync();
            foreach(var b in books)
            {
                System.Console.WriteLine(b.Id+" "+b.Title+" "+b.PublishedYear+" "+b.TotalCopies);
            }
            break;
        case "2":
            System.Console.Write("Enter id: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var bb = await repo.GetBookAsync(id);
            System.Console.WriteLine(bb.Id+" "+bb.Title+" "+bb.PublishedYear+" "+bb.TotalCopies);
            break;
        case "3":
            System.Console.Write("Enter title: ");
            var t = Console.ReadLine()!;
            System.Console.Write("Enter published year: ");
            var py = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter total copies: ");
            var tc = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter book author id: ");
            var bookAuthorId = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter book genre id: ");
            var bookGenreId = Convert.ToInt32(Console.ReadLine());
            var bbb = new Book()
            {
                Title = t,
                PublishedYear = py,
                TotalCopies = tc,
                AuthorId = bookAuthorId,
                GenreId = bookGenreId
            };
            await repo.AddBookAsync(bbb);
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
            System.Console.Write("Enter author id: ");
            var updateBookAuthorId = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter genre id: ");
            var updateBookGenreId = Convert.ToInt32(Console.ReadLine());
            var ubbb = new Book()
            {
                Id = uid,
                Title = ut,
                PublishedYear = upy,
                TotalCopies = utc,
                AuthorId = updateBookAuthorId,
                GenreId = updateBookGenreId
            };
            await repo.UpdateBookAsync(ubbb);
            break;
        case "5":
            System.Console.Write("Enter id: ");
            var did = Convert.ToInt32(Console.ReadLine());
            await repo.DeleteBookAsync(did);
            break;
        case "6":
            var authors = await authorRepo.GetAuthorsAsync();
            foreach (var authori in authors)
            {
                System.Console.WriteLine($"{authori.Id} {authori.Fullname} {authori.Country} {authori.BirthYear}");
            }
            break;
        case "7":
            System.Console.Write("Enter id: ");
            var authorId = Convert.ToInt32(Console.ReadLine());
            var selectedAuthor = await authorRepo.GetAuthorAsync(authorId);
            System.Console.WriteLine($"{selectedAuthor.Id} {selectedAuthor.Fullname} {selectedAuthor.Country} {selectedAuthor.BirthYear}");
            break;
        case "8":
            System.Console.Write("Enter fullname: ");
            var fullname = Console.ReadLine()!;
            System.Console.Write("Enter country: ");
            var country = Console.ReadLine()!;
            System.Console.Write("Enter birth year: ");
            var birthYear = Convert.ToInt32(Console.ReadLine());
            await authorRepo.AddAuthorAsync(new Author
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
            await authorRepo.UpdateAuthorAsync(new Author
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
            await authorRepo.DeleteAuthorAsync(deleteAuthorId);
            break;
        case "11":
            System.Console.Write("Enter author id: ");
            var searchAuthorId = Convert.ToInt32(Console.ReadLine());
            var author = await authorRepo.GetAuthorAsync(searchAuthorId);
            foreach (var book in await repo.SearchByAuthorAsync(author.Fullname))
            {
                System.Console.WriteLine($"{book.Id} {book.Title} {book.PublishedYear} {book.TotalCopies}");
            }
            break;
        case "12":
            System.Console.Write("Enter genre id: ");
            var searchGenreId = Convert.ToInt32(Console.ReadLine());
            var booksByGenre = await repo.SearchByGenreAsync(searchGenreId);
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
