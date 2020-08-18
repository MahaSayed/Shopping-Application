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
    public partial class Admin_Settings : Form
    {
        int mm = 0;
        static string constr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hr;Password=hr;";

        OracleDataAdapter adapter;

        OracleCommandBuilder builder;
        DataSet my_data_set = new DataSet();
        OracleConnection conn = new OracleConnection(constr);
        int ID;
        int next_ID;
        string admin_email;
        string product_name, product_id;
        string IMAGE;

        public Admin_Settings(string email, bool customer, bool admin)
        {
            InitializeComponent();
            conn.Open();
            admin_email = email;
            Fetch_Email();
            fetch_customer_email();
            fetch_seller_email();
            get_product_name();

            check_email.Text = email;
            check_name.Text = "null";
            check_customer.Text = customer.ToString();
            check_admin.Text = admin.ToString();



            OracleDataAdapter admin_adapter = new OracleDataAdapter("select * from products", constr);
            admin_adapter.Fill(my_data_set, "products_info");
            OracleDataAdapter admin_seller_adapter = new OracleDataAdapter("select * from products_seller", constr);
            admin_seller_adapter.Fill(my_data_set, "products_seller_info");

            DataRelation relation = new DataRelation("fk", my_data_set.Tables[1].Columns["productid"], my_data_set.Tables[2].Columns["productid"]);
            my_data_set.Relations.Add(relation);

            BindingSource bs_master_admin = new BindingSource(my_data_set, "products_info");
            BindingSource bs_admin_seller = new BindingSource(bs_master_admin, "fk");

            admin_dataGridView.DataSource = bs_master_admin;
            admin_seller_dataGridView.DataSource = bs_admin_seller;
        }

        void Fetch_Email()
        {
           
            string cmdstr = "select * from seller";
            adapter = new OracleDataAdapter(cmdstr, constr);

            adapter.Fill(my_data_set);

            for (int i = 0; i < my_data_set.Tables[0].Rows.Count; i++)
            {

                Seller_email_update_comboBox.Items.Add(my_data_set.Tables[0].Rows[i]["seller_email"].ToString());
                seller_delete_email_comboBox.Items.Add(my_data_set.Tables[0].Rows[i]["seller_email"].ToString());
            }

        }
        void fetch_customer_email()
        {
            OracleCommand cmd = new OracleCommand();
            // conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "select customer_email from customer";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                customer_delete_email_comboBox.Items.Add(dr[0]);
            }
            dr.Close();
            // conn.Close();
        }

        void fetch_seller_email()
        {
            OracleCommand cmd = new OracleCommand();
            //conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "select seller_email from seller";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!product_update_seller_comboBox.Items.Contains(dr.ToString()) && !seller_comboBox.Items.Contains(dr[0].ToString()))
                {
                    seller_comboBox.Items.Add(dr[0]);
                    product_update_seller_comboBox.Items.Add(dr[0]);
                }
            }
            dr.Close();
        }
        void get_product_name()
        {
            OracleCommand cmd = new OracleCommand();
            //conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "select * from products";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr_select = cmd.ExecuteReader();
            while (dr_select.Read())
            {
                pname_comboBox.Items.Add(dr_select[0] + " " + dr_select[1]);
                delete_product_id_comboBox.Items.Add(dr_select[0] + " " + dr_select[1]);
                product_name = dr_select[1].ToString();
                product_id = dr_select[0].ToString();
            }
            dr_select.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void add_seller_button_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            int row_count = my_data_set.Tables[0].Rows.Count;

            DataRow new_row = my_data_set.Tables[0].NewRow();
            ////add_admin_seller();
            //DataRow new_row_admin_seller = my_data_set.Tables[3].NewRow();

            new_row["sellername"] = seller_name_textBox.Text;
            new_row["address"] = seller_address_textBox.Text;
            new_row["telephone"] = seller_telephone_textBox.Text;
            new_row["seller_email"] = seller_email_textBox.Text;
            new_row["quantity"] = seller_quantity_textBox.Text;
            new_row["rating"] = seller_rating_textBox.Text;

            //new_row_admin_seller["admin_email"] = admin_email;
            //new_row_admin_seller["seller_email"] = seller_email_textBox.Text;

            if (string.IsNullOrWhiteSpace(seller_name_textBox.Text) || string.IsNullOrWhiteSpace(seller_address_textBox.Text) || string.IsNullOrWhiteSpace(seller_telephone_textBox.Text)
                || string.IsNullOrWhiteSpace(seller_quantity_textBox.Text) || string.IsNullOrWhiteSpace(seller_rating_textBox.Text) || string.IsNullOrWhiteSpace(seller_email_textBox.Text))
            {
                MessageBox.Show("Please fill the required fields!!");
            }
            else
            {
                my_data_set.Tables[0].Rows.Add(new_row);
                //my_data_set.Tables[3].Rows.Add(new_row_admin_seller);
                adapter.Update(my_data_set.Tables[0]);
               // adapter.Update(my_data_set.Tables[3]);
                Seller_email_update_comboBox.Items.Add(new_row["seller_email"].ToString());
                seller_delete_email_comboBox.Items.Add(new_row["seller_email"].ToString());
                MessageBox.Show("Seller is successfully added");
                seller_comboBox.Items.Add(new_row["seller_email"].ToString());
                row_count++;
                clear_info();
            }
        }

        void add_admin_seller()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"insert into admin_seller
                                values(:admin, :seller)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("admin", seller_email_textBox.Text);
            cmd.Parameters.Add("seller", admin_email);
            cmd.ExecuteNonQuery();

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
            Form1 f = new Form1(check_email.Text, check_name.Text, false, true);
            this.Hide();
            f.Show();
        }

        private void seller_update_button_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);

            my_data_set.Tables[0].Rows[ID]["sellername"] = seller_name_update_textBox.Text;
            my_data_set.Tables[0].Rows[ID]["address"] = seller_address_update_textBox.Text;
            my_data_set.Tables[0].Rows[ID]["telephone"] = seller_telephone_update_textBox.Text;
            my_data_set.Tables[0].Rows[ID]["quantity"] = seller_quantity_update_textBox.Text;
            my_data_set.Tables[0].Rows[ID]["rating"] = seller_rating_update_textBox.Text;
            if (string.IsNullOrWhiteSpace(seller_name_update_textBox.Text) || string.IsNullOrWhiteSpace(seller_address_update_textBox.Text) || string.IsNullOrWhiteSpace(seller_telephone_update_textBox.Text)
                || string.IsNullOrWhiteSpace(seller_quantity_update_textBox.Text) || string.IsNullOrWhiteSpace(seller_rating_update_textBox.Text) || Seller_email_update_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill the required fields!!");
            }
            else
            {
                adapter.Update(my_data_set.Tables[0]);
                MessageBox.Show("Seller is updated successfully");
                clear_info();
            }
        }


        void Get_Seller_Info()
        {
            try
            {
                ID = Seller_email_update_comboBox.SelectedIndex;
                seller_name_update_textBox.Text = my_data_set.Tables[0].Rows[ID]["sellername"].ToString();
                seller_address_update_textBox.Text = my_data_set.Tables[0].Rows[ID]["address"].ToString();
                seller_telephone_update_textBox.Text = my_data_set.Tables[0].Rows[ID]["telephone"].ToString();
                seller_quantity_update_textBox.Text = my_data_set.Tables[0].Rows[ID]["quantity"].ToString();
                seller_rating_update_textBox.Text = my_data_set.Tables[0].Rows[ID]["rating"].ToString();
            }
            catch(Exception)
            {

            }
        }
      public   string tagroba;
        private void delete_seller_button_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            ID = seller_delete_email_comboBox.SelectedIndex;
            string e_mail = seller_delete_email_comboBox.SelectedItem.ToString();
            DataRow row = my_data_set.Tables[0].Rows[ID];
            row.Delete();

            Seller_email_update_comboBox.Items.Remove(e_mail);
            seller_delete_email_comboBox.Items.Remove(e_mail);

            MessageBox.Show("Seller is successfully deleted!");
            clear_info();


            adapter.Update(my_data_set.Tables[0]);




        }

        private void Seller_email_update_comboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Get_Seller_Info();
        }

        private void seller_delete_email_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void clear_info()
        {
            seller_address_textBox.Clear();
            seller_address_update_textBox.Clear();
            seller_email_textBox.Clear();
            Seller_email_update_comboBox.SelectedItem = null;
            seller_delete_email_comboBox.SelectedItem = null;
            seller_name_update_textBox.Clear();
            seller_name_textBox.Clear();
            seller_quantity_update_textBox.Clear();
            seller_rating_textBox.Clear();
            seller_rating_update_textBox.Clear();
            seller_telephone_textBox.Clear();
            seller_telephone_update_textBox.Clear();

            product_name_textBox.Clear();
            product_price_textBox.Clear();
            product_quantity_textBox.Clear();
            //product_rating_textBox.Clear();
            add_brand_comboBox.SelectedItem = null;
            add_brand_comboBox.SelectedItem = null;
            seller_comboBox.SelectedItem = null;

            product_update_name_textBox.Clear();
            product_update_quantity_textBox.Clear();
            product_update_price_textBox.Clear();
            update_category_comboBox.SelectedItem = null;
            update_brand_comboBox.SelectedItem = null;
            product_update_seller_comboBox.SelectedItem = null;
            product_update_discount_textBox.Clear();
            product_update_rating_textBox.Clear();

            delete_product_id_comboBox.SelectedItem = null;

        }

        private void delete_customer_button_Click(object sender, EventArgs e)
        {
            string remove_email = customer_delete_email_comboBox.SelectedItem.ToString();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete_customer";
            cmd.CommandType = CommandType.StoredProcedure;
            if (customer_delete_email_comboBox.SelectedItem.ToString() == null)
            {
                MessageBox.Show("Please select an item!");
            }
            else
            {
                cmd.Parameters.Add("email", remove_email);
                cmd.ExecuteNonQuery();
                customer_delete_email_comboBox.Items.Remove(remove_email);
                MessageBox.Show("Customer is successfully deleted");
                customer_delete_email_comboBox.SelectedItem = null;

            }
        }


        private void add_product_Click(object sender, EventArgs e)
        {
            /*adding into products*/
            string image_Text = openFileDialog1.FileName;
            OracleCommand cmd2 = new OracleCommand();
            next_ID = select_ID();
            cmd2.Connection = conn;
            cmd2.CommandText = "insert into products values(:ID, :prodname, :prodquan, :rate, :prodprice, :discount, :cat_name, :prod_brand, :image)";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("ID", next_ID);
            cmd2.Parameters.Add("prodname", product_name_textBox.Text);
            cmd2.Parameters.Add("prodquan", product_quantity_textBox.Text);
            cmd2.Parameters.Add("rate", product_rating_textBox.Text);
            cmd2.Parameters.Add("prodprice", product_price_textBox.Text);
            cmd2.Parameters.Add("discount", product_discount_textBox.Text);
            //cmd2.Parameters.Add("sold", Sold_textBox.Text);
            cmd2.Parameters.Add("cat_name", add_category_comboBox.SelectedItem.ToString());
            cmd2.Parameters.Add("cat_brand", add_brand_comboBox.SelectedItem.ToString());
            cmd2.Parameters.Add("image", image_Text);

            if (string.IsNullOrWhiteSpace(product_name_textBox.Text) || string.IsNullOrWhiteSpace(product_quantity_textBox.Text) || string.IsNullOrWhiteSpace(product_price_textBox.Text)
                || string.IsNullOrWhiteSpace(product_rating_textBox.Text) || string.IsNullOrWhiteSpace(product_discount_textBox.Text) 
                || add_category_comboBox.SelectedItem.ToString() == null || add_brand_comboBox.SelectedItem.ToString() == null || string.IsNullOrWhiteSpace(image_Text) || add_pictureBox.Image == null)
            {
                MessageBox.Show("Please fill the required fields!!");
            }
            else
            {
                cmd2.ExecuteNonQuery();
                insert();
                MessageBox.Show("Product is successfully added");
                pname_comboBox.Items.Add(next_ID.ToString() + " " + product_name_textBox.Text);
                delete_product_id_comboBox.Items.Add(next_ID.ToString() + " " + product_name_textBox.Text);

                clear_info();
            }

        }
        int select_ID()
        {
            int ID = 0;
            OracleCommand select = new OracleCommand();
            select.Connection = conn;
            select.CommandText = "select product_seq.nextval from products";
            select.CommandType = CommandType.Text;
            OracleDataReader dr = select.ExecuteReader();
            if(dr.Read())
            {
                ID = Convert.ToInt32(dr[0].ToString());
            }

            return ID;
        }
        void insert()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into products_admin values(:prodid, :adminemail)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("prodid", next_ID);
                cmd.Parameters.Add("adminemail", admin_email);
                cmd.ExecuteNonQuery();

                OracleCommand cmd1 = new OracleCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = "insert into products_seller values(:prodid, :selleremail)";
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.Add("prodid", next_ID);
                cmd1.Parameters.Add("selleremail", seller_comboBox.SelectedItem.ToString());
                cmd1.ExecuteNonQuery();
            }
            catch(Exception x)
            { }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public string name, quantity, id, price, rating, discount,categoryname,brand;
        private void update_product_Click(object sender, EventArgs e)
        {
            name = product_update_name_textBox.Text;
            quantity = product_update_quantity_textBox.Text;
            price = product_update_price_textBox.Text;
            rating = product_update_rating_textBox.Text;
            discount = product_update_discount_textBox.Text;
            categoryname = update_category_comboBox.SelectedItem.ToString();
            brand = update_brand_comboBox.SelectedItem.ToString();
            
            // openFileDialog1.ShowDialog();
            //string image_Text = openFileDialog1.FileName;
            string get_ID = pname_comboBox.SelectedItem.ToString();
            string[] ID = get_ID.Split(' ');
           // MessageBox.Show(image_Text);
            get_product_info();
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = @"update products set product_name = :name, product_quantity = :prodquan, rating = :rate, price = :prodprice, 
                                    discount = :dis, category_name = :cat_name, product_brand = :brand
                                where productid = :prodid";

            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.Add("name", name);
            cmd1.Parameters.Add("prodquan", quantity);            
            cmd1.Parameters.Add("rate",rating );
            cmd1.Parameters.Add("prodprice",price );
            cmd1.Parameters.Add("dis",discount );
            cmd1.Parameters.Add("cat_name", categoryname );
            cmd1.Parameters.Add("brand", brand);
            //cmd1.Parameters.Add("image", image_Text);
            cmd1.Parameters.Add("prodid", ID[0]);

            if (string.IsNullOrWhiteSpace(product_update_name_textBox.Text) || string.IsNullOrWhiteSpace(product_update_discount_textBox.Text) || string.IsNullOrWhiteSpace(product_update_price_textBox.Text)
               || string.IsNullOrWhiteSpace(product_update_rating_textBox.Text) || string.IsNullOrWhiteSpace(product_update_rating_textBox.Text) || string.IsNullOrWhiteSpace(product_update_quantity_textBox.Text) || update_category_comboBox.SelectedItem.ToString() == null ||
                    update_brand_comboBox.SelectedItem.ToString() == null || pname_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill the required fields!!");
            }
            else
            {
                cmd1.ExecuteNonQuery();
                update_product_seller();
                MessageBox.Show("Product is successfully updated!");
                  product_update_name_textBox.Text=name;
                  product_update_quantity_textBox.Text = quantity;
                  product_update_price_textBox.Text = price;
                  product_update_rating_textBox.Text = rating;
                  product_update_discount_textBox.Text = discount;
                  update_category_comboBox.SelectedItem = categoryname;
                  update_brand_comboBox.SelectedItem = brand;
                clear_info();
            }
        }

        private void add_category_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Mobiles and Tablets
            //Electronics
            //Appliances
            //Health and Beauty
            //Sports
            //Fashion
            if (add_category_comboBox.SelectedItem.ToString() == "Mobiles and Tablets")
            {
                add_brand_comboBox.Items.Add("Laptops");
                add_brand_comboBox.Items.Add("TVs");
                add_brand_comboBox.Items.Add("Camera");
                add_brand_comboBox.Items.Add("Audio and Video");
            }
            else if(add_category_comboBox.SelectedItem.ToString() == "Mobiles and Tablets")
            {
                add_brand_comboBox.Items.Add("Iphone");
                add_brand_comboBox.Items.Add("Samsung");
                add_brand_comboBox.Items.Add("Huawei");
                add_brand_comboBox.Items.Add("Lenovo");
                add_brand_comboBox.Items.Add("Nokia");
                add_brand_comboBox.Items.Add("Sony");
                add_brand_comboBox.Items.Add("HTC");
                add_brand_comboBox.Items.Add("LG");
            }
            else if (add_category_comboBox.SelectedItem.ToString() == "Mobiles and Tablets")
            {
                add_brand_comboBox.Items.Add("Iphone");
                add_brand_comboBox.Items.Add("Samsung");
                add_brand_comboBox.Items.Add("Huawei");
                add_brand_comboBox.Items.Add("Lenovo");
                add_brand_comboBox.Items.Add("Nokia");
                add_brand_comboBox.Items.Add("Sony");
                add_brand_comboBox.Items.Add("HTC");
                add_brand_comboBox.Items.Add("LG");
            }
            else if (add_category_comboBox.SelectedItem.ToString() == "Appliances")
            {
                add_brand_comboBox.Items.Add("Samsung");
                add_brand_comboBox.Items.Add("LG");
                add_brand_comboBox.Items.Add("Whirlpool");
                add_brand_comboBox.Items.Add("TOSHIBA");
                add_brand_comboBox.Items.Add("Carrier");
              
            }
            else if (add_category_comboBox.SelectedItem.ToString() == "Health and Beauty")
            {
                add_brand_comboBox.Items.Add("NARS");
                add_brand_comboBox.Items.Add("HUDA BEAUTY");
                add_brand_comboBox.Items.Add("KIKO");
                add_brand_comboBox.Items.Add("maybelline");
                add_brand_comboBox.Items.Add("L'Oréal");
                add_brand_comboBox.Items.Add("Lovea");
                add_brand_comboBox.Items.Add("Sephora");
                add_brand_comboBox.Items.Add("Rimmel");
                add_brand_comboBox.Items.Add("Elvive");
                add_brand_comboBox.Items.Add("Nivea");
                add_brand_comboBox.Items.Add("JOHNSON");
                add_brand_comboBox.Items.Add("Body Shop");
                add_brand_comboBox.Items.Add("Bubblez");
                add_brand_comboBox.Items.Add("VICHY");
            }
            else if (add_category_comboBox.SelectedItem.ToString() == "Sports")
            {
                add_brand_comboBox.Items.Add("Nike");
                add_brand_comboBox.Items.Add("Addidas");
                add_brand_comboBox.Items.Add("Rebook");
                add_brand_comboBox.Items.Add("skechers");
                add_brand_comboBox.Items.Add("active");
                add_brand_comboBox.Items.Add("Puma");
            }
            else if (add_category_comboBox.SelectedItem.ToString() == "Fashion")
            {
                add_brand_comboBox.Items.Add("Victoria's Secret");
                add_brand_comboBox.Items.Add("H&M");
                add_brand_comboBox.Items.Add("Zara");
                add_brand_comboBox.Items.Add("Pull&Bear");
                add_brand_comboBox.Items.Add("Pixi");
                add_brand_comboBox.Items.Add("Deja vu");
                add_brand_comboBox.Items.Add("aldo");
                add_brand_comboBox.Items.Add("Show room");
            }
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void customer_delete_email_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

        }

        void update_product_seller()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update products_seller set seller_email = :email where productid = :prodid";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("prodid", product_id);
            cmd.Parameters.Add("email", seller_address_update_textBox.Text);
            cmd.ExecuteNonQuery();
        }
        private void delete_product_Click(object sender, EventArgs e)
        {
            string get_ID = delete_product_id_comboBox.SelectedItem.ToString();
            string[] ID = get_ID.Split(' ');
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete_product";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("prodid", ID[0]);

            if (delete_product_id_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill the required fields!!");
            }
            else
            {
                cmd.ExecuteNonQuery();
                delete_other_tables(ID[0]);
                MessageBox.Show("Product is successfully deleted!");
                delete_product_id_comboBox.Items.Remove(product_id + " " + product_name);
                pname_comboBox.Items.Remove(product_id + " " + product_name);
                clear_info();
            }

        }
        void delete_other_tables(string id)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from products_admin where productid = :prodid";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("prodid", id);
            cmd.ExecuteNonQuery();

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "delete from products_seller where productid = :prodid";
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.Add("prodid", id);
            cmd1.ExecuteNonQuery();

        }
        private void Upload_picture_button_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                add_pictureBox.ImageLocation = openFileDialog1.FileName;
                //this.Text = openFileDialog1.FileName;
                String TheImageFile = openFileDialog1.FileName;

                Image TheImage = Image.FromFile(TheImageFile);
                add_pictureBox.Image = TheImage;


            }
            catch (Exception)
            {

            }
        }

        private void update_picture_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                update_pictureBox.ImageLocation = openFileDialog1.FileName;
                //this.Text = openFileDialog1.FileName;
                String TheImageFile = openFileDialog1.FileName;

                Image TheImage = Image.FromFile(TheImageFile);
                update_pictureBox.Image = TheImage;


            }
            catch (Exception)
            {

            }
        }

        private void pname_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_product_info();
            //MessageBox.Show()
        }

        private void Admin_Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
        }



        void get_product_info()
        {
            string ID = pname_comboBox.SelectedItem.ToString();
            string[] array = ID.Split(' ');
            
            OracleCommand select = new OracleCommand();
            select.Connection = conn;
            select.CommandText = "select * from products where productid = :prod_id";
            select.CommandType = CommandType.Text;
            select.Parameters.Add("prod_id", array[0]);
            OracleDataReader dr_products = select.ExecuteReader();
            if (dr_products.Read())
            {
                string IMAGE = dr_products[8].ToString();
                Image TheImage = Image.FromFile(IMAGE);
                update_pictureBox.Image = TheImage;

                product_update_name_textBox.Text = dr_products[1].ToString();
                product_update_quantity_textBox.Text = dr_products[2].ToString();
                product_update_rating_textBox.Text = dr_products[3].ToString();
                product_update_price_textBox.Text = dr_products[4].ToString();
                product_update_discount_textBox.Text = dr_products[5].ToString();
                update_category_comboBox.SelectedItem = dr_products[6].ToString();
                update_brand_comboBox.SelectedItem = dr_products[7].ToString();
            }
            dr_products.Close();
            OracleCommand select_seller_email = new OracleCommand();
            select_seller_email.Connection = conn;
             
            select_seller_email.CommandText = @"select seller_email
                                                from  products, products_seller
                                                where products.productid = :prod_iD and products.productid = products_seller.productid";
            select_seller_email.CommandType = CommandType.Text;
            select_seller_email.Parameters.Add("prod_id", product_id);
            OracleDataReader dr_seller = select_seller_email.ExecuteReader();
            while(dr_seller.Read())
            {
                //if(product_update_seller_comboBox.Items.Contains(dr_seller[0].ToString()))
                    product_update_seller_comboBox.Text = dr_seller[0].ToString();
            }
            dr_seller.Close();
            //return IMAGE;


        }
    }
}
