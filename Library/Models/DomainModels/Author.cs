using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{

    public class Author
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }

        [Range(0,1)]
        public int Gender { get; set; } //0 = Male, 1 = Female

        public DateTime DOB { get; set; }

        public bool IsAlive { get; set; }

        public List<Book> Books { get; set; }

        public List<Series> Series { get; set; }

        public string Name { get => FirstName + " " + LastName; }

        public bool Favorite { get; set; }
    }
}
