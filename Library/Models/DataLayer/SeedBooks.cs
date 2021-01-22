using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library.Models.DataLayer
{
    public class SeedBooks : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.HasData(
                new Book { BookId = 1, Title = "Harry Potter and the Sorcerer's Stone", AuthorId = 1, GenreId = "F", ReleaseDate = DateTime.ParseExact("1996-06-26", "yyyy-MM-dd", null), SeriesId = 1, Bookshelf = 1, Favorite = true }
                );
        }
    }
}
