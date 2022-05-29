using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Cinema
{
    class FreeSeat : Seat
    {
        public override void Visiualize(Point Location, int width, int height)
        {
            base.Visiualize(Location, width, height);
            base.ColorFill = Color.Blue;
        }
    }
}
