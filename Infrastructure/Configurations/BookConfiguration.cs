using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("books");

        builder.HasKey(book => book.Id);

        builder.Property(book => book.Id)
            .HasColumnName("id");

        builder.Property(book => book.Title)
            .HasColumnName("title")
            .IsRequired();

        builder.Property(book => book.PublishedYear)
            .HasColumnName("publishedyear")
            .IsRequired();

        builder.Property(book => book.TotalCopies)
            .HasColumnName("totalcopies")
            .IsRequired();

        builder.Property(book => book.AuthorId)
            .HasColumnName("authorid");

        builder.Property(book => book.GenreId)
            .HasColumnName("genreid");

        builder.HasOne<Author>()
            .WithMany()
            .HasForeignKey(book => book.AuthorId);

        builder.HasOne<Genre>()
            .WithMany()
            .HasForeignKey(book => book.GenreId);
    }
}
