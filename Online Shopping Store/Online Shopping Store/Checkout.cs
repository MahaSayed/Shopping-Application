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
    public partial class Checkout : Form
    {
        int mm = 0;
        public Checkout(string email, int ID)
        {
            InitializeComponent();
            //el parameter ely hnb3ato
            checkout_address_edit.Text = email;
            order_id_textBox.Text = ID.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void createaccount_button_Click(object sender, EventArgs e)
        {
            if(cash_radio_button.Checked == true)
            {
                MessageBox.Show("Order Placed !");
            }
            else if(credit_card_radioButton.Checked == true)
            {
                if(string.IsNullOrWhiteSpace(card_number_textBox.Text) || string.IsNullOrWhiteSpace(card_name_textBox.Text) || string.IsNullOrWhiteSpace(card_expiry_month_textBox.Text) || string.IsNullOrWhiteSpace(card_expiry_year_textBox.Text) || string.IsNullOrWhiteSpace(card_verification_code_textBox.Text))
                {
                    MessageBox.Show("Please fill the required fields!");
                }
                else
                {

                    MessageBox.Show("Order Placed !");
                }
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(checkout_address_edit.Text, "null", true, false);
            this.Hide();
            f.Show();
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

        private void Edit_Address_btn_Click(object sender, EventArgs e)
        {
            checkout_address_edit.ReadOnly = false;
            checkout_address_edit.Clear();
        }

        private void credit_card_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (credit_card_radioButton.Checked == true)
            {
                card_number_textBox.ReadOnly = false;
                card_name_textBox.ReadOnly = false;
                card_expiry_month_textBox.ReadOnly = false;
                card_expiry_year_textBox.ReadOnly = false;
                card_verification_code_textBox.ReadOnly = false;
            }
            else
            {
                card_number_textBox.ReadOnly = true;
                card_name_textBox.ReadOnly = true;
                card_expiry_month_textBox.ReadOnly = true;
                card_expiry_year_textBox.ReadOnly = true;
                card_verification_code_textBox.ReadOnly = true;
            }
        }

        private void card_expiry_month_textBox_Click(object sender, EventArgs e)
        {
            //card_expiry_month_textBox.Text = 
        }
    }
}
