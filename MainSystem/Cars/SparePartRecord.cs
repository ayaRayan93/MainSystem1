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
    public partial class SparePartRecord : Form
    {
        MySqlConnection dbconnection;
        SparePart sparePart;
        XtraTabControl xtraTabControlCarsContent;

        public SparePartRecord(SparePart sparePart, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.sparePart = sparePart;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnAddSparePart_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select SparePart_ID from sparepart where SparePart_Name='" + txtName.Text + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                
                if (com.ExecuteScalar() == null)
                {
                    if (txtName.Text != "")
                    {
                        string qeury = "insert into sparepart (SparePart_Name,SparePart_Info)values(@Name,@info)";
                        com = new MySqlCommand(qeury, dbconnection);
                        com.Parameters.Add("@Name", MySqlDbType.VarChar, 255);
                        com.Parameters["@Name"].Value = txtName.Text;
                        com.Parameters.Add("@info", MySqlDbType.VarChar, 255);
                        com.Parameters["@info"].Value = txtInfo.Text;

                        com.ExecuteNonQuery();
                        sparePart.DisplaySparePart();
                        MessageBox.Show("add success");
                        clear();
                        txtName.Focus();

                    }
                    else
                    {
                        MessageBox.Show("enter Name");
                    }
                }
                else
                {
                    MessageBox.Show("This Store already exist");
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
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

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل قطع الغيار");
                if (!IsClear())
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                else
                    xtraTabPage.ImageOptions.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public void clear()
        {
            txtName.Text = txtInfo.Text = "";
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
            foreach (Control item in this.Controls["panContent"].Controls)
            {
                if (item is TextBox)
                {
                    if (item.Text != "")
                        return false; 
                }

            }
         
            return true;
        }

      
    }

}
