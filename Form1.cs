using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace infinite_field
{
    public partial class Form1 : Form
    {
        double zoom = 1.0f;
        Timer t = new Timer();
        Bitmap bg;
        int n = 7;
        bool drawed = false;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            t.Interval = 30;
            t.Tick += T_Tick;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            drawed = false;
            switch (e.KeyCode)
            {

                case Keys.Up:
                    zoom -= 0.01;
                    break;
                case Keys.Down:
                    zoom += 0.01;
                    break;
                case Keys.Right:
                    n++;
                    zoom = 1.0f;
                    break;
                case Keys.Left:
                    n--;
                    zoom = 1.0f;
                    break;
            }
            this.Text = "zoom: " + zoom+" n: "+n;
        }

        double fmod(double x,double y)
        {
            return ((x / y) - (int)(x / y)) * y;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(ClientSize.Width != 0 && ClientSize.Height != 0)
            {
                bg.Dispose();
                bg = new Bitmap(ClientSize.Width, ClientSize.Height);
                draw(e.Graphics);
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            draw(CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
            bg = new Bitmap(ClientSize.Width, ClientSize.Height);
            t.Start();
        }

        void draw(Graphics g)
        {
            if (bg != null && drawed==false) 
            {
                buff(Graphics.FromImage(bg));
                g.DrawImage(bg, 0, 0);
                drawed = true;
            }
            g.Dispose();
        }
        void buff(Graphics g)
        {
            g.Clear(BackColor);
            double w = ClientSize.Width / (n) * zoom;
            double h = ClientSize.Height / (n) * zoom;
            /*double min = 999, max = -999;
            for(double i = 0; i < n/zoom; i+=(zoom))
            {
                for(double j = 0;j< n/zoom; j+=zoom)
                {
                    double tmp = fmod(i * j, n);
                    if (tmp > max) max = tmp;
                    if(tmp < min) min = tmp;
                }
            }*/
            for (double i = 0; i < n / zoom; i += (zoom))
            {
                for (double j = 0; j < n / zoom; j += zoom)
                {
                    double c = fmod(i * j, n);
                    //int C = (int)((c - min) / (max - min)) * 255;
                    int C = (int)((c * 255.0f) / n);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(C, C, C)), (float)(j * w), (float)(i * h), (float)w, (float)h);
                }
            }
            g.Dispose();
        }
    }
}
