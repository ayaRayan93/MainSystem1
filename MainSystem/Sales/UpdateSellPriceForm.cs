using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class UpdateSellPriceForm : Form
    {
        MySqlConnection dbconnection;
        List<DataRowView> rows = null;
        ProductsSellPriceForm productsSellPriceForm = null;
        String query = "";
        XtraTabControl xtraTabControlSalesContent = null;
        bool load = false,flag=false;
        public UpdateSellPriceForm(List<DataRowView> rows,ProductsSellPriceForm productsSellPriceForm,string query, XtraTabControl xtraTabControlSalesContent)
        {
            try
            {
                InitializeComponent();
                this.xtraTabControlSalesContent = xtraTabControlSalesContent;
                this.rows = rows;
                this.productsSellPriceForm = productsSellPriceForm;
                this.query = query;
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void UpdateSellPriceForm_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string ids = "";
                for (int i = 0; i < rows.Count-1; i++)
                {
                    ids += rows[i][0] + ",";
                }
                ids += rows[rows.Count - 1][0] ;
                query = "SELECT SellPrice.SellPrice_ID, data.Code as 'الكود',concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,color.Color_Name,' ' ,size.Size_Value )as 'البند',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة',sellprice.Price as 'السعر',sellprice.Price_Type as 'نوع السعر',sellprice.Sell_Discount as 'خصم البيع',sellprice.Normal_Increase as 'الزيادة العادية',sellprice.Categorical_Increase as 'الزيادة القطعية',sellprice.ProfitRatio as 'نسبة البيع',sellprice.Sell_Price as 'سعر البيع',sellprice.PercentageDelegate as 'نسية المندوب',sellprice.Date as 'التاريخ'   from data INNER JOIN sellprice on sellprice.Code=data.Code  INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where   SellPrice.SellPrice_ID in(" + ids + ")";

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                   
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Width = 140;
                gridView1.Columns[2].Width = 200;
               
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }

    
        private void radioList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                layoutSell.Text = "خصم البيع";
                layoutNormal.Visibility =DevExpress.XtraLayout.Utils.LayoutVisibility.Always ;
                layoutUnNormal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                if (load)
                {
                    if (flag)
                    {
                        XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                        if (!IsClearForUpdateList())
                            xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                        else
                            xtraTabPage.ImageOptions.Image = null;
                    }
                    else
                    {
                        XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                        if (!IsClear())
                            xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                        else
                            xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioQata3y_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                layoutSell.Text = "نسبة البيع";
                layoutNormal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutUnNormal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (load )
                {
                    if (flag)
                    {
                        XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                        if (!IsClearForUpdateList())
                            xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                        else
                            xtraTabPage.ImageOptions.Image = null;
                    }
                    else
                    {
                        XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                        if (!IsClear())
                            xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                        else
                            xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridControl1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                if (row != null)
                {
                    txtCode.Text = row[0].ToString();
                    txtPercentageDelegate.Focus();
                    txtPercentageDelegate.SelectAll();
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
                dbconnection.Open();
                double price = double.Parse(txtPrice.Text);
                double SellPercent = double.Parse(txtSell.Text);

                if (radioQata3y.Checked == true)
                {
                    #region set qata3yPrice for list item
              
                        double NormalPercent = double.Parse(txtNormal.Text);
                        double UnNormalPercent = double.Parse(txtUnNormal.Text);

                        DataTable dataTable = (DataTable)gridControl1.DataSource;
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            String query = "update sellprice set Sell_Discount=@Sell_Discount,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,Price_Type=@Price_Type,Sell_Price=@Sell_Price,ProfitRatio=@ProfitRatio,Price=@Price,PercentageDelegate=@PercentageDelegate where SellPrice_ID=" + dataTable.Rows[i][0].ToString();

                            MySqlCommand command = new MySqlCommand(query, dbconnection);
                            command.Parameters.AddWithValue("@Price_Type", "قطعى");
                            command.Parameters.AddWithValue("@Sell_Price", price + (price * SellPercent / 100.0));
                            command.Parameters.AddWithValue("@ProfitRatio", SellPercent);               
                            command.Parameters.AddWithValue("@Sell_Discount", 0.00);
                            command.Parameters.AddWithValue("@Price", price);
                            command.Parameters.AddWithValue("@Normal_Increase", 0.00);
                            command.Parameters.AddWithValue("@Categorical_Increase", 0.00);
                            command.Parameters.AddWithValue("@PercentageDelegate", double.Parse(txtPercentageDelegate.Text));
                        
                            command.ExecuteNonQuery();

                            UserControl.ItemRecord("sellprice", "تعديل", Convert.ToInt16(dataTable.Rows[i][0].ToString()), DateTime.Now,"", dbconnection);
                        dbconnection.Open();
                    }
                                      
                    #endregion
                }
                else
                {
                    #region set priceList for collection of items
                
                        double NormalPercent = double.Parse(txtNormal.Text);
                        double unNormalPercent = double.Parse(txtUnNormal.Text);

                        double sellPrice = (price + NormalPercent) - ((price + NormalPercent) * SellPercent / 100.0);

                        sellPrice = sellPrice + unNormalPercent;

                        DataTable dataTable = (DataTable)gridControl1.DataSource;
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string query = "update sellprice set ProfitRatio=@ProfitRatio, Price_Type=@Price_Type,Sell_Price=@Sell_Price,Sell_Discount=@Sell_Discount,Price=@Price,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,PercentageDelegate=@PercentageDelegate where SellPrice_ID =" + dataTable.Rows[i][0].ToString();

                            MySqlCommand command = new MySqlCommand(query, dbconnection);
                            command.Parameters.AddWithValue("@Price_Type", "لستة");
                            command.Parameters.AddWithValue("@Sell_Price", sellPrice);
                            command.Parameters.AddWithValue("@ProfitRatio", 0.00);
                            command.Parameters.AddWithValue("@Sell_Discount", double.Parse(txtSell.Text));
                            command.Parameters.AddWithValue("@Price", price);
                            command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                            command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                            command.Parameters.AddWithValue("@PercentageDelegate", double.Parse(txtPercentageDelegate.Text));
                        
                            command.ExecuteNonQuery();

                            UserControl.ItemRecord("sellprice", "تعديل", Convert.ToInt16(dataTable.Rows[i][0].ToString()), DateTime.Now,"", dbconnection);
                            dbconnection.Open();
                       }

                    #endregion
                }
                displayData();

                productsSellPriceForm.displayProducts();

                XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
      
                xtraTabPage.ImageOptions.Image = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    if (flag)
                    {
                        XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                        if (!IsClearForUpdateList())
                            xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                        else
                            xtraTabPage.ImageOptions.Image = null;
                    }
                    else
                    {
                        XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                        if (!IsClear())
                            xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                        else
                            xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //finction
        public void displayData()
        {
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void setData(DataRowView row1)
        {
            txtCode.Text = row1["الكود"].ToString();
            txtPrice.Text = row1["السعر"].ToString();
            string str = row1["نوع السعر"].ToString();
            if (str == "لستة")
            {
                txtSell.Text = row1["خصم البيع"].ToString();
                txtNormal.Text = row1["الزيادة العادية"].ToString();
                txtUnNormal.Text = row1["الزيادة القطعية"].ToString();
                radioList.Checked = true;
            }
            else
            {
                txtSell.Text = row1["نسبة البيع"].ToString();
                radioQata3y.Checked = true;
            }
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlSalesContent.TabPages.Count; i++)
                if (xtraTabControlSalesContent.TabPages[i].Text == text)
                {
                    return xtraTabControlSalesContent.TabPages[i];
                }
            return null;
        }
        public bool IsClear()
        {
            if (txtCode.Text == rows[0]["الكود"].ToString() &&
            txtPrice.Text == rows[0]["السعر"].ToString()
          )
            {
                string str = rows[0]["نوع السعر"].ToString();
                if (str == "لسته")
                {
                    if (txtSell.Text == rows[0]["خصم البيع"].ToString() &&
                     txtNormal.Text == rows[0]["زيادة عادية"].ToString() &&
                     txtUnNormal.Text == rows[0]["زيادة قطعية"].ToString() &&
                     radioList.Checked == true)
                        return true;
                }
                else
                {
                    if (txtSell.Text == rows[0]["نسبة البيع"].ToString() &&
                    radioQata3y.Checked == true)
                        return true;
                }

                return false;
            }
            else
                return false;

        }
        public bool IsClearForUpdateList()
        {
            if (txtCode.Text == "" &&
            txtPrice.Text == "0" &&
           txtSell.Text == "0" &&
                     txtNormal.Text == "0" &&
                     txtUnNormal.Text == "0" &&
                     radioList.Checked == true)
                return true;

            else
                return false;

        }
    }
}
