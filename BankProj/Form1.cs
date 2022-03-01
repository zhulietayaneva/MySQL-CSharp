﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace BankProj
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public static string connString = "SERVER=localhost;PORT=3306;DATABASE=bank;UID=root";
        public static MySqlConnection conn = new MySqlConnection(connString);



        private void button1_Click(object sender, EventArgs e)
        {


            if (conn.State.ToString() != "Open")
            {

                try
                {
                    conn.Open();
                    button1.BackColor = Color.LightGreen;
                    button1.Text = $"Connected to DB {conn.Database}";

                }
                catch (Exception)
                {

                    button1.Text = "Ne raboti";
                }

            }
            else
            {
                button1.BackColor = Color.LightPink;
                conn.Close();
                button1.Text = "Connect to DB";
               

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("select * from currency");



            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd.CommandText,conn);
                
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var userid = textBox1.Text.Trim();
            MySqlCommand cmd = new MySqlCommand($"select * from assets where user_id={userid}");
            if (userid == String.Empty)
            {
                MessageBox.Show("Please enter User ID", "User ID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(cmd.ExecuteNonQuery()==0)
            {
                MessageBox.Show("Please enter a valid User ID", "User ID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
