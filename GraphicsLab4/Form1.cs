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

        private Point point = new Point(-1,-1);
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
            tasksPanels = new List<Panel>(7);
            InitializeTasksPanels();
        }

        private void InitializeTasksPanels()
        {
            Initialize3TaskPanel();
            Initialize4TaskPanel();
            Initialize5TaskPanel();
        }

        private void pointButton_Click(object sender, EventArgs e)
        {
            foreach (var item in tasksPanels)
            {
                item.Enabled = false;
                item.Visible = false;
            }
            mode = "point";
            figType = FigType.Point;
        }

        private void segmentButton_Click(object sender, EventArgs e)
        {
            foreach (var item in tasksPanels)
            {
                item.Enabled = false;
                item.Visible = false;
            }
            mode = "segment";
            figType = FigType.Segment;
            segment = new Segment();
        }

        private void polygonButton_Click(object sender, EventArgs e)
        {
            foreach (var item in tasksPanels)
            {
                item.Enabled = false;
                item.Visible = false;
            }
            mode = "polygon";
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
            if(polygon != null)
                polygon.corners.Clear();
        }

        private void DrawSegment(Segment segment, Pen pen)
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.DrawLine(pen, segment.start, segment.end);
                segment.initialized = false;
            }
        }

        private void DrawPolygon(Polygon polygon, Pen pen)
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                for (int i = 0; i < polygon.corners.Count - 1; ++i)
                    g.DrawLine(pen, polygon.corners[i], polygon.corners[i + 1]);
                g.DrawLine(pen, polygon.corners.Last(), polygon.corners.First());

            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        { 
            if (mode == "aroundPoint")
            {
                centrePoint = e.Location;
                tasksPanels[2].Controls.Find("task5LabelDirection", false).First().Text = "Введите угол...";
                return;
            }
            switch (figType)
            {
                case FigType.Point:
                    point = e.Location;
                    ((Bitmap)pictureBox1.Image).SetPixel(e.Location.X, e.Location.Y, Color.Red);
                    break;
                case FigType.Segment:
                    if (segment.initialized)
                    {
                        segment.end = e.Location;
                        DrawSegment(segment, redPen);
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
                        polygon.corners.Add(e.Location);
                        ++anglesRead;
                        DrawPolygon(polygon, redPen);
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (figType == 0 && point == new Point(-1, -1))
                return;
            foreach (var item in tasksPanels)
            {
                item.Enabled = false;
                item.Visible = false;
            }
            tasksPanels[((ListBox)sender).SelectedIndex].Enabled = true;
            tasksPanels[((ListBox)sender).SelectedIndex].Visible = true;
            if (((ListBox)sender).SelectedIndex == 1)
            {
                mode = "90turn";
            }
            if (((ListBox)sender).SelectedIndex == 2)
            {
                mode = "aroundPoint";
                tasksPanels[2].Controls.Find("task5LabelDirection", false).First().Text = "Выберите точку..."; 
            }
        }
      
        #region tasks3_4_5
        private string mode = "";
        private List<string> activeItems = new List<string>();
        private List<System.Windows.Forms.Panel> tasksPanels;
        private Point centrePoint = new Point(-1, -1);
        private int[,] MultMatrix(int [,] a, int [,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            int[,] r = new int[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        private double[,] MultMatrix(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        private bool CheckCorrectPoint(double w, double h) =>
             w > 0 && w < pictureBox1.Width && 0 < h && h < pictureBox1.Height;

        #region task3
        private void moveButton_Click(object sender, EventArgs e)
        {
            if (figType == 0 && point == new Point(-1, -1))
                return;
            mode = "move";
            foreach (var ai in activeItems)
            {
                var item = tasksPanels[0].Controls.Find(ai, false).First();
                item.Visible = false;
                item.Enabled = false;
            }
            var strs = new List<string> { "task3LabelDirection", "task3XLabel", "task3YLabel", "task3XTextBox", "task3YTextBox", "task3ButtonOk" };
            foreach (var s in strs)
            {
                var item = tasksPanels[0].Controls.Find(s, false).First();
                if (item.Name == "task3LabelDirection")
                    item.Text = "Задайте смещение: ";
                else if (item.Name == "task3ButtonOk")
                    item.Text = "Сместить";
                item.Visible = true;
                item.Enabled = true;
            }
            activeItems = strs;
        }

        private void turnButton_Click(object sender, EventArgs e)
        {
            if (figType == 0 && point == new Point(-1, -1))
                return;
            mode = "turn";
            foreach (var ai in activeItems)
            {
                var item = tasksPanels[0].Controls.Find(ai, false).First();
                item.Visible = false;
                item.Enabled = false;
            }
            var strs = new List<string> { "task3LabelDirection", "task3AngleLabel", "task3AngleTextBox", "task3ButtonOk" };
            foreach (var s in strs)
            {
                var item = tasksPanels[0].Controls.Find(s, false).First();
                if (item.Name == "task3LabelDirection")
                    item.Text = "Задайте поворот: ";
                else if (item.Name == "task3ButtonOk")
                    item.Text = "Повернуть";
                item.Visible = true;
                item.Enabled = true;
            }
            activeItems = strs;
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            if (figType == 0 && point == new Point(-1, -1))
                return;
            mode = "scale";
            foreach (var ai in activeItems)
            {
                var item = tasksPanels[0].Controls.Find(ai, false).First();
                item.Visible = false;
                item.Enabled = false;
            }
            var strs = new List<string> { "task3LabelDirection", "task3AngleLabel", "task3AngleTextBox", "task3ButtonOk" };
            foreach (var s in strs)
            {
                var item = tasksPanels[0].Controls.Find(s, false).First();
                if (item.Name == "task3AngleLabel")
                    item.Text = "Значение: ";
                else if (item.Name == "task3LabelDirection")
                    item.Text = "Задайте масштаб: ";
                else if (item.Name == "task3ButtonOk")
                    item.Text = "Масштабировать";
                item.Visible = true;
                item.Enabled = true;
            }
            activeItems = strs;
        }

        private void Initialize3TaskPanel()
        {
            var task3Panel = new Panel();
            task3Panel.Visible = false;
            task3Panel.Enabled = false;
            task3Panel.Location = new Point(600, 220);
            task3Panel.Size = new Size(190, 180);
            this.Controls.Add(task3Panel);
            tasksPanels.Add(task3Panel);
            var task3LabelAbout = new Label();
            task3LabelAbout.Location = new Point(22, 2);
            task3LabelAbout.Size = new Size(180, 50);
            task3LabelAbout.Text
                = "Применение аффинных преобразований к примитиву: смещение, поворот, масштаб.";
            var task3ButtonMove = new Button();
            task3ButtonMove.Text = "C";
            task3ButtonMove.Location = new Point(27, 47);
            task3ButtonMove.Size = new Size(40, 22);
            task3ButtonMove.FlatStyle = FlatStyle.Flat;
            task3ButtonMove.Click += moveButton_Click;
            var task3ButtonTurn = new Button();
            task3ButtonTurn.Text = "П";
            task3ButtonTurn.Click += turnButton_Click;
            task3ButtonTurn.Location = new Point(85, 47);
            task3ButtonTurn.Size = new Size(40, 22);
            task3ButtonTurn.FlatStyle = FlatStyle.Flat;
            var task3ButtonScale = new Button();
            task3ButtonScale.Text = "М";
            task3ButtonScale.Location = new Point(140, 47);
            task3ButtonScale.Click += scaleButton_Click;
            task3ButtonScale.Size = new Size(40, 22);
            task3ButtonScale.FlatStyle = FlatStyle.Flat;
            task3Panel.Controls.Add(task3ButtonScale);
            task3Panel.Controls.Add(task3ButtonTurn);
            task3Panel.Controls.Add(task3ButtonMove);
            task3Panel.Controls.Add(task3LabelAbout);
            var task3LabelDirection = new Label();
            task3LabelDirection.Text = "Задайте смещение: ";
            task3LabelDirection.Name = "task3LabelDirection";
            task3LabelDirection.Location = new Point(22, 80);
            task3LabelDirection.Size = new Size(160, 30);
            task3LabelDirection.Enabled = false;
            task3LabelDirection.Visible = false;
            task3Panel.Controls.Add(task3LabelDirection);

            var task3XLabel = new Label();
            task3XLabel.Text = "X: ";
            task3XLabel.Name = "task3XLabel";
            task3XLabel.Location = new Point(30, 110);
            task3XLabel.Size = new Size(20, 20);
            task3XLabel.Enabled = false;
            task3XLabel.Visible = false;
            task3Panel.Controls.Add(task3XLabel);
            var task3XTextBox = new TextBox();
            task3XTextBox.Text = "0";
            task3XTextBox.Name = "task3XTextBox";
            task3XTextBox.Location = new Point(60, 110);
            task3XTextBox.Size = new Size(30, 20);
            task3XTextBox.Enabled = false;
            task3XTextBox.Visible = false;
            task3Panel.Controls.Add(task3XTextBox);

            var task3YLabel = new Label();
            task3YLabel.Text = "Y: ";
            task3YLabel.Name = "task3YLabel";
            task3YLabel.Location = new Point(100, 110);
            task3YLabel.Size = new Size(20, 20);
            task3YLabel.Enabled = false;
            task3YLabel.Visible = false;
            task3Panel.Controls.Add(task3YLabel);
            var task3YTextBox = new TextBox();
            task3YTextBox.Text = "0";
            task3YTextBox.Name = "task3YTextBox";
            task3YTextBox.Location = new Point(130, 110);
            task3YTextBox.Size = new Size(30, 20);
            task3YTextBox.Enabled = false;
            task3YTextBox.Visible = false;
            task3Panel.Controls.Add(task3YTextBox);

            var task3AngleLabel = new Label();
            task3AngleLabel.Text = "Угол: ";
            task3AngleLabel.Name = "task3AngleLabel";
            task3AngleLabel.Location = new Point(50, 110);
            task3AngleLabel.Size = new Size(40, 20);
            task3AngleLabel.Enabled = false;
            task3AngleLabel.Visible = false;
            task3Panel.Controls.Add(task3AngleLabel);
            var task3AngleTextBox = new TextBox();
            task3AngleTextBox.Text = "0";
            task3AngleTextBox.Name = "task3AngleTextBox";
            task3AngleTextBox.Location = new Point(95, 110);
            task3AngleTextBox.Size = new Size(30, 20);
            task3AngleTextBox.Enabled = false;
            task3AngleTextBox.Visible = false;
            task3Panel.Controls.Add(task3AngleTextBox);

            var task3ButtonOk = new Button();
            task3ButtonOk.Text = "Сместить";
            task3ButtonOk.Location = new Point(60, 150);
            task3ButtonOk.Name = "task3ButtonOk";
            task3ButtonOk.Click += task3ButtonOk_Click;
            task3ButtonOk.Size = new Size(80, 23);
            task3ButtonOk.Enabled = false;
            task3ButtonOk.Visible = false; task3ButtonOk.FlatStyle = FlatStyle.Flat;
            task3Panel.Controls.Add(task3ButtonOk);
        }

        private void ScaleFigure()
        {
            var t1 = double.Parse(tasksPanels[0].Controls.Find("task3AngleTextBox", false).First().Text);
            if (t1 == 0)
                return;
            var matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, t1 } };
            switch (figType)
            {
                case FigType.Point:
                    break;
                case FigType.Segment:
                    DrawSegment(segment, new Pen(this.BackColor));
                    var res1 = MultMatrix(new double[,] { { segment.start.X, segment.start.Y, 1 }, { segment.end.X, segment.end.Y, 1 } }, matrix);
                    if (CheckCorrectPoint(res1[0, 0] / t1, res1[0, 1] / t1) && CheckCorrectPoint(res1[1, 0] / t1, res1[1, 1] / t1))
                    {
                        segment.start = new Point((int)(res1[0, 0] / t1), (int)(res1[0, 1] / t1));
                        segment.end = new Point((int)(res1[1, 0] / t1), (int)(res1[1, 1] / t1));
                    }
                    DrawSegment(segment, redPen);
                    pictureBox1.Invalidate();
                    break;
                case FigType.Polygon:
                    DrawPolygon(polygon, new Pen(this.BackColor));
                    var matrix2 = new double[polygon.corners.Count, 3];
                    for (int i = 0; i < polygon.corners.Count; ++i)
                    {
                        matrix2[i, 0] = polygon.corners[i].X;
                        matrix2[i, 1] = polygon.corners[i].Y;
                        matrix2[i, 2] = 1;
                    }
                    var res2 = MultMatrix(matrix2, matrix);
                    bool correctValue = true;
                    var p = new Polygon();
                    for (int i = 0; i < polygon.corners.Count(); ++i)
                    {
                        if (!CheckCorrectPoint(res2[i, 0] / t1, res2[i, 1] / t1))
                        {
                            correctValue = false;
                            break;
                        }
                        p.corners.Add(new Point((int)(res2[i, 0] / t1), (int)(res2[i, 1] / t1)));
                    }
                    if (correctValue)
                    {
                        polygon = p;
                    }
                    DrawPolygon(polygon, redPen);
                    pictureBox1.Invalidate(); break;
                default:
                    break;
            }
        }

        private void TurnFigure()
        {
            var t1 = double.Parse(tasksPanels[0].Controls.Find("task3AngleTextBox", false).First().Text);
            var matrix = new double[,] { { Math.Cos(Math.PI * t1 / 180), Math.Sin(Math.PI * t1 / 180), 0 }, { -Math.Sin(Math.PI * t1 / 180), Math.Cos(Math.PI * t1 / 180), 0 }, { 0, 0, 1 } };
            switch (figType)
            {
                case FigType.Point:
                    break;
                case FigType.Segment:
                    DrawSegment(segment, new Pen(this.BackColor));
                    var res1 = MultMatrix(new double[,] { { segment.start.X, segment.start.Y, 1 }, { segment.end.X, segment.end.Y, 1 } }, matrix);
                    if (CheckCorrectPoint(res1[0, 0], res1[0, 1]) && CheckCorrectPoint(res1[1, 0], res1[1, 1]))
                    {
                        segment.start = new Point((int)res1[0, 0], (int)res1[0, 1]);
                        segment.end = new Point((int)res1[1, 0], (int)res1[1, 1]);
                    }
                    DrawSegment(segment, redPen);
                    pictureBox1.Invalidate();
                    break;
                case FigType.Polygon:
                    DrawPolygon(polygon, new Pen(this.BackColor));
                    double[,] matrix2 = new double[polygon.corners.Count, 3];
                    for (int i = 0; i < polygon.corners.Count; ++i)
                    {
                        matrix2[i, 0] = polygon.corners[i].X;
                        matrix2[i, 1] = polygon.corners[i].Y;
                        matrix2[i, 2] = 1;
                    }
                    var res2 = MultMatrix(matrix2, matrix);
                    bool correctValue = true;
                    var p = new Polygon();
                    for (int i = 0; i < polygon.corners.Count(); ++i)
                    {
                        if (!CheckCorrectPoint(res2[i, 0], res2[i, 1]))
                        {
                            correctValue = false;
                            break;
                        }
                        p.corners.Add(new Point((int)res2[i, 0], (int)res2[i, 1]));
                    }
                    if (correctValue)
                    {
                        polygon = p;
                    }
                    DrawPolygon(polygon, redPen);
                    pictureBox1.Invalidate(); break;
                default:
                    break;
            }
        }

        void MoveFigure()
        {
            var t1 = tasksPanels[0].Controls.Find("task3XTextBox", false).First().Text;
            var t2 = tasksPanels[0].Controls.Find("task3YTextBox", false).First().Text;
            var matrix = new int[,] { { 1, 0, 0 }, { 0, 1, 0 }, { int.Parse(t1), int.Parse(t2), 1 } };
            switch (figType)
            {
                case FigType.Point:
                    ((Bitmap)pictureBox1.Image).SetPixel(point.X, point.Y, this.BackColor);
                    var res = MultMatrix(new int[,] { { point.X, point.Y, 1 } }, matrix);
                    if (CheckCorrectPoint(res[0, 0], res[0, 1]))
                        point = new Point(res[0, 0], res[0, 1]);
                    ((Bitmap)pictureBox1.Image).SetPixel(point.X, point.Y, redPen.Color);
                    pictureBox1.Invalidate();
                    break;
                case FigType.Segment:
                    DrawSegment(segment, new Pen(this.BackColor));
                    var res1 = MultMatrix(new int[,] { { segment.start.X, segment.start.Y, 1 }, { segment.end.X, segment.end.Y, 1 } }, matrix);
                    if (CheckCorrectPoint(res1[0, 0], res1[0, 1]) && CheckCorrectPoint(res1[1, 0], res1[1, 1]))
                    {
                        segment.start = new Point(res1[0, 0], res1[0, 1]);
                        segment.end = new Point(res1[1, 0], res1[1, 1]);
                    }
                    DrawSegment(segment, redPen);
                    pictureBox1.Invalidate();
                    break;
                case FigType.Polygon:
                    DrawPolygon(polygon, new Pen(this.BackColor));
                    int[,] matrix2 = new int[polygon.corners.Count, 3];
                    for (int i = 0; i < polygon.corners.Count; ++i)
                    {
                        matrix2[i, 0] = polygon.corners[i].X;
                        matrix2[i, 1] = polygon.corners[i].Y;
                        matrix2[i, 2] = 1;
                    }
                    var res2 = MultMatrix(matrix2, matrix);
                    bool correctValue = true;
                    var p = new Polygon();
                    for (int i = 0; i < polygon.corners.Count(); ++i)
                    {
                        if (!CheckCorrectPoint(res2[i, 0], res2[i, 1]))
                        {
                            correctValue = false;
                            break;
                        }
                        p.corners.Add(new Point(res2[i, 0], res2[i, 1]));
                    }
                    if (correctValue)
                    {
                        polygon = p;
                    }
                    DrawPolygon(polygon, redPen);
                    pictureBox1.Invalidate();
                    break;
                default:
                    break;
            }
        }
        private void task3ButtonOk_Click(object sender, EventArgs e)
        {
            if (mode == "move")
                MoveFigure();
            else if (mode == "turn")
                TurnFigure();
            else if (mode == "scale")
                ScaleFigure();
        }
        #endregion

        #region task4
        private void Initialize4TaskPanel()
        {
            var task4Panel = new Panel();
            task4Panel.Visible = false;
            task4Panel.Enabled = false;
            task4Panel.Location = new Point(600, 220);
            task4Panel.Size = new Size(190, 180);
            this.Controls.Add(task4Panel);
            tasksPanels.Add(task4Panel);
            var task4LabelAbout = new Label();
            task4LabelAbout.Location = new Point(22, 2);
            task4LabelAbout.Size = new Size(180, 50);
            task4LabelAbout.Text
                = "Поворот ребра на 90 градусов вокруг своего центра.";
            
            var task4ButtonOk = new Button();
            task4ButtonOk.Text = "Повернуть";
            task4ButtonOk.Location = new Point(60, 50);
            task4ButtonOk.Name = "task4ButtonOk";
            task4ButtonOk.Click += task4ButtonOk_Click;
            task4ButtonOk.Size = new Size(80, 23);
            task4ButtonOk.FlatStyle = FlatStyle.Flat;
            task4Panel.Controls.Add(task4ButtonOk);
        }

        private void task4ButtonOk_Click(object sender, EventArgs e)
        {
            mode = "aroundPoint";
            var t1 = 90;
            Point c = new Point(0,0);
            if (figType == FigType.Segment)
            {
                c = new Point((segment.start.X + segment.end.X) / 2, (segment.start.Y + segment.end.Y) / 2);
            }
            else if (figType == FigType.Polygon)
            {
                foreach (var item in polygon.corners)
                {
                    c.X += item.X;
                    c.Y += item.Y;
                }
                c.X = (int)(c.X / polygon.corners.Count);
                c.Y = (int)(c.Y / polygon.corners.Count);
            }
            var matrix = new double[,] {
                {Math.Cos(Math.PI * t1 / 180), Math.Sin(Math.PI * t1 / 180), 0 },
                { -Math.Sin(Math.PI * t1 / 180), Math.Cos(Math.PI * t1 / 180), 0 },
                { -c.X * Math.Cos(Math.PI * t1 / 180) + c.Y * Math.Sin(Math.PI * t1 / 180) + c.X,
                    -c.X * Math.Sin(Math.PI * t1 / 180) - c.Y * Math.Cos(Math.PI * t1 / 180) + c.Y,1 } };
            switch (figType)
            {
                case FigType.Point:
                    break;
                case FigType.Segment:
                    DrawSegment(segment, new Pen(this.BackColor));
                    var res1 = MultMatrix(new double[,] { { segment.start.X, segment.start.Y, 1 }, { segment.end.X, segment.end.Y, 1 } }, matrix);
                    if (CheckCorrectPoint(res1[0, 0], res1[0, 1]) && CheckCorrectPoint(res1[1, 0], res1[1, 1]))
                    {
                        segment.start = new Point((int)res1[0, 0], (int)res1[0, 1]);
                        segment.end = new Point((int)res1[1, 0], (int)res1[1, 1]);
                    }
                    DrawSegment(segment, redPen);
                    pictureBox1.Invalidate();
                    break;
                case FigType.Polygon:
                    DrawPolygon(polygon, new Pen(this.BackColor));
                    double[,] matrix2 = new double[polygon.corners.Count, 3];
                    for (int i = 0; i < polygon.corners.Count; ++i)
                    {
                        matrix2[i, 0] = polygon.corners[i].X;
                        matrix2[i, 1] = polygon.corners[i].Y;
                        matrix2[i, 2] = 1;
                    }
                    var res2 = MultMatrix(matrix2, matrix);
                    bool correctValue = true;
                    var p = new Polygon();
                    for (int i = 0; i < polygon.corners.Count(); ++i)
                    {
                        if (!CheckCorrectPoint(res2[i, 0], res2[i, 1]))
                        {
                            correctValue = false;
                            break;
                        }
                        p.corners.Add(new Point((int)res2[i, 0], (int)res2[i, 1]));
                    }
                    if (correctValue)
                    {
                        polygon = p;
                    }
                    DrawPolygon(polygon, redPen);
                    pictureBox1.Invalidate(); break;
                default:
                    break;
            }
        }
        #endregion

        #region task5
        private void Initialize5TaskPanel()
        {
            var task5Panel = new Panel();
            task5Panel.Visible = false;
            task5Panel.Enabled = false;
            task5Panel.Location = new Point(600, 220);
            task5Panel.Size = new Size(190, 180);
            this.Controls.Add(task5Panel);
            tasksPanels.Add(task5Panel);
            var task5LabelAbout = new Label();
            task5LabelAbout.Location = new Point(22, 2);
            task5LabelAbout.Size = new Size(180, 50);
            task5LabelAbout.Text
                = "Поворот примитива на заданный угол вокруг другой точки (задание точки мышкой).";
            task5Panel.Controls.Add(task5LabelAbout);
            var task5LabelDirection = new Label();
            task5LabelDirection.Text = "Выберите точку...";
            task5LabelDirection.Name = "task5LabelDirection";
            task5LabelDirection.Location = new Point(22, 50);
            task5LabelDirection.Size = new Size(160, 30);
            task5Panel.Controls.Add(task5LabelDirection);

            var task5AngleLabel = new Label();
            task5AngleLabel.Text = "Угол: ";
            task5AngleLabel.Name = "task5AngleLabel";
            task5AngleLabel.Location = new Point(50, 110);
            task5AngleLabel.Size = new Size(40, 20);
            task5Panel.Controls.Add(task5AngleLabel);
            var task5AngleTextBox = new TextBox();
            task5AngleTextBox.Text = "0";
            task5AngleTextBox.Name = "task5AngleTextBox";
            task5AngleTextBox.Location = new Point(95, 110);
            task5AngleTextBox.Size = new Size(30, 20);
            task5Panel.Controls.Add(task5AngleTextBox);

            var task5ButtonOk = new Button();
            task5ButtonOk.Text = "Повернуть";
            task5ButtonOk.Location = new Point(60, 150);
            task5ButtonOk.Name = "task5ButtonOk";
            task5ButtonOk.Click += task5ButtonOk_Click;
            task5ButtonOk.Size = new Size(80, 23);
            task5ButtonOk.FlatStyle = FlatStyle.Flat;
            task5Panel.Controls.Add(task5ButtonOk);
        }

        private void task5ButtonOk_Click(object sender, EventArgs e)
        {
            mode = "aroundPoint";
            var t1 = double.Parse(tasksPanels[2].Controls.Find("task5AngleTextBox", false).First().Text);
            if (t1 == 0 || centrePoint == new Point(-1, -1))
                return;
            var matrix = new double[,] {
                {Math.Cos(Math.PI * t1 / 180), Math.Sin(Math.PI * t1 / 180), 0 },
                { -Math.Sin(Math.PI * t1 / 180), Math.Cos(Math.PI * t1 / 180), 0 },
                { -centrePoint.X * Math.Cos(Math.PI * t1 / 180) + centrePoint.Y * Math.Sin(Math.PI * t1 / 180) + centrePoint.X,
                    -centrePoint.X * Math.Sin(Math.PI * t1 / 180) - centrePoint.Y * Math.Cos(Math.PI * t1 / 180) + centrePoint.Y,1 } };
            switch (figType)
            {
                case FigType.Point:
                    ((Bitmap)pictureBox1.Image).SetPixel(point.X, point.Y, this.BackColor);
                    var res = MultMatrix(new double[,] { { point.X, point.Y, 1 } }, matrix);
                    if (CheckCorrectPoint(res[0, 0], res[0, 1]))
                        point = new Point((int)res[0, 0], (int)res[0, 1]);
                    ((Bitmap)pictureBox1.Image).SetPixel(point.X, point.Y, redPen.Color);
                    pictureBox1.Invalidate();
                    break;
                case FigType.Segment:
                    DrawSegment(segment, new Pen(this.BackColor));
                    var res1 = MultMatrix(new double[,] { { segment.start.X, segment.start.Y, 1 }, { segment.end.X, segment.end.Y, 1 } }, matrix);
                    if (CheckCorrectPoint(res1[0, 0], res1[0, 1]) && CheckCorrectPoint(res1[1, 0], res1[1, 1]))
                    {
                        segment.start = new Point((int)res1[0, 0], (int)res1[0, 1]);
                        segment.end = new Point((int)res1[1, 0], (int)res1[1, 1]);
                    }
                    DrawSegment(segment, redPen);
                    pictureBox1.Invalidate();
                    break;
                case FigType.Polygon:
                    DrawPolygon(polygon, new Pen(this.BackColor));
                    double[,] matrix2 = new double[polygon.corners.Count, 3];
                    for (int i = 0; i < polygon.corners.Count; ++i)
                    {
                        matrix2[i, 0] = polygon.corners[i].X;
                        matrix2[i, 1] = polygon.corners[i].Y;
                        matrix2[i, 2] = 1;
                    }
                    var res2 = MultMatrix(matrix2, matrix);
                    bool correctValue = true;
                    var p = new Polygon();
                    for (int i = 0; i < polygon.corners.Count(); ++i)
                    {
                        if (!CheckCorrectPoint(res2[i, 0], res2[i, 1]))
                        {
                            correctValue = false;
                            break;
                        }
                        p.corners.Add(new Point((int)res2[i, 0], (int)res2[i, 1]));
                    }
                    if (correctValue)
                    {
                        polygon = p;
                    }
                    DrawPolygon(polygon, redPen);
                    pictureBox1.Invalidate(); break;
                default:
                    break;
            }
        }
        #endregion

        #endregion
    }
}
