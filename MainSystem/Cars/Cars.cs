using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
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
    public partial class Cars : Form
    {
        MainForm MainForm;
        MySqlConnection dbconnection;
        public Cars(MainForm MainForm)
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

        private void Cars_Load(object sender, EventArgs e)
        {
            try
            {
                DisplayCars();
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
                MainForm.bindRecordCarForm(this);
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
                MainForm.bindUpdateCarForm(carRow, this);
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
                        string Query = "delete from cars where Car_ID=" + setRow[0].ToString();
                        MySqlCommand MyCommand = new MySqlCommand(Query, dbconnection);
                        MyCommand.ExecuteNonQuery();

                        string query = "ALTER TABLE cars AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                      //  UserControl.UserRecord("store", "delete", setRow[0].ToString(), DateTime.Now, dbconnection);
                        DisplayCars();

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
                MainForm.bindReportCarsForm(gridControlStores);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCarPapers_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView carRow = (DataRowView)(((GridView)gridControlStores.MainView).GetRow(((GridView)gridControlStores.MainView).GetSelectedRows()[0]));
                MainForm.bindCarPaperForm(carRow, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCarDrivers_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView carRow = (DataRowView)(((GridView)gridControlStores.MainView).GetRow(((GridView)gridControlStores.MainView).GetSelectedRows()[0]));
                MainForm.bindCarDriversForm(carRow, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSpareParts_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView carRow = (DataRowView)(((GridView)gridControlStores.MainView).GetRow(((GridView)gridControlStores.MainView).GetSelectedRows()[0]));
                MainForm.bindSparePartForm(carRow, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCarLicense_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView carRow = (DataRowView)(((GridView)gridControlStores.MainView).GetRow(((GridView)gridControlStores.MainView).GetSelectedRows()[0]));
                MainForm.bindCarLicenseForm(carRow, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void DisplayCars()
        {
            string qeury = "select cars.Car_ID as 'الكود', Car_Number as 'رقم السيارة' ,Car_Capacity as 'سعة التحميل',meter_reading as 'قراءة العداد',Openning_Account as 'رصيد بداية المدة',Car_Value as 'القيمة الحالية للسيارة',DepreciationPeriod as 'فترة الاهلاك',PremiumDepreciation as 'قسط الاهلاك' from cars ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(qeury, dbconnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            gridControlStores.DataSource = dataSet.Tables[0];
            // gridView1.Columns[0].Visible = false;

      
        }
        
    }
}
