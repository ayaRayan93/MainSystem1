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
    public partial class CarExpensesUpdate : Form
    {
        MySqlConnection dbconnection;
        DataRowView row;
        XtraTabControl xtraTabControlCarsContent;
        CarExpenses CarExpenses;
        bool load = false;

        public CarExpensesUpdate(DataRowView r,CarExpenses CarExpenses, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                row = r;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                this.CarExpenses = CarExpenses;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
    
        }

        private void Form11_Load(object sender, EventArgs e)
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
                comCarNumber.Text = row[1].ToString();

                query = "select * from expense_type";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type"].ToString();
                comType.ValueMember = dt.Columns["ID"].ToString();

                comType.Text = row[2].ToString();

                txtCost.Text = row[3].ToString();

                dateTimePicker1.Text = row[4].ToString();

                txtNote.Text = row[5].ToString();

                load = true;
       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "update car_expenses set Car_ID=" + comCarNumber.SelectedValue + " ,Cost=" + txtCost.Text + " , Note='" + txtNote.Text + "' ,Date='" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' , Expenses_Type='" + comType.Text + "' where ID=" + row[0].ToString();
                dbconnection.Open();
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                comand.ExecuteNonQuery();
           
                double totalCost = 0;
                try
                {
                   totalCost = Convert.ToDouble(txtCost.Text);
                }
                catch 
                {
                    MessageBox.Show("insert correct value");
                }
               
                query = "select TotalSafay from Total_Revenue_Of_CarIncom  where Car_ID="+comCarNumber.SelectedValue;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                double totalSafay = Convert.ToDouble(com.ExecuteScalar());
                query = "update Total_Revenue_Of_CarIncom set TotalSafay=" + (totalSafay - totalCost) + " where Car_ID="+comCarNumber.SelectedValue;
                com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
            
                MessageBox.Show("updated");
                CarExpenses.displayData();
                XtraTabPage xtraTabPage = getTabPage("تعديل مصروف");
                xtraTabPage.ImageOptions.Image = null;
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
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل مصروف");
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

        //functions
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
            DateTime date = dateTimePicker1.Value.Date;
            string d = date.ToString("yyyy-MM-dd");
            DateTime str =(DateTime) row[4];
            string dd = str.ToString("yyyy-MM-dd");
            if (comCarNumber.Text == row[1].ToString() &&
            comType.Text == row[2].ToString() && txtCost.Text == row[3].ToString() && txtNote.Text == row[5].ToString() && d == dd
           )
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
