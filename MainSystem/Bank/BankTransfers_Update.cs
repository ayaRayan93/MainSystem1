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
    public partial class BankTransfers_Update : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        //string User_ID = "";
        XtraTabPage xtraTabPage;
        public static bool updateBankTransferTextChangedFlag = false;
        DataRowView selRow;

        public BankTransfers_Update(DataRowView SelRow)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            selRow = SelRow;
        }

        private void BankTransfers_Update_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromBranch.DataSource = dt;
                comFromBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comFromBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comFromBranch.Text = selRow["من"].ToString().Split(',')[1];
                
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comToBranch.DataSource = dt;
                comToBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comToBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comToBranch.Text = selRow["الى"].ToString().Split(',')[1];

                //SELECT BankTransfer_ID as 'التسلسل',FromBank_ID,CONCAT(FromBank_Name, ',', FromBranch_Name) as 'من',ToBank_ID,CONCAT(ToBank_Name, ',', ToBranch_Name) as 'الى',Money as 'المبلغ',Date as 'تاريخ التحويل',Description as 'البيان',Error
                //comFromBank.Text = selRow["من"].ToString().Split(',')[0];
                //comToBank.Text = selRow["الى"].ToString().Split(',')[0];

                query = "select Bank_Type from bank where Bank_ID=" + selRow["FromBank_ID"].ToString();
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                string FromBankType = comand.ExecuteScalar().ToString();

                query = "select Bank_Type from bank where Bank_ID=" + selRow["ToBank_ID"].ToString();
                comand = new MySqlCommand(query, dbconnection);
                string ToBankType = comand.ExecuteScalar().ToString();

                if(FromBankType=="خزينة")
                {
                    radFromSafe.Checked = true;
                }
                else if (FromBankType == "حساب بنكى")
                {
                    radFromBank.Checked = true;
                }

                if (ToBankType == "خزينة")
                {
                    radToSafe.Checked = true;
                }
                else if (ToBankType == "حساب بنكى")
                {
                    radToBank.Checked = true;
                }

                comFromBank.SelectedValue = selRow["FromBank_ID"].ToString();
                comToBank.SelectedValue = selRow["ToBank_ID"].ToString();
                txtMoney.Text = selRow["المبلغ"].ToString();
                txtDescription.Text = selRow["البيان"].ToString();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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
                    dbconnection.Open();
                    string query = "select Bank_Stock from bank where Bank_ID=" + comFromBank.SelectedValue;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    double FromBank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());
                    FromBank_Stock += Convert.ToDouble(selRow["المبلغ"].ToString());

                    query = "select Bank_Stock from bank where Bank_ID=" + comToBank.SelectedValue;
                    comand = new MySqlCommand(query, dbconnection);
                    double ToBank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());
                    ToBank_Stock -= Convert.ToDouble(selRow["المبلغ"].ToString());

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

                    Form prompt = new Form()
                    {
                        Width = 500,
                        Height = 220,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "",
                        StartPosition = FormStartPosition.CenterScreen,
                        MaximizeBox = false,
                        MinimizeBox = false
                    };
                    Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب التعديل؟" };
                    TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 385, Multiline = true, Height = 80, RightToLeft = RightToLeft };
                    Button confirmation = new Button() { Text = "تأكيد", Left = 200, Width = 100, Top = 140, DialogResult = DialogResult.OK };
                    prompt.Controls.Add(textBox);
                    prompt.Controls.Add(confirmation);
                    prompt.Controls.Add(textLabel);
                    prompt.AcceptButton = confirmation;
                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        if (textBox.Text != "")
                        {

                            string q = "UPDATE bank SET Bank_Stock = " + (FromBank_Stock - money) + " where Bank_ID=" + comFromBank.SelectedValue;
                            MySqlCommand command = new MySqlCommand(q, dbconnection);
                            command.ExecuteNonQuery();

                            q = "UPDATE bank SET Bank_Stock = " + (ToBank_Stock + money) + " where Bank_ID=" + comToBank.SelectedValue;
                            command = new MySqlCommand(q, dbconnection);
                            command.ExecuteNonQuery();

                            MySqlCommand com = dbconnection.CreateCommand();
                            com.CommandText = "update bank_Transfer set Money=@Money,Description=@Description where BankTransfer_ID=" + selRow["التسلسل"].ToString();
                            com.Parameters.Add("@Money", MySqlDbType.Decimal, 10).Value = money;
                            com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescription.Text;
                            com.ExecuteNonQuery();

                            //////////record adding/////////////
                            query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                            com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank_Transfer";
                            com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تعديل";
                            com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow["التسلسل"].ToString();
                            com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                            com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
                            com.ExecuteNonQuery();
                            //////////////////////
                            dbconnection.Close();

                            MessageBox.Show("تم");
                            updateBankTransferTextChangedFlag = false;
                            xtraTabPage.ImageOptions.Image = null;
                            MainForm.tabControlBank.TabPages.Remove(BankTransfers_Report.MainTabPageUpdateBankTransfer);
                        }
                        else
                        {
                            MessageBox.Show("يجب كتابة السبب");
                        }
                    }
                    else
                    { }
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
                    xtraTabPage = getTabPage("tabPageUpdateBankTransfer");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                        updateBankTransferTextChangedFlag = true;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                        updateBankTransferTextChangedFlag = false;
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
