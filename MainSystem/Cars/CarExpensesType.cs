using DevExpress.XtraGrid.Views.Grid;
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
    public partial class CarExpensesType : Form
    {
        MySqlConnection dbconnection;

        public CarExpensesType()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void CarExpensesType_Load(object sender, EventArgs e)
        {
            try
            {
                displayType();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (txtType.Text != "")
                {
                    insertType();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            dbconnection.Close();
        }
 

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    insertType();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select ID as 'كود',Type as 'انواع المصروفات' from expense_type where Type like'" + txtType.Text + "%'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                DataTable dtaple = new DataTable();
                adapter.Fill(dtaple);
                gridControl1.DataSource = dtaple;
                gridView1.Columns[0].Width = 30;
                gridView1.Columns[1].Width = 120;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DataRowView type = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                if (type != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {

                        string query = "delete from expense_type where ID=" + type[0].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                        displayType();
                    }
                    else if (dialogResult == DialogResult.No)
                    { }
                }
                else
                {
                    MessageBox.Show("select row");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //function
        //display expenses types
        public void displayType()
        {
            string query = "select ID as 'كود', Type as 'انواع المصروفات' from expense_type";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gridControl1.DataSource = dt;
            gridView1.Columns[0].Width = 30;
            gridView1.Columns[1].Width = 120;
        }
        //insert expenses types
        public void insertType()
        {
            string query = "select ID from expense_type where Type='" + txtType.Text + "'";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() == null)
            {
                query = "insert into expense_type  (Type)values(@Type)";
                com = new MySqlCommand(query, dbconnection);

                com.Parameters.Add("@Type", MySqlDbType.VarChar);
                com.Parameters["@Type"].Value = txtType.Text;
                com.ExecuteNonQuery();
                displayType();
        
            }
            else
            {
                MessageBox.Show("This type already exist");
            }
        }

      
    }
}
