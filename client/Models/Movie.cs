using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Movie
    {
        public int id { get; set; } = 0;
        public string movieName { get; set; } = string.Empty;
        public string coverImage { get; set; } = string.Empty;
        public string premierYear { get; set; } = string.Empty;
        public string imdbLink { get; set; } = string.Empty;
    }
}
