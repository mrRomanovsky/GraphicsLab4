using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLab4
{
    class Polygon
    {
        public Polygon()
        {
            corners = new List<Point>();
        }

        public Polygon(List<Point> points)
        {
            corners = points;
        }
        public List<Point> corners;
    }
}
