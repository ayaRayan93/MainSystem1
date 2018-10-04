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
    public partial class CarLicenseRecord : Form
    {
        MySqlConnection dbconnection;
        DataRowView CarRow = null;
        int CarId;
        Cars cars=null;
        XtraTabControl xtraTabControlCarsContent;
        string date;
        MainForm MainForm;
        public CarLicenseRecord(DataRowView CarRow,Cars cars, XtraTabControl xtraTabControlCarsContent,MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.CarRow = CarRow;
                txtCarNumber.Text = CarRow[1].ToString();
                CarId = Convert.ToInt16(CarRow[0].ToString());
                this.cars = cars;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                this.MainForm = MainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarLicenseRecord_Load(object sender, EventArgs e)
        {
            try
            {
                date = dateTimePicker1.Text + "*";
                date+= dateTimePicker2.Text;
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
                dbconnection.Open();
                string query = "select Car_License_ID from car_license where Car_ID=" + CarId + "";
                MySqlCommand com = new MySqlCommand(query, dbconnection);


                if (com.ExecuteScalar() == null)
                {
                    if (txtCarNumber.Text != "")
                    {
                        string qeury = "insert into car_license (Car_ID,Car_License_Number,Car_Shaza_Number,Car_Model,Car_Company,Start_License_Date,End_License_Date)values(@Car_ID,@Car_License_Number,@Car_Shaza_Number,@Car_Model,@Car_Company,@Start_License_Date,@End_License_Date)";
                        com = new MySqlCommand(qeury, dbconnection);
                        com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                        com.Parameters["@Car_ID"].Value = CarId;
                        com.Parameters.Add("@Car_License_Number", MySqlDbType.VarChar,255);
                        com.Parameters["@Car_License_Number"].Value = txtLicenseNumber.Text;
                        com.Parameters.Add("@Car_Shaza_Number", MySqlDbType.VarChar,255);
                        com.Parameters["@Car_Shaza_Number"].Value =txtShaza.Text;
                        com.Parameters.Add("@Car_Model", MySqlDbType.VarChar,255);
                        com.Parameters["@Car_Model"].Value = txtModel.Text;
                        com.Parameters.Add("@Car_Company", MySqlDbType.VarChar);
                        com.Parameters["@Car_Company"].Value = txtCarCampany.Text;
                        com.Parameters.Add("@Start_License_Date", MySqlDbType.Date);
                        com.Parameters["@Start_License_Date"].Value = dateTimePicker1.Value.Date;
                        com.Parameters.Add("@End_License_Date", MySqlDbType.Date);
                        com.Parameters["@End_License_Date"].Value = dateTimePicker2.Value.Date;

                        com.ExecuteNonQuery();
                        
                        MessageBox.Show("add success");
                        clear();
                        XtraTabPage xtraTabPage = getTabPage("تسجيل رخصة العربية");
                        xtraTabPage.ImageOptions.Image = null;

                        MainForm.displayNotification(dbconnection);
                    }
                    else
                    {
                        MessageBox.Show("enter Car Number");
                    }
                }
                else
                {
                    MessageBox.Show("This Car already exist");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
   
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                calLicenseAvaliblePeriod();
                XtraTabPage xtraTabPage = getTabPage("تسجيل رخصة العربية");
                if (dateTimePicker1.Text != date.Split('*')[0]||!IsClear())
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                else
                    xtraTabPage.ImageOptions.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                calLicenseAvaliblePeriod();
                XtraTabPage xtraTabPage = getTabPage("تسجيل رخصة العربية");
                if (dateTimePicker2.Text!=date.Split('*')[1] || !IsClear())
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                else
                    xtraTabPage.ImageOptions.Image = null;
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
                XtraTabPage xtraTabPage = getTabPage("تسجيل رخصة العربية");
                if (dateTimePicker1.Text != date.Split('*')[0]|| dateTimePicker2.Text != date.Split('*')[1]||!IsClear())
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
        public void calLicenseAvaliblePeriod()
        {
            TimeSpan d = dateTimePicker2.Value.Date - DateTime.Now.Date;
            labLicenceRestPeriod.Text = d.Days.ToString();

            int daysNum = Convert.ToInt16(d.Days.ToString());
            if (daysNum > 0)
            {
                labLicenceRestPeriod.Text = "المدة المتبقية " + daysNum + "يوم ";
            }
            else
            {
                labLicenceRestPeriod.Text = "انتهت الرخصة منذ  "+ (daysNum*-1) + " يوم";
            }

        }

    
        private void clear()
        {
            foreach (Control item in this.Controls["panContent"].Controls)
            {
                if (item is TextBox)
                    item.Text = "";
                else if (item is DateTimePicker)
                    item.Text=DateTime.Now.Date.ToString();
            }
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
        private bool IsClear()
        {
            foreach (Control item in this.Controls["panContent"].Controls)
            {
                if (item is TextBox)
                {
                    if (item.Name != "txtCarNumber")
                    {
                        if (item.Text != "")
                            return false;
                    }
                }
            }
            return true;
        }

    
    }
}
