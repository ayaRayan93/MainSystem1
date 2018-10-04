using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;

namespace MainSystem
{
    public partial class StoresDetails : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        string code = "";

        public StoresDetails(string Code)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            code = Code;
        }

        private void StoresDetails_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select Store_Name as 'المخزن' ,sum(Total_Meters) as 'الكمية' from storage where Data_ID=" + code + " group by Data_ID ,Store_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}