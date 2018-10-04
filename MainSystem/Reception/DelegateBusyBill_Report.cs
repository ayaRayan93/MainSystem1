using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class DelegateBusyBill_Report : Form
    {
        MySqlConnection conn;

        public DelegateBusyBill_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
        }

        private void DelegateBusyBill_Report_Load(object sender, EventArgs e)
        {
            try
            {
                search();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        public void search()
        {
            string query = "SELECT dash_delegate_bill.Delegate_ID,dash_delegate_bill.Delegate_Name as 'المندوب',dash_delegate_bill.Bill_Number as 'الفاتورة' FROM dash_delegate_bill where dash_delegate_bill.Branch_ID=" + UserControl.UserBranch(conn);
            conn.Open();
            MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
            DataSet dset = new DataSet();
            adpt.Fill(dset);
            gridControl1.DataSource = dset.Tables[0];
            gridView1.Columns[0].Visible = false;
            conn.Close();
        }
    }
}