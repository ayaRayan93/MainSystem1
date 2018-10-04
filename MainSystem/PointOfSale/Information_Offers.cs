using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;

namespace MainSystem
{
    public partial class Information_Offers : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection3;
        DataRow row1;

        bool loaded = false;

        MainForm main;

        public Information_Offers(MainForm Min)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);

            main = Min;
        }

        private void Information_Products_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("select offer.Offer_ID as 'الكود',Offer_Name as 'الاسم',Price as 'السعر',sum(storage.Total_Meters) as 'الكمية',offer.Description as 'البيان',offer_photo.Photo as 'الصورة' from offer INNER JOIN offer_photo ON offer_photo.Offer_ID = offer.Offer_ID LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID where offer.Offer_ID=0 group by offer.Offer_ID", dbconnection);
                DataTable dtf = new DataTable();
                adapter.Fill(dtf);
                gridControl1.DataSource = dtf;
                layoutView1.Columns[0].Visible = false;

                string query = "select offer.Offer_ID as 'الكود',Offer_Name as 'الاسم',Price as 'السعر',sum(storage.Total_Meters) as 'الكمية',offer.Description as 'البيان',offer_photo.Photo as 'الصورة' from offer INNER JOIN offer_photo ON offer_photo.Offer_ID = offer.Offer_ID LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID group by offer.Offer_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    layoutView1.AddNewRow();
                    int rowHandle = layoutView1.GetRowHandle(layoutView1.DataRowCount);
                    if (layoutView1.IsNewItemRow(rowHandle))
                    {
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكود"], dr["الكود"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الاسم"], dr["الاسم"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["السعر"], dr["السعر"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["البيان"], dr["البيان"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الصورة"], dr["الصورة"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكمية"], dr["الكمية"]);
                    }
                }
                dr.Close();

                if (layoutView1.IsLastVisibleRow)
                {
                    layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
                }

                search();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comOffer_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    checkEditOffers.Checked = false;
                    string query = "select offer.Offer_ID as 'الكود',Offer_Name as 'الاسم',Price as 'السعر',sum(storage.Total_Meters) as 'الكمية',offer.Description as 'البيان',offer_photo.Photo as 'الصورة' from offer INNER JOIN offer_photo ON offer_photo.Offer_ID = offer.Offer_ID LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID where offer.Offer_ID=" + comOffer.SelectedValue.ToString() + " group by offer.Offer_ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    layoutView1.Columns[0].Visible = false;

                    if (layoutView1.IsLastVisibleRow)
                    {
                        layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void checkEditOffers_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEditOffers.Checked)
                {
                    comOffer.Text = "";
                    string query = "select offer.Offer_ID as 'الكود',Offer_Name as 'الاسم',Price as 'السعر',sum(storage.Total_Meters) as 'الكمية',offer.Description as 'البيان',offer_photo.Photo as 'الصورة' from offer INNER JOIN offer_photo ON offer_photo.Offer_ID = offer.Offer_ID LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID group by offer.Offer_ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    layoutView1.Columns[0].Visible = false;

                    if (layoutView1.IsLastVisibleRow)
                    {
                        layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void layoutView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                row1 = layoutView1.GetDataRow(layoutView1.GetRowHandle(layoutView1.FocusedRowHandle));
                XtraTabPage xtraTabPage = getTabPage("tabPageProductsReport");
                if (xtraTabPage == null)
                {
                    MainForm.tabPageProductsReport.Name = "tabPageProductsReport";
                    MainForm.tabPageProductsReport.Text = "عرض البنود";
                    MainForm.panelProductsReport.Name = "panelProductsReport";
                    MainForm.panelProductsReport.Dock = DockStyle.Fill;

                    MainForm.ProductsReport = new Products_Report(main, row1, "عرض");
                    MainForm.ProductsReport.Size = new Size(1109, 660);
                    MainForm.ProductsReport.TopLevel = false;
                    MainForm.ProductsReport.FormBorderStyle = FormBorderStyle.None;
                    MainForm.ProductsReport.Dock = DockStyle.Fill;
                    //}
                    MainForm.panelProductsReport.Controls.Clear();
                    MainForm.panelProductsReport.Controls.Add(MainForm.ProductsReport);
                    MainForm.tabPageProductsReport.Controls.Add(MainForm.panelProductsReport);
                    MainForm.tabControlPointSale.TabPages.Add(MainForm.tabPageProductsReport);
                    MainForm.ProductsReport.Show();
                    MainForm.tabControlPointSale.SelectedTabPage = MainForm.tabPageProductsReport;
                }
                else
                {
                    MainForm.ProductsReport = new Products_Report(main, row1, "عرض");
                    MainForm.ProductsReport.Size = new Size(1109, 660);
                    MainForm.ProductsReport.TopLevel = false;
                    MainForm.ProductsReport.FormBorderStyle = FormBorderStyle.None;
                    MainForm.ProductsReport.Dock = DockStyle.Fill;

                    MainForm.panelProductsReport.Controls.Clear();
                    MainForm.panelProductsReport.Controls.Add(MainForm.ProductsReport);
                    MainForm.ProductsReport.Show();
                    MainForm.tabControlPointSale.SelectedTabPage = MainForm.tabPageProductsReport;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions
        public void search()
        {
            string query = "select * from offer";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comOffer.DataSource = dt;
            comOffer.DisplayMember = dt.Columns["Offer_Name"].ToString();
            comOffer.ValueMember = dt.Columns["Offer_ID"].ToString();
            comOffer.Text = "";
            loaded = true;
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
    }
}