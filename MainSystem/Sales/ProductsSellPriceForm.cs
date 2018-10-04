using DevExpress.XtraGrid.Views.Grid;
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
    public partial class ProductsSellPriceForm : Form
    {
        MySqlConnection dbconnection;
        MainForm salesMainForm = null;
        string query = "";
        bool loaded = false;

        public ProductsSellPriceForm(MainForm salesMainForm)
        {
            try
            {
                InitializeComponent();
                this.salesMainForm = salesMainForm;
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void ProductsSellPriceForm_Load(object sender, EventArgs e)
        {
            try
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
                txtType.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";

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

                loaded = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ComboBox comBox = (ComboBox)sender;

                switch (comBox.Name)
                {
                    case "comType":
                        txtType.Text = comType.SelectedValue.ToString();
                        displayProducts();
                        break;
                    case "comFactory":
                        txtFactory.Text = comFactory.SelectedValue.ToString();
                        displayProducts();
                        break;
                    case "comGroup":
                        txtGroup.Text = comGroup.SelectedValue.ToString();
                        displayProducts();
                        break;
                    case "comProduct":
                        txtProduct.Text = comProduct.SelectedValue.ToString();
                        displayProducts();
                        break;
                }
            }
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                    dbconnection.Close();
                                    displayProducts();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtFactory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtGroup.Focus();
                                    dbconnection.Close();
                                    displayProducts();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select Group_Name from groupo where Group_ID='" + txtGroup.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtProduct.Focus();
                                    dbconnection.Close();
                                    displayProducts();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtProduct":
                                query = "select Product_Name from product where Product_ID='" + txtProduct.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtType.Focus();
                                    dbconnection.Close();
                                    displayProducts();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                displayProducts();
                chBoxSelectAll.Checked = false;
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
                int[] rows = (((GridView)gridControl1.MainView).GetSelectedRows());
               List<DataRowView> recordList=new List<DataRowView>();
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRowView a = (DataRowView)(((GridView)gridControl1.MainView).GetRow(rows[i]));
                    recordList.Add(a);
                }
                
                if (chBoxSelectAll.Checked)
                {
                    salesMainForm.bindUpdateSellPriceForm(null, this, query);
                }
                else if (recordList != null)
                {
                    salesMainForm.bindUpdateSellPriceForm(recordList, this, "");
                }
                else
                {
                    MessageBox.Show("you must select an item");
                }
            }
            catch
            {
                MessageBox.Show("you must select an item");
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                salesMainForm.bindReportSellPriceForm(gridControl1);
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
                salesMainForm.bindRecordSellPriceForm(this);
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
                dbconnection.Open();
                DataRowView row1 = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                if (chBoxSelectAll.Checked)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DataTable dataTable = (DataTable)gridControl1.DataSource;
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string query = "delete from sellprice where SellPrice_ID='" + dataTable.Rows[i][0].ToString()+"'";
                            MySqlCommand comand = new MySqlCommand(query, dbconnection);
                          
                            comand.ExecuteNonQuery();

                            UserControl.ItemRecord("sellprice", "حذف", Convert.ToInt16(row1[0].ToString()), DateTime.Now,"", dbconnection);
                            dbconnection.Open();

                        }
                    }
                   
                }
                else if (row1 != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string query = "delete from sellprice where SellPrice_ID='" + row1[0].ToString()+"'";
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                     
                        comand.ExecuteNonQuery();

                        UserControl.ItemRecord("sellprice", "حذف", Convert.ToInt16(row1[0].ToString()), DateTime.Now,"", dbconnection);

                    }
                    else if (dialogResult == DialogResult.No)
                    { }
                }
                else
                {
                    MessageBox.Show("you must select an item");
                }

                displayProducts();
            }
            catch
            {
                MessageBox.Show("you must select an item");
            }
            dbconnection.Close();
        }

        private void chBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gridView1.OptionsSelection.MultiSelect = true;
                if (chBoxSelectAll.Checked)
                {
                    gridView1.SelectRows(0, gridView1.RowCount - 1);
                }
                else
                {
                    int selectedRowsCount = gridView1.SelectedRowsCount;
                    for (int i = 0; i < selectedRowsCount; i++)
                    {
                        gridView1.UnselectRow(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    displayProducts();
                    chBoxSelectAll.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //function
        public void displayProducts()
        {
            try
            {

                string q1, q2, q3, q4, fQuery = "";
                if (txtType.Text == "")
                {
                    q1 = "select Type_ID from data";
                }
                else
                {
                    q1 = txtType.Text;
                }
                if (txtFactory.Text == "")
                {
                    q2 = "select Factory_ID from data";
                }
                else
                {
                    q2 = txtFactory.Text;
                }
                if (txtProduct.Text == "")
                {
                    q3 = "select Product_ID from data";
                }
                else
                {
                    q3 = txtProduct.Text;
                }
                if (txtGroup.Text == "")
                {
                    q4 = "select Group_ID from data";
                }
                else
                {
                    q4 = txtGroup.Text;
                }
                if (comSize.Text != "")
                {
                    fQuery += "and size.Size_ID='" + comSize.SelectedValue + "'";
                }
                if (txtClassification.Text != "")
                {
                    fQuery += "and data.Classification='" + txtClassification.Text + "'";
                }
                if (comColor.Text != "")
                {
                    fQuery += "and color.Color_ID='" + comColor.SelectedValue + "'";
                }
                if (comSort.Text != "")
                {
                    fQuery += "and Sort.Sort_ID='" + comSort.SelectedValue + "'";
                }

                query = "SELECT SellPrice.SellPrice_ID, data.Code as 'الكود',concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,color.Color_Name,' ' ,size.Size_Value )as 'البند',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة',sellprice.Price as 'السعر',sellprice.Price_Type as 'نوع السعر',sellprice.Sell_Discount as 'خصم البيع',sellprice.Normal_Increase as 'الزيادة العادية',sellprice.Categorical_Increase as 'الزيادة القطعية',sellprice.ProfitRatio as 'نسبة البيع',sellprice.Sell_Price as 'سعر البيع',sellprice.PercentageDelegate as 'نسية المندوب',sellprice.Date as 'التاريخ'   from data INNER JOIN sellprice on sellprice.Data_ID=data.Data_ID  INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Width = 140;
                gridView1.Columns[2].Width = 200;
                fQuery = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           }

    }
}
