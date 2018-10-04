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
    public partial class El3tat : Form
    {

        MySqlConnection dbconnection;
        XtraTabControl xtraTabControlCarsContent;
        public El3tat(XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT distinct Car_ID,Car_Number FROM cars";
                MySqlDataAdapter adpt = new MySqlDataAdapter(query, dbconnection);
                DataSet dset = new DataSet();
                adpt.Fill(dset);
                cmbCar.ValueMember = "Car_ID";
                cmbCar.DisplayMember = "Car_Number";
                cmbCar.DataSource = dset.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dbconnection.Close();
        }

        private void cmbCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                label2.Visible = true;
                label3.Visible = true;
                lblMeter.Visible = true;
                txtMeterNow.Visible = true;

                lblSub.Text = "";
                lblSub.Visible = false;
                label4.Visible = false;
                txtMeterNow.Text = "";

                string query = "select meter_reading from cars where Car_ID=" + cmbCar.SelectedValue.ToString();
                MySqlCommand command = new MySqlCommand(query, dbconnection);
            
                string reader = command.ExecuteScalar().ToString();
                lblMeter.Text = reader;
         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtMeterNow_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    lblSub.Visible = true;
                    label4.Visible = true;
                    lblSub.Text = (Convert.ToInt32(txtMeterNow.Text) - Convert.ToInt32(lblMeter.Text)).ToString();

                    string query = "update cars set meter_reading=" + txtMeterNow.Text + " where Car_ID=" + cmbCar.SelectedValue.ToString();
                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                    command.ExecuteNonQuery();

                    XtraTabPage xtraTabPage = getTabPage("تسجيل قراءة العداد");
                    xtraTabPage.ImageOptions.Image = null;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void cmbCar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل قراءة العداد");
                if (!IsClear())
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                else
                {
                    lblMeter.Text = "";
                    lblSub.Text = "";
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
            if (cmbCar.Text == "" && txtMeterNow.Text == "" )
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
