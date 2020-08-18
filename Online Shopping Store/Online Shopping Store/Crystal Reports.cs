using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Online_Shopping_Store
{
    public partial class Crystal_Reports : Form
    {
        int mm;
        public Crystal_Reports()
        {
            InitializeComponent();
        }

        private void close_Paint(object sender, PaintEventArgs e)
        {
            Graphics z = e.Graphics;
            Pen p = new Pen(Color.FromArgb(255, 165, 0));
            z.DrawLine(p, 7, 7, 19, 19);
            z.DrawLine(p, 7, 19, 19, 7);
            z.DrawLine(p, 8, 7, 20, 19);
            z.DrawLine(p, 8, 19, 20, 7);
        }

        private void maximize_Paint(object sender, PaintEventArgs e)
        {
            Graphics z = e.Graphics;
            Color c = Color.FromArgb(255, 165, 0);
            SolidBrush b = new SolidBrush(c);
            Pen p = new Pen(Color.FromArgb(255, 165, 0));
            z.DrawRectangle(p, 7, 7, 12, 12);
            z.FillRectangle(b, 7, 7, 12, 12);
            z.DrawRectangle(p, 9, 7, 7, 10);
            z.DrawRectangle(p, 12, 12, 10, 10);
        }

        private void minimize_Paint(object sender, PaintEventArgs e)
        {
            Graphics z = e.Graphics;
            Color c = Color.FromArgb(255, 165, 0);
            SolidBrush b = new SolidBrush(c);
            Pen p = new Pen(Color.FromArgb(255, 165, 0));
            z.DrawRectangle(p, 7, 16, 12, 4);
            z.FillRectangle(b, 7, 16, 12, 4);
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void maximize_Click(object sender, EventArgs e)
        {
            if (mm == 0)
            {
                WindowState = FormWindowState.Maximized;
                mm = 1;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                mm = 0;
            }
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            //Form1 f = new Form1();
            //this.Hide();
            //f.Show();
        }
    }
}
