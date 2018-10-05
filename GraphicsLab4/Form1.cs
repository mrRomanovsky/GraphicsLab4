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

        private FigType figType;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void pointButton_Click(object sender, EventArgs e)
        {
            figType = FigType.Point;
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
    }
}
