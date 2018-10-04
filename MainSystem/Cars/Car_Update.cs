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
    public partial class Car_Update : Form
    {
        MySqlConnection dbconnection;
        DataRowView row1 =null;
        Cars cars;
        XtraTabControl xtraTabControlCarsContent;
        bool load = false;
        public Car_Update(DataRowView row1,Cars cars, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.cars = cars;
                this.row1 = row1;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                SetData(row1);
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void btnCarUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                if (txtCarNumber.Text != "")
                {
                    string qeury = "update  cars set Car_Number=@Car_Number,Car_Capacity=@Car_Capacity,meter_reading=@meter_reading,Openning_Account=@Openning_Account,Car_Value=@Car_Value,DepreciationPeriod=@DepreciationPeriod,PremiumDepreciation=@PremiumDepreciation where Car_ID=" + row1["car_id"].ToString();
                    MySqlCommand com = new MySqlCommand(qeury, dbconnection);
                    com.Parameters.Add("@Car_Number", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_Number"].Value = txtCarNumber.Text;
                    com.Parameters.Add("@Car_Capacity", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_Capacity"].Value = txtCarCapacity.Text;
                    com.Parameters.Add("@meter_reading", MySqlDbType.Int32);
                    com.Parameters["@meter_reading"].Value = Convert.ToInt32(txtMeterReading.Text);
                    com.Parameters.Add("@Openning_Account", MySqlDbType.Double);
                    com.Parameters["@Openning_Account"].Value = Convert.ToDouble(txtOpenning_Account.Text);
                    com.Parameters.Add("@Car_Value", MySqlDbType.Double);
                    com.Parameters["@Car_Value"].Value = Convert.ToDouble(txtCarValue.Text);
                    com.Parameters.Add("@DepreciationPeriod", MySqlDbType.Double);
                    com.Parameters["@DepreciationPeriod"].Value = Convert.ToDouble(txtDepreciationPeriod.Text);
                    com.Parameters.Add("@PremiumDepreciation", MySqlDbType.Double);
                    com.Parameters["@PremiumDepreciation"].Value = Convert.ToDouble(txtPremiumDepreciation.Text);

                    com.ExecuteNonQuery();
                    cars.DisplayCars();
                    MessageBox.Show("update success");
                   
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

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    TextBox t = (TextBox)sender;
                    switch (t.Name)
                    {
                        case "txtCarNumber":
                            txtOpenning_Account.Focus();
                            txtOpenning_Account.SelectAll();
                            break;
                        case "txtOpenning_Account":
                            txtCarValue.Focus();
                            txtCarValue.SelectAll();
                            break;
                        case "txtCarValue":
                            txtDepreciationPeriod.Focus();
                            txtDepreciationPeriod.SelectAll();
                            break;
                        case "txtDepreciationPeriod":
                            txtCarCapacity.Focus();
                            txtCarCapacity.SelectAll();
                            break;
                        case "txtCarCapacity":
                            txtMeterReading.Focus();
                            txtMeterReading.SelectAll();
                            break;
                        case "txtMeterReading":
                            txtPremiumDepreciation.Focus();
                            txtPremiumDepreciation.SelectAll();
                            break;
                        case "txtPremiumDepreciation":
                            btnUpdate.Focus();

                            break;
                    }
                }
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
                    XtraTabPage xtraTabPage = getTabPage("تعديل سيارة");
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
            if (txtCarNumber.Text == row1[1].ToString() &&
              txtCarCapacity.Text == row1[2].ToString() &&
              txtMeterReading.Text == row1[3].ToString() &&
              txtOpenning_Account.Text == row1[4].ToString() &&
              txtCarValue.Text == row1[5].ToString() &&
              txtDepreciationPeriod.Text == row1[6].ToString() &&
              txtPremiumDepreciation.Text == row1[7].ToString())
                return true;
            else
                return false;
        }
        public void SetData(DataRowView row)
        {
            txtCarNumber.Text = row[1].ToString();
            txtCarCapacity.Text = row[2].ToString();
            txtMeterReading.Text = row[3].ToString();
            txtOpenning_Account.Text = row[4].ToString();
            txtCarValue.Text = row[5].ToString();
            txtDepreciationPeriod.Text = row[6].ToString();
            txtPremiumDepreciation.Text = row[7].ToString();
        }

        
    }
   
}
