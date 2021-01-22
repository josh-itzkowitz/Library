using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Series
    {
        public int SeriesId { get; set; }

        [Required(ErrorMessage = "Please enter a series name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select an author")]
        [Range(0,int.MaxValue,ErrorMessage ="Please select an author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int NumBooks { get; set; }

        [Required(ErrorMessage = "Please select a genre")]       
        public string GenreId { get; set; }
        public Genre Genre { get; set; }

        public List<Book> Books { get; set; }
    }
}
