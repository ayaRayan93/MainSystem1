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
    public partial class CarLicenseUpdate : Form
    {
        MySqlConnection dbconnection;
        DataRowView CarRow = null;
        int CarId;
        //Cars cars=null;
        XtraTabControl xtraTabControlCarsContent;
        bool load = false, flag = false;
        string date;
        MainForm MainForm;

        public CarLicenseUpdate(DataRowView CarRow,Cars cars, XtraTabControl xtraTabControlCarsContent,MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
                this.CarRow = CarRow;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                txtCarNumber.Text = CarRow[1].ToString();
                CarId = Convert.ToInt16(CarRow[0].ToString());
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public CarLicenseUpdate(string carId,string carNum, XtraTabControl xtraTabControlCarsContent, MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                this.MainForm = MainForm;
                txtCarNumber.Text = carNum;
                CarId = Convert.ToInt16(carId);
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void CarLicenseUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (!flag)
                    SetData(CarRow);
                else
                    SetData(CarId);
                load = true;
                date = dateTimePicker1.Text + "*";
                date += dateTimePicker2.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
               
                if (txtCarNumber.Text != "")
                {
                    string qeury = "update car_license set Car_License_Number=@Car_License_Number,Car_Shaza_Number=@Car_Shaza_Number,Car_Model=@Car_Model,Car_Company=@Car_Company,Start_License_Date=@Start_License_Date,End_License_Date=@End_License_Date where Car_ID="+CarId;
                    MySqlCommand com = new MySqlCommand(qeury, dbconnection);
                        
                    com.Parameters.Add("@Car_License_Number", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_License_Number"].Value = txtLicenseNumber.Text;
                    com.Parameters.Add("@Car_Shaza_Number", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_Shaza_Number"].Value = txtShaza.Text;
                    com.Parameters.Add("@Car_Model", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_Model"].Value = txtModel.Text;
                    com.Parameters.Add("@Car_Company", MySqlDbType.VarChar);
                    com.Parameters["@Car_Company"].Value = txtCarCampany.Text;
                    com.Parameters.Add("@Start_License_Date", MySqlDbType.Date);
                    com.Parameters["@Start_License_Date"].Value = dateTimePicker1.Value.Date;
                    com.Parameters.Add("@End_License_Date", MySqlDbType.Date);
                    com.Parameters["@End_License_Date"].Value = dateTimePicker2.Value.Date;

                    com.ExecuteNonQuery();

                    MessageBox.Show("update success");

                    MainForm.displayNotification(dbconnection);
                    XtraTabPage xtraTabPage = getTabPage("تعديل رخصة العربية");
                    xtraTabPage.ImageOptions.Image = null;
                    

                }
                else
                {
                    MessageBox.Show("enter Car Number");
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
            calLicenseAvaliblePeriod();
            if (load)
            {
                try
                {
                    dbconnection.Open();

                    XtraTabPage xtraTabPage = getTabPage("تعديل رخصة العربية");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                    else
                        xtraTabPage.ImageOptions.Image = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            calLicenseAvaliblePeriod();
            if (load)
            {
                try
                {
                    dbconnection.Open();

                    XtraTabPage xtraTabPage = getTabPage("تعديل رخصة العربية");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                    else
                        xtraTabPage.ImageOptions.Image = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            if (load)
            {
                try
                {
                    dbconnection.Open();
                    XtraTabPage xtraTabPage = getTabPage("تعديل رخصة العربية");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                    else
                        xtraTabPage.ImageOptions.Image = null;
                 }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

      

        //function
        public void calLicenseAvaliblePeriod()
        {
            TimeSpan d = dateTimePicker2.Value.Date - DateTime.Now.Date;
            int daysNum = Convert.ToInt16(d.Days.ToString());
            if (daysNum > 0)
            {
                labLicenceRestPeriod.Text = "المدة المتبقية " + daysNum + "يوم ";
            }
            else
            {
                labLicenceRestPeriod.Text = "انتهت الرخصة منذ  " + (daysNum * -1) + " يوم";
            }
        }
        public void SetData(DataRowView row)
        {
            String query = "select Car_License_Number,Car_Shaza_Number,Car_Model,Car_Company,Start_License_Date,End_License_Date,Car_ID from car_license where Car_ID=" + row[0].ToString();
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtLicenseNumber.Text = dr["Car_License_Number"].ToString();
                txtShaza.Text = dr["Car_Shaza_Number"].ToString();
                txtModel.Text = dr["Car_Model"].ToString();
                txtCarCampany.Text = dr["Car_Company"].ToString();
                dateTimePicker1.Text = dr["Start_License_Date"].ToString();
                dateTimePicker2.Text = dr["End_License_Date"].ToString();
            }
            dr.Close();
        }
        public void SetData(int id)
        {
            String query = "select Car_License_Number,Car_Shaza_Number,Car_Model,Car_Company,Start_License_Date,End_License_Date,Car_ID from car_license where Car_ID=" + id;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtLicenseNumber.Text = dr["Car_License_Number"].ToString();
                txtShaza.Text = dr["Car_Shaza_Number"].ToString();
                txtModel.Text = dr["Car_Model"].ToString();
                txtCarCampany.Text = dr["Car_Company"].ToString();
                dateTimePicker1.Text = dr["Start_License_Date"].ToString();
                dateTimePicker2.Text = dr["End_License_Date"].ToString();
            }
            dr.Close();
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
            String query = "select Car_License_Number,Car_Shaza_Number,Car_Model,Car_Company,Start_License_Date,End_License_Date,Car_ID from car_license where Car_ID=" +CarId;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                if (txtLicenseNumber.Text == dr["Car_License_Number"].ToString() &&
                 txtShaza.Text == dr["Car_Shaza_Number"].ToString() &&
                 txtModel.Text == dr["Car_Model"].ToString() &&
                 txtCarCampany.Text == dr["Car_Company"].ToString() &&
                 dateTimePicker1.Text == date.Split('*')[0] &&
                 dateTimePicker2.Text == date.Split('*')[1])
                    return true;
            }

            dr.Close();
            return false;
        }

    
    }
}
