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
    public partial class Commitment_Report : Form
    {
        MySqlConnection conn;
        bool loaded = false;
        public Commitment_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);

            comDelegate.AutoCompleteMode = AutoCompleteMode.Suggest;
            comDelegate.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Commitment_Report_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from delegate where Branch_ID=" + UserControl.UserBranch(conn) + " and Error=0";
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";
                conn.Close();
                loaded = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    XtraTabPage xtraTabPage = getTabPage("tabPageCommitmentReport");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void search()
        {
            try
            {
                conn.Open();

                string query = "";
                if (comDelegate.Text != "")
                {
                    //FORMAT(SUBTIME(cast(attendance.Departure_Date as time),cast(attendance.Attendance_Date as time)), N'hh:mm')
                    query = "select Attendance_ID as 'التسلسل', Name as 'المندوب',SUBTIME(cast(attendance.Departure_Date as time),cast(attendance.Attendance_Date as time)) as 'المدة' from attendance where DATE_FORMAT(Attendance_Date,'%Y-%m-%d') = '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Delegate_ID=" + comDelegate.SelectedValue.ToString() + " and Error=0";
                }
                else
                {
                    query = "select Attendance_ID as 'التسلسل', Name as 'المندوب',SUBTIME(cast(attendance.Departure_Date as time),cast(attendance.Attendance_Date as time)) as 'المدة' from attendance where DATE_FORMAT(Attendance_Date,'%Y-%m-%d') = '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Error=0";
                }

                MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
                DataSet dset = new DataSet();
                adpt.Fill(dset);

                gridControl1.DataSource = dset.Tables[0];
                gridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlPointSale.TabPages.Count; i++)
                if (MainForm.tabControlPointSale.TabPages[i].Name == text)
                {
                    return MainForm.tabControlPointSale.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }
    }
}
