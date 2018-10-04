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
    public partial class UpdateCodeStorage : Form
    {
        MySqlConnection dbconnection;
        int[] courrentIDs;//store ids of products which added to gridview2
        //int count = 0;//store count of products in grid view
        bool loaded = false;
        //int startNewRecord;
        Storage storage;
        List<DataRowView> rows;
        DataRowView row;
        XtraTabControl xtraTabControlStoresContent;
        public UpdateCodeStorage(List<DataRowView> rows,Storage storage, XtraTabControl xtraTabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                courrentIDs = new int[100];
                dbconnection = new MySqlConnection(connection.connectionString);
                this.storage = storage;
                this.rows = rows;
                this.row = rows[0];
                this.xtraTabControlStoresContent = xtraTabControlStoresContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
            
                string query = "select * from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";

                displayProducts();
                setData(rows[0]);
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    dbconnection.Open();
                    string query = "select * from store_places where Store_ID="+comStore.SelectedValue;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comStorePlace.DataSource = dt;
                    comStorePlace.DisplayMember = dt.Columns["Store_Place_Code"].ToString();
                    comStorePlace.ValueMember = dt.Columns["Store_Place_ID"].ToString();
                    comStorePlace.Text = "";
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void gridControl1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                if (row != null)
                {
                    setData(row);
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

                if (comStorePlace.Text != "" && txtCode.Text != "")
                {                 
                    string query = "update Storage set Store_ID=@Store_ID,Store_Name=@Store_Name,Storage_Date=@Storage_Date,Data_ID=@Data_ID,Store_Place_ID=@Store_Place_ID,Total_Meters=@Total_Meters,Note=@Note where Storage_ID=" + rows[0][0].ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = comStore.SelectedValue;
                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar);
                    com.Parameters["@Store_Name"].Value = comStore.Text;
                    com.Parameters.Add("@Storage_Date", MySqlDbType.Date, 0);
                    com.Parameters["@Storage_Date"].Value = dateTimePicker1.Value;                  
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters["@Data_ID"].Value = Convert.ToInt16(row[1].ToString());
                    com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_Place_ID"].Value = comStorePlace.SelectedValue;
                    com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                    com.Parameters["@Total_Meters"].Value = txtTotalMeter.Text;
                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                    com.Parameters["@Note"].Value = txtNote.Text;
                    com.ExecuteNonQuery();
                    MessageBox.Show("Add success");
                    storage.displayProducts();
                    displayProducts();
                    XtraTabPage xtraTabPage = getTabPage("تعديل  كمية بند");
                    xtraTabPage.ImageOptions.Image = null;
                }
                else
                {
                    MessageBox.Show("you must fill all fields please");
                    dbconnection.Close();
                    return;
                }
              
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل  كمية بند");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    else
                        xtraTabPage.ImageOptions.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //draft
        private void txtNoPalatat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                string code = txtCode.Text;
                int StoreID = int.Parse(comStore.SelectedValue.ToString());
                string q = "select carton from data where Code='" + code + "'";
                MySqlCommand com = new MySqlCommand(q, dbconnection);
                double carton = double.Parse(com.ExecuteScalar().ToString());
                int NoBalatat;
                int.TryParse(txtNoPalatat.Text, out NoBalatat);
                int NoCartons;
                int.TryParse(txtNoCarton.Text, out NoCartons);
                double total = carton * NoBalatat * NoCartons;
                labTotalMeter.Text = (total).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
       

        //functions
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlStoresContent.TabPages.Count; i++)
                if (xtraTabControlStoresContent.TabPages[i].Text == text)
                {
                    return xtraTabControlStoresContent.TabPages[i];
                }
            return null;
        }
        public bool IsClear()
        {
            DateTime date = dateTimePicker1.Value.Date;
            string d = date.ToString("dd/MM/yyyy");

            if (txtCode.Text == row["كود"].ToString() &&
            comStore.Text == row["المخزن"].ToString() &&
            comStorePlace.Text == row["مكان التخزين"].ToString() &&
            labTotalMeter.Text == row["اجمالي عدد الامتار"].ToString() &&
            txtNoCarton.Text == row["عدد الكراتين"].ToString() &&
            row["تاريخ التخزين"].ToString().Split(' ')[0] == d&&
            txtNoPalatat.Text == row["بلتات"].ToString() &&
            txtNote.Text == row["ملاحظة"].ToString())
                return true;
            else
                return false;

        }
        public void displayProducts()
        {
            try
            {
                string ids = "";
                for (int i = 0; i < rows.Count - 1; i++)
                {
                    ids += rows[i][0] + ",";
                }
                ids += rows[rows.Count - 1][0];
                string qq = "select Storage_ID, data.Code as 'كود',type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'المنتج', store.Store_Name as 'المخزن', storage.Supplier_Name as 'المورد',storage.Total_Meters as 'الكمية', storage.Storage_Date as 'تاريخ التخزين' , Store_Place_Code as 'مكان التخزين'  , storage.Note as 'ملاحظة' from storage INNER JOIN store on storage.Store_ID=store.Store_ID INNER JOIN store_places on storage.Store_Place_ID=store_places.Store_Place_ID  INNER JOIN data  ON storage.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  where Storage_ID IN(" + ids + ")";
                MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void setData(DataRowView row)
        {
            txtCode.Text = row["كود"].ToString();
            comStore.Text = row["المخزن"].ToString();
            setComStorePlacesValue();
            comStorePlace.Text = row["مكان التخزين"].ToString();
            labTotalMeter.Text = row["اجمالي عدد الامتار"].ToString();
            dateTimePicker1.Text = row["تاريخ التخزين"].ToString();
            txtNote.Text = row["ملاحظة"].ToString();
        }
        public void setComStorePlacesValue()
        {
            string query = "select * from store_places where Store_ID=" + comStore.SelectedValue;
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comStorePlace.DataSource = dt;
            comStorePlace.DisplayMember = dt.Columns["Store_Place_Code"].ToString();
            comStorePlace.ValueMember = dt.Columns["Store_Place_ID"].ToString();
        }
    }


}
