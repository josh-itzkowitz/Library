using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class AddToSeriesViewModel
    {
        public int SrsId { get; set; }

        public int BkId { get; set; }

        public string Name { get; set; }

        public string AuthName { get; set; }

        public int SrsNum { get; set; }
     
        public Dictionary<string, string> Books { get; set; }
    }
}
