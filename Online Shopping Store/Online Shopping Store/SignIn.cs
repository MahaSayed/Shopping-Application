using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;

namespace Online_Shopping_Store
{
    public partial class SignIn : Form

    {
        int mm = 0;
        public static string constr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hr;Password=hr;";
        OracleConnection conn = new OracleConnection(constr);
        bool entered = false;
        bool customer = false;
        bool admin = false;
        public SignIn()
        {
            InitializeComponent();
            conn.Open();
            string address = email_textBox.Text;

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
            Form1 f = new Form1("null", "null", false, false);
            this.Hide();
            f.Show();
        }

        private void singin_createaccount_button_Click(object sender, EventArgs e)
        {
            Register f = new Register();
            this.Hide();
            f.Show();
        }

        bool check_customers()
        {
            string email = email_textBox.Text;
            OracleCommand cmd = new OracleCommand();
            string name;
            cmd.Connection = conn;
            cmd.CommandText = "select * from customer where customer_email=:email and passwordd=:pass";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("email", email_textBox.Text);
            cmd.Parameters.Add("pass", signin_password_textBox.Text);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                name = dr[0].ToString();
                entered = true;
                customer = true;
                MessageBox.Show("Login Successfuly");
                Form1 f = new Form1(email_textBox.Text, name, customer, admin);
                this.Hide();
                f.Show();
                return true;
            }
            return false;
        }
        bool check_admins()
        {
            string email = email_textBox.Text;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from admins where admin_email = :email and passwordd = :pass";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("email", email_textBox.Text);
            cmd.Parameters.Add("pass", signin_password_textBox.Text);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                entered = true;
                admin = true;
                MessageBox.Show("Login Successfuly");
                Admin_Settings f = new Admin_Settings(email, customer, admin);
                this.Hide();
                f.Show();
                return true;

            }
            return false;
        }
        private void signin_button_Click(object sender, EventArgs e)
        {
            if (check_admins() == false && check_customers() == false)
            {
                MessageBox.Show("Please check your email and password");
            }


        }

    }
}
