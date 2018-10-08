using System.Drawing;

namespace GraphicsLab4
{
    public partial class SegmentsIntersection
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.seg1x2 = new System.Windows.Forms.TextBox();
            this.seg1x1 = new System.Windows.Forms.TextBox();
            this.seg1y2 = new System.Windows.Forms.TextBox();
            this.seg1y1 = new System.Windows.Forms.TextBox();
            this.seg2x1 = new System.Windows.Forms.TextBox();
            this.seg2y1 = new System.Windows.Forms.TextBox();
            this.seg2x2 = new System.Windows.Forms.TextBox();
            this.seg2y2 = new System.Windows.Forms.TextBox();
            this.intersectx1 = new System.Windows.Forms.TextBox();
            this.intersecty1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Отрезок 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Отрезок 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Точка пересечения";
            // 
            // seg1x2
            // 
            this.seg1x2.Location = new System.Drawing.Point(519, 68);
            this.seg1x2.Name = "seg1x2";
            this.seg1x2.Size = new System.Drawing.Size(100, 20);
            this.seg1x2.TabIndex = 3;
            // 
            // seg1x1
            // 
            this.seg1x1.Location = new System.Drawing.Point(211, 68);
            this.seg1x1.Name = "seg1x1";
            this.seg1x1.Size = new System.Drawing.Size(100, 20);
            this.seg1x1.TabIndex = 4;
            // 
            // seg1y2
            // 
            this.seg1y2.Location = new System.Drawing.Point(649, 68);
            this.seg1y2.Name = "seg1y2";
            this.seg1y2.Size = new System.Drawing.Size(100, 20);
            this.seg1y2.TabIndex = 5;
            // 
            // seg1y1
            // 
            this.seg1y1.Location = new System.Drawing.Point(380, 68);
            this.seg1y1.Name = "seg1y1";
            this.seg1y1.Size = new System.Drawing.Size(100, 20);
            this.seg1y1.TabIndex = 6;
            // 
            // seg2x1
            // 
            this.seg2x1.Location = new System.Drawing.Point(211, 152);
            this.seg2x1.Name = "seg2x1";
            this.seg2x1.Size = new System.Drawing.Size(100, 20);
            this.seg2x1.TabIndex = 7;
            // 
            // seg2y1
            // 
            this.seg2y1.Location = new System.Drawing.Point(380, 152);
            this.seg2y1.Name = "seg2y1";
            this.seg2y1.Size = new System.Drawing.Size(100, 20);
            this.seg2y1.TabIndex = 8;
            // 
            // seg2x2
            // 
            this.seg2x2.Location = new System.Drawing.Point(519, 152);
            this.seg2x2.Name = "seg2x2";
            this.seg2x2.Size = new System.Drawing.Size(100, 20);
            this.seg2x2.TabIndex = 9;
            // 
            // seg2y2
            // 
            this.seg2y2.Location = new System.Drawing.Point(649, 152);
            this.seg2y2.Name = "seg2y2";
            this.seg2y2.Size = new System.Drawing.Size(100, 20);
            this.seg2y2.TabIndex = 10;
            // 
            // intersectx1
            // 
            this.intersectx1.Location = new System.Drawing.Point(211, 273);
            this.intersectx1.Name = "intersectx1";
            this.intersectx1.Size = new System.Drawing.Size(100, 20);
            this.intersectx1.TabIndex = 11;
            // 
            // intersecty1
            // 
            this.intersecty1.Location = new System.Drawing.Point(380, 273);
            this.intersecty1.Name = "intersecty1";
            this.intersecty1.Size = new System.Drawing.Size(100, 20);
            this.intersecty1.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "x1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "x1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(239, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "x1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(554, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "x2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(554, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "x2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(413, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "y1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(413, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "y1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(687, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "y2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(687, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "y2";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(413, 237);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "y1";
            // 
            // SegmentsIntersection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.intersecty1);
            this.Controls.Add(this.intersectx1);
            this.Controls.Add(this.seg2y2);
            this.Controls.Add(this.seg2x2);
            this.Controls.Add(this.seg2y1);
            this.Controls.Add(this.seg2x1);
            this.Controls.Add(this.seg1y1);
            this.Controls.Add(this.seg1y2);
            this.Controls.Add(this.seg1x1);
            this.Controls.Add(this.seg1x2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SegmentsIntersection";
            this.Text = "SegmentsIntersection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox seg1x2;
        private System.Windows.Forms.TextBox seg1x1;
        private System.Windows.Forms.TextBox seg1y2;
        private System.Windows.Forms.TextBox seg1y1;
        private System.Windows.Forms.TextBox seg2x1;
        private System.Windows.Forms.TextBox seg2y1;
        private System.Windows.Forms.TextBox seg2x2;
        private System.Windows.Forms.TextBox seg2y2;
        private System.Windows.Forms.TextBox intersectx1;
        private System.Windows.Forms.TextBox intersecty1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}