using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library.Models.DataLayer
{
    internal class SeedSeries : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> entity)
        {
            entity.HasData(
                new Series { SeriesId = 1, Name = "Harry Potter", AuthorId = 1, NumBooks = 7, GenreId = "F"}
                );
        }
    }
}
