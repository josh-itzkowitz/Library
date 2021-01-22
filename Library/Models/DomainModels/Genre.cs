using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Genre
    {
        public string GenreId { get; set; }

        [Required(ErrorMessage = "Please enter genre name")]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
