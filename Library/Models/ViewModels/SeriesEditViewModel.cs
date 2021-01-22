using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class SeriesEditViewModel
    {
        public List<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }
        public Series Series { get; set; }
    }
}
