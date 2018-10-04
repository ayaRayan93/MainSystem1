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
    public partial class Car_Record : Form
    {
        MySqlConnection dbconnection;
        Cars cars;
        XtraTabControl xtraTabControlCarsContent;
        public Car_Record(Cars cars, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.cars = cars;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }


        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                dbconnection.Open();
                string query = "select Car_Number from cars where Car_Number='" + txtCarNumber.Text + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);


                if (com.ExecuteScalar() == null)
                {
                    if (txtCarNumber.Text != "")
                    {
                        string qeury = "insert into cars (Car_Number,Car_Capacity,meter_reading,Openning_Account,Car_Value,DepreciationPeriod,PremiumDepreciation)values(@Car_Number,@Car_Capacity,@meter_reading,@Openning_Account,@Car_Value,@DepreciationPeriod,@PremiumDepreciation)";
                        com = new MySqlCommand(qeury, dbconnection);
                        com.Parameters.Add("@Car_Number", MySqlDbType.VarChar, 255);
                        com.Parameters["@Car_Number"].Value = txtCarNumber.Text;
                        com.Parameters.Add("@Car_Capacity", MySqlDbType.Double);
                        com.Parameters["@Car_Capacity"].Value = Convert.ToDouble(txtCarCapacity.Text);
                        com.Parameters.Add("@meter_reading", MySqlDbType.Int32);
                        com.Parameters["@meter_reading"].Value =Convert.ToInt32(txtMeterReading.Text);
                        com.Parameters.Add("@Openning_Account", MySqlDbType.Double);
                        com.Parameters["@Openning_Account"].Value = Convert.ToDouble(txtOpenning_Account.Text);
                        com.Parameters.Add("@Car_Value", MySqlDbType.Double);
                        com.Parameters["@Car_Value"].Value = Convert.ToDouble(txtCarValue.Text);
                        com.Parameters.Add("@DepreciationPeriod", MySqlDbType.Double);
                        com.Parameters["@DepreciationPeriod"].Value = Convert.ToDouble(txtDepreciationPeriod.Text);
                        com.Parameters.Add("@PremiumDepreciation", MySqlDbType.Double);
                        com.Parameters["@PremiumDepreciation"].Value = Convert.ToDouble(txtPremiumDepreciation.Text);

                        com.ExecuteNonQuery();

                        MessageBox.Show("add success");
                        clear();
                        cars.DisplayCars();
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
                            break;
                        case "txtOpenning_Account":
                            txtCarValue.Focus();
                            break;
                        case "txtCarValue":
                            txtDepreciationPeriod.Focus();
                            break;
                        case "txtDepreciationPeriod":
                            txtCarCapacity.Focus();
                            break;
                        case "txtCarCapacity":
                            txtMeterReading.Focus();
                            break;
                        case "txtMeterReading":
                            txtPremiumDepreciation.Focus();
                            break;
                        case "txtPremiumDepreciation":
                            btnAdd.Focus();
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
                XtraTabPage xtraTabPage = getTabPage("أضافة سيارة");
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
        public void clear()
        {
            foreach (Control item in this.Controls["panContent"].Controls)
            {
                if (item is TextBox)
                    item.Text = "";
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
                    if (item.Text != "")
                        return false;
                }
            }
            return true;
        }
        
    }
   
}
