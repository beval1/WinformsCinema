using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Movie
    {
        public int Id { get; set; } = 0;
        public string MovieName { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string PremierYear { get; set; } = string.Empty;
        public string ImdbLink { get; set; } = string.Empty;
        public List<Genre> Genres { get; set; } = null;
    }
}
