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
    public partial class SparePartUpdate : Form
    {
        MySqlConnection dbconnection;
        DataRowView row1 = null;
        SparePart sparePart;
        XtraTabControl xtraTabControlCarsContent;
        bool load = false;
        public SparePartUpdate(DataRowView row1,SparePart sparePart, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.sparePart = sparePart;
                this.row1 = row1;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            dbconnection.Close();
        }

        private void SparePartUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                SetData(row1);
                load = true;
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
                if (txtName.Text != "")
                {
                    dbconnection.Open();
                    String query = "update sparepart set SparePart_Name=@Name,SparePart_Info=@info where SparePart_ID='" + row1[0].ToString() + "'";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    
                    com.Parameters.Add("@Name", MySqlDbType.VarChar, 255).Value = txtName.Text;
                    com.Parameters.Add("@info", MySqlDbType.VarChar, 255).Value = txtInfo.Text;

                    com.ExecuteNonQuery();
                    sparePart.DisplaySparePart();
                    MessageBox.Show("udpate success");
                    txtName.Focus();

                    XtraTabPage xtraTabPage = getTabPage("تعديل قطع الغيار");
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
                dbconnection.Close();
            }
            dbconnection.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {

                    XtraTabPage xtraTabPage = getTabPage("تعديل قطع الغيار");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                    else
                        xtraTabPage.ImageOptions.Image = null;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                            txtInfo.Focus();
                            break;
                        case "txtInfo":
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
  

        //functions

        private void SetData(DataRowView row)
        {
            txtName.Text = row[1].ToString();
            txtInfo.Text = row[2].ToString();
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlCarsContent.TabPages.Count; i++)
                if (xtraTabControlCarsContent.TabPages[i].Text == text)
                {
                    return xtraTabControlCarsContent.TabPages[i];
                }
            return null;
        }

        private bool IsClear()
        {
            if (txtName.Text == row1[1].ToString() &&
             txtInfo.Text == row1[2].ToString())

                return true;
            else
                return false;
        }

       
    }
}
