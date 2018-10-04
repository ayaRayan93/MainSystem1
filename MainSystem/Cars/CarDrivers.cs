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
    public partial class CarDrivers : Form
    {

        MySqlConnection dbconnection;
        DataRowView CarRow = null;
        //DataGridViewRow carDrivers =null;
        int CarId;
        Cars cars;
        XtraTabControl xtraTabControlCarsContent;
        public CarDrivers(DataRowView CarRow,Cars cars, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.CarRow = CarRow;
                labStoreName.Text = CarRow[1].ToString();
                CarId =Convert.ToInt16(CarRow[0].ToString());
                this.cars = cars;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CarDrivers_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                displayCarDrivers();
                string query = "select Driver_ID,Driver_Name from drivers";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDrivers.DataSource = dt;
                comDrivers.DisplayMember = dt.Columns["Driver_Name"].ToString();
                comDrivers.ValueMember = dt.Columns["Driver_ID"].ToString();
                comDrivers.Text = "";
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
                dbconnection.Open();
                string query = "select Driver_ID from driver_car where Driver_ID=" + comDrivers.SelectedValue+ " and Car_ID="+CarId;
                MySqlCommand com = new MySqlCommand(query, dbconnection);

                if (com.ExecuteScalar() == null)
                {
                    if (comDrivers.Text != "")
                    {
                        string qeury = "insert into driver_car (Car_ID,Driver_ID)values(@Car_ID,@Driver_ID)";
                        com = new MySqlCommand(qeury, dbconnection);
                        com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                        com.Parameters["@Car_ID"].Value = CarId;
                        com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                        com.Parameters["@Driver_ID"].Value = comDrivers.SelectedValue;
                        

                        com.ExecuteNonQuery();
                        // MessageBox.Show("add success");

                        displayCarDrivers();
                        comDrivers.Focus();
                    }
                    else
                    {
                        MessageBox.Show("enter Name");
                    }
                }
                else
                {
                    MessageBox.Show("This Driver already exist to this car");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void btnDeleteCarDriver_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DataRowView carDrivers = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                if (carDrivers != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        
                        string query = "delete from driver_car where Driver_ID=" + carDrivers[0].ToString()+" and Car_ID";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                        displayCarDrivers();
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

        private void comDrivers_TextChanged(object sender, EventArgs e)
        {
            XtraTabPage xtraTabPage = getTabPage("سائقي السيارة");
            if (comDrivers.Text!="")
                xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
            else
                xtraTabPage.ImageOptions.Image = null;
        }
        
        //functions
        //display cars driver
        public void displayCarDrivers()
        {
            String query = "select driver_car.Driver_ID as 'الكود' ,Driver_Name as 'أسم السائق' from driver_car inner join drivers on drivers.Driver_ID=driver_car.Driver_ID where Car_ID=" + CarId;
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
            DataTable dtaple = new DataTable();
            adapter.Fill(dtaple);
            gridControl1.DataSource = dtaple;
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlCarsContent.TabPages.Count; i++)
                if (xtraTabControlCarsContent.TabPages[i].Text == text)
                {
                    return xtraTabControlCarsContent.TabPages[i];
                }
            return null;
        }

    
    }
}
