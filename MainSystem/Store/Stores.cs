using DevExpress.XtraGrid;
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
    public partial class Stores : Form
    {
        MySqlConnection dbconnection;
        MainForm storeMainForm;

        public Stores(MainForm storeMainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.storeMainForm = storeMainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Stores_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DisplayStores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                storeMainForm.bindRecordStoresForm(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView storeRow = (DataRowView)(((GridView)gridControlStores.MainView).GetRow(((GridView)gridControlStores.MainView).GetSelectedRows()[0]));
                storeMainForm.bindUpdateStoresForm(storeRow,this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                
                storeMainForm.bindReportStoresForm(gridControlStores);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnStorePlaces_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView storeRow = (DataRowView)(((GridView)gridControlStores.MainView).GetRow(((GridView)gridControlStores.MainView).GetSelectedRows()[0]));
                storeMainForm.bindStorePlacesForm(storeRow);
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
                DataRowView setRow = (DataRowView)(((GridView)gridControlStores.MainView).GetRow(((GridView)gridControlStores.MainView).GetSelectedRows()[0]));

                if (setRow != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string Query = "delete from store where Store_ID=" + setRow[0].ToString();
                        MySqlCommand MyCommand = new MySqlCommand(Query, dbconnection);
                        MyCommand.ExecuteNonQuery();
                       
                        string query = "ALTER TABLE store AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        //UserControl.UserRecord("store", "delete", setRow[0].ToString(), DateTime.Now, dbconnection);
                        DisplayStores();
                       
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
        
        //functions
        //display stores
        public void DisplayStores()
        {
            string qeury = "select Store_ID as 'الكود',Store_Name as 'المخزن',Store_Address as 'العنوان',Store_Phone as 'التلفون' from store";
            MySqlDataAdapter adapter = new MySqlDataAdapter(qeury, dbconnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            gridControlStores.DataSource = dataSet.Tables[0];
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

   
    }
}
