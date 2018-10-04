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
    public partial class initialCodeStorage : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        double noMeter = 0;
        Storage storage;
        XtraTabControl xtraTabControlStoresContent;
        int Data_ID;
        public initialCodeStorage(Storage storage, XtraTabControl xtraTabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.storage = storage;
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

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
               
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
                        displayData();
                        break;
                    case "comFactory":
                        txtFactory.Text = comFactory.SelectedValue.ToString();
                        displayData();
                        break;
                    case "comGroup":
                        txtGroup.Text = comGroup.SelectedValue.ToString();
                        displayData();
                        break;
                    case "comProduct":
                        txtProduct.Text = comProduct.SelectedValue.ToString();
                        displayData();
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
                                    displayData();
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
                                    displayData();
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
                                    displayData();
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
                                    displayData();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart1":
                                query = "select Type_Name from type where Type_ID='" + txtCodePart1.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    makeCode(txtCodePart1);
                                    txtCodePart2.Focus();
                                    dbconnection.Close();                                
                                }
                                else
                                {
                                    MessageBox.Show("هذا الكود غير مسجل");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart2":
                                query = "select Factory_Name from factory where Factory_ID='" + txtCodePart2.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    makeCode(txtCodePart2);
                                    txtCodePart3.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("هذا الكود غير مسجل");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart3":
                                query = "select Group_Name from Groupo where Group_ID='" + txtCodePart3.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    makeCode(txtCodePart3);
                                    txtCodePart4.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("هذا الكود غير مسجل");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart4":
                                query = "select Product_Name from Product where Product_ID='" + txtCodePart4.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    makeCode(txtCodePart4);
                                    txtCodePart5.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("هذا الكود غير مسجل");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart5":
                                makeCode(txtCodePart5);
                                txtTotalMeter.Focus();
                                break;
                            case "txtTotalMeter":
                                add2Store();
                                txtTotalMeter.Text=txtCode.Text= txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
                                txtCodePart1.Focus();

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
                    comStorePlace.Visible = true;
                    label11.Visible = true;
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
                displayData();
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
                    txtCode.Text = row[1].ToString();
                    noMeter = Convert.ToDouble(row[11].ToString());
                    Data_ID = Convert.ToInt16(row[0].ToString());
                    String code = txtCode.Text;
                    displayCode(code);
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
                add2Store();              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comStore.Text = "";
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";

                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";

                displayData();
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
                //  labTotalMeter.Text = (total).ToString();
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
                XtraTabPage xtraTabPage;
                TextBox txtBox;
                double noCarton = 0, noPalatat = 0;
                if (loaded)
                {
                    xtraTabPage = getTabPage("تسجيل كميات البنود");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    else
                        xtraTabPage.ImageOptions.Image = null;

                    Control c= (Control)sender;
                    if (c is TextBox)
                    {
                        txtBox = (TextBox)sender;
                        if (txtBox.Name == "txtNoCarton")
                        {
                            if (txtBox.Text != "")
                                noCarton = Convert.ToDouble(txtBox.Text);
                            if (txtNoPalatat.Text != "")
                                noPalatat = Convert.ToDouble(txtNoPalatat.Text);
                        }
                        else if (txtBox.Name == "txtNoPalatat")
                        {
                            if (txtBox.Text != "")
                                noPalatat = Convert.ToDouble(txtBox.Text);
                            if (txtNoCarton.Text != "")
                                noCarton = Convert.ToDouble(txtNoCarton.Text);
                        }

                        double result = noCarton * noPalatat * noMeter;
                       // labTotalMeter.Text = result + "";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     

        //functions
        public void clear()
        {
            txtCode.Text = txtNoCarton.Text = txtNoPalatat.Text = txtNote.Text = comStorePlace.Text = "";
            comStorePlace.Visible = false;
        }
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
            foreach (Control item in this.Controls["tableLayoutPanel1"].Controls)
            {
                if (item is TextBox)
                {
                    if (item.Text != "")
                        return false;
                }
                else if (item is ComboBox)
                {
                    if (item.Text != "")
                        return false;
                }
                else if (item is DateTimePicker)
                {
                    DateTimePicker item1 = (DateTimePicker)item;
                    if (item1.Value.Date != DateTime.Now.Date)
                        return false;
                }
            }
            return true;
        }
        public void displayData()
        {
            string q1, q2, q3, q4;
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

            string query = "SELECT data.Data_ID, data.Code as 'الكود',product.Product_Name as 'الصنف',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") ";

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Width = 200;
        }
        public bool validation(int storeID,int StorePlaceID)
        {
            string query = "select Storage_ID from storage where Data_ID="+Data_ID+" and Store_ID="+ storeID + " and Store_Place_ID="+ StorePlaceID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public void displayCode(string code)
        {
            char[] arrCode = code.ToCharArray();
            txtCodePart1.Text =Convert.ToInt16(arrCode[0].ToString() + arrCode[1].ToString() + arrCode[2].ToString() + arrCode[3].ToString()) + "";
            txtCodePart2.Text = Convert.ToInt16(arrCode[4].ToString() + arrCode[5].ToString() + arrCode[6].ToString() + arrCode[7].ToString() )+ "";
            txtCodePart3.Text = Convert.ToInt16(arrCode[8].ToString() + arrCode[9].ToString() + arrCode[10].ToString() + arrCode[11].ToString()) + "";
            txtCodePart4.Text = Convert.ToInt16(arrCode[12].ToString() + arrCode[13].ToString() + arrCode[14].ToString() + arrCode[15].ToString() )+ "";
            txtCodePart5.Text = "" + Convert.ToInt16(arrCode[16].ToString() + arrCode[17].ToString() + arrCode[18].ToString() + arrCode[19].ToString());
        }
        public void makeCode(TextBox txtBox)
        {
            string code =txtCode.Text;
            int j = 4 - txtBox.TextLength;
            for (int i = 0; i < j; i++)
            {
                code += "0";
            }
            code += txtBox.Text;


            txtCode.Text = code;

        }
        public void add2Store()
        {
            if (comStorePlace.Text != "" && txtCode.TextLength == 20)
            {
                if (validation((int)comStore.SelectedValue, (int)comStorePlace.SelectedValue))
                {
                    string query = "select Data_ID from data where Code='"+txtCode.Text+"'";
                    MySqlCommand com = new MySqlCommand(query,dbconnection);
                    Data_ID = Convert.ToInt16(com.ExecuteScalar());

                    query = "insert into Storage (Store_ID,Type,Storage_Date,Data_ID,Store_Place_ID,Total_Meters,Note) values (@Store_ID,@Type,@Date,@Data_ID,@PlaceOfStore,@TotalOfMeters,@Note)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = comStore.SelectedValue;
                    com.Parameters.Add("@Type", MySqlDbType.VarChar);
                    com.Parameters["@Type"].Value = "بند";
                    com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value;
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters["@Data_ID"].Value = Data_ID;
                    com.Parameters.Add("@PlaceOfStore", MySqlDbType.Int16);
                    com.Parameters["@PlaceOfStore"].Value = comStorePlace.SelectedValue;
                    com.Parameters.Add("@TotalOfMeters", MySqlDbType.Decimal);
                    com.Parameters["@TotalOfMeters"].Value = txtTotalMeter.Text;
                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                    com.Parameters["@Note"].Value = txtNote.Text;
                    com.ExecuteNonQuery();
                    MessageBox.Show("Add success");
                    storage.displayProducts();
                }
                else
                {
                    MessageBox.Show("هذا البند مضاف فعلا");
                }
            }
            else
            {
                MessageBox.Show("you must fill all fields please");
                dbconnection.Close();
                return;
            }

     }


}

}
