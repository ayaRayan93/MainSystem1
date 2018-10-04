using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using DevExpress.XtraTab;

namespace MainSystem
{
    public partial class Car_Papers : Form
    {
        MySqlConnection dbconnection;
        byte[] mFile;
        DataRowView CarRow = null;
        //DataRowView carSparePart = null;
        int CarId;
        Cars cars;
        XtraTabControl xtraTabControlCarsContent;

        public Car_Papers(DataRowView CarRow, Cars cars, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.CarRow = CarRow;
                txtCarNumber.Text = CarRow[1].ToString();
                CarId = Convert.ToInt16(CarRow[0].ToString());
                this.cars = cars;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Car_Papers_Load(object sender, EventArgs e)
        {
            try
            {
                imageSlider1.Images.Add(Properties.Resources.BackgroundF);
                imageSlider1.Images.Add(Properties.Resources.BackgroundF1);
                imageSlider1.Images.Add(Properties.Resources.BackgroundF2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                dbconnection.Open();
               
                if (txtCarNumber.Text != "")
                {
                    string qeury = "insert into car_paper (Car_ID,Car_Paper_Info,Car_Paper)values(@Car_ID,@info,@Car_Paper)";
                    MySqlCommand com = new MySqlCommand(qeury, dbconnection);
                    com.Parameters.Add("@Car_ID", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_ID"].Value = CarId;
                    com.Parameters.Add("@info", MySqlDbType.VarChar, 255);
                    com.Parameters["@info"].Value = txtInfo.Text;
                    com.Parameters.AddWithValue("@Car_Paper", mFile);

                    com.ExecuteNonQuery();
                    MessageBox.Show("add success");
                    clear();
                    txtCarNumber.Focus();
                }
                else
                {
                    MessageBox.Show("enter Name");
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
                            txtCarNumber.Focus();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog1.FileName;
                    txtFileName.Text = selectedFile;
                    imageSlider1.Images.Add( Image.FromFile(openFileDialog1.FileName));
                    imageSlider1.SetCurrentImageIndex(imageSlider1.Images.Count - 1,false);
                    mFile = File.ReadAllBytes(selectedFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PrintableComponentLink pcl = new PrintableComponentLink(new PrintingSystem());      
            pcl.Component = gridControl1;
            pcl.CreateReportHeaderArea += new CreateAreaEventHandler(pcl_CreateReportHeaderArea);
            pcl.CreateDocument();
            pcl.ShowPreviewDialog();
        }

        void pcl_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.DrawImage(new Bitmap(imageSlider1.CurrentImage), new RectangleF(0, 0, imageSlider1.CurrentImage.Width, imageSlider1.CurrentImage.Height));
            //e.Graph.DrawString("", new RectangleF(500, 0, 1000, 20));
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {

                XtraTabPage xtraTabPage = getTabPage("تعديل سيارة");
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

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlCarsContent.TabPages.Count; i++)
                if (xtraTabControlCarsContent.TabPages[i].Text == text)
                {
                    return xtraTabControlCarsContent.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            if (txtCarNumber.Text == "" && txtInfo.Text == "")
                return true;
            else
                return false;
        }

        public void clear()
        {
            txtCarNumber.Text = txtInfo.Text = "";
        }
        
    }
   
}
