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
    public partial class Customer_settings : Form
    {
        int mm = 0;
        static string constr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hr;Password=hr;";
        OracleConnection conn = new OracleConnection(constr);


        public Customer_settings(string customer_email)
        {
            InitializeComponent();
            email_textBox.Text = customer_email;
            delete_customer_email_textBox.Text = customer_email;
            conn.Open();
            get_customer_info();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(email_textBox.Text, "null", true, false);
            this.Hide();
            f.Show();
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

        private void close_Paint(object sender, PaintEventArgs e)
        { 
            Graphics z = e.Graphics;
            Pen p = new Pen(Color.FromArgb(255, 165, 0));
            z.DrawLine(p, 7, 7, 19, 19);
            z.DrawLine(p, 7, 19, 19, 7);
            z.DrawLine(p, 8, 7, 20, 19);
            z.DrawLine(p, 8, 19, 20, 7);

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

        private void deactivateaccount_button_Click(object sender, EventArgs e)
        {
            get_customer_info();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete_customer";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("email", delete_customer_email_textBox.Text);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Account Deactivated!");

            Form1 f = new Form1(email_textBox.Text, "null", true, false);
            this.Hide();
            f.Show();
        }
        public string customername, customeremail, customerpasswords, customeraddress, customertelephone;
        private void customer_update_button_Click(object sender, EventArgs e)
        {
            customername = name_textBox.Text;
            customeremail = email_textBox.Text;
            customerpasswords = password_textBox.Text;
            customeraddress = address_textBox.Text;
            customertelephone = telephone_textBox.Text;
            
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update_customer";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("customer_name",customername );
            cmd.Parameters.Add("email", customeremail);
            cmd.Parameters.Add("customer_password",customerpasswords );
            cmd.Parameters.Add("customer_address", customeraddress);
            cmd.Parameters.Add("customer_telephone",customertelephone );

            if (password_textBox.Text != confirm_password_textBox.Text)
            {
                MessageBox.Show("Please make sure your password matches");
                password_textBox.Clear();
                confirm_password_textBox.Clear();
            }
            else if (string.IsNullOrWhiteSpace(name_textBox.Text) || string.IsNullOrWhiteSpace(password_textBox.Text) || string.IsNullOrWhiteSpace(telephone_textBox.Text)
               || string.IsNullOrWhiteSpace(confirm_password_textBox.Text) || string.IsNullOrWhiteSpace(address_textBox.Text))
            {
                MessageBox.Show("Please fill the required fields!!");
            }
            else
            {
                  cmd.ExecuteNonQuery();
                  name_textBox.Text = customername;
                  email_textBox.Text = customeremail;
                  password_textBox.Text = customerpasswords;
                  address_textBox.Text = customeraddress;
                  telephone_textBox.Text = customertelephone;
                MessageBox.Show("Information is successfully updated!");
            }
            
        }
        void clear_info()
        {
            email_textBox.Clear();
            name_textBox.Clear();
            address_textBox.Clear();
            telephone_textBox.Clear();
            password_textBox.Clear();
            confirm_password_textBox.Clear();
        }
        void get_customer_info1()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "get_customer_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cus_name", OracleDbType.Varchar2, ParameterDirection.Output);
            cmd.Parameters.Add("cus_pass", OracleDbType.Varchar2, ParameterDirection.Output);
            cmd.Parameters.Add("cus_add", OracleDbType.Varchar2, ParameterDirection.Output);
            cmd.Parameters.Add("cus_tele", OracleDbType.Varchar2, ParameterDirection.Output);
            cmd.Parameters.Add("email", email_textBox.Text);
            OracleDataReader r = cmd.ExecuteReader();

            if (r.Read())
            {
                name_textBox.Text = r[0].ToString();
                password_textBox.Text = r[3].ToString();
                address_textBox.Text = r[4].ToString();
                telephone_textBox.Text = r[5].ToString();

            }

        }
        void get_customer_info()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "get_customer_info";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cus_name", OracleDbType.Varchar2, ParameterDirection.Output).Size= 100;
            cmd.Parameters.Add("cus_pass", OracleDbType.Varchar2, ParameterDirection.Output).Size = 100;
            cmd.Parameters.Add("cus_add", OracleDbType.Varchar2, ParameterDirection.Output).Size = 100;
            cmd.Parameters.Add("cus_tele", OracleDbType.Varchar2, ParameterDirection.Output).Size = 100;
            cmd.Parameters.Add("email", email_textBox.Text);
            //MessageBox.Show(cmd.Parameters["cus_name"].Value.ToString());
            cmd.ExecuteNonQuery();

            try
            {
                name_textBox.Text = cmd.Parameters["cus_name"].Value.ToString();
                password_textBox.Text = cmd.Parameters["cus_pass"].Value.ToString();
                address_textBox.Text = cmd.Parameters["cus_add"].Value.ToString();
                telephone_textBox.Text = cmd.Parameters["cus_tele"].Value.ToString();

            }
            catch (Exception x)
            {
                MessageBox.Show("Hello");
            }

        }
    }
}
