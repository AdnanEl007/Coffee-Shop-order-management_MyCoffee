using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System;

namespace MyCoffee
{
    public partial class MyCoffee : Form
    {
        private MySqlConnection con = new MySqlConnection();
        public MyCoffee()
        {
            InitializeComponent();
            con.ConnectionString = @"server=localhost;database=purchase_info;userid=root;password=;";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.BlueViolet;
            radioButton2.ForeColor = System.Drawing.Color.RosyBrown;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Coffee 1");
            comboBox1.Items.Add("Coffee 2");
            comboBox1.Items.Add("Coffee 3");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.RosyBrown;
            radioButton2.ForeColor = System.Drawing.Color.BlueViolet;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Dessert 1");
            comboBox1.Items.Add("Dessert 2");
            comboBox1.Items.Add("Dessert 3");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Coffee 1")
            {
                textBox1.Text = "50";
            }
            else if (comboBox1.SelectedItem.ToString() == "Coffee 2")
            {
                textBox1.Text = "100";
            }
            else if (comboBox1.SelectedItem.ToString() == "Coffee 3")
            {
                textBox1.Text = "150";
            }
            else if (comboBox1.SelectedItem.ToString() == "Dessert 1")
            {
                textBox1.Text = "80";
            }
            else if (comboBox1.SelectedItem.ToString() == "Dessert 2")
            {
                textBox1.Text = "190";
            }
            else if (comboBox1.SelectedItem.ToString() == "Dessert 3")
            {
                textBox1.Text = "230";
            }
            else
            {
                textBox1.Text = "0";
            }
            textBox3.Text = "";
            textBox2.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length > 0)
            {
                textBox3.Text = (Convert.ToInt64(textBox1.Text) * Convert.ToInt64(textBox2.Text)).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(comboBox1.Text, textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Text);
            //sum
            textBox4.Text = (Convert.ToInt16(textBox4.Text) + Convert.ToInt16(textBox3.Text)).ToString();
            //clearing fields after adding
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // deleting in datagrid
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i< dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected)
                    {
                        textBox4.Text = (Convert.ToInt16(textBox4.Text) - Convert.ToInt16(dataGridView1.Rows[i].Cells[3].Value)).ToString();
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length> 0)
            {
                textBox6.Text= (Convert.ToInt16(textBox4.Text) - Convert.ToInt16(textBox5.Text)).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i =0;i < dataGridView1.Rows.Count; i++)
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO purch_tbl values('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added de DB");
                con.Close();
            }
            dataGridView1.Rows.Clear();
            textBox4.Text = "0";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void MyCoffee_Load(object sender, EventArgs e)
        {
            //test con DB
            /*con.Open();
            if (con.State == ConnectionState.Open)
            {
                MessageBox.Show("Successfull con");
                con.Close();
            }*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoadData LDForm = new LoadData();
            LDForm.ShowDialog();
            LDForm = null;
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string total = textBox4.Text;
            string payal = textBox5.Text;
            string repayal = textBox6.Text;

            Bitmap bmp = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bmp, new Rectangle(0,0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bmp, 100, 200);

            
            e.Graphics.DrawString("Total Amount: " + total, new Font("Microsoft Sans Serif", 12), Brushes.Black, new PointF(100, 100));
            e.Graphics.DrawString("Payal Amount: " + payal, new Font("Microsoft Sans Serif", 12), Brushes.Black, new PointF(100, 130));
            e.Graphics.DrawString("Repayal Amount: " + repayal, new Font("Microsoft Sans Serif", 12), Brushes.Black, new PointF(100, 160));
            e.Graphics.DrawString("MyCoffee Ticket", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new PointF(250, 50));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox4.Text = "0";
            textBox5.Text = "";
            textBox6.Text = "";
        }
    }
}