using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsLab4
{
    public partial class Form1 : Form
    {
        enum FigType {
            Point, Segment, Polygon
        };



        private Point point;
        private Segment segment;
        private Polygon polygon;
        private int polygonAngles;
        private int anglesRead = 0;
        private Pen redPen = new Pen(Color.Red);
        const float EPS = 1E-9f;
        private FigType figType;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void pointButton_Click(object sender, EventArgs e)
        {
            figType = FigType.Point;
            point = new Point();
        }

        private void segmentButton_Click(object sender, EventArgs e)
        {
            figType = FigType.Segment;
            segment = new Segment();
        }

        private void polygonButton_Click(object sender, EventArgs e)
        {
            figType = FigType.Polygon;
            polygon = new Polygon();
            polygonAngles = int.Parse(angleCount.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            anglesRead = 0;
            polygon.corners.Clear();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (figType)
            {
                case FigType.Point:
                    ((Bitmap)pictureBox1.Image).SetPixel(e.Location.X, e.Location.Y, Color.Red);
                    point.X = e.Location.X;
                    point.Y = e.Location.Y;
                    break;
                case FigType.Segment:
                    if (segment.initialized)
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                        {
                            segment.end = e.Location;
                            g.DrawLine(redPen, segment.start, segment.end);
                            segment.initialized = false;
                        }
                    else
                    {
                        segment.start = e.Location;
                        segment.initialized = true;
                    }
                    break;
                case FigType.Polygon:
                    if (anglesRead == polygonAngles - 1)
                    {
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                        {
                            ++anglesRead;
                            polygon.corners.Add(e.Location);
                            for (int i = 0; i < polygon.corners.Count - 1; ++i)
                                g.DrawLine(redPen, polygon.corners[i], polygon.corners[i + 1]);
                            g.DrawLine(redPen, polygon.corners.Last(), polygon.corners.First());

                        }

                    }
                    else if (anglesRead < polygonAngles - 1)
                    {
                        polygon.corners.Add(e.Location);
                        ++anglesRead;
                    }
                    else
                    {
                        polygon.corners.Clear();
                        anglesRead = 1;
                        polygon.corners.Add(e.Location);
                    }
                    break;
            }
            pictureBox1.Invalidate();
        }

        private void angleCount_TextChanged(object sender, EventArgs e)
        {

        }

        private bool equal(float f, float l)
        {
            return Math.Abs(f - l) < EPS;
        }

        private bool less(float f, float l)
        {
            return (f < l) && (Math.Abs(f - l) >= EPS);
        }

        private bool lessOrEqual(float f, float l)
        {
            return less(f, l) || equal(f, l);
        }

        private bool isCrossed(Point p1, Point p2, Point l1, Point l2)
        {
            float a1 = p1.Y - p2.Y;
            float b1 = p2.X - p1.X;
            float c1 = p1.X * p2.Y - p2.X * p1.Y;

            float a2 = l1.Y - l2.Y;
            float b2 = l2.X - l1.X;
            float c2 = l1.X * l2.Y - l2.X * l1.Y;

            float zn = a1 * b2 - a2 * b1;
            if (Math.Abs(zn) < EPS)
                return false;

            float x = (-1) * (c1 * b2 - c2 * b1) / zn;
            float y = (-1) * (a1 * c2 - a2 * c1) / zn;

            if (equal(x, 0))
                x = 0;
            if (equal(y, 0))
                y = 0;

            bool t1 = lessOrEqual(Math.Min(p1.X, p2.X), x) && lessOrEqual(x, Math.Max(p1.X, p2.X)) && lessOrEqual(Math.Min(p1.Y, p2.Y), y) && lessOrEqual(y, Math.Max(p1.Y, p2.Y));
            bool t2 = lessOrEqual(Math.Min(l1.X, l2.X), x) && lessOrEqual(x, Math.Max(l1.X, l2.X)) && lessOrEqual(Math.Min(l1.Y, l2.Y), y) && lessOrEqual(y, Math.Max(l1.Y, l2.Y));
            return t1 && t2;
        }

        private Point crossPoint(Point l1, Point l2)
        {
            int XX = (l1.X + l2.X) / 2;
            int YY = (l1.Y + l2.Y) / 2;
            return new Point(XX, YY);
        }

        private void PointPolygon_Click(object sender, EventArgs e)
        {
            Point line = new Point(pictureBox1.Width, point.Y);
            var minDist = double.MaxValue;
            var minPoint = new Point();
            var cnt = 0;
            double dist;
            int pC = 0;
            var pnt = new Point();
            for (int i = 0; i < polygon.corners.Count - 1; i++) {
                pnt = crossPoint(polygon.corners[i], polygon.corners[i + 1]);
                dist = Math.Sqrt((point.X - pnt.X) * (point.X - pnt.X) + (point.Y - pnt.Y) * (point.Y - pnt.Y));
                if(dist < minDist)
                {
                    minDist = dist;
                    minPoint = pnt;
                    pC = i;
                }
                cnt += isCrossed(line, point,  polygon.corners[i], polygon.corners[i+1]) ? 1 : 0 ;
            }
            cnt += isCrossed(line, point, polygon.corners.First(), polygon.corners.Last()) ? 1 : 0;
            pnt = crossPoint(polygon.corners.Last(), polygon.corners.First());
            dist = Math.Sqrt((point.X - pnt.X) * (point.X - pnt.X) + (point.Y - pnt.Y) * (point.Y - pnt.Y));
            String leftOrRight = "";
            if (dist < minDist)
            {
                minDist = dist;
                minPoint = pnt;
                pC = polygon.corners.Count - 1;
                leftOrRight = (polygon.corners[0].X - polygon.corners[pC].X) * (point.Y - polygon.corners[pC].Y) - (polygon.corners[0].Y - polygon.corners[pC].Y) * (point.X - polygon.corners[pC].X) < 0
                ? "and right"
                : "and left";
            }else
                leftOrRight = (polygon.corners[pC + 1].X - polygon.corners[pC].X) * (point.Y - polygon.corners[pC].Y) - (polygon.corners[pC + 1].Y - polygon.corners[pC].Y) * (point.X - polygon.corners[pC].X) < 0
                    ? "and right"
                    : "and left";
            var position = cnt % 2 == 0 ? "Outside " : "Inside ";
            pointPolygonInfo.Text = position + leftOrRight;
        }
    }
}
