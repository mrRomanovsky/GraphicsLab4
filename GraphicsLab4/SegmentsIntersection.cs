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
    public partial class SegmentsIntersection : Form
    {
        public SegmentsIntersection()
        {
            InitializeComponent();
        }

        public SegmentsIntersection(Segment seg1, Segment seg2, PointF intersect)
        {

          /*  seg1x1 = new TextBox();
            seg1y1 = new TextBox();
            seg1x2 = new TextBox();
            seg1y2 = new TextBox();
            seg2x1 = new TextBox();
            seg2y1 = new TextBox();
            seg2x2 = new TextBox();
            seg2y2 = new TextBox();*/
            InitializeComponent();
            //intersectx1 = new TextBox();
            //intersecty1 = new TextBox();
            seg1x1.Text = seg1.start.X.ToString();
            seg1y1.Text = seg1.start.Y.ToString();
            seg1x2.Text = seg1.end.X.ToString();
            seg1y2.Text = seg1.end.Y.ToString();
            seg2x1.Text = seg2.start.X.ToString();
            seg2y1.Text = seg2.start.Y.ToString();
            seg2x2.Text = seg2.end.X.ToString();
            seg2y2.Text = seg2.end.Y.ToString();
            intersectx1.Text = intersect.X.ToString();
            intersecty1.Text = intersect.Y.ToString();

        }
    }
}
