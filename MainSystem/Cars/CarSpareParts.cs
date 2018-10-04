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
    public partial class CarSpareParts : Form
    {

        MySqlConnection dbconnection;
        DataRowView CarRow = null;
        DataGridViewRow carSparePart=null;
        int CarId;
        Cars cars;
        XtraTabControl xtraTabControlCarsContent;
        MainForm MainForm;
        public CarSpareParts(DataRowView CarRow,Cars cars, XtraTabControl xtraTabControlCarsContent,MainForm MainForm)
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
                this.MainForm = MainForm;
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
                displayCarSpareParts();
                string query = "select SparePart_ID,SparePart_Name from sparepart";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSpareParts.DataSource = dt;
                comSpareParts.DisplayMember = dt.Columns["SparePart_Name"].ToString();
                comSpareParts.ValueMember = dt.Columns["SparePart_ID"].ToString();
                comSpareParts.Text = "";
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
             
                if (comSpareParts.Text != "")
                {
                    string qeury = "insert into car_sparepart (Car_ID,SparePart_ID,Car_SparePart_Info,Car_SpareDate)values(@Car_ID,@SparePart_ID,@Car_SparePart_Info,@Car_SpareDate)";
                    MySqlCommand com = new MySqlCommand(qeury, dbconnection);
                    com.Parameters.Add("@Car_ID", MySqlDbType.Int32);
                    com.Parameters["@Car_ID"].Value = CarId;
                    com.Parameters.Add("@SparePart_ID", MySqlDbType.Int16);
                    com.Parameters["@SparePart_ID"].Value = comSpareParts.SelectedValue;
                    com.Parameters.Add("@Car_SparePart_Info", MySqlDbType.VarChar,255);
                    com.Parameters["@Car_SparePart_Info"].Value = txtInfo.Text;
                    com.Parameters.Add("@Car_SpareDate", MySqlDbType.Date);
                    com.Parameters["@Car_SpareDate"].Value = dateTimePicker1.Value.Date;

                    com.ExecuteNonQuery();

                    displayCarSpareParts();
                    comSpareParts.Focus();
                    MainForm.displayNotification(dbconnection);
                }
                else
                {
                    MessageBox.Show("enter Name");
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDeleteCarDriver_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DataRowView setRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));

                if (carSparePart != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        
                        string query = "delete from car_sparepart where SparePart_ID=" + carSparePart.Cells[0].Value.ToString()+" and Car_ID="+CarId;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                        displayCarSpareParts();
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

        private void comSpareParts_TextChanged(object sender, EventArgs e)
        {
            XtraTabPage xtraTabPage = getTabPage("قطع الغيار");
            if (comSpareParts.Text != "" && txtInfo.Text != "")
                xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
            else
                xtraTabPage.ImageOptions.Image = null;
        }
        //functions
        //display cars driver
        public void displayCarSpareParts()
        {
            String query = "select car_sparepart.SparePart_ID as 'الكود' ,sparepart.SparePart_Name as 'قطعة الغيار',car_sparepart.Car_SparePart_Info as 'البيان',car_sparepart.Car_SpareDate as 'التاريخ' from sparepart inner join car_sparepart on sparepart.SparePart_ID=car_sparepart.SparePart_ID where Car_ID=" + CarId;
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
