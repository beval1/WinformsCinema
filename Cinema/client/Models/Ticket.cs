using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Ticket
    {
        public string Uuid { get; set; }
        public int ProjectionId { get; set; }
        public int SeatRow { get;  set; }
        public int SeatCol { get; set; }
        public string OwnerFullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
