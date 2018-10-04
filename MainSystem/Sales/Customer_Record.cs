using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
    public partial class Customer_Record : Form
    {
        MySqlConnection dbconnection;
        string Customer_Type = "";
        bool flag = false; //to check if the customer have guide or not
        XtraTabPage xtraTabPage;

        public Customer_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            
            comEnginner.AutoCompleteMode = AutoCompleteMode.Suggest;
            comEnginner.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void btnAddPhone_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (checkPhoneExist())
                {
                    for (int i = 0; i < checkedListBoxControlPhone.ItemCount; i++)
                    {
                        if (txtPhone.Text == checkedListBoxControlPhone.Items[i].Value.ToString())
                        {
                            MessageBox.Show("هذا الرقم تم اضافتة");
                            dbconnection.Close();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("هذا الرقم موجود من قبل");
                    dbconnection.Close();
                    return;
                }

                checkedListBoxControlPhone.Items.Add(txtPhone.Text);
                txtPhone.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDeletePhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxControlPhone.CheckedItemsCount > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    ArrayList temp = new ArrayList();
                    foreach (int index in checkedListBoxControlPhone.CheckedIndices)
                        temp.Add(checkedListBoxControlPhone.Items[index]);
                    foreach (object item in temp)
                        checkedListBoxControlPhone.Items.Remove(item);
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
                if (txtName.Text != "" && checkedListBoxControlPhone.ItemCount > 0)
                {
                    if (Customer_Type == "عميل" && flag)
                    {
                        if (comEnginner.Text != "")
                        { }
                        else
                        {
                            MessageBox.Show("يجب اختيار الضامن");
                            dbconnection.Close();
                            return;
                        }
                    }
                    dbconnection.Open();
                    if (checkPhonesExist())
                    {
                        string query = "insert into customer (Customer_NationalID,Customer_Email,Customer_Name,Customer_Address,Customer_Info,Customer_Start,Customer_Type) values(@Customer_NationalID,@Customer_Email,@Customer_Name,@Customer_Address,@Customer_Info,@Customer_Start,@Customer_Type)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255);
                        com.Parameters["@Customer_Name"].Value = txtName.Text;
                        //com.Parameters.Add("@Customer_Phone", MySqlDbType.VarChar, 255);
                        //com.Parameters["@Customer_Phone"].Value = txtPhone.Text;
                        com.Parameters.Add("@Customer_Address", MySqlDbType.VarChar, 255);
                        com.Parameters["@Customer_Address"].Value = txtAddress.Text;
                        com.Parameters.Add("@Customer_Email", MySqlDbType.VarChar, 255);
                        com.Parameters["@Customer_Email"].Value = txtEmail.Text;
                        if (txtNationalID.Text != "")
                        {
                            com.Parameters.Add("@Customer_NationalID", MySqlDbType.VarChar, 255);
                            com.Parameters["@Customer_NationalID"].Value = txtNationalID.Text;
                        }
                        else
                        {
                            com.Parameters.Add("@Customer_NationalID", MySqlDbType.VarChar, 255);
                            com.Parameters["@Customer_NationalID"].Value = null;
                        }
                        com.Parameters.Add("@Customer_Start", MySqlDbType.Date, 255);
                        com.Parameters["@Customer_Start"].Value = DateTime.Now.Date;
                        com.Parameters.Add("@Customer_Info", MySqlDbType.VarChar, 255);
                        com.Parameters["@Customer_Info"].Value = txtINF.Text;
                        if (Customer_Type != "")
                        {
                            com.Parameters.Add("@Customer_Type", MySqlDbType.VarChar, 255);
                            com.Parameters["@Customer_Type"].Value = Customer_Type;
                        }
                        else
                        {
                            MessageBox.Show("برجاء اختيار النوع");
                            dbconnection.Close();
                            return;
                        }
                        com.ExecuteNonQuery();

                        AddPhoneNumbers();

                        if (Customer_Type == "عميل" && flag)
                        {
                            AddClientToEng_Con();
                        }
                        //MessageBox.Show("تم");
                        clear();
                        xtraTabPage.ImageOptions.Image = null;
                        MainForm.objFormCustomer.search();
                    }
                    else
                    {
                        MessageBox.Show("يوجد رقم موجود من قبل");
                        dbconnection.Close();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال البيانات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            flag = false;//to check if the customer have guide or not
            if (Customer_Type == "عميل")
            {
                radCon.Visible = true;
                radEng.Visible = true;
                radDealer.Visible = true;
                comEnginner.Visible = true;
                labelName.Visible = true;
            }
            else
            {
                radEng.Visible = false;
                radCon.Visible = false;
                radDealer.Visible = false;
                comEnginner.Visible = false;
                labelName.Visible = false;
                radEng.Checked = false;
                radCon.Checked = false;
                radDealer.Checked = false;
            }
        }
        //check type of CustomerGuide if engineer or contract
        private void radType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            flag = true;//client have guide engineer or contract
            string CustomType = radio.Text;
            try
            {
                dbconnection.Open();
                if (CustomType == "مهندس")
                {
                    string query = "select * from customer where Customer_Type='مهندس'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEnginner.DataSource = dt;
                    comEnginner.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEnginner.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comEnginner.Text = "";
                }
                else if (CustomType == "مقاول")
                {
                    string query = "select * from customer where Customer_Type='مقاول'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEnginner.DataSource = dt;
                    comEnginner.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEnginner.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comEnginner.Text = "";
                }
                else if (CustomType == "تاجر")
                {
                    string query = "select * from customer where Customer_Type='تاجر'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEnginner.DataSource = dt;
                    comEnginner.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEnginner.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comEnginner.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //function
        //check if name already used
        public bool checkPhonesExist()
        {
            MySqlCommand com;
            //string query = "select Customer_ID from customer where Customer_Phone='"+txtPhone.Text+"'";
            for (int i = 0; i < checkedListBoxControlPhone.ItemCount; i++)
            {
                string query = "SELECT customer.Customer_ID FROM customer INNER JOIN customer_phone ON customer_phone.Customer_ID = customer.Customer_ID where customer_phone.Phone='" + checkedListBoxControlPhone.Items[i].Value.ToString() + "'";
                com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                    return false;
            }
            return true;
        }

        public bool checkPhoneExist()
        {
            string query = "SELECT customer.Customer_ID FROM customer INNER JOIN customer_phone ON customer_phone.Customer_ID = customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
                return false;
            else
                return true;
        }

        //insert phone numbers to client
        void AddPhoneNumbers()
        {
            string query = "SELECT Customer_ID FROM customer ORDER BY Customer_ID DESC LIMIT 1";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int id = 0;
            if (com.ExecuteScalar() != null)
            {
                id = (int)com.ExecuteScalar();
            }

            for (int i = 0; i < checkedListBoxControlPhone.ItemCount; i++)
            {
                query = "insert into customer_phone(Customer_ID,phone) values(@Customer_ID,@Phone)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11);
                com.Parameters["@Customer_ID"].Value = id;
                com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
                com.Parameters["@Phone"].Value = checkedListBoxControlPhone.Items[i].Value.ToString();
                com.ExecuteNonQuery();
            }
        }

        //link clients wither thier guides 
        public void AddClientToEng_Con()
        {
            string query = "SELECT Customer_ID FROM customer ORDER BY Customer_ID DESC LIMIT 1";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int id = 0;
            if (com.ExecuteScalar() != null)
            {
                id = (int)com.ExecuteScalar();
            }

            query = "insert into Custmer_Client(Customer_ID,Client_ID)values(@Customer_ID,@Client_ID)";
            com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11);
            com.Parameters["@Customer_ID"].Value = comEnginner.SelectedValue;
            com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11);
            com.Parameters["@Client_ID"].Value = id;
            com.ExecuteNonQuery();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (comEnginner.Text != "")
                //{
                    xtraTabPage = getTabPage("tabPageAddCustomer");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //clear function
        public void clear()
        {
            foreach (Control co in this.tableLayoutPanel1.Controls)
            {
                //if (co is GroupBox)
                //{
                foreach (Control item in co.Controls)
                {
                    if (item is System.Windows.Forms.ComboBox)
                    {
                        item.Text = "";
                    }
                    else if (item is TextBox)
                    {
                        item.Text = "";
                    }
                    else if (item is CheckedListBoxControl)
                    {
                        int cont = checkedListBoxControlPhone.ItemCount;
                        for (int i = 0; i < cont; i++)
                        {
                            checkedListBoxControlPhone.Items.RemoveAt(0);
                        }
                    }
                    else if (item is GroupBox)
                    {
                        item.Controls.Clear();
                    }
                }
                //}
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlSales.TabPages.Count; i++)
                if (MainForm.tabControlSales.TabPages[i].Name == text)
                {
                    return MainForm.tabControlSales.TabPages[i];
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
                    if (item is System.Windows.Forms.ComboBox)
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
