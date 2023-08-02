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

namespace LaptopDemo
{
    public partial class Store : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        bool havestoreinfo = false;
        public Store()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadStore();
        }

        public void LoadStore() 
        {
            try
            {

                cn.Open();
                cm = new SqlCommand("SELECT * FROM tbStore", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    havestoreinfo = true;
                    txtStName.Text = dr["store"].ToString();
                    txtAddress.Text = dr["address"].ToString();
                }
                else
                {
                    txtStName.Clear();
                    txtAddress.Clear();
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnAccSave2_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Save store details?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)                   
                    if (havestoreinfo)
                    {
                        cn.Open();
                        cm = new SqlCommand("UPDATE tbStore SET store = '" + txtStName.Text + "', address= '" + txtAddress.Text + "'",cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    else
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tbStore (store,address) VALUES ('" + txtStName.Text + "','" + txtAddress.Text + "')",cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    MessageBox.Show("Store details have been successfully saved!", "Save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnAccCancel_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void Store_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
            { this.Dispose(); }
        }
    }
}
