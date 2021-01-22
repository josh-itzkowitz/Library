using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library.Models.DataLayer
{
    internal class SeedAuthors : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> entity)
        {
            entity.HasData(
                new Author { AuthorId = 1, FirstName = "J.K.", LastName = "Rowling", Gender = 1, DOB = DateTime.ParseExact("1965-07-31", "yyyy-MM-dd", null), IsAlive = true },
                new Author { AuthorId = 2, FirstName = "Stephen", LastName = "King", Gender = 0, DOB = DateTime.ParseExact("1947-09-21", "yyyy-MM-dd", null), IsAlive = true }
                );
        }
    }
}
