using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public abstract class Watchable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverImage { get; set; }
        public string PremierYear { get; set; }
        public string ImdbLink { get; set; }
        public List<Genre> Genres { get; set; }


    }
}
