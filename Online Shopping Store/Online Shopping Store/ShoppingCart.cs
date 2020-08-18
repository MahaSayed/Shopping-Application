using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Online_Shopping_Store
{
    public partial class ShoppingCart : Form
    {
        int mm = 0;
        float price = 0.0f;
        static string constr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hr;Password=hr;";
        OracleConnection conn = new OracleConnection(constr);
        
        public ShoppingCart(string email, int ID)
        {
            InitializeComponent();
            check_email.Text = email;
            label9.Text = ID.ToString();
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
            Form1 f = new Form1(check_email.Text, "null", true, false);
            this.Hide();
            f.Show();
        }

        private void proceedtocheckout_button_Click(object sender, EventArgs e)
        {
            Checkout f = new Checkout(check_email.Text,int.Parse(label9.Text));
            this.Hide();
            f.Show();
        }

        private void ShoppingCart_Load(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            
            cmd.CommandText = "select item_name, price , num_of_items from shoppingcart where customer_email = :cust_email  ";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("cust_email" , check_email.Text);
            OracleDataReader r = cmd.ExecuteReader();
            float totalprice = 0;
            while (r.Read())
            {
                cont(r[1].ToString(), r[2].ToString(), r[4].ToString(), r[3].ToString());
                totalprice += Convert.ToInt32( r[2].ToString());
            }
            r.Close();
        }
        private void cont(string name, string price, string seller, string pic_path)
        {
            Panel panel2 = new Panel();
            PictureBox pictureBox1 = new PictureBox();
            Label label4 = new Label();
            Label label8 = new Label();
            Label label6 = new Label();

            // panel2
            // 
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new System.Drawing.Point(18, 68);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(686, 186);
            panel2.TabIndex = 0;
            flowLayoutPanel1.Controls.Add(panel2);
            //
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(20, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(211, 159);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.BackgroundImage = new Bitmap("pic_path");
            panel2.Controls.Add(pictureBox1);
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label4.Location = new System.Drawing.Point(498, 21);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(99, 34);
            label4.TabIndex = 1;
            label4.Text = name;
            panel2.Controls.Add(label4);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label6.Location = new System.Drawing.Point(498, 77);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(99, 34);
            label6.TabIndex = 2;
            label6.Text = price;
            panel2.Controls.Add(label6);
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label8.Location = new System.Drawing.Point(596, 139);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(72, 23);
            label8.TabIndex = 3;
            label8.Text = seller;
            panel2.Controls.Add(label8);
            // 
            // 
            // remove_btn
            // 
            remove_btn.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            remove_btn.Location = new System.Drawing.Point(582, 14);
            remove_btn.Name = "remove_btn";
            remove_btn.Size = new System.Drawing.Size(90, 31);
            remove_btn.TabIndex = 4;
            remove_btn.Text = "Remove";
            remove_btn.UseVisualStyleBackColor = true;
            remove_btn.Click += delegate
            {
                flowLayoutPanel1.Controls.Remove(panel2);
            };
            panel2.Controls.Add(remove_btn);



        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
