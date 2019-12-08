﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS
{
    public partial class purchase : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=IMS;Integrated Security=True");

        public purchase()
        {
            InitializeComponent();
        }

        private void purchase_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            con.Open();
            fill_product_name();
            fill_employee_name();
            fill_Department_name();
            cbProductName.Focus();
           

        }

        public void fill_product_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from products";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                cbProductName.Items.Add(dr["Name"].ToString());
            }
        }
        public void fill_employee_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from Employee";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cbEmpName.Items.Add(dr["Username"].ToString());
            }
        }
        public void fill_Department_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from Department";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cbDepartment.Items.Add(dr["Name"].ToString());
            }
        }
     
     

        private void cbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from products where name = '" + cbProductName.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                lblCategorySelection.Text = dr["Category"].ToString();
                lblPriceSelect.Text = dr["Price"].ToString();


            }
        }

        

        private void txtQuantity_Leave_1(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from products where name = '" + cbProductName.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                lblPurTotal.Text = Convert.ToString(Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(dr["Price"]));

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {// having issues sending data to db gives error 
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //(product_name, product_qty, product_price, prodcut_total, purchase_date, purchase_party_name)
            
            cmd.CommandText = "Insert into Purchase  values ('" + cbProductName.Text + "','" + txtQuantity.Text + "','" + txtQuantity.Text + "','" + lblPurTotal.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + cbEmpName.Text + "')";
            cmd.ExecuteNonQuery();
           
            MessageBox.Show("New purchase added!");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
           
           
        }
    }
}
