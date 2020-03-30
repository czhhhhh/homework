using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CayleyTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (graphics == null)
                {
                    graphics = this.panel1.CreateGraphics();
                }
                if (textBox1.Text == null) throw new Exception();
                drawCayleyTree(int.Parse(textBox1.Text), x0, y0, 100, -Math.PI / 2);
            }catch
            {
                MessageBox.Show("深度不能为空", "Operation Exception");
            }
            
        }

        private Graphics graphics;
        
        double per1 = 0.6;
        double x0 = 200;
        double y0 = 300;
        double per2 = 0.7;
        public Color brushColor = new Color();

        void drawCayleyTree(int n,double x0,double y0,double leng,double th)
        {
            if (n == 0) return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            
            Pen pen = new Pen(colorDialog1.Color);
            graphics.DrawLine(pen, (int)x0, (int)y0, (int)x1, (int)y1);

            double th1 = trackBar1.Value * Math.PI / 180;
            double th2 = trackBar2.Value * Math.PI / 180;

            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayleyTree(n - 1, x1, y1, per2 * leng, th - th2);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(panel1.BackColor);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (graphics == null) return;
            graphics.Clear(panel1.BackColor);
            drawCayleyTree(int.Parse(textBox1.Text), x0, y0, 100, -Math.PI / 2);
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (graphics == null) return;
            graphics.Clear(panel1.BackColor);
            drawCayleyTree(int.Parse(textBox1.Text), x0, y0, 100, -Math.PI / 2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (graphics == null) return;
            graphics.Clear(panel1.BackColor);
            drawCayleyTree(int.Parse(textBox1.Text), x0, y0, 100, -Math.PI / 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult color = colorDialog1.ShowDialog();
            if (color == DialogResult.OK)
            {
                if (graphics == null) return;
                graphics.Clear(panel1.BackColor);
                drawCayleyTree(int.Parse(textBox1.Text), x0, y0, 100, -Math.PI / 2);
            }
        }
    }
}
