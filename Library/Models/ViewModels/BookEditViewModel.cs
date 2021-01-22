using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookEditViewModel
    {
        public List<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Series> Series { get; set; }
        public Book Book { get; set; }
    }
}
