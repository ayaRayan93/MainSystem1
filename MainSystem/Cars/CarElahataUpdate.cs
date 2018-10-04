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
    public partial class CarElahataUpdate : Form
    {
        MySqlConnection dbconnection;
        int ID;
        CarEl3hata carEl3hata;
        XtraTabControl xtraTabControlCarsContent;
        List<string> data;
        bool load = false;
        public CarElahataUpdate(int id, CarEl3hata carEl3hata, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                ID = id;
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                this.carEl3hata = carEl3hata;
                data = new List<string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateElahata_Load(object sender, EventArgs e)
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

                query = "select * from car_travel inner join cars on cars.Car_ID=car_travel.Car_ID where ID=" + ID + "";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    txtTravelDes.Text = dr["Travel_Destination"].ToString();
                    data.Add(dr["Travel_Destination"].ToString());
                    txtCustody.Text = dr["Custody"].ToString();
                    data.Add(dr["Custody"].ToString());
                    txtSettlement_Custody.Text = dr["Settlement_Custody"].ToString();
                    data.Add(dr["Settlement_Custody"].ToString());
                    txtRest.Text = dr["Rest_Custody"].ToString();
                    data.Add(dr["Rest_Custody"].ToString());
                    txtPreRest.Text = dr["Previous_Custody"].ToString();
                    data.Add(dr["Previous_Custody"].ToString());
                    txtNote.Text = dr["Note"].ToString();
                    data.Add(dr["Note"].ToString());
                    dateTimePicker1.Text = dr["Date"].ToString();
                    data.Add(dr["Date"].ToString());
                    comCarNumber.Text = dr["Car_Number"].ToString();
                    data.Add(dr["Car_Number"].ToString());
                }
                dr.Close();

                double totalSafay = 0;
                query = "select TotalSafay from Total_Revenue_Of_CarIncom where Car_ID=" + comCarNumber.SelectedValue + "";
                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    totalSafay = Convert.ToDouble(dr["TotalSafay"].ToString());
                }
                totalSafay += Convert.ToDouble(txtRest.Text);
                dr.Close();

                query = "update Total_Revenue_Of_CarIncom set TotalSafay=" + totalSafay + " where Car_ID=" + comCarNumber.SelectedValue;
                com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
                load = true;
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
                if (ID > -1)
                {
                    dbconnection.Open();
                  
                    string query = "update car_travel set Car_ID ='" + comCarNumber.SelectedValue + "' ,Travel_Destination='" + txtTravelDes.Text + "',Custody='" + txtCustody.Text + "',Settlement_Custody=" + txtSettlement_Custody.Text + ",Rest_Custody=" + txtRest.Text + ",Note='" + txtNote.Text + "' where ID=" + ID + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    double totalSafay = 0;
                    query = "select TotalSafay from Total_Revenue_Of_CarIncom where Car_ID=" + comCarNumber.SelectedValue + "";
                    com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        totalSafay = Convert.ToDouble(dr["TotalSafay"].ToString());
                    }
                    totalSafay -= Convert.ToDouble(txtRest.Text);
                    dr.Close();

                    query = "update Total_Revenue_Of_CarIncom set TotalSafay=" + totalSafay + " where Car_ID=" + comCarNumber.SelectedValue;
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    MessageBox.Show("Done");
                    carEl3hata.displayData();
                    XtraTabPage xtraTabPage = getTabPage("تعديل العهدة");
                    xtraTabPage.ImageOptions.Image = null;
                }
                else
                {
                    MessageBox.Show("select row please");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void calRestCustody_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtCustody.Text != "" && txtSettlement_Custody.Text != "")
                    {
                        double Custody = (Convert.ToDouble(txtPreRest.Text) + Convert.ToDouble(txtCustody.Text)) - Convert.ToDouble(txtSettlement_Custody.Text);
                        txtRest.Text = Custody + "";
                    }
                    if (txt.Name == "txtCustody")
                        txtSettlement_Custody.Focus();
                    else
                        txtCustody.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void txtTravelDes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل العهدة");
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

            if (comCarNumber.Text == data[7].ToString() &&
            txtTravelDes.Text == data[0].ToString() && txtCustody.Text == data[1].ToString() && txtSettlement_Custody.Text == data[2].ToString()&& txtRest.Text==data[3].ToString() && d == data[6].ToString().Split(' ')[0]
            && txtPreRest.Text==data[4].ToString()&& txtNote.Text==data[5].ToString())
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
