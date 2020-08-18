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
    public partial class Register : Form
    {
        int mm = 0;
        static string constr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hr;Password=hr;";
        OracleConnection conn = new OracleConnection(constr);
        int month;
        public Register()
        {
            InitializeComponent();
            conn.Open();
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

        private void create_account_signin_button_Click(object sender, EventArgs e)
        {
            SignIn f = new SignIn();
            this.Hide();
            f.Show();
        }

        private void createaccount_button_Click(object sender, EventArgs e)
        {
            string name = name_textBox.Text;
            string address = address_textBox.Text;
            int day = int.Parse(day_comboBox.SelectedItem.ToString());
            int year = int.Parse(year_comboBox.SelectedItem.ToString());

            DateTime birthdate = new DateTime(year, month, day);
            DateTime today = DateTime.Today;
            TimeSpan span = today - birthdate;
            DateTime zeroTime = new DateTime(1, 1, 1);
            // -1 3shan ebtadena mn year 1
            int age = (zeroTime + span).Year - 1;
            //MessageBox.Show(age.ToString());
            string telephone = telephone_textBox.Text;
            string email = email_textBox.Text;
            string password = password_textBox.Text;
            string confirm_password = confirmpassword_textBox.Text;
            if(password != confirm_password)
            {
                MessageBox.Show("Please make sure your password matches");
                password_textBox.Clear();
                confirmpassword_textBox.Clear();
            }
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "register_customer";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("customer_name", name);
                cmd.Parameters.Add("customer_age", age);
                cmd.Parameters.Add("email", email);
                cmd.Parameters.Add("customer_password", password);
                cmd.Parameters.Add("customer_address", address);
                cmd.Parameters.Add("customer_telephone", telephone);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registration is completed");
                clear_info();

            }
        }
        void clear_info()
        {
            name_textBox.Clear();
            address_textBox.Clear();
            telephone_textBox.Clear();
            email_textBox.Clear();
            password_textBox.Clear();
            confirmpassword_textBox.Clear();
            day_comboBox.SelectedItem = null;
            month_comboBox.SelectedItem = null;
            year_comboBox.SelectedItem = null;
        }

        private void firstname_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void last_name_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void month_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_month = month_comboBox.SelectedItem.ToString();
            
         
            if (selected_month == "January")
            {
                month = 1;
            }
            else if(selected_month == "February")
            {
                month = 2;
            }
            else if (selected_month == "March")
            {
                month = 3;
            }
            else if (selected_month == "April")
            {
                month = 4;
            }
            else if (selected_month == "May")
            {
                month = 5;
            }
            else if (selected_month == "June")
            {
                month = 6;
            }
            else if (selected_month == "July")
            {
                month = 7;
            }
            else if (selected_month == "August")
            {
                month = 8;
            }
            else if (selected_month == "September")
            {
                month = 9;
            }
            else if (selected_month == "October")
            {
                month = 10;
            }
            else if (selected_month == "November")
            {
                month = 11;
            }
            else if (selected_month == "December")
            {
                month = 12;
            }

        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            //conn.Dispose();
        }
    }
}
