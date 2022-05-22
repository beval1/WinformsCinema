using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    class Ticket
    {
        public string Uuid { get; private set; }
        public int ProjectionId { get; private set; }
        public int SeatId { get; private set; }
        public string OwnerFullName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
