
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
    public partial class Store_Update : Form
    {
        MySqlConnection conn;
        int ID;
        Stores stores = null;
        XtraTabControl xtraTabControlStoresContent = null;
        string[] oldData=new string[3];
        bool load = false;
        public Store_Update(int id, Stores stores,XtraTabControl xtraTabControlStoresContent)
        {
            InitializeComponent();
            this.xtraTabControlStoresContent = xtraTabControlStoresContent;
            ID = id;
            this.stores = stores;
            string query = "select * from store where Store_ID='" + ID + "'";
            conn = new MySqlConnection(connection.connectionString);
            try
            {
                conn.Open();
                MySqlCommand com = new MySqlCommand(query, conn);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                   
                    txtName.Text = dr["Store_Name"].ToString();
                    txtAddress.Text = dr["Store_Address"].ToString();
                    txtPhone.Text = dr["Store_Phone"].ToString();
                }
                oldData[0] = txtName.Text;
                oldData[1] = txtAddress.Text;
                oldData[2] = txtPhone.Text;
                load = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            conn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "")
                {
                    conn.Open();
                    String query = "update store set Store_Name=@Name,Store_Address=@Address,Store_Phone=@Phone where Store_ID='" + ID + "'";
                    MySqlCommand com = new MySqlCommand(query, conn);
                    
                    com.Parameters.Add("@Name", MySqlDbType.VarChar, 255).Value = txtName.Text;
                    com.Parameters.Add("@Address", MySqlDbType.VarChar, 255).Value = txtAddress.Text;
                    com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255).Value = txtPhone.Text;

                    com.ExecuteNonQuery();

                   // UserControl.UserRecord("store", "update", ID.ToString(), DateTime.Now, conn);

                    MessageBox.Show("udpate success :)");
                    txtName.Focus();
                    stores.DisplayStores();

                    XtraTabPage xtraTabPage = getTabPage("تعديل مخزن");
                    xtraTabPage.ImageOptions.Image = null;
                }
                else
                {
                    MessageBox.Show("enter name");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
            conn.Close();
        }
        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    TextBox t = (TextBox)sender;
                    switch (t.Name)
                    {
                        case "txtName":
                            txtAddress.Focus();
                            break;
                        case "txtAddress":
                            txtPhone.Focus();
                            break;
                        case "txtPhone":
                            txtName.Focus();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل مخزن");
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
            if (txtName.Text == oldData[0] && txtAddress.Text == oldData[1] && txtPhone.Text == oldData[2])
                return true;
            else
                return false;

        }
    }
}
