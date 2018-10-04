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
    public partial class Customer_Update : Form
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection2;
        string Customer_Type = "";
        bool flag = false;    //to check if the customer have guide or not
        XtraTabPage xtraTabPage;
        DataRowView selRow;

        bool loadedflag = false;

        public Customer_Update(DataRowView SelRow)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            selRow = SelRow;
            
            comEnginner.AutoCompleteMode = AutoCompleteMode.Suggest;
            comEnginner.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Customer_Update_Load(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = selRow["الاسم"].ToString();
                //txtPhone.Text = selRow["رقم التليفون"].ToString();
                txtAddress.Text = selRow["العنوان"].ToString();
                txtEmail.Text = selRow["الايميل"].ToString();
                txtNationalID.Text = selRow["الرقم القومى"].ToString();
                txtINF.Text = selRow["البيان"].ToString();

                dbconnection2.Open();
                string qury = "SELECT customer.Customer_ID as 'التسلسل',Phone as 'رقم التليفون' FROM customer_phone inner join customer on customer.Customer_ID=customer_phone.Customer_ID where customer.Customer_ID=" + selRow[0].ToString();
                MySqlCommand comm = new MySqlCommand(qury, dbconnection2);
                MySqlDataReader dr1 = comm.ExecuteReader();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        checkedListBoxControlPhone.Items.Add(dr1["رقم التليفون"].ToString());
                    }
                }
                dr1.Close();

                if (selRow["النوع"].ToString() == "عميل")
                {
                    radClient.Checked = true;

                    string query = "select customer1.Customer_Type as 'النوع', customer1.Customer_Name as 'المسئول' from customer as customer1 INNER JOIN custmer_client ON custmer_client.Customer_ID = customer1.Customer_ID INNER JOIN customer as customer2 ON custmer_client.Client_ID = customer2.Customer_ID where customer2.Customer_ID=" + selRow[0].ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection2);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if(dr["النوع"].ToString() == "مهندس")
                            {
                                radEng.Checked = true;
                            }
                            else if (dr["النوع"].ToString() == "مقاول")
                            {
                                radCon.Checked = true;
                            }
                            else if (dr["النوع"].ToString() == "تاجر")
                            {
                                radDealer.Checked = true;
                            }
                            comEnginner.Text = dr["المسئول"].ToString();
                        }
                        dr.Close();
                    }
                }
                else if (selRow["النوع"].ToString() == "مهندس")
                {
                    radMEng.Checked = true;
                }
                else if (selRow["النوع"].ToString() == "مقاول")
                {
                    radMCon.Checked = true;
                }
                else if (selRow["النوع"].ToString() == "تاجر")
                {
                    radMDealer.Checked = true;
                }
                loadedflag = true;
                //dbconnection2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //dbconnection2.Close();
            }
            dbconnection2.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
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
                        dbconnection.Open();
                        string query = "update customer set Customer_NationalID=@Customer_NationalID,Customer_Email=@Customer_Email,Customer_Address=@Customer_Address,Customer_Info=@Customer_Info,Customer_Type=@Customer_Type where Customer_ID=" + selRow[0].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
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
                        if (Customer_Type == "عميل" && flag)
                        {
                            AddClientToEng_Con();
                        }
                        
                        UserControl.ItemRecord("customer", "تعديل", Convert.ToInt16(selRow[0].ToString()), DateTime.Now, textBox.Text, dbconnection);
                        
                        //MessageBox.Show("تم");
                        //clear();
                        xtraTabPage.ImageOptions.Image = null;
                        MainForm.objFormCustomer.search();
                        MainForm.tabControlSales.TabPages.Remove(MainForm.MainTabPageUpdateCustomer);
                    }
                    else
                    {
                        MessageBox.Show("يجب كتابة السبب");
                    }
                }
                else
                { }
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
        //link clients wither thier guides 
        public void AddClientToEng_Con()
        {
            string query = "SELECT Customer_ID FROM custmer_client where Client_ID=" + selRow[0].ToString();
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() == null)
            {
                query = "insert into Custmer_Client(Customer_ID,Client_ID) values(@Customer_ID,@Client_ID)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 255);
                com.Parameters["@Customer_ID"].Value = comEnginner.SelectedValue;
                com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 255);
                com.Parameters["@Client_ID"].Value = selRow[0].ToString();
                com.ExecuteNonQuery();
            }
            else
            {
                query = "update Custmer_Client set Customer_ID=@Customer_ID where Client_ID=" + selRow[0].ToString();
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 255);
                com.Parameters["@Customer_ID"].Value = comEnginner.SelectedValue;
                com.ExecuteNonQuery();
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loadedflag)
                {
                    //if (comEnginner.Text != "")
                    //{
                        xtraTabPage = getTabPage("tabPageUpdateCustomer");
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

        private void comEnginner_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if(loadedflag)
                {
                    flag = true; //client have guide engineer or contract
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                AddPhoneNumbers();
                checkedListBoxControlPhone.Items.Add(txtPhone.Text);
                txtPhone.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //function
        public bool checkPhoneExist()
        {
            string query = "SELECT customer.Customer_ID FROM customer INNER JOIN customer_phone ON customer_phone.Customer_ID = customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
                return false;
            else
                return true;
        }

        void AddPhoneNumbers()
        {
            string query = "insert into customer_phone(Customer_ID,phone) values(@Customer_ID,@Phone)";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11);
            com.Parameters["@Customer_ID"].Value = Convert.ToInt16(selRow[0].ToString());
            com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
            com.Parameters["@Phone"].Value = txtPhone.Text;
            com.ExecuteNonQuery();
        }

        private void btnDeletePhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxControlPhone.CheckedItemsCount > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    dbconnection.Open();
                    ArrayList temp = new ArrayList();
                    foreach (int index in checkedListBoxControlPhone.CheckedIndices)
                        temp.Add(checkedListBoxControlPhone.Items[index]);
                    foreach (object item in temp)
                    {
                        string query = "delete from customer_phone where Customer_ID=" + selRow[0].ToString() + " and phone='" + item.ToString() + "'";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        checkedListBoxControlPhone.Items.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
    }
}
