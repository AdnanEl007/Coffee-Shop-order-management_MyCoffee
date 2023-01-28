using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MyCoffee
{
    public partial class LoadData : Form
    {
        private MySqlConnection con = new MySqlConnection();
        public LoadData()
        {
            InitializeComponent();
            con.ConnectionString = @"server=localhost;database=purchase_info;userid=root;password=;";
        }

        private void LoadData_Load(object sender, EventArgs e)
        {
            try 
            {
                con.Open();
                MySqlCommand coman = new MySqlCommand();
                coman.Connection = con;
                string query = "select * from purchase_tbl";
                coman.CommandText = query;
                MySqlDataAdapter da = new MySqlDataAdapter(coman);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error" + ex);
            }
        }
    }
}
