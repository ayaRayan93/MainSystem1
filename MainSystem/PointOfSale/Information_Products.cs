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
    public partial class Information_Products : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        DataRow row1;

        bool loaded = false;

        //public static Products_Report ProductsReport;

        //XtraTabPage tabPageProductsReport;
        //Panel panelProductsReport;

        MainForm main;

        public Information_Products(MainForm Min)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            //tabPageProductsReport = new XtraTabPage();
            //panelProductsReport = new Panel();

            main = Min;
        }

        private void Information_Products_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT data.Code as 'الكود',CONCAT(product.Product_Name, ' , ', type.Type_Name, ' , ', factory.Factory_Name, ' , ', groupo.Group_Name) as 'الاسم',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',sellprice.Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية',data.Description as 'البيان',data_photo.Photo as 'الصورة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID INNER JOIN data_photo ON data_photo.Data_ID = data.Data_ID where sellprice.Price_Type='لستة' group by data.Data_ID", dbconnection);
                DataTable dtf = new DataTable();
                adapter.Fill(dtf);

                adapter = new MySqlDataAdapter("SELECT data.Code as 'الكود',CONCAT(product.Product_Name, ' , ', type.Type_Name, ' , ', factory.Factory_Name, ' , ', groupo.Group_Name) as 'الاسم',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',sellprice.Sell_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية',data.Description as 'البيان',data_photo.Photo as 'الصورة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID INNER JOIN data_photo ON data_photo.Data_ID = data.Data_ID where sellprice.Price_Type='قطعى' group by data.Data_ID", dbconnection);
                DataTable dtf2 = new DataTable();
                adapter.Fill(dtf2);

                dtf.Merge(dtf2);

                gridControl1.DataSource = dtf;

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

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                System.Windows.Forms.ComboBox comBox = (System.Windows.Forms.ComboBox)sender;

                switch (comBox.Name)
                {
                    case "comType":
                        {
                            string query = "select * from color where color.Type_ID=" + comType.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comColor.DataSource = dt;
                            comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                            comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                            comColor.Text = "";

                            string qall = "";
                            if (comType.Text != "")
                            {
                                qall += " and product.Type_ID=" + comType.SelectedValue.ToString();
                            }
                            if (comFactory.Text != "")
                            {
                                qall += " and product.Factory_ID=" + comFactory.SelectedValue.ToString();
                            }
                            if (comGroup.Text != "")
                            {
                                qall += " and product.Product_ID IN (select Product_ID from product_group where Group_ID=" + comGroup.SelectedValue.ToString() + ")";
                            }
                            query = "select * from product where product.Product_ID is not null " + qall;
                            da = new MySqlDataAdapter(query, dbconnection);
                            dt = new DataTable();
                            da.Fill(dt);
                            comProduct.DataSource = dt;
                            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                            comProduct.Text = "";
                        }
                        break;
                    case "comFactory":
                        {
                            string query = "select * from size where size.Factory_ID=" + comFactory.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comSize.DataSource = dt;
                            comSize.DisplayMember = dt.Columns["Size_Value"].ToString();
                            comSize.ValueMember = dt.Columns["Size_ID"].ToString();
                            comSize.Text = "";

                            string qall = "";
                            if (comType.Text != "")
                            {
                                qall += " and product.Type_ID=" + comType.SelectedValue.ToString();
                            }
                            if (comFactory.Text != "")
                            {
                                qall += " and product.Factory_ID=" + comFactory.SelectedValue.ToString();
                            }
                            if (comGroup.Text != "")
                            {
                                qall += " and product.Product_ID IN (select Product_ID from product_group where Group_ID=" + comGroup.SelectedValue.ToString() + ")";
                            }
                            query = "select * from product where product.Product_ID is not null " + qall;
                            da = new MySqlDataAdapter(query, dbconnection);
                            dt = new DataTable();
                            da.Fill(dt);
                            comProduct.DataSource = dt;
                            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                            comProduct.Text = "";
                        }
                        break;
                    case "comGroup":
                        {
                            string qall = "";
                            if (comType.Text != "")
                            {
                                qall += " and product.Type_ID=" + comType.SelectedValue.ToString();
                            }
                            if (comFactory.Text != "")
                            {
                                qall += " and product.Factory_ID=" + comFactory.SelectedValue.ToString();
                            }
                            if (comGroup.Text != "")
                            {
                                qall += " and product.Product_ID IN (select Product_ID from product_group where Group_ID=" + comGroup.SelectedValue.ToString() + ")";
                            }
                            string query = "select * from product where product.Product_ID is not null " + qall;
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comProduct.DataSource = dt;
                            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                            comProduct.Text = "";
                        }
                        break;
                }
            }
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

                    MainForm.ProductsReport = new Products_Report(main, row1, "بند");
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
                    MainForm.ProductsReport = new Products_Report(main, row1, "بند");
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q1, q2, q3, q4, qall = "";
                if (comType.Text == "")
                {
                    q1 = "select Type_ID from type";
                }
                else
                {
                    q1 = comType.SelectedValue.ToString();
                }
                if (comFactory.Text == "")
                {
                    q2 = "select Factory_ID from factory";
                }
                else
                {
                    q2 = comFactory.SelectedValue.ToString();
                }
                if (comGroup.Text == "")
                {
                    q3 = "select Group_ID from groupo";
                }
                else
                {
                    q3 = comGroup.SelectedValue.ToString();
                }
                if (comProduct.Text == "")
                {
                    q4 = "select Product_ID from product";
                }
                else
                {
                    q4 = comProduct.SelectedValue.ToString();
                }

                if (comSort.Text != "")
                {
                    qall += " and data.Sort_ID=" + comSort.SelectedValue.ToString();
                }
                if (comSize.Text != "")
                {
                    qall += " and data.Size_ID=" + comSize.SelectedValue.ToString();
                }
                if (comColor.Text != "")
                {
                    qall += " and data.Color_ID=" + comColor.SelectedValue.ToString();
                }

                dbconnection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT data.Code as 'الكود',CONCAT(product.Product_Name, ' , ', type.Type_Name, ' , ', factory.Factory_Name, ' , ', groupo.Group_Name) as 'الاسم',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',sellprice.Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية',data.Description as 'البيان',data_photo.Photo as 'الصورة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID INNER JOIN data_photo ON data_photo.Data_ID = data.Data_ID where sellprice.Price_Type='لستة' and data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Group_ID IN (" + q3 + ") and data.Product_ID IN (" + q4 + ") " + qall + " group by data.Data_ID", dbconnection);
                DataTable dtf = new DataTable();
                adapter.Fill(dtf);

                adapter = new MySqlDataAdapter("SELECT data.Code as 'الكود',CONCAT(product.Product_Name, ' , ', type.Type_Name, ' , ', factory.Factory_Name, ' , ', groupo.Group_Name) as 'الاسم',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',sellprice.Sell_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية',data.Description as 'البيان',data_photo.Photo as 'الصورة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID INNER JOIN data_photo ON data_photo.Data_ID = data.Data_ID where sellprice.Price_Type='قطعى' and data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Group_ID IN (" + q3 + ") and data.Product_ID IN (" + q4 + ") " + qall + " group by data.Data_ID", dbconnection);
                DataTable dtf2 = new DataTable();
                adapter.Fill(dtf2);

                dtf.Merge(dtf2);

                gridControl1.DataSource = dtf;
                
                if (layoutView1.IsLastVisibleRow)
                {
                    layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
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
            dbconnection.Open();

            string query = "select * from type";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comType.DataSource = dt;
            comType.DisplayMember = dt.Columns["Type_Name"].ToString();
            comType.ValueMember = dt.Columns["Type_ID"].ToString();
            comType.Text = "";
            //txtType.Text = "";

            query = "select * from factory";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comFactory.DataSource = dt;
            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
            comFactory.Text = "";
            //txtFactory.Text = "";

            query = "select * from groupo";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comGroup.DataSource = dt;
            comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
            comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
            comGroup.Text = "";
            //txtGroup.Text = "";

            query = "select * from product";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comProduct.DataSource = dt;
            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
            comProduct.Text = "";
            //txtProduct.Text = "";
            
            query = "select * from size";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSize.DataSource = dt;
            comSize.DisplayMember = dt.Columns["Size_Value"].ToString();
            comSize.ValueMember = dt.Columns["Size_ID"].ToString();
            comSize.Text = "";

            query = "select * from color";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comColor.DataSource = dt;
            comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
            comColor.ValueMember = dt.Columns["Color_ID"].ToString();
            comColor.Text = "";

            query = "select * from sort";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSort.DataSource = dt;
            comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
            comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
            comSort.Text = "";
            
            dbconnection.Close();
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