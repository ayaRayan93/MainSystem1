using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class Product_Update : Form
    {
        byte[] selectedImage;
        bool loaded = false,flag=false, factoryFlage=false, groupFlage=false;
        MySqlConnection dbconnection, dbconnection1;
        string code = "",lastPartCode="";
        int Data_ID;
        int color_id = -1;
        int size_id = -1;
        int sort_id = -1;
        DataRowView prodRow;
        Products products = null;
        XtraTabControl xtraTabControlStoresContent = null;
        public Product_Update(DataRowView prodRow,Products products,XtraTabControl xtraTabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                this.products = products;
                this.xtraTabControlStoresContent = xtraTabControlStoresContent;
                this.prodRow = prodRow;
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Product_Update_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                loadData();
                code = prodRow[4].ToString();
                displayData(prodRow);             
                loaded = true;
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        if (loaded)
                        {
                            txtType.Text = comType.SelectedValue.ToString();
                            string query = "select * from factory where Type_ID=" + txtType.Text;
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comFactory.DataSource = dt;
                            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                            comFactory.Text = "";
                            txtFactory.Text = "";

                            factoryFlage = true;

                            query = "select * from color where Type_ID=" + txtType.Text;
                            da = new MySqlDataAdapter(query, dbconnection);
                            dt = new DataTable();
                            da.Fill(dt);
                            comColour.DataSource = dt;
                            comColour.DisplayMember = dt.Columns["Color_Name"].ToString();
                            comColour.ValueMember = dt.Columns["Color_ID"].ToString();
                            comColour.Text = "";

                            comFactory.Focus();
                        }
                        break;
                    case "comFactory":
                        if (factoryFlage)
                        {
                            txtFactory.Text = comFactory.SelectedValue.ToString();

                            string query2 = "select * from groupo where Factory_ID=" + txtFactory.Text;
                            MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            comGroup.DataSource = dt2;
                            comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                            comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                            comGroup.Text = "";
                            txtGroup.Text = "";

                            groupFlage = true;

                            query2 = "select * from size where Factory_ID=" + txtFactory.Text;
                            da2 = new MySqlDataAdapter(query2, dbconnection);
                            dt2 = new DataTable();
                            da2.Fill(dt2);
                            comSize.DataSource = dt2;
                            comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                            comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                            comSize.Text = "";
                            comGroup.Focus();
                        }
                        break;
                    case "comGroup":
                        if (groupFlage)
                        {
                            txtGroup.Text = comGroup.SelectedValue.ToString();

                            string query3 = "select distinct  product.Product_ID,Product_Name  from product inner join product_group on product.Product_ID=product_group.Product_ID  where product.Type_ID=" + txtType.Text + " and product.Factory_ID=" + txtFactory.Text + " and product_group.Group_ID=" + txtGroup.Text + "  order by product.Product_ID";
                            MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);
                            comProduct.DataSource = dt3;
                            comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                            comProduct.Text = "";
                            txtProduct.Text = "";

                            comProduct.Focus();
                        }
                        break;
                    case "comProduct":
                        txtProduct.Text = comProduct.SelectedValue.ToString();
                        comColour.Focus();
                        break;
                    case "comColour":
                        txtColor.Text = comColour.SelectedValue.ToString();
                        comSize.Focus();
                        break;

                    case "comSize":
                        txtSize.Text = comSize.SelectedValue.ToString();
                        txtClassification.Focus();
                        break;

                    case "comSort":
                        txtSort.Text = comSort.SelectedValue.ToString();
                        txtCarton.Focus();
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
            if (loaded)
            {
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
                                        comColour.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("there is no item with this id");
                                        dbconnection.Close();
                                        return;
                                    }
                                    break;
                                case "txtColor":
                                    query = "select Color_Name from color where Color_ID='" + txtColor.Text + "' and Type_ID='" + txtType.Text + "'";
                                    com = new MySqlCommand(query, dbconnection);
                                    if (com.ExecuteScalar() != null)
                                    {
                                        Name = (string)com.ExecuteScalar();
                                        comColour.Text = Name;
                                        txtSize.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("there is no item with this id");
                                        txtColor.Text = "";
                                        dbconnection.Close();
                                        return;
                                    }
                                    break;
                                case "txtSize":
                                    query = "select Size_Value from size where Size_ID='" + txtSize.Text + "' and Factory_ID='" + txtFactory.Text + "'";
                                    com = new MySqlCommand(query, dbconnection);
                                    if (com.ExecuteScalar() != null)
                                    {
                                        Name = (string)com.ExecuteScalar();
                                        comSize.Text = Name;
                                        txtClassification.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("there is no item with this id");
                                        txtSize.Text = "";
                                        txtClassification.Focus();
                                        dbconnection.Close();
                                        return;
                                    }
                                    break;
                                case "txtClassification":
                                    txtDescription.Focus();

                                    break;
                                case "txtDescription":
                                    txtDescription.Focus();
                                    txtSort.Focus();
                                    break;
                                case "txtSort":
                                    query = "select Sort_Value from sort where Sort_ID='" + txtSort.Text + "'";
                                    com = new MySqlCommand(query, dbconnection);
                                    if (com.ExecuteScalar() != null)
                                    {
                                        Name = (string)com.ExecuteScalar();
                                        comSort.Text = Name;
                                        txtCarton.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("there is no item with this id");
                                        txtSort.Text = "";
                                        txtCarton.Focus();
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
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFactory.Text != "" && txtGroup.Text != "" && txtProduct.Text != "" && txtType.Text != "" )
                {
                  
                    double carton = 0;
                    string classification, description = "";
                    if (comColour.Text != "")
                    {
                        try
                        {
                            color_id = Convert.ToInt16(comColour.SelectedValue.ToString());
                        }
                        catch
                        {
                            
                        }
                        
                    }
                    if (comSize.Text != "")
                    {
                        try
                        {
                            size_id = Convert.ToInt16(comSize.SelectedValue.ToString());
                        }
                        catch
                        {

                        }
                        
                    }
                    if (comSort.Text != "")
                    {
                        try
                        {
                            sort_id = Convert.ToInt16(comSort.SelectedValue.ToString());
                        }
                        catch
                        {

                        }
                        
                    }
                    if (txtCarton.Text != "")
                    {
                        if (double.TryParse(txtCarton.Text, out carton))
                        {
                        }
                        else
                        {
                            MessageBox.Show("carton must be numeric");
                            dbconnection.Close();
                            return;
                        }
                    }
                    classification = txtClassification.Text;
                    description = txtDescription.Text;

                    string q = "SELECT Data_ID from data where Color_ID=" + color_id + " and Size_ID=" + size_id + " and Sort_ID=" + sort_id + " and Description='" + description + "' and Carton=" + carton + " and Type_ID=" + txtType.Text + " and Factory_ID=" + txtFactory.Text + " and Group_ID=" + txtGroup.Text + " and Product_ID=" + txtProduct.Text + " and Classification='" + classification + "'";
                    MySqlCommand comand = new MySqlCommand(q, dbconnection);
                    dbconnection.Open();
                    var resultValue = comand.ExecuteReader();
                    if (!resultValue.HasRows)
                    {
                        dbconnection.Close();

                        MySqlCommand command = dbconnection.CreateCommand();

                        string code = txtType.Text;

                        int typecount = txtType.Text.Length;

                        int factorycount = txtFactory.Text.Length;

                        int groupcount = txtGroup.Text.Length;

                        int productcount = txtProduct.Text.Length;

                        while (typecount < 4)
                        {
                            code = "0" + code;
                            typecount++;
                        }

                        string code2 = txtFactory.Text;

                        while (factorycount < 4)
                        {
                            code2 = "0" + code2;
                            factorycount++;
                        }

                        code = code + code2;

                        string code3 = txtGroup.Text;

                        while (groupcount < 4)
                        {
                            code3 = "0" + code3;
                            groupcount++;
                        }

                        code = code + code3;

                        string code4 = txtProduct.Text;

                        while (productcount < 4)
                        {
                            code4 = "0" + code4;
                            productcount++;
                        }

                        code = code + code4;
                        string code5 = "";

                        if (!IsMainChang())
                        {
                            string query2 = "SELECT count(Code) FROM data where Type_ID=" + txtType.Text + " and Factory_ID=" + txtFactory.Text + " and Group_ID=" + txtGroup.Text + " and Product_ID=" + txtProduct.Text;
                            dbconnection.Open();
                            MySqlCommand adpt = new MySqlCommand(query2, dbconnection);
                            int result = Convert.ToInt16(adpt.ExecuteScalar().ToString());
                            result = result + 1;
                            dbconnection.Close();

                            int resultcount = result.ToString().Length;

                            code5 = result.ToString();

                            while (resultcount < 4)
                            {
                                code5 = "0" + code5;
                                resultcount++;
                            }
                        }
                        else
                        {
                            code5 = lastPartCode;
                        }
                        code = code + code5;

                        command.CommandText = "update  data set Color_ID=?Color_ID,Size_ID=?Size_ID,Sort_ID=?Sort_ID,Description=?Description,Carton=?Carton,Code=?Code,Type_ID=?Type_ID,Factory_ID=?Factory_ID,Group_ID=?Group_ID,Product_ID=?Product_ID,Classification=?Classification where Data_ID="+ Data_ID;

                        command.Parameters.AddWithValue("?Color_ID", color_id);
                        command.Parameters.AddWithValue("?Size_ID", size_id);
                        command.Parameters.AddWithValue("?Sort_ID", sort_id);
                        command.Parameters.AddWithValue("?Description", description);
                        command.Parameters.AddWithValue("?Carton", carton);
                        command.Parameters.AddWithValue("?Code", code);
                        command.Parameters.AddWithValue("?Type_ID", int.Parse(txtType.Text));
                        command.Parameters.AddWithValue("?Factory_ID", int.Parse(txtFactory.Text));
                        command.Parameters.AddWithValue("?Group_ID", int.Parse(txtGroup.Text));
                        command.Parameters.AddWithValue("?Product_ID", int.Parse(txtProduct.Text));
                        command.Parameters.AddWithValue("?Classification", classification);
                        dbconnection.Open();
                        command.ExecuteNonQuery();
                        dbconnection.Close();


                        //save image as bytes
                        command = dbconnection.CreateCommand();
                        command.CommandText = "update data_details set Photo =?Photo where Data_ID=" + Data_ID;
                        command.Parameters.AddWithValue("?Photo", selectedImage);
                        dbconnection.Open();
                        command.ExecuteNonQuery();
                        dbconnection.Close();

                        
                      //  UserControl.UserRecord("data", "update",code, DateTime.Now, dbconnection);

                        MessageBox.Show("updated");
                        products.displayProducts();

                        XtraTabPage xtraTabPage = getTabPage("تعديل بند");
                        xtraTabPage.ImageOptions.Image = null;
                        comType.Focus();
                    }
                    else
                    {
                        MessageBox.Show("This item already exist");
                    }
                }
                else
                {
                    MessageBox.Show("you should fill all fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtClassification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescription.Focus();
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comSort.Focus();
            }
        }    

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog1.FileName;
              
                ImageProduct.Image = Image.FromFile(openFileDialog1.FileName);                
                selectedImage= File.ReadAllBytes(selectedFile);
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (flag)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل بند");
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

        private void ImageProduct_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog1.FileName;

                    ImageProduct.Image = Image.FromFile(openFileDialog1.FileName);
                    selectedImage = File.ReadAllBytes(selectedFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public void loadData()
        {
            string query = "select * from type";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comType.DataSource = dt;
            comType.DisplayMember = dt.Columns["Type_Name"].ToString();
            comType.ValueMember = dt.Columns["Type_ID"].ToString();
            comType.Text = "";

            query = "select * from factory";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comFactory.DataSource = dt;
            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
            comFactory.Text = "";

            query = "select * from groupo";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comGroup.DataSource = dt;
            comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
            comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
            comGroup.Text = "";

            query = "select * from product";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comProduct.DataSource = dt;
            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
            comProduct.Text = "";

            query = "select * from sort";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSort.DataSource = dt;
            comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
            comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
            comSort.Text = "";

        }

        public void displayData(DataRowView row1)
        {
            ImageProduct.Image = Properties.Resources.animated_gif_loading;
            Thread t1 = new Thread(displayImage);
            t1.Start();

            comType.Text = row1["النوع"].ToString();             
            comFactory.Text = row1["المصنع"].ToString();       
            comGroup.Text = row1["المجموعة"].ToString();
            comProduct.Text = row1["الصنف"].ToString();
            displayCode();
            TypeColor();
            FactorySize();

            txtCarton.Text = row1["الكرتنة"].ToString();

            comColour.Text =row1["اللون"].ToString();
            if(comColour.Text!="")
                txtColor.Text = row1["Color_ID"].ToString();

            comSize.Text = row1["المقاس"].ToString();
            if (comSize.Text != "")
                txtSize.Text = row1["Size_ID"].ToString();

            comSort.Text = row1["الفرز"].ToString();
            if (comSort.Text != "")
                txtSort.Text = row1["Sort_ID"].ToString();



            txtDescription.Text = row1["التصنيف"].ToString();
            txtClassification.Text = row1["الوصف"].ToString();

            Data_ID =Convert.ToInt16(row1[0]);
        }

        public void displayImage()
        {
            try
            {
                dbconnection1.Open();
                string query = "select Photo from data_details where Code='" + code + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection1);
                byte[] photo = (byte[])com.ExecuteScalar();
               
                if (photo != null)
                {
                    MemoryStream ms = new MemoryStream(photo);
                    ImageProduct.Image = Image.FromStream(ms);
                }
                else
                {
                    ImageProduct.Image = null;// Properties.Resources.notFound;
                }
            }
            catch
            {
               //MessageBox.Show(ex.Message);
               ImageProduct.Image = Properties.Resources.notFound;
            }
            dbconnection1.Close();
        }

        public void displayCode()
        {
            char[] arrCode = code.ToCharArray();
            txtType.Text =Convert.ToInt16(arrCode[0].ToString() + arrCode[1].ToString() + arrCode[2].ToString() + arrCode[3].ToString() + "").ToString();
            txtFactory.Text = Convert.ToInt16(arrCode[4].ToString() + arrCode[5].ToString() + arrCode[6].ToString() + arrCode[7].ToString() + "").ToString();
            txtGroup.Text = Convert.ToInt16(arrCode[8].ToString() + arrCode[9].ToString() + arrCode[10].ToString() + arrCode[11].ToString() + "").ToString();
            txtProduct.Text = Convert.ToInt16(arrCode[12].ToString() + arrCode[13].ToString() + arrCode[14].ToString() + arrCode[15].ToString() + "").ToString();
            lastPartCode = "" + arrCode[16] + arrCode[17] + arrCode[18] + arrCode[19];
        }

        private void TypeColor()
        {
            string query = "select * from color where Type_ID=" + Convert.ToInt16(comType.SelectedValue);
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comColour.DataSource = dt;
            comColour.DisplayMember = dt.Columns["Color_Name"].ToString();
            comColour.ValueMember = dt.Columns["Color_ID"].ToString();
            comColour.Text = "";
           
           // comFactory.Focus();
        }

        private void FactorySize()
        {
            string query2 = "select * from size where Factory_ID=" + Convert.ToInt16(comFactory.SelectedValue)+" and Group_ID="+txtGroup.Text;
            MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comSize.DataSource = dt2;
            comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
            comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
            comSize.Text = "";
            //comGroup.Focus();
        }
        
        public bool IsClear()
        {
            if (comType.Text == prodRow["النوع"].ToString() &&
            comFactory.Text == prodRow["المصنع"].ToString() &&
            comGroup.Text == prodRow["المجموعة"].ToString() &&
            comProduct.Text == prodRow["الصنف"].ToString() &&
            txtCarton.Text == prodRow["الكرتنة"].ToString() &&
            comColour.Text == prodRow["اللون"].ToString() &&
            comSize.Text == prodRow["المقاس"].ToString() &&
            comSort.Text == prodRow["الفرز"].ToString() &&
            txtDescription.Text == prodRow["التصنيف"].ToString() &&
            txtClassification.Text == prodRow["الوصف"].ToString())
                return true;

            return false;
        }

        public bool IsMainChang()
        {
            if (comType.Text == prodRow["النوع"].ToString() &&
            comFactory.Text == prodRow["المصنع"].ToString() &&
            comGroup.Text == prodRow["المجموعة"].ToString() &&
            comProduct.Text == prodRow["الصنف"].ToString() )
                return true;

            return false;
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

      

    }
    
}
