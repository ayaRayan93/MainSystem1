using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
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
    public partial class CarIncomes : Form
    {
        MySqlConnection dbconnection;
        MainForm MainForm;
        public CarIncomes(MainForm MainForm)
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
        private void MainSystems_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                displayData();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.bindRecordIncomesForm(this);
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
                DataRowView incomesRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                MainForm.bindUpdateIncomesForm(incomesRow, this);
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
                DataRowView setRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));


                if (setRow != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string Query = "delete from car_income where Car_Income_ID=" + setRow[0].ToString();
                        MySqlCommand MyCommand = new MySqlCommand(Query, dbconnection);
                        MyCommand.ExecuteNonQuery();

                        string query = "ALTER TABLE car_income AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        //  UserControl.UserRecord("store", "delete", setRow[0].ToString(), DateTime.Now, dbconnection);
                        displayData();

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
                MainForm.bindReportIncomesForm(gridControl1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions

        public void displayData()
        {
            string query = "select car_income.Car_Income_ID as 'الرقم المسلسل', Address as 'العنوان',GROUP_CONCAT(Permission_Number) as 'رقم الاذن',NoCarton as 'عدد الكراتين',NoSets as 'عدد الاطقم',NoDocks as 'عدد الاحواض',NoColumns as 'عدد العواميد',NoCompinations as 'عدد الكوبينشن',NoPanio as 'عدد البانيوهات',Nolon as 'النولون',Gate as 'البوابة',Taateg as 'التعتيق',Safay as 'الصافي',cars.Car_Number as 'رقم العربيه',Date as 'التاريخ',Note as'ملاحظات' from car_income inner join cars on cars.Car_ID=car_income.Car_ID inner join car_permission on car_permission.Car_Income_ID=car_income.Car_Income_ID group by car_income.Car_Income_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

     

      

       

      
    }
    
}
