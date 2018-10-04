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
    public partial class SpecialOrders_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl MainTabControlPS;

        //Panel panelAddCustomer;
        //Panel panelUpdateCustomer;
        //Panel panelPrintCustomer;

        public static Customer_Print customerPrint;

        public static GridControl gridcontrol;
        int EmpBranchId = 0;

        public SpecialOrders_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            MainTabControlPS = MainForm.tabControlSales;

            //panelAddCustomer = new Panel();
            //panelUpdateCustomer = new Panel();
            //panelPrintCustomer = new Panel();

            gridcontrol = gridControl1;

            EmpBranchId = UserControl.UserBranch(conn);
            
            //gridView1.PostEditor();
            //gridView1.UpdateCurrentRow();
            gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            gridView1.OptionsEditForm.CustomEditFormLayout = new AdvancedEditForm(EmpBranchId);
            //gridView1.ShowInplaceEditForm();
            //gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Default;
        }

        private void Delegate_Report_Load(object sender, EventArgs e)
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
                //this.gridView1.FocusedRowHandle = GridControl.NewItemRowHandle;
                //this.gridView1.ShowPopupEditForm();
                /*XtraTabPage xtraTabPage = getTabPage("tabPageAddCustomer");
                if (xtraTabPage == null)
                {
                    SalesMainForm.MainTabPageAddCustomer.Controls.Clear();
                    Customer_Record form = new Customer_Record();
                    SalesMainForm.MainTabPageAddCustomer.Name = "tabPageAddCustomer";
                    SalesMainForm.MainTabPageAddCustomer.Text = "اضافة عميل";
                    SalesMainForm.MainTabPageAddCustomer.ImageOptions.Image = null;
                    //panelAddCustomer.Name = "panelAddCustomer";
                    //panelAddCustomer.Dock = DockStyle.Fill;
                    form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    form.TopLevel = false;
                    //panelAddCustomer.Controls.Add(form);
                    SalesMainForm.MainTabPageAddCustomer.Controls.Add(form);
                    MainTabControlPS.TabPages.Add(SalesMainForm.MainTabPageAddCustomer);
                    MainTabControlPS.SelectedTabPage = SalesMainForm.MainTabPageAddCustomer;
                    form.Show();
                }
                else if (xtraTabPage.ImageOptions.Image != null)
                {
                    DialogResult dialogResult = MessageBox.Show("هناك تعديلات لم تحفظ بعد..هل انت متاكد انك تريد التجاهل؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SalesMainForm.MainTabPageAddCustomer.ImageOptions.Image = null;
                        SalesMainForm.MainTabPageAddCustomer.Controls.Clear();
                        //panelAddCustomer.Controls.Clear();
                        MainTabControlPS.SelectedTabPage = SalesMainForm.MainTabPageAddCustomer;
                        Customer_Record form = new Customer_Record();
                        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        form.Dock = DockStyle.Fill;
                        form.TopLevel = false;
                        //panelAddCustomer.Controls.Add(form);
                        SalesMainForm.MainTabPageAddCustomer.Controls.Add(form);
                        form.Show();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MainTabControlPS.SelectedTabPage = SalesMainForm.MainTabPageAddCustomer;
                    }
                }
                else
                {
                    SalesMainForm.MainTabPageAddCustomer.Controls.Clear();
                    //panelAddCustomer.Controls.Clear();
                    MainTabControlPS.SelectedTabPage = SalesMainForm.MainTabPageAddCustomer;
                    Customer_Record form = new Customer_Record();
                    form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    form.TopLevel = false;
                    //panelAddCustomer.Controls.Add(form);
                    SalesMainForm.MainTabPageAddCustomer.Controls.Add(form);
                    form.Show();
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void search()
        {
            DataTable sourceData = new DataTable();
            MySqlDataAdapter adapterCustomer = new MySqlDataAdapter("SELECT requests.BranchBillNumber as 'رقم الطلب',special_order.Description as 'الوصف' FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID INNER JOIN requests ON special_order.SpecialOrder_ID = requests.SpecialOrder_ID where special_order.Record=0 AND dash.Branch_ID=" + EmpBranchId, conn);
            adapterCustomer.Fill(sourceData);
            gridControl1.DataSource = sourceData;
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainTabControlPS.TabPages.Count; i++)
                if (MainTabControlPS.TabPages[i].Name == text)
                {
                    return MainTabControlPS.TabPages[i];
                }
            return null;
        }
    }
}
