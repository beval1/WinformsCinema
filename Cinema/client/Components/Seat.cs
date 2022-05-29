using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Cinema
{
    public abstract class Seat : Rectangle
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Selected { get; set; }  
        public virtual void Visiualize(Point Location, int width, int height)
        {
            base.Position = Location;
            base.Width = width;
            base.Height = height;
        }
        public void SetColor(Color color)
        {
            ColorFill = color;
            Selected = true;
        }
    }
}
