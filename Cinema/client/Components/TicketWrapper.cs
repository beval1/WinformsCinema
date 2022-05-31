using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class TicketWrapper
    {
        public Projection Projection { get; set; }
        public int SeatRow { get; set; }
        public int SeatCol { get; set; }    
        public string OwnerFullName { get; set; }
        public SceneSeats Scene { get; set; }
    }
}
