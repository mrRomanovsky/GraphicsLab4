using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLab4
{
    class Segment
    {
        public Segment()
        {
            start = new Point();
            end = new Point();
            initialized = false;
        }

        public Segment(Point p1, Point p2)
        {
            start = p1;
            end = p2;
            initialized = true;
        }

        public Point start;
        public Point end;
        public bool initialized;
    }
}
