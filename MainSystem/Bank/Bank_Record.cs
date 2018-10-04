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
    public partial class Bank_Record : Form
    {
        MySqlConnection conn;
        bool loaded = false;
        public static bool addBankTextChangedFlag = false;
        XtraTabPage xtraTabPage;

        public Bank_Record()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);

            cmbType.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbType.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBranch.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Bank_Record_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loaded)
                {
                    loadBranch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbType.Text != "")
                {
                    bool check = false;
                    if (cmbType.Text == "خزينة")
                    {
                        check = (txtName.Text != "" && cmbBranch.Text != "" && txtStock.Text != "" && dateEdit1.Text != "");
                    }
                    else if (cmbType.Text == "حساب بنكى")
                    {
                        check = (txtName.Text != "" && txtAccountNumber.Text != "" && txtAccountType.Text != "" && txtStock.Text != "" && dateEdit1.Text != "");
                    }
                    else if (cmbType.Text == "فيزا")
                    {
                        check = (txtName.Text != "" && cmbBranch.Text != "" && txtAccountNumber.Text != "" && txtAccountType.Text != "" && txtStock.Text != "" && txtMachineID.Text != "" && dateEdit1.Text != "");
                    }

                    if (check)
                    {
                        string query = "";
                        if (cmbType.Text == "خزينة")
                        {
                            query = "select Bank_Name from bank where Branch_ID=" + cmbBranch.SelectedValue.ToString() + " and Bank_Name='" + txtName.Text + "'";
                        }
                        else if (cmbType.Text == "حساب بنكى")
                        {
                            query = "select Bank_Account from bank where Bank_Account='" + txtAccountNumber.Text + "' and Bank_Name='" + txtName.Text + "'";
                        }
                        else if (cmbType.Text == "فيزا")
                        {
                            query = "SELECT Machine_ID FROM bank where Branch_ID=" + cmbBranch.SelectedValue.ToString() + " and Machine_ID='" + txtMachineID.Text + "' and Bank_Name='" + txtName.Text + "' and BankVisa_ID=" + cmbBank.SelectedValue;
                        }

                        MySqlCommand com = new MySqlCommand(query, conn);
                        conn.Open();

                        if (com.ExecuteScalar() == null)
                        {
                            conn.Close();
                            double stock = 0;
                            if (double.TryParse(txtStock.Text, out stock))
                            {
                                MySqlCommand command = conn.CreateCommand();
                                command.CommandText = "INSERT INTO bank (Bank_Type,Bank_Name,Branch_ID,Branch_Name,Bank_Stock,Start_Date,Bank_Info,Bank_Account,BankAccount_Type,BankVisa_ID,BankVisa,Machine_ID,User_ID,User_Name) VALUES (?Bank_Type,?Bank_Name,?Branch_ID,?Branch_Name,?Bank_Stock,?Start_Date,?Bank_Info,?Bank_Account,?BankAccount_Type,?BankVisa_ID,?BankVisa,?Machine_ID,?User_ID,?User_Name)";

                                if (cmbType.Text == "خزينة")
                                {
                                    command.Parameters.AddWithValue("?Bank_Type", "خزينة");
                                    command.Parameters.AddWithValue("?Bank_Name", txtName.Text);
                                    command.Parameters.AddWithValue("?Branch_ID", cmbBranch.SelectedValue.ToString());
                                    command.Parameters.AddWithValue("?Branch_Name", cmbBranch.Text);
                                    command.Parameters.AddWithValue("?Bank_Account", "");
                                    command.Parameters.AddWithValue("?BankAccount_Type", "");
                                    command.Parameters.AddWithValue("?BankVisa_ID", null);
                                    command.Parameters.AddWithValue("?BankVisa", null);
                                    command.Parameters.AddWithValue("?Machine_ID", null);
                                }
                                else if (cmbType.Text == "حساب بنكى")
                                {
                                    command.Parameters.AddWithValue("?Bank_Type", "حساب بنكى");
                                    command.Parameters.AddWithValue("?Bank_Name", txtName.Text);
                                    command.Parameters.AddWithValue("?Branch_ID", null);
                                    command.Parameters.AddWithValue("?Branch_Name", null);
                                    command.Parameters.AddWithValue("?Bank_Account", txtAccountNumber.Text);
                                    command.Parameters.AddWithValue("?BankAccount_Type", txtAccountType.Text);
                                    command.Parameters.AddWithValue("?BankVisa_ID", null);
                                    command.Parameters.AddWithValue("?BankVisa", null);
                                    command.Parameters.AddWithValue("?Machine_ID", null);
                                }
                                else if (cmbType.Text == "فيزا")
                                {
                                    command.Parameters.AddWithValue("?Bank_Type", "فيزا");
                                    command.Parameters.AddWithValue("?Bank_Name", txtName.Text);
                                    command.Parameters.AddWithValue("?Branch_ID", cmbBranch.SelectedValue.ToString());
                                    command.Parameters.AddWithValue("?Branch_Name", cmbBranch.Text);
                                    command.Parameters.AddWithValue("?Bank_Account", txtAccountNumber.Text);
                                    command.Parameters.AddWithValue("?BankAccount_Type", txtAccountType.Text);
                                    command.Parameters.AddWithValue("?BankVisa_ID", cmbBank.SelectedValue);
                                    command.Parameters.AddWithValue("?BankVisa", cmbBank.Text);
                                    command.Parameters.AddWithValue("?Machine_ID", txtMachineID.Text);
                                }
                                command.Parameters.AddWithValue("?Bank_Stock", stock);
                                command.Parameters.AddWithValue("?Start_Date", dateEdit1.DateTime.Date);
                                command.Parameters.AddWithValue("?Bank_Info", txtInformation.Text);
                                command.Parameters.AddWithValue("?User_ID", UserControl.userID);
                                command.Parameters.AddWithValue("?User_Name", UserControl.userName);
                                conn.Open();
                                command.ExecuteNonQuery();

                                //////////record adding/////////////
                                query = "select Bank_ID from bank order by Bank_ID desc limit 1";
                                com = new MySqlCommand(query, conn);
                                string BankID = com.ExecuteScalar().ToString();

                                query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                com = new MySqlCommand(query, conn);
                                com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank";
                                com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                                com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = BankID;
                                com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                                com.ExecuteNonQuery();
                                //////////////////////

                                conn.Close();
                                MessageBox.Show("تمت الاضافة");
                                cmbType.SelectedIndex = -1;
                                txtName.Text = "";
                                cmbBranch.SelectedIndex = -1;
                                txtStock.Text = "";
                                dateEdit1.Text = "";
                                txtInformation.Text = "";
                                txtAccountNumber.Text = "";
                                txtAccountType.Text = "";
                                cmbBank.SelectedIndex = -1;
                                txtMachineID.Text = "";

                                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelBranch.Text = "";
                                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelName.Text = "";
                                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelStock.Text = "";
                                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelDate.Text = "";
                                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelInfo.Text = "";
                                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelAccountNumber.Text = "";
                                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelAccountType.Text = "";
                                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelBank.Text = "";
                                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                labelID.Text = "";
                                loadBranch();
                                xtraTabPage.ImageOptions.Image = null;
                                addBankTextChangedFlag = false;
                            }
                            else
                            {
                                MessageBox.Show("الرصيد يجب ان يكون رقم");
                            }
                        }
                        else
                        {
                            MessageBox.Show("هذا العنصر تم ادخالة من قبل");
                        }
                    }
                    else
                    {
                        MessageBox.Show("برجاء ادخال جميع البيانات المطلوبة");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbType.Text == "خزينة")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBranch.Text = "*";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountNumber.Text = "";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountType.Text = "";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Text = "";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelID.Text = "";
            }
            else if (cmbType.Text == "حساب بنكى")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBranch.Text = "";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelAccountNumber.Text = "*";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelAccountType.Text = "*";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Text = "";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelID.Text = "";
            }
            else if (cmbType.Text == "فيزا")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBranch.Text = "*";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelAccountNumber.Text = "*";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelAccountType.Text = "*";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Text = "*";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelID.Text = "*";
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    xtraTabPage = getTabPage("tabPageAddBank");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                        addBankTextChangedFlag = true;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                        addBankTextChangedFlag = false;
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
            foreach (Control co in this.panel1.Controls)
            {
                //if (co is GroupBox)
                //{
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        item.Text = "";
                    }
                    else if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                //}
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

        //functions
        private void loadBranch()
        {
            conn.Open();
            string query = "select * from branch";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBranch.DataSource = dt;
            cmbBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            cmbBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            cmbBranch.SelectedIndex = -1;

            query = "select * from bank where Branch_ID is null and BankVisa_ID is null";
            da = new MySqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);
            cmbBank.DataSource = dt;
            cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
            cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
            cmbBank.SelectedIndex = -1;

            loaded = true;
        }
    }
}
