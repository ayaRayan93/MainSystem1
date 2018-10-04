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
    public partial class CarEl3htaCreate : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        CarEl3hata carEl3hata;
        XtraTabControl xtraTabControlCarsContent;

        public CarEl3htaCreate(CarEl3hata carEl3hata, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                groupBox1.Visible = false;
                this.carEl3hata = carEl3hata;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   
        }

        private void Form8_Load(object sender, EventArgs e)
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
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comCarNumber_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    clear();
                    dbconnection.Open();
                    string query = "select Rest_Custody from car_travel where Car_ID='"+comCarNumber.SelectedValue+"' order by ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        txtPreRest.Text = com.ExecuteScalar().ToString();
                    }
                    else
                    {
                        txtPreRest.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void calRestCustody_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtCustody.Text!=""&&txtSettlement_Custody.Text!="")
                    {
                        double Custody = (Convert.ToDouble(txtPreRest.Text) + Convert.ToDouble(txtCustody.Text))- Convert.ToDouble(txtSettlement_Custody.Text);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comCarNumber.Text!=""&&txtCustody.Text!="" && txtPreRest.Text != "" && txtRest.Text != "" && txtSettlement_Custody.Text != "" && txtTravelDes.Text != "" )
                {
                    dbconnection.Open();
                    string query = "insert into car_travel (Date,Car_ID,Travel_Destination,Custody,Settlement_Custody,Rest_Custody,Previous_Custody,Note) values (@Date,@Car_ID,@Travel_Destination,@Custody,@Settlement_Custody,@Rest_Custody,@Previous_Custody,@Note)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);

                    com.Parameters.Add("@Date", MySqlDbType.Date);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value.Date;
                    com.Parameters.Add("@Car_ID", MySqlDbType.VarChar);
                    com.Parameters["@Car_ID"].Value = comCarNumber.SelectedValue;
                    com.Parameters.Add("@Travel_Destination", MySqlDbType.VarChar);
                    com.Parameters["@Travel_Destination"].Value = txtTravelDes.Text;
                    com.Parameters.Add("@Custody", MySqlDbType.VarChar);
                    com.Parameters["@Custody"].Value = txtCustody.Text;
                    com.Parameters.Add("@Settlement_Custody", MySqlDbType.VarChar);
                    com.Parameters["@Settlement_Custody"].Value = txtSettlement_Custody.Text;
                    com.Parameters.Add("@Rest_Custody", MySqlDbType.VarChar);
                    com.Parameters["@Rest_Custody"].Value = txtRest.Text;
                    com.Parameters.Add("@Previous_Custody", MySqlDbType.VarChar);
                    com.Parameters["@Previous_Custody"].Value =txtPreRest.Text;

                    if (txtNote.Text != "")
                    {
                        com.Parameters.Add("@Note", MySqlDbType.Double);
                        com.Parameters["@Note"].Value = txtNote.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@Note", MySqlDbType.Double);
                        com.Parameters["@Note"].Value = null;
                    }
                    com.ExecuteNonQuery();

                    double totalSafay = Convert.ToDouble(txtRest.Text);
                    query = "select TotalSafay from Total_Revenue_Of_CarIncom where Car_ID=" + comCarNumber.SelectedValue + "";
                    com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    bool flag = false;
                    while (dr.Read())
                    {
                        totalSafay = Convert.ToDouble(dr["TotalSafay"].ToString());
                        flag = true;
                    }
                    
                    dr.Close();
                    if (flag)
                    {
                        totalSafay -= Convert.ToDouble(txtRest.Text);
                        query = "update Total_Revenue_Of_CarIncom set TotalSafay=" + totalSafay + " where Car_ID=" + comCarNumber.SelectedValue + "";
                        com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "insert into  Total_Revenue_Of_CarIncom  (TotalSafay,TotalGate, Car_ID) values(@TotalSafay,@TotalGate,@Car_ID)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@TotalSafay", MySqlDbType.Double);
                        com.Parameters["@TotalSafay"].Value = -totalSafay;
                        com.Parameters.Add("@TotalGate", MySqlDbType.Double);
                        com.Parameters["@TotalGate"].Value = 0;
                        com.Parameters.Add("@Car_ID", MySqlDbType.VarChar);
                        com.Parameters["@Car_ID"].Value = comCarNumber.Text;
                        com.ExecuteNonQuery();
                    }
                    MessageBox.Show("Done");
                    update();
                    txtTravelDes.Text = "";
                    txtNote.Text = "";
                    carEl3hata.displayData();
                }
                else
                {
                    MessageBox.Show("insert required fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtTravelDes_TextChanged(object sender, EventArgs e)
        {
            if (txtTravelDes.Text != "")
            {
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }

            XtraTabPage xtraTabPage = getTabPage("تسجيل العهدة");
            if (!IsClear())
                xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
            else
                xtraTabPage.ImageOptions.Image = null;
        }

        private void txtSettlement_Custody_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل العهدة");
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
            if (comCarNumber.Text == "" &&
            txtCustody.Text == "" && txtRest.Text == "" && txtNote.Text == ""&& txtSettlement_Custody.Text ==""&& txtTravelDes.Text == "" && dateTimePicker1.Value.Date == DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //clear
        public void clear()
        {
            txtCustody.Text = txtNote.Text = txtRest.Text = txtSettlement_Custody.Text = txtTravelDes.Text = "";
        }

        //update elahata
        public void update()
        {
                clear();
                string query = "select Rest_Custody from car_travel where Car_ID='" + comCarNumber.SelectedValue + "' order by ID desc limit 1";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    txtPreRest.Text = com.ExecuteScalar().ToString();
                }
                else
                {
                    txtPreRest.Text = "0";
                }
           
        }

      
    }
}
