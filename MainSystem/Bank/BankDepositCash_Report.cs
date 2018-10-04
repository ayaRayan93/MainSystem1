using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
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
    public partial class BankDepositCash_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl MainTabControlBank;

        public static XtraTabPage MainTabPageRecordDepositCash;
        Panel panelRecordDepositCash;

        public static XtraTabPage MainTabPageUpdateDepositCash;
        Panel panelUpdateDepositCash;

        public static XtraTabPage MainTabPagePrintingDepositCash;
        Panel panelPrintingDepositCash;


        public static BankDepositCash_Print bankPrint;

        public static GridControl gridcontrol;

        public BankDepositCash_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            MainTabControlBank = MainForm.tabControlBank;

            MainTabPageRecordDepositCash = new XtraTabPage();
            panelRecordDepositCash = new Panel();

            MainTabPageUpdateDepositCash = new XtraTabPage();
            panelUpdateDepositCash = new Panel();

            MainTabPagePrintingDepositCash = new XtraTabPage();
            panelPrintingDepositCash = new Panel();


            gridcontrol = gridControl1;
        }

        private void BankDepositCash_Report_Load(object sender, EventArgs e)
        {
            try
            {
                search();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (BankDepositCash_Record.addBankDepositCashTextChangedFlag == false)
                {
                    MainTabPageRecordDepositCash.Name = "tabPageRecordDepositCash";
                    MainTabPageRecordDepositCash.Text = "اضافة ايداع-كاش";
                    MainTabPageRecordDepositCash.ImageOptions.Image = null;
                    panelRecordDepositCash.Name = "panelRecordDepositCash";
                    panelRecordDepositCash.Dock = DockStyle.Fill;
                    
                    panelRecordDepositCash.Controls.Clear();
                    BankDepositCash_Record form = new BankDepositCash_Record();
                    form.Size = new Size(1059, 638);
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    panelRecordDepositCash.Controls.Add(form);
                    MainTabPageRecordDepositCash.Controls.Add(panelRecordDepositCash);
                    MainTabControlBank.TabPages.Add(MainTabPageRecordDepositCash);
                    form.Show();
                    MainTabControlBank.SelectedTabPage = MainTabPageRecordDepositCash;
                }
                else
                {
                    if (MessageBox.Show("هناك تعديلات لم تحفظ بعد..هل انت متاكد انك تريد التجاهل؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        MainTabControlBank.SelectedTabPage = MainTabPageRecordDepositCash;
                        return;
                    }
                    else
                    {
                        panelRecordDepositCash.Controls.Clear();
                        MainTabPageRecordDepositCash.ImageOptions.Image = null;
                        BankDepositCash_Record form = new BankDepositCash_Record();
                        form.Size = new Size(1059, 638);
                        form.TopLevel = false;
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.Dock = DockStyle.Fill;
                        panelRecordDepositCash.Controls.Add(form);
                        form.Show();
                        MainTabControlBank.SelectedTabPage = MainTabPageRecordDepositCash;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));

                if (selRow["Error"].ToString() == "0")
                {
                    if (BankDepositCash_Update.updateBankDepositCashTextChangedFlag == false)
                    {
                        if (selRow[0].ToString() != "")
                        {
                            MainTabPageUpdateDepositCash.Name = "tabPageUpdateDepositCash";
                            MainTabPageUpdateDepositCash.Text = "تعديل ايداع-كاش";
                            MainTabPageUpdateDepositCash.ImageOptions.Image = null;
                            panelUpdateDepositCash.Name = "panelUpdateDepositCash";
                            panelUpdateDepositCash.Dock = DockStyle.Fill;

                            panelUpdateDepositCash.Controls.Clear();
                            BankDepositCash_Update form = new BankDepositCash_Update(selRow);
                            form.Size = new Size(1059, 638);
                            form.TopLevel = false;
                            form.FormBorderStyle = FormBorderStyle.None;
                            form.Dock = DockStyle.Fill;
                            panelUpdateDepositCash.Controls.Add(form);
                            MainTabPageUpdateDepositCash.Controls.Add(panelUpdateDepositCash);
                            MainTabControlBank.TabPages.Add(MainTabPageUpdateDepositCash);
                            form.Show();
                            MainTabControlBank.SelectedTabPage = MainTabPageUpdateDepositCash;
                        }
                        else
                        {
                            MessageBox.Show("يجب ان تختار عنصر");
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("هناك تعديلات لم تحفظ بعد..هل انت متاكد انك تريد التجاهل؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        {
                            MainTabControlBank.SelectedTabPage = MainTabPageUpdateDepositCash;
                            return;
                        }
                        else
                        {
                            if (selRow[0].ToString() != "")
                            {
                                panelUpdateDepositCash.Controls.Clear();
                                MainTabPageUpdateDepositCash.ImageOptions.Image = null;
                                BankDepositCash_Update form = new BankDepositCash_Update(selRow);
                                form.Size = new Size(1059, 638);
                                form.TopLevel = false;
                                form.FormBorderStyle = FormBorderStyle.None;
                                form.Dock = DockStyle.Fill;
                                panelUpdateDepositCash.Controls.Add(form);
                                form.Show();
                                MainTabControlBank.SelectedTabPage = MainTabPageUpdateDepositCash;
                            }
                            else
                            {
                                MessageBox.Show("يجب ان تختار عنصر");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("هذا العنصر تم حذفة من قبل");
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
                GridView view = gridView1 as GridView;
                delete(view);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                MainTabPagePrintingDepositCash.Name = "tabPagePrintingDepositCash";
                MainTabPagePrintingDepositCash.Text = "طباعة الايداعات-كاش";
                panelPrintingDepositCash.Name = "panelPrintingDepositCash";
                panelPrintingDepositCash.Dock = DockStyle.Fill;
                
                panelPrintingDepositCash.Controls.Clear();
                bankPrint = new BankDepositCash_Print();
                bankPrint.Size = new Size(1059, 638);
                bankPrint.TopLevel = false;
                bankPrint.FormBorderStyle = FormBorderStyle.None;
                bankPrint.Dock = DockStyle.Fill;
                panelPrintingDepositCash.Controls.Add(bankPrint);
                MainTabPagePrintingDepositCash.Controls.Add(panelPrintingDepositCash);
                MainTabControlBank.TabPages.Add(MainTabPagePrintingDepositCash);
                bankPrint.Show();
                MainTabControlBank.SelectedTabPage = MainTabPagePrintingDepositCash;

                MainForm.loadedPrintCash = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //GridView view = sender as GridView;
                    GridView view = gridView1 as GridView;
                    delete(view);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            try
            {
                if (e.SelectedControl != gridControl1) return;
                GridHitInfo gridhitinfo = gridView1.CalcHitInfo(e.ControlMousePosition);
                object o = gridhitinfo.HitTest.ToString();
                string text = gridhitinfo.HitTest.ToString();
                e.Info = new DevExpress.Utils.ToolTipControlInfo(o, text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Error"]);
                    //if (category == "1")
                    //{
                    //    e.Appearance.BackColor = Color.Salmon;
                    //}

                    int mod = e.RowHandle % 2;
                    //odd=1 , even=0
                    if (mod == 1)
                    {
                        if (category == "1")
                        {
                            e.Appearance.BackColor = Color.Salmon;
                        }
                        else
                        {
                            e.Appearance.BackColor = Color.DarkGray;
                        }
                    }
                    if (mod == 0)
                    {
                        if (category == "1")
                        {
                            e.Appearance.BackColor = Color.Salmon;
                        }
                        else
                        {
                            e.Appearance.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void search()
        {
            //,customer.Customer_Name as 'المهندس/المقاول/التاجر'
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT transitions.Transition_ID as 'التسلسل',transitions.Bill_Number as 'الفاتورة',transitions.Branch_Name as 'الفرع',transitions.Bank_ID,transitions.Bank_Name as 'الخزينة',transitions.Amount as 'المبلغ',transitions.Date as 'التاريخ',transitions.Payment_Method as 'طريقة الدفع',transitions.Client_ID,transitions.Client_Name as 'العميل',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك/الكارت',transitions.Visa_Type as 'نوع الكارت',transitions.Operation_Number as 'رقم العملية',transitions.Data as 'البيان',transitions.Error,transitions.Branch_ID FROM transitions where transitions.Transition='ايداع' and transitions.Type='كاش' order by transitions.Date", conn);

            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);

            gridControl1.DataSource = sourceDataSet.Tables[0];
            gridView1.Columns[8].Visible = false;
            gridView1.Columns[15].Visible = false;
            gridView1.Columns[3].Visible = false;
            gridView1.Columns["Branch_ID"].Visible = false;

            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 150;
            }
        }

        void delete(GridView view)
        {
            int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
            if (selRows.Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                
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
                Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب الحذف؟" };
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
                        conn.Open();
                        for (int i = 0; i < selRows.Length; i++)
                        {
                            DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[i]));

                            string query = "update Transitions set Error=1 where Transition_ID=" + selRow[0].ToString();
                            MySqlCommand comand = new MySqlCommand(query, conn);
                            comand.ExecuteNonQuery();

                            query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                            comand = new MySqlCommand(query, conn);
                            comand.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                            comand.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "transitions";
                            comand.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "حذف";
                            comand.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow[0].ToString();
                            comand.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                            comand.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
                            comand.ExecuteNonQuery();

                            //Transition_ID,Bill_Number,Branch_Name,Bank_ID,Transition_Case,Transition_Amount,Transition_Date,Transition_Type,Client_ID,Client_Name,Payday,Check_Number,Visa_Type,Operation_Number,Transition_Data
                            MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + selRow[3].ToString(), conn);
                            double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                            amount2 -= Convert.ToDouble(selRow[5].ToString());
                            MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + selRow[3].ToString(), conn);
                            com3.ExecuteNonQuery();

                            query = "update customer_bill set Paid_Status=2 where Branch_ID=" + selRow["Branch_ID"].ToString() + " and Branch_BillNumber=" + selRow["Bill_Number"].ToString() + " and Type_Buy='كاش'";
                            comand = new MySqlCommand(query, conn);
                            comand.ExecuteNonQuery();
                        }
                        conn.Close();
                        search();
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
                MessageBox.Show("يجب ان تختار عنصر للحذف");
            }
        }
    }
}
