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
    public partial class BankTransfers_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl MainTabControlBank;

        public static XtraTabPage MainTabPageRecordBankTransfer;
        Panel panelRecordBankTransfer;

        public static XtraTabPage MainTabPageUpdateBankTransfer;
        Panel panelUpdateBankTransfer;

        public static XtraTabPage MainTabPagePrintingBankTransfer;
        Panel panelPrintingBankTransfer;
        
        public static BankTransfers_Print bankPrint;

        public static GridControl gridcontrol;

        public BankTransfers_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            MainTabControlBank = MainForm.tabControlBank;

            MainTabPageRecordBankTransfer = new XtraTabPage();
            panelRecordBankTransfer = new Panel();

            MainTabPageUpdateBankTransfer = new XtraTabPage();
            panelUpdateBankTransfer = new Panel();

            MainTabPagePrintingBankTransfer = new XtraTabPage();
            panelPrintingBankTransfer = new Panel();
            
            gridcontrol = gridControl1;
        }

        private void BankTransfers_Report_Load(object sender, EventArgs e)
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
                if (BankTransfers_Record.addBankTransferTextChangedFlag == false)
                {
                    MainTabPageRecordBankTransfer.Name = "tabPageRecordBankTransfer";
                    MainTabPageRecordBankTransfer.Text = "اضافة تحويل";
                    MainTabPageRecordBankTransfer.ImageOptions.Image = null;
                    panelRecordBankTransfer.Name = "panelRecordBankTransfer";
                    panelRecordBankTransfer.Dock = DockStyle.Fill;

                    panelRecordBankTransfer.Controls.Clear();
                    BankTransfers_Record form = new BankTransfers_Record();
                    form.Size = new Size(1059, 638);
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    panelRecordBankTransfer.Controls.Add(form);
                    MainTabPageRecordBankTransfer.Controls.Add(panelRecordBankTransfer);
                    MainTabControlBank.TabPages.Add(MainTabPageRecordBankTransfer);
                    form.Show();
                    MainTabControlBank.SelectedTabPage = MainTabPageRecordBankTransfer;
                }
                else
                {
                    if (MessageBox.Show("هناك تعديلات لم تحفظ بعد..هل انت متاكد انك تريد التجاهل؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        MainTabControlBank.SelectedTabPage = MainTabPageRecordBankTransfer;
                        return;
                    }
                    else
                    {
                        panelRecordBankTransfer.Controls.Clear();
                        MainTabPageRecordBankTransfer.ImageOptions.Image = null;
                        BankTransfers_Record form = new BankTransfers_Record();
                        form.Size = new Size(1059, 638);
                        form.TopLevel = false;
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.Dock = DockStyle.Fill;
                        panelRecordBankTransfer.Controls.Add(form);
                        form.Show();

                        MainTabControlBank.SelectedTabPage = MainTabPageRecordBankTransfer;
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
                    if (BankTransfers_Update.updateBankTransferTextChangedFlag == false)
                    {
                        if (selRow[0].ToString() != "")
                        {
                            MainTabPageUpdateBankTransfer.Name = "tabPageUpdateBankTransfer";
                            MainTabPageUpdateBankTransfer.Text = "تعديل تحويل";
                            MainTabPageUpdateBankTransfer.ImageOptions.Image = null;
                            panelUpdateBankTransfer.Name = "PanelUpdateBankTransfer";
                            panelUpdateBankTransfer.Dock = DockStyle.Fill;

                            panelUpdateBankTransfer.Controls.Clear();
                            BankTransfers_Update form = new BankTransfers_Update(selRow);
                            form.Size = new Size(1059, 638);
                            form.TopLevel = false;
                            form.FormBorderStyle = FormBorderStyle.None;
                            form.Dock = DockStyle.Fill;
                            panelUpdateBankTransfer.Controls.Add(form);
                            MainTabPageUpdateBankTransfer.Controls.Add(panelUpdateBankTransfer);
                            MainTabControlBank.TabPages.Add(MainTabPageUpdateBankTransfer);
                            form.Show();
                            MainTabControlBank.SelectedTabPage = MainTabPageUpdateBankTransfer;
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
                            MainTabControlBank.SelectedTabPage = MainTabPageUpdateBankTransfer;
                            return;
                        }
                        else
                        {
                            if (selRow[0].ToString() != "")
                            {
                                panelUpdateBankTransfer.Controls.Clear();
                                MainTabPageUpdateBankTransfer.ImageOptions.Image = null;
                                BankTransfers_Update form = new BankTransfers_Update(selRow);
                                form.Size = new Size(1059, 638);
                                form.TopLevel = false;
                                form.FormBorderStyle = FormBorderStyle.None;
                                form.Dock = DockStyle.Fill;
                                panelUpdateBankTransfer.Controls.Add(form);
                                form.Show();
                                MainTabControlBank.SelectedTabPage = MainTabPageUpdateBankTransfer;
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
                MainTabPagePrintingBankTransfer.Name = "tabPagePrintingTransfers";
                MainTabPagePrintingBankTransfer.Text = "طباعة التحويلات";
                panelPrintingBankTransfer.Name = "panelPrintingTransfers";
                panelPrintingBankTransfer.Dock = DockStyle.Fill;
                
                panelPrintingBankTransfer.Controls.Clear();
                bankPrint = new BankTransfers_Print();
                bankPrint.Size = new Size(1059, 638);
                bankPrint.TopLevel = false;
                bankPrint.FormBorderStyle = FormBorderStyle.None;
                bankPrint.Dock = DockStyle.Fill;
                panelPrintingBankTransfer.Controls.Add(bankPrint);
                MainTabPagePrintingBankTransfer.Controls.Add(panelPrintingBankTransfer);
                MainTabControlBank.TabPages.Add(MainTabPagePrintingBankTransfer);
                bankPrint.Show();
                MainTabControlBank.SelectedTabPage = MainTabPagePrintingBankTransfer;

                MainForm.loadedPrintAgl = true;
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
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT BankTransfer_ID as 'التسلسل',FromBank_ID,CONCAT (FromBank_Name , ',' , FromBranch_Name) as 'من',ToBank_ID,CONCAT (ToBank_Name , ',' , ToBranch_Name) as 'الى',Money as 'المبلغ',Date as 'تاريخ التحويل',Description as 'البيان',Error FROM bank_transfer", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);

            gridControl1.DataSource = sourceDataSet.Tables[0];
            gridView1.Columns["FromBank_ID"].Visible = false;
            gridView1.Columns["ToBank_ID"].Visible = false;
            gridView1.Columns["Error"].Visible = false;
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 150;
            }
        }

        void delete(GridView view)
        {
            //MessageBox.Show("لا يمكنك الحذف");
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

                            string qury = "select Bank_Stock from bank where Bank_ID=" + selRow["FromBank_ID"].ToString();
                            MySqlCommand com = new MySqlCommand(qury, conn);
                            double FromBank_Stock = Convert.ToDouble(com.ExecuteScalar().ToString());

                            qury = "select Bank_Stock from bank where Bank_ID=" + selRow["ToBank_ID"].ToString();
                            com = new MySqlCommand(qury, conn);
                            double ToBank_Stock = Convert.ToDouble(com.ExecuteScalar().ToString());

                            if (Convert.ToDouble(selRow["المبلغ"].ToString()) > ToBank_Stock)
                            {
                                MessageBox.Show("لا يوجد ما يكفى");
                                conn.Close();
                                return;
                            }

                            string query = "update bank_Transfer set Error=1 where BankTransfer_ID=" + selRow["التسلسل"].ToString();
                            MySqlCommand comand = new MySqlCommand(query, conn);
                            comand.ExecuteNonQuery();

                            query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                            comand = new MySqlCommand(query, conn);
                            comand.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                            comand.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank_Transfer";
                            comand.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "حذف";
                            comand.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow["التسلسل"].ToString();
                            comand.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                            comand.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
                            comand.ExecuteNonQuery();

                            //SELECT BankTransfer_ID as 'التسلسل',FromBank_ID,CONCAT(FromBank_Name, ',', FromBranch_Name) as 'من',ToBank_ID,CONCAT(ToBank_Name, ',', ToBranch_Name) as 'الى',Money as 'المبلغ',Date as 'تاريخ التحويل',Description as 'البيان',Error
                            string q = "UPDATE bank SET Bank_Stock = " + (FromBank_Stock + Convert.ToDouble(selRow["المبلغ"].ToString())) + " where Bank_ID=" + selRow["FromBank_ID"].ToString();
                            MySqlCommand command = new MySqlCommand(q, conn);
                            command.ExecuteNonQuery();

                            q = "UPDATE bank SET Bank_Stock = " + (ToBank_Stock - Convert.ToDouble(selRow["المبلغ"].ToString())) + " where Bank_ID=" + selRow["ToBank_ID"].ToString();
                            command = new MySqlCommand(q, conn);
                            command.ExecuteNonQuery();
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
