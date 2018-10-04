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
    public partial class SparePart : Form
    {
        MainForm MainForm;
        MySqlConnection dbconnection;
        public SparePart(MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void SparePart_Load(object sender, EventArgs e)
        {
            try
            {
                DisplaySparePart();
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
               MainForm.bindRecordSparePartForm(this);
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
                DataRowView carRow = (DataRowView)(((GridView)gridControlStores.MainView).GetRow(((GridView)gridControlStores.MainView).GetSelectedRows()[0]));
                MainForm.bindUpdateSparePartForm(carRow, this);
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
                        string Query = "delete from sparepart where SparePart_ID=" + setRow[0].ToString();
                        MySqlCommand MyCommand = new MySqlCommand(Query, dbconnection);
                        MyCommand.ExecuteNonQuery();

                        string query = "ALTER TABLE sparepart AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        // UserControl.UserRecord("store", "delete", setRow[0].ToString(), DateTime.Now, dbconnection);
                        DisplaySparePart();

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

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.bindReportSparePartForm(gridControlStores);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //functions
        public void DisplaySparePart()
        {
            string qeury = "select SparePart_ID as 'الكود',SparePart_Name as 'لأسم',SparePart_Info as 'بيان' from sparepart";
            MySqlDataAdapter adapter = new MySqlDataAdapter(qeury, dbconnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            gridControlStores.DataSource = dataSet.Tables[0];
        }

      
    }
}
