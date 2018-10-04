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
    public partial class Driver_Update : Form
    {
        MySqlConnection dbconnection;
        DataRowView DriverRow = null;
        Drivers drivers;
        XtraTabControl xtraTabControlCarsContent;
        string date;
        bool load = false;

        public Driver_Update(DataRowView DriverRow, Drivers drivers, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                SetData(DriverRow);
                this.DriverRow = DriverRow;
                this.drivers = drivers;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Driver_Update_Load(object sender, EventArgs e)
        {
            try
            {
                SetData(DriverRow);
                load = true;
                date = dTPBirthDate.Text + "*";
                date += dTPWorkStartDate.Text;
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

                    XtraTabPage xtraTabPage = getTabPage("تعديل بيانات سائق");
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
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                   
                    XtraTabPage xtraTabPage = getTabPage("تعديل بيانات سائق");
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
        private void btnUpdateDriver_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string qeury = "update drivers set Driver_Name=@Driver_Name,Driver_Phone=@Driver_Phone,Driver_Address=@Driver_Address ,Driver_BairthDate=@Driver_BairthDate,Driver_License=@Driver_License,Driver_NationalID=@Driver_NationalID,Driver_StartWorkDate=@Driver_StartWorkDate where Driver_ID="+DriverRow[0].ToString();
                MySqlCommand com = new MySqlCommand(qeury, dbconnection);
                com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar, 255);
                com.Parameters["@Driver_Name"].Value = txtDriverName.Text;
                com.Parameters.Add("@Driver_Phone", MySqlDbType.VarChar, 255);
                com.Parameters["@Driver_Phone"].Value = txtPhone.Text;
                com.Parameters.Add("@Driver_Address", MySqlDbType.VarChar);
                com.Parameters["@Driver_Address"].Value =txtAddress.Text;

                com.Parameters.Add("@Driver_BairthDate", MySqlDbType.Date);
                com.Parameters["@Driver_BairthDate"].Value = dTPBirthDate.Value.Date;
                com.Parameters.Add("@Driver_License", MySqlDbType.VarChar, 255);
                com.Parameters["@Driver_License"].Value = txtLicese.Text;
                com.Parameters.Add("@Driver_NationalID", MySqlDbType.VarChar, 255);
                com.Parameters["@Driver_NationalID"].Value = txtNationalID.Text;
                com.Parameters.Add("@Driver_StartWorkDate", MySqlDbType.Date);
                com.Parameters["@Driver_StartWorkDate"].Value = dTPWorkStartDate.Value.Date;

                com.ExecuteNonQuery();
                MessageBox.Show("update success");
                drivers.DisplayDrivers();
                XtraTabPage xtraTabPage = getTabPage("تعديل بيانات سائق");
                xtraTabPage.ImageOptions.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
     
        //function
        public void clear()
        {
            foreach (Control item in this.Controls["panContent"].Controls)
            {
                if (item is TextBox)
                    item.Text = "";
                else if (item is DateTimePicker)
                    item.Text = DateTime.Now.ToString();
            }
        }

        public void SetData(DataRowView row)
        {
            txtDriverName.Text = row[1].ToString();
            txtPhone.Text = row[2].ToString();
            txtAddress.Text = row[3].ToString();
            dTPBirthDate.Text = row[4].ToString();
            txtLicese.Text = row[5].ToString();
            txtNationalID.Text = row[6].ToString();
            dTPWorkStartDate.Text = row[7].ToString();
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
            if (txtDriverName.Text == DriverRow[1].ToString() &&
            txtPhone.Text == DriverRow[2].ToString() &&
            txtAddress.Text == DriverRow[3].ToString() &&
            txtLicese.Text == DriverRow[5].ToString() &&
            txtNationalID.Text == DriverRow[6].ToString() &&
            dTPBirthDate.Text == date.Split('*')[0] &&
            dTPWorkStartDate.Text == date.Split('*')[1])
                return true;
            else
                return false;
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Control t = (Control)sender;
                    switch (t.Name)
                    {
                        case "txtDriverName":
                            txtNationalID.Focus();
                            break;
                        case "txtNationalID":
                            txtPhone.Focus();
                            break;
                        case "txtPhone":
                            txtAddress.Focus();
                            break;
                        case "txtAddress":
                            txtLicese.Focus();
                            break;
                        case "txtLicese":
                            dTPBirthDate.Focus();
                            break;
                        case "dTPBirthDate":
                            dTPWorkStartDate.Focus();
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
