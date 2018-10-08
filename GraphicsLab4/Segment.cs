using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLab4
{
    public class Line
    {
        public double a;
        public double b;
        public double c;

        public Line() { }

        public Line(PointF p, PointF q)
        {
            a = p.Y - q.Y;
            b = q.X - p.X;
            c = -a * p.X - b * p.Y;
            norm();
        }

        public void norm()
        {
            double z = Math.Sqrt(a * a + b * b);
            if (Math.Abs(z) > EPS)
            {
                a /= z;
                b /= z;
                c /= z;
            }
        }

        public double dist(PointF p) {
            return a* p.X + b* p.Y + c;
        }

    public static double EPS = 1E-9f;
    }

    /*
     * 	double a, b, c;
 
	line() {}
	line (pt p, pt q) {
		a = p.y - q.y;
		b = q.x - p.x;
		c = - a * p.x - b * p.y;
		norm();
	}
 
	void norm() {
		double z = sqrt (a*a + b*b);
		if (abs(z) > EPS)
			a /= z,  b /= z,  c /= z;
	}
 
	double dist (pt p) const {
		return a * p.x + b * p.y + c;
	}
};
     */
    public class Segment
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

        public static double EPS = 1E-9f;

        public static bool between(double l, double r, double x)
        {
            return Math.Min(l, r) <= x + EPS && x <= Math.Max(l, r) + EPS;
        }

        public static bool intersect_1d(double a, double b, double c, double d)
        {
            double t;
            if (a > b)
            {
                t = a;
                a = b;
                b = t;
            }

            if (c > d)
            {
                t = c;
                c = d;
                d = t;
            }
            return Math.Max(a, c) <= Math.Min(b, d) + EPS;
        }

        private static double det(double a, double b, double c, double d)
        {
            return a * d - b * c;
        }

        private static bool less(PointF a, PointF b)
        {
                return a.X < b.X - EPS || Math.Abs(a.X - b.X) < EPS && a.Y < b.Y - EPS;
        }

        /*
         * struct line {

         */
        public static bool intersect(PointF a, PointF b, PointF c, PointF d, ref PointF left, ref PointF right)
        {
            if (!intersect_1d(a.X, b.X, c.X, d.X) || !intersect_1d(a.Y, b.Y, c.Y, d.Y))
                return false;
            //line m(a, b);
            //line n(c, d);
            Line m = new Line(a, b);
            Line n = new Line(c, d);
            double zn = det(m.a, m.b, n.a, n.b);
            float xtmp;
            float ytmp;
            if (Math.Abs(zn) < EPS)
            {
                if (Math.Abs(m.dist(c)) > EPS || Math.Abs(n.dist(a)) > EPS)
                    return false;
                if (less(b, a))
                {
                    xtmp = b.X;
                    ytmp = b.Y;
                    b.X = a.X;
                    b.Y = a.Y;
                    a.X = xtmp;
                    a.Y = ytmp;
                    //swap(a, b);
                }

                if (less(d, c))
                {
                    xtmp = c.X;
                    ytmp = c.Y;
                    c.X = d.X;
                    c.Y = d.Y;
                    d.X = xtmp;
                    d.Y = ytmp;
                    //swap(c, d);
                }

                left = less(a, c) ? c : a;
                right = less(b, d) ? b : d;
                return true;
            }
            else
            {
                left.X = right.X = (float)(-det(m.c, m.b, n.c, n.b) / zn);
                left.Y = right.Y = (float)(-det(m.a, m.c, n.a, n.c) / zn);
                return between(a.X, b.X, left.X)
                       && between(a.Y, b.Y, left.Y)
                       && between(c.X, d.X, left.X)
                       && between(c.Y, d.Y, left.Y);
            }
        }
    }
}
