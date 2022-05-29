using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Rectangle
    {
        protected Point Position { get; set; }
        protected int Width { get; set; }
        protected int Height { get; set; }
        protected Color ColorFill { get; set; }
        protected Color ColorBorder { get; set; }

        public void Paint(Graphics graphics)
        {
            Pen pen = new Pen(ColorBorder, 2);
            Brush brush = new SolidBrush(ColorFill);
            graphics.FillRectangle(brush, Position.X, Position.Y, Width, Height);
            graphics.DrawRectangle(pen, Position.X, Position.Y, Width, Height);
        }

        public bool PointInShape(Point point)
        {
            return Position.X <= point.X && point.X <= Position.X + Width &&
                   Position.Y <= point.Y && point.Y <= Position.Y + Height;
        }
    }
}
