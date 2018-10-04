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
    public partial class CarIncomeRecord : Form
    {
        MySqlConnection dbconnection;
        double gate=0;
        double Taateg=0;
        double SafayCar_Number=0;
        CarIncomes carIncomes;
        XtraTabControl xtraTabControlCarsContent;
        bool load = false;

        public CarIncomeRecord(CarIncomes carIncomes, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.carIncomes = carIncomes;
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
                dbconnection.Open();
                string query = "select * from cars";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comCarNumber.DataSource = dt;
                comCarNumber.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCarNumber.ValueMember = dt.Columns["Car_ID"].ToString();
                comCarNumber.Text = "";
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            
        }

        private void btnAddPermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPermissionNumber.Text != "")
                {
                    int n = dataGridView1.Rows.Add();
                    foreach (DataGridViewColumn item in dataGridView1.Columns)
                    {
                        dataGridView1.Rows[n].Cells[item.Index].Value = txtPermissionNumber.Text;
                    }
                    txtPermissionNumber.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                if (row != null)
                {
                    row.DataGridView.Rows.Remove(row);
                }
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
                if (txtAddress.Text != "" && txtNolone.Text != "")
                {
                    string query = "insert into car_Income (Car_ID,Address,NoCarton,NoSets,NoDocks,NoColumns,NoCompinations,NoPanio,Nolon,Gate,Taateg,Safay,Date,Note) values (@Car_ID,@Address,@NoCarton,@NoSets,@NoDocks,@NoColumns,@NoCompinations,@NoPanio,@Nolon,@Gate,@Taateg,@Safay,@Date,@Note)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);

                    com.Parameters.Add("@Date", MySqlDbType.Date);
                    com.Parameters["@Date"].Value =dateTimePicker1.Value.Date;
                    com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                    com.Parameters["@Car_ID"].Value = comCarNumber.SelectedValue;
                    com.Parameters.Add("@Address", MySqlDbType.VarChar);
                    com.Parameters["@Address"].Value = txtAddress.Text;
                    com.Parameters.Add("@NoCarton", MySqlDbType.Double);
                    com.Parameters["@NoCarton"].Value = txtNoCarton.Text;
                    com.Parameters.Add("@NoSets", MySqlDbType.Double);
                    com.Parameters["@NoSets"].Value = txtNoSets.Text;
                    com.Parameters.Add("@NoDocks", MySqlDbType.Double);
                    com.Parameters["@NoDocks"].Value = txtNoDocks.Text;
                    com.Parameters.Add("@NoColumns", MySqlDbType.Double);
                    com.Parameters["@NoColumns"].Value = txtNoColumns.Text;
                    com.Parameters.Add("@NoCompinations", MySqlDbType.Double);
                    com.Parameters["@NoCompinations"].Value = txtNoComp.Text;
                    com.Parameters.Add("@NoPanio", MySqlDbType.Double);
                    com.Parameters["@NoPanio"].Value = txtNoPanio.Text;
                    com.Parameters.Add("@Nolon", MySqlDbType.Double);
                    com.Parameters["@Nolon"].Value = txtNolone.Text;
                    com.Parameters.Add("@Gate", MySqlDbType.Double);
                    com.Parameters["@Gate"].Value = getGateValue();
                    com.Parameters.Add("@Taateg", MySqlDbType.Double);
                    com.Parameters["@Taateg"].Value = getTaategValue();
                    com.Parameters.Add("@Safay", MySqlDbType.Double);
                    com.Parameters["@Safay"].Value = getSafayValue();
                    if (txtNote.Text != "")
                    {
                        com.Parameters.Add("@Note", MySqlDbType.VarChar);
                        com.Parameters["@Note"].Value = txtNote.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@Note", MySqlDbType.VarChar);
                        com.Parameters["@Note"].Value = null;
                    }
                    com.ExecuteNonQuery();

                    double totalSafay=0, totalGate=0;
                    query = "select TotalSafay,TotalGate from Total_Revenue_Of_CarIncom where Car_ID= " + comCarNumber.SelectedValue + "";
                    com = new MySqlCommand(query,dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    bool flag = false;
                    while (dr.Read())
                    {
                        totalSafay =Convert.ToDouble(dr["TotalSafay"].ToString());
                        if(dr["TotalGate"].ToString()!="")
                        totalGate = Convert.ToDouble(dr["TotalGate"].ToString());
                        flag = true;
                    }
                    totalSafay += getSafayValue();
                    totalGate += getGateValue();
                    dr.Close();
                    if (flag)
                    {
                        query = "update Total_Revenue_Of_CarIncom set TotalSafay=" + totalSafay + ",TotalGate=" + totalGate + " where Car_ID= " + comCarNumber.SelectedValue;
                        com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "insert into  Total_Revenue_Of_CarIncom  (TotalSafay,TotalGate, Car_ID) values(@TotalSafay,@TotalGate,@Car_ID)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@TotalSafay", MySqlDbType.Double);
                        com.Parameters["@TotalSafay"].Value = totalSafay;
                        com.Parameters.Add("@TotalGate", MySqlDbType.Double);
                        com.Parameters["@TotalGate"].Value = totalGate;
                        com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                        com.Parameters["@Car_ID"].Value = comCarNumber.SelectedValue;
                        com.ExecuteNonQuery();
                    }


                    query = "select Car_Income_ID from Car_Income order by Car_Income_ID Desc limit 1";
                    com = new MySqlCommand(query,dbconnection);
                    int id=0;
                    if (com.ExecuteScalar() != null)
                    {
                        id = Convert.ToInt16(com.ExecuteScalar());
                    }
                    else
                    {
                        MessageBox.Show("error :(");
                        dbconnection.Close();
                        return;
                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (id > 0)
                        {
                            query = "insert into Car_Permission (Car_Income_ID,Permission_Number) values (@Car_Income_ID,@Permission_Number)";
                            com = new MySqlCommand(query, dbconnection);

                            com.Parameters.Add("@Car_Income_ID", MySqlDbType.Int16);
                            com.Parameters["@Car_Income_ID"].Value = id;
                            com.Parameters.Add("@Permission_Number", MySqlDbType.VarChar);
                            com.Parameters["@Permission_Number"].Value = row.Cells[0].Value;
                            com.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("ADD Success");
                    carIncomes.displayData();
                    clear();

                   
                }
                else
                {
                    MessageBox.Show("insert data to all fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comCarNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    txtPermissionNumber.Focus();
                    dataGridView1.Rows.Clear();
                    XtraTabPage xtraTabPage = getTabPage("تسجيل إيراد");
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

        private void txtPermissionNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtPermissionNumber.Text != "")
                    {
                        int n = dataGridView1.Rows.Add();
                        foreach (DataGridViewColumn item in dataGridView1.Columns)
                        {
                            dataGridView1.Rows[n].Cells[item.Index].Value = txtPermissionNumber.Text;
                        }
                        txtPermissionNumber.Clear();
                        txtPermissionNumber.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    TextBox txtBox = (TextBox)sender;
                    switch (txtBox.Name)
                    {
                        case "txtAddress":
                            txtNoCarton.Focus();
                            break;
                        case "txtNoCarton":
                            txtNoPanio.Focus();
                            break;
                        case "txtNoPanio":
                            txtNoSets.Focus();
                            break;
                        case "txtNoSets":
                            txtNoColumns.Focus();
                            break;
                        case "txtNoColumns":
                            txtNoDocks.Focus();
                            break;
                        case "txtNoDocks":
                            txtNoComp.Focus();
                            break;
                        case "txtNoComp":
                            txtNolone.Focus();
                            break;
                        case "txtNolone":
                            txtNote.Focus();
                            break;
                        case "txtNote":
                            comCarNumber.Focus();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPermissionNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل إيراد");
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل إيراد");
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

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل إيراد");
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

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل إيراد");
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
            comCarNumber.Text = "";
            txtAddress.Text = txtNolone.Text = txtNote.Text = "";
            txtNoSets.Text = txtNoPanio.Text = txtNoDocks.Text = txtNoComp.Text = txtNoColumns.Text = txtNoCarton.Text = "0";
            dataGridView1.Rows.Clear();
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
            if (comCarNumber.Text == "" &&
            txtAddress.Text == "" && txtNolone.Text == "" && txtNote.Text == "" && txtPermissionNumber.Text==""&&
            txtNoSets.Text == "0" && txtNoPanio.Text == "0" && txtNoDocks.Text == "0" && txtNoComp.Text == "0" && txtNoColumns.Text == "0" && txtNoCarton.Text == "0" &&
            dataGridView1.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        
        //calculate gate
        public double getGateValue()
        {
           return gate= Convert.ToDouble(txtNoCarton.Text) * .3 + Convert.ToDouble(txtNoSets.Text) * 2 + Convert.ToDouble(txtNoDocks.Text) * 2 + Convert.ToDouble(txtNoColumns.Text) * 2 + Convert.ToDouble(txtNoComp.Text) * 2 + Convert.ToDouble(txtNoPanio.Text) * 3;
        }
        public double getTaategValue()
        {
           return Taateg = Convert.ToDouble(txtNoCarton.Text) * .5 + Convert.ToDouble(txtNoSets.Text) * 5 + Convert.ToDouble(txtNoPanio.Text) * 5;
        }
        public double getSafayValue()
        {
            return SafayCar_Number = Convert.ToDouble(txtNolone.Text) - (gate + Taateg);
        }

       
    }
    
}
