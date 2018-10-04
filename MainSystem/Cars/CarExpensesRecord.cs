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
    public partial class CarExpensesRecord : Form
    {
        MySqlConnection dbconnection;
        CarExpenses carExpenses;
        XtraTabControl xtraTabControlCarsContent;

        public CarExpensesRecord(CarExpenses carExpenses, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                this.carExpenses = carExpenses;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from cars";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comCarNumber.DataSource = dt;
                comCarNumber.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCarNumber.ValueMember = dt.Columns["Car_ID"].ToString();
                comCarNumber.Text = "";
                query = "select * from expense_type";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type"].ToString();
                comType.ValueMember = dt.Columns["ID"].ToString();
                comType.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnAddCarExpenses_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (txtCost.Text != "" && comCarNumber.Text != "" && comType.Text != "")
                {
                    string query = "insert into car_expenses  (Car_ID,Cost,Note,Date,Expenses_Type)values(@Car_ID,@Cost,@Note,@Date,@Expenses_Type)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);

                    com.Parameters.Add("@Date", MySqlDbType.Date);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value.Date;
                    com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                    com.Parameters["@Car_ID"].Value = comCarNumber.SelectedValue;
                    if (txtNote.Text != "")
                    {
                        com.Parameters.Add("@Note", MySqlDbType.VarChar);
                        com.Parameters["@Note"].Value = txtNote.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@Note", MySqlDbType.VarChar);
                        com.Parameters["@Note"].Value = txtNote.Text;
                    }
                    com.Parameters.Add("@Expenses_Type", MySqlDbType.VarChar);
                    com.Parameters["@Expenses_Type"].Value = comType.Text;
                    double cost;
                    if (double.TryParse(txtCost.Text, out cost))
                    {
                        com.Parameters.Add("@Cost", MySqlDbType.VarChar);
                        com.Parameters["@Cost"].Value = txtCost.Text;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value to cost field");
                        dbconnection.Close();
                        return;
                    }
                    com.ExecuteNonQuery();
                    DateTime date = dateTimePicker1.Value.Date;
                    string d = date.ToString("yyyy-MM-dd");
                    query = "select sum(Cost) from car_expenses where Car_ID=" +comCarNumber.SelectedValue  + " and Date ='" + d + "'";
                    com = new MySqlCommand(query,dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        double totalCost = Convert.ToDouble(txtCost.Text);
                        query = "select TotalSafay from Total_Revenue_Of_CarIncom where Car_ID=" + comCarNumber.SelectedValue + "";
                        com = new MySqlCommand(query,dbconnection);
                        double totalSafay = 0;
                        if (com.ExecuteScalar() != null)
                        {
                            totalSafay = Convert.ToDouble(com.ExecuteScalar());
                            query = "update Total_Revenue_Of_CarIncom set TotalSafay=" + (totalSafay - totalCost) + " where Car_ID=" + comCarNumber.SelectedValue + "";
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            query = "insert into  Total_Revenue_Of_CarIncom  (TotalSafay,TotalGate, Car_ID) values(@TotalSafay,@TotalGate,@Car_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@TotalSafay", MySqlDbType.Double);
                            com.Parameters["@TotalSafay"].Value = -totalCost;
                            com.Parameters.Add("@TotalGate", MySqlDbType.Double);
                            com.Parameters["@TotalGate"].Value = 0;
                            com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                            com.Parameters["@Car_ID"].Value = comCarNumber.SelectedValue;
                            com.ExecuteNonQuery();
                        }
                      
                    }
                    else
                    {
                        MessageBox.Show("error");
                        dbconnection.Close();
                        return;
                    }
                    
                    MessageBox.Show("Done");
                    carExpenses.displayData();
                    clear();
                }
                else
                {
                    MessageBox.Show("fill required fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            dbconnection.Close();
        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل مصروف");
                if (!IsClear())
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                else
                    xtraTabPage.ImageOptions.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlCarsContent.TabPages.Count; i++)
                if (xtraTabControlCarsContent.TabPages[i].Text == text)
                {
                    return xtraTabControlCarsContent.TabPages[i];
                }
            return null;
        }

        private bool IsClear()
        {
            if (comCarNumber.Text == "" &&
            comType.Text == "" && txtCost.Text == "" && txtNote.Text == "" && dateTimePicker1.Value.Date == DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void clear()
        {
            comCarNumber.Text = comType.Text = txtCost.Text = txtNote.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
        }

        //display expenses types in combox
        public void displayTypeInComBox()
        {
            string query = "select * from expense_type";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comType.DataSource = dt;
            comType.DisplayMember = dt.Columns["Type"].ToString();
            comType.ValueMember = dt.Columns["ID"].ToString();
            comType.Text = "";
            
        }

   
    }
}
