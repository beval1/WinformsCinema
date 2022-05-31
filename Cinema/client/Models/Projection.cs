using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Projection
    {
        public int Id { get; set; }
        public int SceneId{ get; set; }
        public Scene Scene { get; set; }
        public SceneSeats SceneSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public DateTime ProjectionTime { get; set; }
        public Movie Movie { get; set; }

    }
}
