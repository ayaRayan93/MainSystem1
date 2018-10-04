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
    public partial class BankTransfers_Record : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        //string User_ID = "";
        XtraTabPage xtraTabPage;
        public static bool addBankTransferTextChangedFlag = false;

        public BankTransfers_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            //User_ID = user_ID;
        }

        private void BankTransfers_Record_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromBranch.DataSource = dt;
                comFromBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comFromBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comFromBranch.SelectedIndex = -1;
                
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comToBranch.DataSource = dt;
                comToBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comToBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comToBranch.SelectedIndex = -1;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comFromBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    radFromSafe.Enabled = true;
                    radFromBank.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comToBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    radToSafe.Enabled = true;
                    radToBank.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radFromSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from bank where Bank_Type='خزينة' and Branch_ID=" + comFromBranch.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromBank.DataSource = dt;
                comFromBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comFromBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                comFromBank.SelectedIndex = -1;
                comFromBank.Enabled = true;
                txtMoney.Enabled = true;
                txtDescription.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radFromBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from bank where Bank_Type='حساب بنكى' and Branch_ID=" + comFromBranch.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromBank.DataSource = dt;
                comFromBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comFromBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                comFromBank.SelectedIndex = -1;
                comFromBank.Enabled = true;
                txtMoney.Enabled = true;
                txtDescription.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radToSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from bank where Bank_Type='خزينة' and Branch_ID=" + comToBranch.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comToBank.DataSource = dt;
                comToBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comToBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                comToBank.SelectedIndex = -1;
                comToBank.Enabled = true;
                txtMoney.Enabled = true;
                txtDescription.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radToBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from bank where Bank_Type='حساب بنكى' and Branch_ID=" + comToBranch.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comToBank.DataSource = dt;
                comToBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comToBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                comToBank.SelectedIndex = -1;
                comToBank.Enabled = true;
                txtMoney.Enabled = true;
                txtDescription.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comFromBranch.SelectedIndex != -1 && comToBranch.SelectedIndex != -1 && comFromBank.SelectedIndex != -1 && comToBank.SelectedIndex != -1 && txtMoney.Text != "")
                {
                    if ((comFromBranch.Text + comFromBank.Text) != (comToBranch.Text + comToBank.Text))
                    {
                        dbconnection.Open();
                        string query = "select Bank_Stock from bank where Bank_ID=" + comFromBank.SelectedValue;
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        double FromBank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());

                        query = "select Bank_Stock from bank where Bank_ID=" + comToBank.SelectedValue;
                        comand = new MySqlCommand(query, dbconnection);
                        double ToBank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());

                        double money = 0;
                        if (double.TryParse(txtMoney.Text, out money))
                        {
                            if (money > FromBank_Stock)
                            {
                                MessageBox.Show("لا يوجد ما يكفى");
                                dbconnection.Close();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("المبلغ المدفوع يجب ان يكون عدد");
                            dbconnection.Close();
                            return;
                        }

                        string q = "UPDATE bank SET Bank_Stock = " + (FromBank_Stock - money) + " where Bank_ID=" + comFromBank.SelectedValue;
                        MySqlCommand command = new MySqlCommand(q, dbconnection);
                        command.ExecuteNonQuery();

                        q = "UPDATE bank SET Bank_Stock = " + (ToBank_Stock + money) + " where Bank_ID=" + comToBank.SelectedValue;
                        command = new MySqlCommand(q, dbconnection);
                        command.ExecuteNonQuery();

                        MySqlCommand com = dbconnection.CreateCommand();
                        com.CommandText = "INSERT INTO bank_Transfer (FromBranch_ID,FromBranch_Name,FromBank_ID,FromBank_Name,ToBranch_ID,ToBranch_Name,ToBank_ID,ToBank_Name,Money,Date,Description,Error) VALUES (@FromBranch_ID,@FromBranch_Name,@FromBank_ID,@FromBank_Name,@ToBranch_ID,@ToBranch_Name,@ToBank_ID,@ToBank_Name,@Money,@Date,@Description,@Error)";
                        com.Parameters.Add("@FromBranch_ID", MySqlDbType.Int16, 11).Value = comFromBranch.SelectedValue;
                        com.Parameters.Add("@FromBranch_Name", MySqlDbType.VarChar, 255).Value = comFromBranch.Text;
                        com.Parameters.Add("@FromBank_ID", MySqlDbType.Int16, 11).Value = comFromBank.SelectedValue;
                        com.Parameters.Add("@FromBank_Name", MySqlDbType.VarChar, 255).Value = comFromBank.Text;
                        com.Parameters.Add("@ToBranch_ID", MySqlDbType.Int16, 11).Value = comToBranch.SelectedValue;
                        com.Parameters.Add("@ToBranch_Name", MySqlDbType.VarChar, 255).Value = comToBranch.Text;
                        com.Parameters.Add("@ToBank_ID", MySqlDbType.Int16, 11).Value = comToBank.SelectedValue;
                        com.Parameters.Add("@ToBank_Name", MySqlDbType.VarChar, 255).Value = comToBank.Text;
                        com.Parameters.Add("@Money", MySqlDbType.Decimal, 10).Value = money;
                        com.Parameters.Add("@Date", MySqlDbType.Date, 0).Value = DateTime.Now.Date;
                        com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescription.Text;
                        com.Parameters.Add("@Error", MySqlDbType.Int16, 11).Value = 0;
                        com.ExecuteNonQuery();

                        //////////record adding/////////////
                        query = "select BankTransfer_ID from bank_Transfer order by BankTransfer_ID desc limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        string bankTransferID = com.ExecuteScalar().ToString();

                        query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                        com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank_Transfer";
                        com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                        com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = bankTransferID;
                        com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                        com.ExecuteNonQuery();
                        //////////////////////
                        dbconnection.Close();

                        MessageBox.Show("تم");
                        clear();
                        addBankTransferTextChangedFlag = false;
                        xtraTabPage.ImageOptions.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("لا يمكنك التحويل الى نفس المصدر");
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال جميع البيانات المطلوبة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    xtraTabPage = getTabPage("tabPageRecordBankTransfer");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                        addBankTransferTextChangedFlag = true;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                        addBankTransferTextChangedFlag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //clear function
        public void clear()
        {
            comFromBranch.SelectedIndex = -1;
            comToBranch.SelectedIndex = -1;
            comFromBank.SelectedIndex = -1;
            comToBank.SelectedIndex = -1;
            foreach (Control co in this.layoutControl1.Controls)
            {
                if (co is ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";

                }
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlBank.TabPages.Count; i++)
                if (MainForm.tabControlBank.TabPages[i].Name == text)
                {
                    return MainForm.tabControlBank.TabPages[i];
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
