using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using System.IO;
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;

namespace MainSystem
{
    public partial class SalaryUpdate : Form
    {
        MySqlConnection dbconnection;
        bool load = false;
        Salary salary;
        DataRowView row;
        XtraTabControl xtraTabControlHRContent;
        public SalaryUpdate(DataRowView r,Salary salary, XtraTabControl xtraTabControlHRContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.salary = salary;
                this.row = r;
                this.xtraTabControlHRContent = xtraTabControlHRContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
     
        private void SalaryRecord_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

               
                setData();
                load = true;

                

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            dbconnection.Close();
        }

        private void comEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    dbconnection.Open();
                    if (rEmployee.Checked)
                    {
                        string query = "SELECT Employee_Number FROM employee WHERE Employee_ID = " + comEmployee.SelectedValue;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        txtEmployee_Number.Text = com.ExecuteScalar().ToString();
                    }
                    else if (rDelegate.Checked)
                    {
                        string query = "SELECT Delegate_Number FROM delegate WHERE Delegate_ID = " + comEmployee.SelectedValue;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        txtEmployee_Number.Text = com.ExecuteScalar().ToString();
                    }

                    load = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtEmployeeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    if (rEmployee.Checked)
                    {
                        string query = "SELECT Employee_Name,Employee_ID FROM employee WHERE Employee_Number = " + txtEmployee_Number.Text;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        dbconnection.Close();
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comEmployee.DataSource = dt;
                        comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                        comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                    }
                    else if (rDelegate.Checked)
                    {
                        string query = "SELECT Delegate_Name,Delegate_ID FROM delegate WHERE Delegate_Number = " + txtEmployee_Number.Text;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        dbconnection.Close();
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comEmployee.DataSource = dt;
                        comEmployee.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                        comEmployee.ValueMember = dt.Columns["Delegate_ID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtStimulus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTotalSalary.Text = (Double.Parse(txtSalary.Text) + Double.Parse(txtStimulus.Text) - Double.Parse(txtDeductions.Text) - Double.Parse(txtDeductionsSocial.Text) ).ToString();
            }
        }

        private void btnAddSalary_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string insert = "update  Salary  set Employee_ID=@Employee_ID,Salary=@Salary,Stimulus=@Stimulus,Deductions=@Deductions,DeductionsSocial=@DeductionsSocial,Salary_Total=@Salary_Total,Date=@Date,Worker_Type=@Worker_Type,Note=@Note where Salary_ID="+row[0].ToString();

                MySqlCommand cmd = new MySqlCommand(insert, dbconnection);

                cmd.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11);
                cmd.Parameters["@Employee_ID"].Value =Convert.ToInt16(row[0].ToString());
                cmd.Parameters.Add("@Salary", MySqlDbType.Decimal, 10);

                if (txtSalary.Text != "")
                    cmd.Parameters["@Salary"].Value =Convert.ToDecimal(txtSalary.Text);
                else
                    cmd.Parameters["@Salary"].Value = 0.00;

                cmd.Parameters.Add("@Stimulus", MySqlDbType.Decimal, 10);
                cmd.Parameters["@Stimulus"].Value = Convert.ToDecimal(txtStimulus.Text);
                cmd.Parameters.Add("@Deductions", MySqlDbType.Decimal, 10);
                cmd.Parameters["@Deductions"].Value = Convert.ToDecimal(txtDeductions.Text);
                cmd.Parameters.Add("@DeductionsSocial", MySqlDbType.Decimal, 10);
                cmd.Parameters["@DeductionsSocial"].Value = Convert.ToDecimal(txtDeductionsSocial.Text);
                cmd.Parameters.Add("@Date", MySqlDbType.Date);
                cmd.Parameters["@Date"].Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add("@Salary_Total", MySqlDbType.Decimal, 10);
                cmd.Parameters["@Salary_Total"].Value = Convert.ToDecimal(txtTotalSalary.Text);
               
                cmd.Parameters.Add("@Note", MySqlDbType.VarChar, 255);
                cmd.Parameters["@Note"].Value = txtNotes.Text;

                if (rEmployee.Checked)
                {
                    cmd.Parameters.Add("@Worker_Type", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Worker_Type"].Value = "موظف";
                }
                else if (rDelegate.Checked)
                {
                    cmd.Parameters.Add("@Worker_Type", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Worker_Type"].Value = "مندوب";
                }

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("تم ادخال البيانات بنجاح");
                }
                salary.displaySalaries();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void rEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rEmployee.Checked)
                {
                    dbconnection.Open();
                    string query = "SELECT Employee_Name,Employee_ID FROM employee";// WHERE Employee_Number = " + txtEmployee_Number.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    load = false;
                    comEmployee.DataSource = dt;
                    comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                    comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                    comEmployee.Text = "";
                    load = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void rDelegate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rDelegate.Checked)
                {
                    dbconnection.Open();
                    string query = "SELECT Delegate_Name,Delegate_ID FROM delegate ";//WHERE Delegate_Number = " + txtEmployee_Number.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    load = false;
                    comEmployee.DataSource = dt;
                    comEmployee.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                    comEmployee.ValueMember = dt.Columns["Delegate_ID"].ToString();
                    comEmployee.Text = "";
                    load = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void SalaryRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل راتب موظف");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                    else
                        xtraTabPage.ImageOptions.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //function 
        //set data
        public void setData()
        {
            if (row[2].ToString() == "موظف")
            {
                rEmployee.Checked = true;

                string query = "SELECT Employee_Name,Employee_ID FROM employee ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comEmployee.DataSource = dt;
                comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
            }
            else
            {
                rDelegate.Checked = true;
                string query = "SELECT Delegate_Name,Delegate_ID FROM delegate ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comEmployee.DataSource = dt;
                comEmployee.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comEmployee.ValueMember = dt.Columns["Delegate_ID"].ToString();
            }

            txtEmployee_Number.Text = row["الرقم الوظيفي"].ToString();
            comEmployee.Text = row["اسم الموظف"].ToString();
            txtSalary.Text = row["المرتب الاساسي"].ToString();
            txtStimulus.Text = row["الحوافز"].ToString();
            txtDeductions.Text = row["الاستقطاعات"].ToString();
            txtDeductionsSocial.Text = row["استقطاعات التامين الاجتماعي"].ToString();
            txtTotalSalary.Text = row["صافي المرتب"].ToString();
            dateTimePicker1.Text = row["التاريخ"].ToString();
          
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlHRContent.TabPages.Count; i++)
                if (xtraTabControlHRContent.TabPages[i].Text == text)
                {
                    return xtraTabControlHRContent.TabPages[i];
                }
            return null;
        }

        private bool IsClear()
        {
            if (txtEmployee_Number.Text == row["الرقم الوظيفي"].ToString()&&
            comEmployee.Text == row["اسم الموظف"].ToString()&&
            txtSalary.Text == row["المرتب الاساسي"].ToString()&&
            txtStimulus.Text == row["الحوافز"].ToString()&&
            txtDeductions.Text == row["الاستقطاعات"].ToString()&&
            txtDeductionsSocial.Text == row["استقطاعات التامين الاجتماعي"].ToString()&&
            txtTotalSalary.Text == row["صافي المرتب"].ToString()&&
            dateTimePicker1.Value.ToString() == row["التاريخ"].ToString())
                return true;
            else
                return false;
        }

      
    }
}
