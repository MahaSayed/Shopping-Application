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
    public partial class Form1 : Form
    {
        int mm = 0;
        //string email;
        static string constr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hr;Password=hr;";
        OracleConnection conn = new OracleConnection(constr);
        int ID = 0;
        public Form1(string email, string name, bool customer, bool admin)
        {
            InitializeComponent();
            conn.Open();
            timer1.Start();
            check_email.Text = email;
            check_name.Text = name;
            check_customer.Text = customer.ToString();
            check_admin.Text = admin.ToString();
            if(check_customer.Text == "False" || check_admin.Text == "False")
            {
                logOutToolStripMenuItem.Visible = false;
            }
            
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
            Form1 f = new Form1(check_email.ToString(), check_name.ToString(), bool.Parse(check_customer.ToString()), bool.Parse(check_admin.ToString()));
            this.Hide();
            f.Show();
        }

        private void shopping_cart_btn_Click(object sender, EventArgs e)
        {
            if (check_email.Text == "null")
            {
                MessageBox.Show("You Should login first!");
            }
            else
            {
                ShoppingCart f = new ShoppingCart(check_email.Text, ID);
                this.Hide();
                f.Show();
            }
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (check_email.Text == "null")
            {
                MessageBox.Show("You Should login first!");
            }
            else
            {
                Customer_settings f = new Customer_settings(check_email.Text);
                this.Hide();
                f.Show();
            }
        }

        private void crystal_Click(object sender, EventArgs e)
        {
            Crystal_Reports f = new Crystal_Reports();
            this.Hide();
            f.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void slideshow6_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (slideshow6.Visible == true)
            {
                slideshow6.Visible = false;
                slideshow5.Visible = true;
            }
            else if (slideshow5.Visible == true)
            {
                slideshow5.Visible = false;
                slideshow4.Visible = true;
            }
            else if (slideshow4.Visible == true)
            {
                slideshow4.Visible = false;
                slideshow3.Visible = true;
            }
            else if (slideshow3.Visible == true)
            {
                slideshow3.Visible = false;
                slideshow2.Visible = true;
            }
            else if (slideshow2.Visible == true)
            {
                slideshow2.Visible = false;
                slideshow1.Visible = true;
            }
            else if (slideshow1.Visible == true)
            {
                slideshow1.Visible = false;
                slideshow6.Visible = true;
            }

        }


        //A.5 Get multiple rows using refcursor.
        private void Form1_Load(object sender, EventArgs e)
        {
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "get_products";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("outProducts", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                lol(dr[0].ToString(), dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        private void lol (string name , string brand , string price , string discount ) //string path
        {
            Panel panel3 = new Panel();      //el kebera
            Panel panel4 = new Panel();     //el khat
            PictureBox PanelPrictureBox = new PictureBox();
            Label product_Panel_Name = new Label();
            Label product_panel_catg = new Label();
            Label product_panel_dis = new Label();
            Label product_panel_seller = new Label();
            Button proceedtocheckout_button = new Button();
            GroupBox groupBox1 = new GroupBox();

            // panel3
            // 
            panel3.Location = new System.Drawing.Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(189, 277); 
            panel3.TabIndex = 0;
            panel3.Controls.Add(groupBox1);
            // 
            //// PanelPrictureBox
            //// 
            //PanelPrictureBox.Location = new System.Drawing.Point(17, 21);
            //PanelPrictureBox.Name = "PanelPrictureBox";
            //PanelPrictureBox.Size = new System.Drawing.Size(150, 117);
            //PanelPrictureBox.TabIndex = 0;
            //PanelPrictureBox.TabStop = false;
            //pictureBox1.BackgroundImage = new Bitmap("path");
            //panel3.Controls.Add(PanelPrictureBox);
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.Orange;
            panel4.Location = new System.Drawing.Point(17, 144);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(150, 1);
            panel4.TabIndex = 1;
            groupBox1.Controls.Add(panel4);
            // 
            // product_Panel_Name
            // product name
            product_Panel_Name.AutoSize = true;
            product_Panel_Name.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            product_Panel_Name.Location = new System.Drawing.Point(67, 148);
            product_Panel_Name.Name = "product_Panel_Name";
            product_Panel_Name.Size = new System.Drawing.Size(52, 19);
            product_Panel_Name.TabIndex = 1;
            // product_Panel_Name.Text = "label1";
            product_Panel_Name.Text = name;
            groupBox1.Controls.Add(product_Panel_Name);
            // 
            // product_panel_catg
            // price
            product_panel_catg.AutoSize = true;
            product_panel_catg.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            product_panel_catg.Location = new System.Drawing.Point(21, 177);
            product_panel_catg.Name = "product_panel_catg";
            product_panel_catg.Size = new System.Drawing.Size(52, 19);
            product_panel_catg.TabIndex = 2;
            // product_panel_catg.Text = "label2";
            product_panel_catg.Text = price;
            groupBox1.Controls.Add(product_panel_catg);
            // 
            // product_panel_dis
            // price after discount
            product_panel_dis.AutoSize = true;
            product_panel_dis.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            product_panel_dis.Location = new System.Drawing.Point(21, 209);
            product_panel_dis.Name = "product_panel_dis";
            product_panel_dis.Size = new System.Drawing.Size(52, 19);
            product_panel_dis.TabIndex = 3;
            //product_panel_dis.Text = "label3";
            product_panel_dis.Visible = false;
            product_panel_dis.Text = discount;
            groupBox1.Controls.Add(product_panel_dis);
            // 
            // product_panel_seller
            // brand
            product_panel_seller.AutoSize = true;
            product_panel_seller.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            product_panel_seller.Location = new System.Drawing.Point(115, 228);
            product_panel_seller.Name = "product_panel_seller";
            product_panel_seller.Size = new System.Drawing.Size(52, 19);
            product_panel_seller.TabIndex = 4;
            //product_panel_seller.Text = "label1";
            product_panel_seller.Text = brand;
            groupBox1.Controls.Add(product_panel_seller);


            groupBox1.Controls.Add(product_panel_seller);
            groupBox1.Controls.Add(panel4);
            groupBox1.Controls.Add(PanelPrictureBox);
            groupBox1.Controls.Add(product_Panel_Name);
            groupBox1.Controls.Add(product_panel_catg);
            groupBox1.Controls.Add(product_panel_dis);
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(183, 271);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
            panel3.Controls.Add(groupBox1);

            // proceedtocheckout_button
            // 
            proceedtocheckout_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            proceedtocheckout_button.BackColor = System.Drawing.Color.Orange;
            proceedtocheckout_button.FlatAppearance.BorderSize = 0;
            proceedtocheckout_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            proceedtocheckout_button.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            proceedtocheckout_button.Location = new System.Drawing.Point(100, 203);
            proceedtocheckout_button.Margin = new System.Windows.Forms.Padding(4);
            proceedtocheckout_button.Name = "proceedtocheckout_button";
            proceedtocheckout_button.Size = new System.Drawing.Size(66, 29);
            proceedtocheckout_button.TabIndex = 17;
            proceedtocheckout_button.Text = "Buy";
            proceedtocheckout_button.UseVisualStyleBackColor = false;
            proceedtocheckout_button.Click += new System.EventHandler(proceedtocheckout_button_Click);
            groupBox1.Controls.Add(proceedtocheckout_button);

            flowLayoutPanel1.Controls.Add(panel3);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        int select_ID()
        {
            
            OracleCommand select = new OracleCommand();
            select.Connection = conn;
            select.CommandText = "select order_id_seq.nextval from shoppingcart";
            select.CommandType = CommandType.Text;
            OracleDataReader dr = select.ExecuteReader();
            if (dr.Read())
            {
                ID = Convert.ToInt32(dr[0].ToString());
            }

            return ID;
        }

        private void proceedtocheckout_button_Click(object sender, EventArgs e)
        {
            int id = select_ID();
            if (check_email.Text == "null")
            {
                MessageBox.Show("You Should login first!");
            }
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"insert into shoppingcart
                                values(:item_name,:order_id, :customer_email, :item_price , :num_of_items)"; 
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("item_name", product_Panel_Name.Text);
                cmd.Parameters.Add("order_id", id.ToString());     //parameter da msh sa7
                cmd.Parameters.Add("customer_email", check_name.Text);
                cmd.Parameters.Add("item_price", product_panel_price.Text.ToString());
                cmd.Parameters.Add("num_of_items", product_panel_price.Text);   //parameter da msh sa7
                int r = cmd.ExecuteNonQuery();
                if (r != -1)
                {
                    MessageBox.Show("Added!");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (Brand_radioButton.Checked)
            {
                cmd.CommandText = @"select product_name , price , product_brand, discount
                                    from products
                                    where product_brand = :brand";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("brand", search_textBox);
                OracleDataReader drt = cmd.ExecuteReader();
                while (drt.Read())
                {
                    lol(drt[0].ToString(), drt[2].ToString(), drt[1].ToString(), drt[3].ToString());
                }
            }

            else if (name_radioButton.Checked)
            {
                cmd.CommandText = @"select product_name , price , product_brand, discount
                                    from products
                                    where product_name = :prod_name";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("prod_name", search_textBox);
                OracleDataReader drr = cmd.ExecuteReader();
                while (drr.Read())
                {
                    lol(drr[0].ToString(), drr[2].ToString(), drr[1].ToString(), drr[3].ToString());
                }
            }
   

        }

        private void myOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (check_email.Text == "null")
            {
                MessageBox.Show("You Should login first!");
            }
            else
            {
                my_orders order = new my_orders(check_email.Text);
                this.Hide();
                order.Show();
            }
        }

        private void dhwdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select product_name , price , product_brand, discount
                                    from products
                                    where category_name = 'Mobiles and Tablets'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lol(dr[0].ToString(), dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        private void nbcncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select product_name , price , product_brand, discount
                                    from products
                                    where category_name = 'Electronics'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lol(dr[0].ToString(), dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        private void appliancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select product_name , price , product_brand, discount
                                    from products
                                    where category_name = 'Appliances'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lol(dr[0].ToString(), dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        private void healthAndBeautyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select product_name , price , product_brand, discount
                                    from products
                                    where category_name = 'Health and Beauty'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lol(dr[0].ToString(), dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        private void sportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select product_name , price , product_brand, discount
                                    from products
                                    where category_name = 'Sports'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lol(dr[0].ToString(), dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        private void fashionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select product_name , price , product_brand, discount
                                    from products
                                    where category_name = 'Fashion'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lol(dr[0].ToString(), dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (check_email.Text == "null")
            {
                MessageBox.Show("You Should login first!");
            }
            else
            {
                Form1 f = new Form1(check_email.ToString(), check_name.ToString(), bool.Parse(check_customer.ToString()), bool.Parse(check_admin.ToString()));
                this.Hide();
                f.Show();
            }
        }

        private void Login_button_Click(object sender, EventArgs e)
        {
            SignIn sign = new SignIn();
            this.Hide();
            sign.Show();
        }
    }
}
