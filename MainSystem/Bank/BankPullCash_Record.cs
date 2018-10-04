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
    public partial class BankPullCash_Record : Form
    {
        MySqlConnection dbconnection, myConnection, connectionReader, connectionReader1, connectionReader2;
        bool flag2 = false;
        int billNumber = 0;
        bool flag = false;
        int customerID = 0;
        int delegateID = 0;
        int clientID = 0;
        string engName = "";
        string clientName = "";
        string delegateName = "";
        int branchID = 0;
        int ID = -1;
        double paidAmount = 0;
        string PaymentMethod;
        int[] arrOFPhaat; //count of each catagory value of money in store
        int[] arrRestMoney;
        int[] arrPaidMoney;
        bool loaded = false;
        public static bool addBankPullCashTextChangedFlag = false;
        XtraTabPage xtraTabPage;
        bool loadedPayType = false;

        public BankPullCash_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            myConnection = new MySqlConnection(connection.connectionString);
            connectionReader = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            arrOFPhaat = new int[9];
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];

            cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBranch.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void BankPullCash_Record_Load(object sender, EventArgs e)
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
            dbconnection.Close();
        }

        private void cmbBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    if (int.TryParse(cmbBranch.SelectedValue.ToString(), out branchID))
                    {
                        txtBillNumber.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (branchID > 0)
                    {
                        if (int.TryParse(txtBillNumber.Text, out billNumber))
                        {
                            dbconnection.Open();
                            string query = "select * from customer_bill where Branch_BillNumber=" + billNumber + " and Branch_ID=" + branchID + " and Type_Buy='كاش'";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            MySqlDataReader dr = com.ExecuteReader();

                            while (dr.Read())
                            {
                                flag2 = true;
                                ID = Convert.ToInt16(dr["CustomerBill_ID"].ToString());

                                myConnection.Open();
                                string query3 = "SELECT sum(Amount) FROM transitions where Bill_Number=" + billNumber + " and Branch_ID=" + branchID + " and Transition='ايداع' group by Bill_Number";
                                MySqlCommand com2 = new MySqlCommand(query3, myConnection);
                                if (com2.ExecuteScalar() != null)
                                {
                                    paidAmount = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                }
                                myConnection.Close();
                                txtPaidMoney.Text = (paidAmount).ToString();

                                txtTotalCost.Text = dr["Total_CostAD"].ToString();
                                if (dr["Customer_ID"].ToString() != "")
                                    customerID = Convert.ToInt16(dr["Customer_ID"].ToString());

                                clientID = Convert.ToInt16(dr["Client_ID"].ToString());
                                delegateID = Convert.ToInt16(dr["Delegate_ID"].ToString());
                            }
                            dr.Close();
                            if (flag2 == true)
                            {
                                //extract delgate info
                                if (delegateID > 0)
                                {
                                    query = "select * from Delegate where Delegate_ID=" + delegateID;
                                    com = new MySqlCommand(query, dbconnection);
                                    dr = com.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        delegateName = dr["Delegate_Name"].ToString();
                                    }
                                    dr.Close();
                                }
                                else
                                {
                                    dbconnection.Close();
                                    flag2 = false;
                                    txtTotalCost.Text = "";
                                    txtPullMoney.Text = "";
                                    MessageBox.Show("لابد من وجود مندوب");
                                    return;
                                }

                                //extract customer info
                                if (clientID > 0)
                                {
                                    query = "select * from customer where Customer_ID=" + clientID;
                                    com = new MySqlCommand(query, dbconnection);
                                    dr = com.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        //txtPhoneNumber.Text = dr["Customer_Phone"].ToString();
                                        clientName = dr["Customer_Name"].ToString();
                                    }
                                    dr.Close();
                                }
                                else
                                {
                                    MessageBox.Show("لابد من وجود عميل");
                                    dbconnection.Close();
                                    return;
                                }
                                if (customerID > 0)
                                {
                                    query = "select * from customer where Customer_ID=" + customerID;
                                    com = new MySqlCommand(query, dbconnection);
                                    dr = com.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        //txtPhoneNumber.Text = dr["Customer_Phone"].ToString();
                                        engName = dr["Customer_Name"].ToString();
                                    }
                                    dr.Close();
                                }
                                flag2 = false;
                            }
                            else
                            {
                                MessageBox.Show("لا يوجد فاتورة بهذا الرقم فى الفرع");
                                clear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("رقم الفاتورة يجب ان يكون رقم");
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب ان تختار فرع اولا");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                myConnection.Close();
            }
        }

        private void radioButtonSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                radCash.Enabled = true;
                radCredit.Enabled = true;
                radBankAccount.Enabled = false;
                //radVisa.Enabled = false;
                radBankAccount.Checked = false;
                //radVisa.Checked = false;
                layoutControlItemBank.Text = "خزينة";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                layoutControlItemVisaType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelVisaType.Visible = false;
                labelVisaType.Text = "";
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelOperationNumber.Visible = false;
                labelOperationNumber.Text = "";

                radCash.Checked = true;
                string query = "select * from bank where Branch_ID=" + branchID + " and Bank_Type='خزينة'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                cmbBank.SelectedIndex = -1;
                loadedPayType = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDate.Visible = false;
                labelDate.Text = "";
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelCheckNumber.Visible = false;
                labelCheckNumber.Text = "";
                groupBox1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radCredit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Visible = true;
                labelDate.Text = "*";
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelCheckNumber.Visible = true;
                labelCheckNumber.Text = "*";
                layoutControlItemPayDate.Text = "تاريخ الاستحقاق";
                layoutControlItemCheck.Text = "رقم الشيك";
                groupBox1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                radCash.Enabled = false;
                radCredit.Enabled = false;
                radCash.Checked = false;
                radCredit.Checked = false;
                radBankAccount.Enabled = true;
                //radVisa.Enabled = true;
                radBankAccount.Checked = true;
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelCheckNumber.Visible = true;
                labelCheckNumber.Text = "*";
                groupBox1.Enabled = false;
                loadedPayType = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radBankAccount_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;
                layoutControlItemBank.Text = "بنك";
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Visible = true;
                labelDate.Text = "*";
                layoutControlItemVisaType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelVisaType.Visible = false;
                labelVisaType.Text = "";
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelOperationNumber.Visible = false;
                labelOperationNumber.Text = "";
                layoutControlItemPayDate.Text = "تاريخ الايداع";
                layoutControlItemCheck.Text = "رقم الحساب";

                string query = "select * from bank where Bank_Type = 'حساب بنكى' and Branch_ID is null and BankVisa_ID is null";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                cmbBank.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radVisa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;
                layoutControlItemBank.Text = "فيزا";
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDate.Visible = false;
                labelDate.Text = "";
                layoutControlItemVisaType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelVisaType.Visible = true;
                labelVisaType.Text = "*";
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelOperationNumber.Visible = true;
                labelOperationNumber.Text = "*";
                layoutControlItemCheck.Text = "رقم الكارت";

                string query = "select * from bank where Branch_ID=" + branchID + " and Bank_Type='فيزا' and BankVisa_ID is not null";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                cmbBank.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool check = false;
                if (PaymentMethod == "نقدى")
                {
                    check = (billNumber != 0 && branchID != 0 && txtPaidMoney.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "");
                }
                else if (PaymentMethod == "شيك")
                {
                    check = (billNumber != 0 && branchID != 0 && txtPaidMoney.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (PaymentMethod == "حساب بنكى")
                {
                    check = (billNumber != 0 && branchID != 0 && txtPaidMoney.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }

                if(check)
                {
                    double outParse;
                    if (double.TryParse(txtPullMoney.Text, out outParse))
                    {
                        double restMoney = 0;
                        if (double.TryParse(txtPaidMoney.Text, out restMoney))
                        {
                            if (outParse <= restMoney)
                            {
                                string opNumString = null;
                                if (txtOperationNumber.Text != "")
                                {
                                    int OpNum = 0;
                                    if (int.TryParse(txtOperationNumber.Text, out OpNum))
                                    {
                                        opNumString = txtOperationNumber.Text;
                                    }
                                    else
                                    {
                                        MessageBox.Show("رقم العملية يجب ان يكون عدد");
                                        dbconnection.Close();
                                        return;
                                    }
                                }


                                dbconnection.Open();
                                string query = "insert into Transitions (Transition,Type,Branch_ID,Branch_Name,Bill_Number,Client_ID,Client_Name,Payment_Method,Bank_ID,Bank_Name,Date,Amount,Data,PayDay,Check_Number,Visa_Type,Operation_Number,Error) values(@Transition,@Type,@Branch_ID,@Branch_Name,@Bill_Number,@Client_ID,@Client_Name,@Payment_Method,@Bank_ID,@Bank_Name,@Date,@Amount,@Data,@PayDay,@Check_Number,@Visa_Type,@Operation_Number,@Error)";
                                MySqlCommand com = new MySqlCommand(query, dbconnection);

                                com.Parameters.Add("@Transition", MySqlDbType.VarChar, 255).Value = "سحب";
                                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "كاش";
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = cmbBranch.SelectedValue;
                                com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value = cmbBranch.Text;
                                com.Parameters.Add("@Bill_Number", MySqlDbType.Int16, 11).Value = billNumber;
                                com.Parameters.Add("@Payment_Method", MySqlDbType.VarChar, 255).Value = PaymentMethod;
                                com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = cmbBank.SelectedValue;
                                com.Parameters.Add("@Bank_Name", MySqlDbType.VarChar, 255).Value = cmbBank.Text;
                                com.Parameters.Add("@Date", MySqlDbType.Date, 0).Value = DateTime.Now.Date;
                                com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = clientID;
                                com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = clientName;
                                com.Parameters.Add("@Operation_Number", MySqlDbType.Int16, 11).Value = opNumString;
                                com.Parameters.Add("@Data", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                                com.Parameters.Add("@Error", MySqlDbType.Int16, 11).Value = 0;
                                
                                com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPullMoney.Text;
                                MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                amount2 -= outParse;
                                MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                com3.ExecuteNonQuery();
                                
                                if (txtVisaType.Text != "")
                                {
                                    com.Parameters.Add("@Visa_Type", MySqlDbType.VarChar, 255).Value = txtVisaType.Text;
                                }
                                else
                                {
                                    com.Parameters.Add("@Visa_Type", MySqlDbType.VarChar, 255).Value = null;
                                }

                                if (dateEdit1.Text != "")
                                {
                                    com.Parameters.Add("@PayDay", MySqlDbType.Date, 0).Value = dateEdit1.DateTime.Date;
                                }
                                else
                                {
                                    com.Parameters.Add("@PayDay", MySqlDbType.Date, 0).Value = null;
                                }

                                if (txtCheckNumber.Text != "")
                                {
                                    com.Parameters.Add("@Check_Number", MySqlDbType.VarChar, 255).Value = txtCheckNumber.Text;
                                }
                                else
                                {
                                    com.Parameters.Add("@Check_Number", MySqlDbType.VarChar, 255).Value = null;
                                }

                                com.ExecuteNonQuery();

                                //////////record adding/////////////
                                query = "select Transition_ID from transitions order by Transition_ID desc limit 1";
                                com = new MySqlCommand(query, dbconnection);
                                string TransitionID = com.ExecuteScalar().ToString();

                                query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "transitions";
                                com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                                com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = TransitionID;
                                com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                                com.ExecuteNonQuery();
                                //////////////////////

                                if (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(txtPullMoney.Text) == 0)
                                {
                                    query = "update customer_bill set Paid_Status=0 where CustomerBill_ID=" + ID;
                                }
                                else if (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(txtPullMoney.Text) > 0)
                                {
                                    query = "update customer_bill set Paid_Status=2 where CustomerBill_ID=" + ID;
                                }

                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();
                                dbconnection.Close();
                                
                                MessageBox.Show("تم");
                                clear();
                                RestMoney.Text = "0";
                                PaidMoney.Text = "0";
                                txtPaidRest.Text = "0";
                                txtPaidRest2.Text = "0";

                                addBankPullCashTextChangedFlag = false;
                                xtraTabPage.ImageOptions.Image = null;
                            }
                            else
                            {
                                MessageBox.Show("برجاء التاكد من المبلغ المدفوع");
                                dbconnection.Close();
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("المبلغ المدفوع يجب ان يكون عدد");
                        dbconnection.Close();
                        return;
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
            connectionReader2.Close();
            connectionReader1.Close();
            connectionReader.Close();
        }

        private void PaidMoney_KeyDown(object sender, KeyEventArgs e)
        {
            double totalPaid = 0;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double total = 0;
                    if (txtPullMoney.Text != "" && double.TryParse(txtPullMoney.Text, out total))
                    {
                        if (cmbBank.Text == "")
                        {
                            MessageBox.Show("يجب ان تختار خزينة");
                            return;
                        }
                        dbconnection.Open();

                        string query = "select ID from categories_money where Bank_ID=" + cmbBank.SelectedValue;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() == null)
                        {
                            MessageBox.Show("هذه الخزينة ليس بها فئات");
                            dbconnection.Close();
                            return;
                        }

                        if (!flag)
                        {
                            string query2 = "select * from categories_money where Bank_ID=" + cmbBank.SelectedValue;
                            MySqlCommand com2 = new MySqlCommand(query2, dbconnection);
                            MySqlDataReader dr = com2.ExecuteReader();
                            while (dr.Read())
                            {
                                arrOFPhaat[0] = Convert.ToInt16(dr["a200"]);
                                arrOFPhaat[1] = Convert.ToInt16(dr["a100"]);
                                arrOFPhaat[2] = Convert.ToInt16(dr["a50"]);
                                arrOFPhaat[3] = Convert.ToInt16(dr["a20"]);
                                arrOFPhaat[4] = Convert.ToInt16(dr["a10"]);
                                arrOFPhaat[5] = Convert.ToInt16(dr["a5"]);
                                arrOFPhaat[6] = Convert.ToInt16(dr["a1"]);
                                arrOFPhaat[7] = Convert.ToInt16(dr["aH"]);
                                arrOFPhaat[8] = Convert.ToInt16(dr["aQ"]);
                            }
                            flag = true;
                        }
                        dbconnection.Close();
                        if (PaidMoney.Text != "")
                        {
                            totalPaid = Convert.ToDouble(PaidMoney.Text);
                        }
                        TextBox txt = (TextBox)sender;
                        string txtValue = txt.Name.Split('t')[1];
                        int num;
                        num = 0;
                        switch (txtValue)
                        {
                            case "200":
                                if (int.TryParse(t200.Text, out num))
                                {
                                    if (arrOFPhaat[0] >= num)
                                    {
                                        arrOFPhaat[0] += arrPaidMoney[0];
                                        arrPaidMoney[0] = num;
                                        arrOFPhaat[0] -= num;
                                        t100.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "100":
                                if (int.TryParse(t100.Text, out num))
                                {
                                    if (arrOFPhaat[1] >= num)
                                    {
                                        arrOFPhaat[1] += arrPaidMoney[1];
                                        arrPaidMoney[1] = num;
                                        arrOFPhaat[1] -= num;
                                        t50.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "50":
                                if (int.TryParse(t50.Text, out num))
                                {
                                    if (arrOFPhaat[2] >= num)
                                    {
                                        arrOFPhaat[2] += arrPaidMoney[2];
                                        arrPaidMoney[2] = num;
                                        arrOFPhaat[2] -= num;
                                        t20.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "20":
                                if (int.TryParse(t20.Text, out num))
                                {
                                    if (arrOFPhaat[3] >= num)
                                    {
                                        arrOFPhaat[3] += arrPaidMoney[3];
                                        arrPaidMoney[3] = num;
                                        arrOFPhaat[3] -= num;
                                        t10.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "10":
                                if (int.TryParse(t10.Text, out num))
                                {
                                    if (arrOFPhaat[4] >= num)
                                    {
                                        arrOFPhaat[4] += arrPaidMoney[4];
                                        arrPaidMoney[4] = num;
                                        arrOFPhaat[4] -= num;
                                        t5.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "5":
                                if (int.TryParse(t5.Text, out num))
                                {
                                    if (arrOFPhaat[5] >= num)
                                    {
                                        arrOFPhaat[5] += arrPaidMoney[5];
                                        arrPaidMoney[5] = num;
                                        arrOFPhaat[5] -= num;
                                        t1.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "1":
                                if (int.TryParse(t1.Text, out num))
                                {
                                    if (arrOFPhaat[6] >= num)
                                    {
                                        arrOFPhaat[6] += arrPaidMoney[6];
                                        arrPaidMoney[6] = num;
                                        arrOFPhaat[6] -= num;
                                        tH.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                            case "H":
                                if (int.TryParse(tH.Text, out num))
                                {
                                    if (arrOFPhaat[7] >= num)
                                    {
                                        arrOFPhaat[7] += arrPaidMoney[7];
                                        arrPaidMoney[7] = num;
                                        arrOFPhaat[7] -= num;
                                        tQ.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                            case "Q":
                                if (int.TryParse(tQ.Text, out num))
                                {
                                    if (arrOFPhaat[8] >= num)
                                    {
                                        arrOFPhaat[8] += arrPaidMoney[8];
                                        arrPaidMoney[8] = num;
                                        arrOFPhaat[8] -= num;
                                        r200.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                        }

                        totalPaid = arrPaidMoney[0] * 200 + arrPaidMoney[1] * 100 + arrPaidMoney[2] * 50 + arrPaidMoney[3] * 20 + arrPaidMoney[4] * 10 + arrPaidMoney[5] * 5 + arrPaidMoney[6] + arrPaidMoney[7] * 0.5 + arrPaidMoney[8] * 0.25;
                        PaidMoney.Text = totalPaid.ToString();
                        if ((total - totalPaid) + Convert.ToDouble(RestMoney.Text) > 0)
                        {
                            txtPaidRest.Text = ((total - totalPaid) + Convert.ToDouble(RestMoney.Text)).ToString();
                            txtPaidRest2.Text = "0";
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.Red;
                        }
                        else if ((total - totalPaid) + Convert.ToDouble(RestMoney.Text) == 0)
                        {
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = "0";
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else
                        {
                            txtPaidRest.Text = "0";
                            double sub = (total - totalPaid);

                            txtPaidRest2.Text = (-1 * (sub + Convert.ToDouble(RestMoney.Text))).ToString();
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }

                        if ((Convert.ToDouble(txtPaidRest.Text) == 0) && (Convert.ToDouble(txtPaidRest2.Text) == 0))
                        {
                            dbconnection.Open();
                            query = "update categories_money set a200=" + arrOFPhaat[0] + ",a100=" + arrOFPhaat[1] + ",a50=" + arrOFPhaat[2] + ",a20=" + arrOFPhaat[3] + ",a10=" + arrOFPhaat[4] + ",a5=" + arrOFPhaat[5] + ",a1=" + arrOFPhaat[6] + ",aH=" + arrOFPhaat[7] + ",aQ=" + arrOFPhaat[8] + " where Bank_ID=" + cmbBank.SelectedValue;
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            MessageBox.Show("تم");
                            t200.Text = "";
                            t100.Text = "";
                            t50.Text = "";
                            t20.Text = "";
                            t10.Text = "";
                            t5.Text = "";
                            t1.Text = "";
                            tH.Text = "";
                            tQ.Text = "";
                            r200.Text = "";
                            r100.Text = "";
                            r50.Text = "";
                            r20.Text = "";
                            r10.Text = "";
                            r5.Text = "";
                            r1.Text = "";
                            rH.Text = "";
                            rQ.Text = "";
                            RestMoney.Text = "0";
                            PaidMoney.Text = "0";
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = "0";
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            for (int i = 0; i < arrPaidMoney.Length; i++)
                                arrPaidMoney[i] = arrRestMoney[i] = 0;
                            flag = false;
                        }
                        else
                        { }
                        dbconnection.Close();
                    }
                    else
                    {
                        dbconnection.Close();
                        MessageBox.Show("تاكد من المبلغ المدفوع اولا");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void RestMoney_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                double total = 0;
                if (txtPullMoney.Text != "" && double.TryParse(txtPullMoney.Text, out total))
                {
                    double totalRest = 0, test = 0;
                    if (RestMoney.Text != "")
                    {
                        totalRest = Convert.ToDouble(RestMoney.Text);
                    }

                    if (txtPaidRest.Text != "")
                        test = Convert.ToDouble(txtPaidRest.Text);

                    if (e.KeyCode == Keys.Enter)
                    {
                        TextBox txt = (TextBox)sender;
                        string txtValue = txt.Name.Split('r')[1];
                        int num;
                        num = 0;
                        switch (txtValue)
                        {
                            case "200":
                                if (int.TryParse(r200.Text, out num))
                                {
                                    arrOFPhaat[0] -= arrRestMoney[0];
                                    arrRestMoney[0] = num;
                                    arrOFPhaat[0] += num;
                                    r100.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "100":
                                if (int.TryParse(r100.Text, out num))
                                {
                                    arrOFPhaat[1] -= arrRestMoney[1];
                                    arrRestMoney[1] = num;
                                    arrOFPhaat[1] += num;
                                    r50.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "50":
                                if (int.TryParse(r50.Text, out num))
                                {
                                    arrOFPhaat[2] -= arrRestMoney[2];
                                    arrRestMoney[2] = num;
                                    arrOFPhaat[2] += num;
                                    r20.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "20":
                                if (int.TryParse(r20.Text, out num))
                                {
                                    arrOFPhaat[3] -= arrRestMoney[3];
                                    arrRestMoney[3] = num;
                                    arrOFPhaat[3] += num;
                                    r10.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "10":
                                if (int.TryParse(r10.Text, out num))
                                {
                                    arrOFPhaat[4] -= arrRestMoney[4];
                                    arrRestMoney[4] = num;
                                    arrOFPhaat[4] += num;
                                    r5.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "5":
                                if (int.TryParse(r5.Text, out num))
                                {
                                    arrOFPhaat[5] -= arrRestMoney[5];
                                    arrRestMoney[5] = num;
                                    arrOFPhaat[5] += num;
                                    r1.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "1":
                                if (int.TryParse(r1.Text, out num))
                                {
                                    arrOFPhaat[6] -= arrRestMoney[6];
                                    arrRestMoney[6] = num;
                                    arrOFPhaat[6] += num;
                                    rH.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "H":
                                if (int.TryParse(rH.Text, out num))
                                {
                                    arrOFPhaat[7] -= arrRestMoney[7];
                                    arrRestMoney[7] = num;
                                    arrOFPhaat[7] += num;
                                    rQ.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "Q":
                                if (int.TryParse(rQ.Text, out num))
                                {
                                    arrOFPhaat[8] -= arrRestMoney[8];
                                    arrRestMoney[8] = num;
                                    arrOFPhaat[8] += num;
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                        }

                        totalRest = arrRestMoney[0] * 200 + arrRestMoney[1] * 100 + arrRestMoney[2] * 50 + arrRestMoney[3] * 20 + arrRestMoney[4] * 10 + arrRestMoney[5] * 5 + arrRestMoney[6] + arrRestMoney[7] * 0.5 + arrRestMoney[8] * 0.25;
                        RestMoney.Text = totalRest.ToString();

                        if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPullMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) < 0)
                        {
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = (-1 * ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPullMoney.Text) - Convert.ToDouble(PaidMoney.Text)))))).ToString();
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPullMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) == 0)
                        {
                            txtPaidRest2.Text = "0";
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);

                            txtPaidRest.Text = "0";
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else
                        {
                            double sub = (Convert.ToDouble(txtPullMoney.Text) - Convert.ToDouble(PaidMoney.Text));
                            txtPaidRest.Text = (Convert.ToDouble(RestMoney.Text) - (-1 * sub)).ToString();
                            txtPaidRest2.Text = "0";
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }

                        if ((Convert.ToDouble(txtPaidRest.Text) == 0) && (Convert.ToDouble(txtPaidRest2.Text) == 0))
                        {
                            dbconnection.Open();
                            string query = "update categories_money set a200=" + arrOFPhaat[0] + ",a100=" + arrOFPhaat[1] + ",a50=" + arrOFPhaat[2] + ",a20=" + arrOFPhaat[3] + ",a10=" + arrOFPhaat[4] + ",a5=" + arrOFPhaat[5] + ",a1=" + arrOFPhaat[6] + ",aH=" + arrOFPhaat[7] + ",aQ=" + arrOFPhaat[8] + " where Bank_ID=" + cmbBank.SelectedValue;
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            MessageBox.Show("تم");
                            t200.Text = "";
                            t100.Text = "";
                            t50.Text = "";
                            t20.Text = "";
                            t10.Text = "";
                            t5.Text = "";
                            t1.Text = "";
                            tH.Text = "";
                            tQ.Text = "";
                            r200.Text = "";
                            r100.Text = "";
                            r50.Text = "";
                            r20.Text = "";
                            r10.Text = "";
                            r5.Text = "";
                            r1.Text = "";
                            rH.Text = "";
                            rQ.Text = "";
                            RestMoney.Text = "0";
                            PaidMoney.Text = "0";
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = "0";
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            for (int i = 0; i < arrRestMoney.Length; i++)
                                arrRestMoney[i] = arrPaidMoney[i] = 0;
                            flag = false;
                        }
                        else
                        { }
                        dbconnection.Close();
                    }
                }
                else
                {
                    dbconnection.Close();
                    MessageBox.Show("تاكد من المبلغ المدفوع اولا");
                    return;
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
                if (loaded || loadedPayType)
                {
                    xtraTabPage = getTabPage("tabPageRecordPullCash");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                        addBankPullCashTextChangedFlag = true;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                        addBankPullCashTextChangedFlag = false;
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
            dbconnection.Open();
            string query = "select * from branch";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBranch.DataSource = dt;
            cmbBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            cmbBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            cmbBranch.SelectedIndex = -1;

            loaded = true;
        }

        /*public void DecreaseProductQuantity()
        {
            connectionReader.Open();
            connectionReader1.Open();
            connectionReader2.Open();
            string q;
            int id;
            bool flag = false;
            double storageQ, productQ;

            string query = "select RecivedType from customer_bill where CustomerBill_ID=" + ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            string store = com.ExecuteScalar().ToString();
            if (store != "العميل")
            {
                #region not customer
                query = "select Code,Quantity,Set_ID from product_bill where CustomerBill_ID=" + ID;
                com = new MySqlCommand(query, connectionReader);
                MySqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    if (dr["Code"].ToString().Length < 20)
                    {
                        query = "select Code,Quantity from offer_details where Offer_ID=" + Convert.ToInt16(dr["Code"].ToString());
                        com = new MySqlCommand(query, connectionReader1);
                        MySqlDataReader dr1 = com.ExecuteReader();

                        while (dr1.Read())
                        {
                            query = "select sum(Total_Meters) from storage where Code='" + dr1["Code"].ToString() + "' and Store_Name='" + store + "'";
                            com = new MySqlCommand(query, connectionReader2);
                            double quantityInStore = 0;
                            if (com.ExecuteScalar().ToString() != "")
                            {
                                quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                            }
                            productQ = Convert.ToDouble(dr1["Quantity"]) * Convert.ToDouble(dr["Quantity"]);
                            if (quantityInStore >= productQ)
                            {
                                query = "select Storage_ID,Total_Meters from storage where Code='" + dr1["Code"].ToString() + "' and Store_Name='" + store + "'";
                                com = new MySqlCommand(query, connectionReader2);
                                MySqlDataReader dr2 = com.ExecuteReader();
                                while (dr2.Read())
                                {

                                    storageQ = Convert.ToDouble(dr2["Total_Meters"]);

                                    if (storageQ > productQ)
                                    {
                                        id = Convert.ToInt16(dr2["Storage_ID"]);
                                        q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                        comm.ExecuteNonQuery();
                                        productQ -= storageQ;
                                        flag = true;
                                        break;
                                    }
                                    else
                                    {
                                        id = Convert.ToInt16(dr2["Storage_ID"]);
                                        q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                        comm.ExecuteNonQuery();
                                        productQ -= storageQ;
                                    }
                                }
                                dr2.Close();

                                if (!flag)
                                {
                                    MessageBox.Show(dr["Code"].ToString() + " لا يوجد منه كمية كافية فى المخزن");
                                    return;
                                }
                                flag = false;
                            }
                            else
                            {
                                MessageBox.Show(dr["Code"].ToString() + " لا يوجد منه كمية كافية فى المخزن");
                                return;
                            }
                        }
                        dr1.Close();
                    }
                    else if (dr["Code"].ToString().Length == 20)
                    {
                        if (dr["Set_ID"].ToString() == "")
                        {
                            #region بند
                            query = "select sum(Total_Meters) from storage where Code='" + dr["Code"].ToString() + "' and Store_Name='" + store + "'";
                            com = new MySqlCommand(query, connectionReader2);
                            double quantityInStore = 0;
                            if (com.ExecuteScalar().ToString() != "")
                            {
                                quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                            }
                            productQ = Convert.ToDouble(dr["Quantity"]);
                            if (quantityInStore >= productQ)
                            {
                                query = "select Storage_ID,Total_Meters from storage where Code='" + dr["Code"].ToString() + "' and Store_Name='" + store + "'";
                                com = new MySqlCommand(query, connectionReader2);
                                MySqlDataReader dr2 = com.ExecuteReader();
                                while (dr2.Read())
                                {

                                    storageQ = Convert.ToDouble(dr2["Total_Meters"]);

                                    if (storageQ > productQ)
                                    {
                                        id = Convert.ToInt16(dr2["Storage_ID"]);
                                        q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                        comm.ExecuteNonQuery();
                                        productQ -= storageQ;
                                        flag = true;
                                        break;
                                    }
                                    else
                                    {
                                        id = Convert.ToInt16(dr2["Storage_ID"]);
                                        q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                        comm.ExecuteNonQuery();
                                        productQ -= storageQ;
                                    }
                                }
                                dr2.Close();

                                if (!flag)
                                {
                                    MessageBox.Show(dr["Code"].ToString() + " لا يوجد منه كمية كافية فى المخزن");
                                    return;
                                }
                                flag = false;
                            }
                            else
                            {
                                MessageBox.Show(dr["Code"].ToString() + " لا يوجد منه كمية كافية فى المخزن");
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            #region طقم
                            query = "select Quantity from set_details where Set_ID=" + dr["Code"].ToString() + " order by SetDetails_ID limit 1";
                            com = new MySqlCommand(query, dbconnection);
                            double itemQuantity = Convert.ToDouble(com.ExecuteScalar());

                            query = "select sum(Total_Meters) from storage where Code='" + dr["Set_ID"].ToString() + "' and Store_Name='" + store + "'";
                            com = new MySqlCommand(query, connectionReader2);
                            double quantityInStore = 0;
                            if (com.ExecuteScalar().ToString() != "")
                            {
                                quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                            }
                            productQ = Convert.ToDouble(dr["Quantity"]) / itemQuantity;
                            if (quantityInStore >= productQ)
                            {
                                query = "select Storage_ID,Total_Meters from storage where Code='" + dr["Set_ID"].ToString() + "' and Store_Name='" + store + "'";
                                com = new MySqlCommand(query, connectionReader2);
                                MySqlDataReader dr2 = com.ExecuteReader();
                                while (dr2.Read())
                                {

                                    storageQ = Convert.ToDouble(dr2["Total_Meters"]);

                                    if (storageQ > productQ)
                                    {
                                        id = Convert.ToInt16(dr2["Storage_ID"]);
                                        q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                        comm.ExecuteNonQuery();
                                        productQ -= storageQ;
                                        flag = true;
                                        break;
                                    }
                                    else
                                    {
                                        id = Convert.ToInt16(dr2["Storage_ID"]);
                                        q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                        comm.ExecuteNonQuery();
                                        productQ -= storageQ;
                                    }
                                }
                                dr2.Close();

                                if (!flag)
                                {
                                    MessageBox.Show(dr["Set_ID"].ToString() + " لا يوجد منه كمية كافية فى المخزن");
                                    return;
                                }
                                flag = false;
                            }
                            else
                            {
                                MessageBox.Show(dr["Set_ID"].ToString() + " لا يوجد منه كمية كافية فى المخزن");
                                return;
                            }
                            #endregion
                        }
                    }
                }
                dr.Close();
                //successFlag = true;

                #endregion
            }
            connectionReader2.Close();
            connectionReader1.Close();
            connectionReader.Close();
        }*/
    }
}
