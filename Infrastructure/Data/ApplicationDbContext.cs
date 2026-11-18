using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{

    
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("authors");
            entity.HasKey(author => author.Id);
            entity.Property(author => author.Id).HasColumnName("id");
            entity.Property(author => author.Fullname).HasColumnName("fullname").IsRequired();
            entity.Property(author => author.Country).HasColumnName("country").IsRequired();
            entity.Property(author => author.BirthYear).HasColumnName("birthyear");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");
            entity.HasKey(book => book.Id);
            entity.Property(book => book.Id).HasColumnName("id");
            entity.Property(book => book.Title).HasColumnName("title").IsRequired();
            entity.Property(book => book.PublishedYear).HasColumnName("publishedyear");
            entity.Property(book => book.TotalCopies).HasColumnName("totalcopies");
            entity.Property(book => book.AuthorId).HasColumnName("authorid");
            entity.Property(book => book.GenreId).HasColumnName("genreid");

            entity.HasOne<Author>()
                .WithMany()
                .HasForeignKey(book => book.AuthorId);

            entity.HasOne<Genre>()
                .WithMany()
                .HasForeignKey(book => book.GenreId);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("genres");
            entity.HasKey(genre => genre.Id);
            entity.Property(genre => genre.Id).HasColumnName("id");
            entity.Property(genre => genre.Name).HasColumnName("name");
        });

        base.OnModelCreating(modelBuilder);
    }
}
