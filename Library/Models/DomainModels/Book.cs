using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
  
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please select an author")]
        [Range(0,int.MaxValue,ErrorMessage ="Please select an author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Required(ErrorMessage = "Please select a genre")]
        public string GenreId { get; set; }
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Please enter a release date")]
        public DateTime ReleaseDate { get; set; }

        public int? SeriesId { get; set; }
        public Series Series { get; set; }

        [Required(ErrorMessage = "Please select a book shelf")]
        [Range(0,2)]
        public int Bookshelf { get; set; } //0 = Reading, 1 = Read, 2 = To Be Read

        public bool Favorite { get; set; }

        public DateTime DateRead { get; set; }

        public int? SeriesNum { get; set; }
    }
}
