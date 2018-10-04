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
    public partial class Salary : Form
    {
        MySqlConnection dbconnection;
        MainForm HRMainForm;
        public Salary(MainForm HRMainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.HRMainForm = HRMainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Salary_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                displaySalaries();
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
                HRMainForm.bindRecordSalariesForm(this);
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
                DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                HRMainForm.bindUpdateSalariesForm(row, this);
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

                        string Query = "delete from salary where Salary_ID=" + setRow[0].ToString();
                        MySqlCommand MyCommand = new MySqlCommand(Query, dbconnection);
                        MyCommand.ExecuteNonQuery();

                        string query = "ALTER TABLE salary AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                       // UserControl.UserRecord("salary", "حذف", setRow[0].ToString(), DateTime.Now, dbconnection);
                
                        displaySalaries();

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
                HRMainForm.bindReportSalariesForm(gridControl1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void displaySalaries()
        {
            string query = "select  Salary_ID,salary.Employee_ID as 'id',Worker_Type, Employee_Number as 'الرقم الوظيفي',Employee_Name as 'اسم الموظف',Salary as 'المرتب الاساسي',Stimulus as 'الحوافز',Deductions as 'الاستقطاعات',DeductionsSocial as 'استقطاعات التامين الاجتماعي',Salary_Total as 'صافي المرتب',Date as 'التاريخ' from salary inner join employee on employee.Employee_ID=salary.Employee_ID where Worker_Type='موظف'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
            DataSet dataSet1 = new DataSet();
            adapter.Fill(dataSet1);

            query = "select  Salary_ID ,Delegate_ID as 'id',Worker_Type, Delegate_Number as 'الرقم الوظيفي',Delegate_Name as 'اسم الموظف' ,Salary as 'المرتب الاساسي',Stimulus as 'الحوافز',Deductions as 'الاستقطاعات',DeductionsSocial as 'استقطاعات التامين الاجتماعي',Salary_Total as 'صافي المرتب',Date as 'التاريخ' from salary inner join Delegate on delegate.Delegate_ID=salary.Employee_ID where Worker_Type='مندوب'";
            adapter = new MySqlDataAdapter(query, dbconnection);
            DataSet dataSet2 = new DataSet();
            adapter.Fill(dataSet2);
            DataSet dataSet3 = new DataSet();

            dataSet3 = dataSet1.Copy();
            dataSet3.Merge(dataSet2, true);

            gridControl1.DataSource = dataSet3.Tables[0];
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[2].Visible = false;
        }
  
    }
}
