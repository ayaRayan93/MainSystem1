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
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraNavBar;
using MySql.Data.MySqlClient;

namespace MainSystem
{
    class CarSystem
    {
    }
    
    public partial class MainForm
    {
        //Cars
        private void navBarItem1_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"السيارات");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("السيارات");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"السيارات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                Cars objForm = new Cars(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                //objForm.DisplayAtaqm();
                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Drivers
        private void navBarItem2_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"السائقين");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("السائقين");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"السائقين");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                Drivers objForm = new Drivers(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;

                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Spare Part
        private void navBarItem3_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"قطع الغيار");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("قطع الغيار");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"قطع الغيار");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                SparePart objForm = new SparePart(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;

                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //MainSystem
        private void navBarItem4_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"الأ يرادات");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("الأ يرادات");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"الأ يرادات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                CarIncomes objForm = new CarIncomes(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;

                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //el3hata
        private void navBarItem5_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"العهدة");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("العهدة");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"العهدة");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                CarEl3hata objForm = new CarEl3hata(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;

                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //car expenses
        private void navBarItem6_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"المصروفات");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("المصروفات");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"المصروفات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                CarExpenses objForm = new CarExpenses(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;

                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //car expens type
        private void navBarItem12_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل نوع المصروف");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("تسجيل نوع المصروف");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل نوع المصروف");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                CarExpensesType objForm = new CarExpensesType();

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;

                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //el3tat
        private void navBarItem8_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل قراءة العدات");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("تسجيل قراءة العداد");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل قراءة العداد");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                El3tat objForm = new El3tat(xtraTabControlCarsContent);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;

                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //calender
        private void navBarItem4_LinkClicked_1(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"الأجندة");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("الأجندة");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"الأجندة");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                //Calender objForm = new Calender();

                //objForm.TopLevel = false;
                //xtraTabPage.Controls.Add(objForm);
                //objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                //objForm.Dock = DockStyle.Fill;

                //objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //function
        //cars
        public void bindRecordCarForm(Cars cars)
        {
            Car_Record objForm = new Car_Record(cars, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"أضافة سيارة");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("أضافة سيارة");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"أضافة سيارة");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindUpdateCarForm(DataRowView carRow, Cars cars)
        {
            Car_Update objForm = new Car_Update(carRow, cars, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل سيارة");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تعديل سيارة");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل سيارة");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReportCarsForm(GridControl gridControl)
        {
            Cars_Report objForm = new Cars_Report(gridControl);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير السيارات");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تقرير السيارات");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير السيارات");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindCarPaperForm(DataRowView carRow, Cars cars)
        {
            Car_Papers objForm = new Car_Papers(carRow, cars, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"اوراق سيارة");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("اوراق سيارة");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"اوراق سيارة");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindCarDriversForm(DataRowView carRow, Cars cars)
        {
            CarDrivers objForm = new CarDrivers(carRow, cars, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"سائقي السيارة");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("سائقي السيارة");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"سائقي السيارة");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindSparePartForm(DataRowView carRow, Cars cars)
        {
            CarSpareParts objForm = new CarSpareParts(carRow, cars, xtraTabControlCarsContent, this);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"قطع الغيار");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("قطع الغيار");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"قطع الغيار");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindCarLicenseForm(DataRowView carRow, Cars cars)
        {
            try
            {
                dbconnection.Open();
                String query = "select Car_License_ID from car_license where Car_ID=" + carRow[0].ToString();
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() == null)
                {
                    CarLicenseRecord objForm = new CarLicenseRecord(carRow, cars, xtraTabControlCarsContent, this);

                    objForm.TopLevel = false;
                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل رخصة العربية");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlCarsContent.TabPages.Add("تسجيل رخصة العربية");
                        xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل رخصة العربية");

                    }
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else
                {
                    CarLicenseUpdate objForm = new CarLicenseUpdate(carRow, cars, xtraTabControlCarsContent, this);

                    objForm.TopLevel = false;
                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل رخصة العربية");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlCarsContent.TabPages.Add("تعديل رخصة العربية");
                        xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل رخصة العربية");

                    }
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //drivers
        public void bindRecordDriverForm(Drivers drivers)
        {
            Driver_Record objForm = new Driver_Record(drivers, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل سائق");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تسجيل سائق");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل سائق");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateDriverForm(DataRowView DriverRow, Drivers drivers)
        {
            Driver_Update objForm = new Driver_Update(DriverRow, drivers, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل بيانات سائق");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تعديل بيانات سائق");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل بيانات سائق");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReportDriverForm(GridControl gridControl)
        {
            Drivers_Report objForm = new Drivers_Report(gridControl);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير السائقين");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تقرير السائقين");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير السائقين");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //spare part
        public void bindRecordSparePartForm(SparePart sparepart)
        {
            SparePartRecord objForm = new SparePartRecord(sparepart, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل قطع الغيار");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تسجيل قطع الغيار");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل قطع الغيار");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateSparePartForm(DataRowView SparePartRow, SparePart sparepart)
        {
            SparePartUpdate objForm = new SparePartUpdate(SparePartRow, sparepart, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل قطع الغيار");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تعديل قطع الغيار");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل قطع الغيار");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReportSparePartForm(GridControl gridControl)
        {
            SparePart_Report objForm = new SparePart_Report(gridControl);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير قطع الغيار");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تقرير قطع الغيار");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير قطع الغيار");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //El3hata
        public void bindRecordEl3hataForm(CarEl3hata carEl3hata)
        {
            CarEl3htaCreate objForm = new CarEl3htaCreate(carEl3hata, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل العهدة");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تسجيل العهدة");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل العهدة");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateEl3hataForm(DataRowView El3htaRow, CarEl3hata carEl3hata)
        {
            CarElahataUpdate objForm = new CarElahataUpdate(Convert.ToInt16(El3htaRow[0].ToString()), carEl3hata, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل العهدة");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تعديل العهدة");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل العهدة");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReportEl3hataForm(GridControl gridControl)
        {
            CarEl3hataReport objForm = new CarEl3hataReport(gridControl);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير العهدة");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تقرير العهدة");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير العهدة");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //car Expenses
        public void bindRecordExpensesForm(CarExpenses carExpenses)
        {
            CarExpensesRecord objForm = new CarExpensesRecord(carExpenses, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل مصروف");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تسجيل مصروف");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل مصروف");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateExpensesForm(DataRowView ExpensesRow, CarExpenses carExpenses)
        {
            CarExpensesUpdate objForm = new CarExpensesUpdate(ExpensesRow, carExpenses, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل مصروف");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تعديل مصروف");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل مصروف");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReportExpensesForm(GridControl gridControl)
        {
            CarExpensesReport objForm = new CarExpensesReport(gridControl);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير المصروفات");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تقرير المصروفات");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير المصروفات");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //car Incomes
        public void bindRecordIncomesForm(CarIncomes carIncomes)
        {
            CarIncomeRecord objForm = new CarIncomeRecord(carIncomes, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل إيراد");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تسجيل إيراد");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تسجيل إيراد");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateIncomesForm(DataRowView incomesRow, CarIncomes carIncomes)
        {
            CarIncomeUpdate objForm = new CarIncomeUpdate(Convert.ToInt16(incomesRow[0].ToString()), carIncomes, xtraTabControlCarsContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل إيراد");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تعديل إيراد");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل إيراد");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReportIncomesForm(GridControl gridControl)
        {
            CarIncomesReport objForm = new CarIncomesReport(gridControl);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير الإيرادات");
            if (xtraTabPage == null)
            {
                xtraTabControlCarsContent.TabPages.Add("تقرير الإيرادات");
                xtraTabPage = getTabPage(xtraTabControlCarsContent,"تقرير الإيرادات");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        void item_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                string str = clickedItem.Text;
                string carNumber = str.Split(',')[0];

                string query = "select Car_ID from cars where Car_Number=" + carNumber;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                string id = com.ExecuteScalar().ToString();

                //// open carsManager Tap
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageCars))
                {
                    xtraTabControlMainContainer.TabPages.Insert(index, CarsTP);
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageCars)];

                //// open carsContent Tap
                if (!xtraTabControlCarsContent.Visible)
                    xtraTabControlCarsContent.Visible = true;

                ////

                CarLicenseUpdate objForm = new CarLicenseUpdate(id, carNumber, xtraTabControlCarsContent, this);
                objForm.TopLevel = false;
                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل رخصة العربية");
                if (xtraTabPage == null)
                {
                    xtraTabControlCarsContent.TabPages.Add("تعديل رخصة العربية");
                    xtraTabPage = getTabPage(xtraTabControlCarsContent,"تعديل رخصة العربية");
                }

                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlCarsContent.SelectedTabPage = xtraTabPage;

                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void labNotify_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(labNotify, new Point(0, labNotify.Height));
        }
      
        public void displayNotification(MySqlConnection dbconnection)
        {
            int x = 0;
            contextMenuStrip1.Items.Clear();
            string query = "select cars.Car_Number, DATEDIFF(car_license.End_License_Date,DATE(NOW())) as 'y' from cars inner join car_license ON cars.Car_ID=car_license.Car_ID where (car_license.End_License_Date-DATE(NOW()))<=7";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                int numDays = Convert.ToInt16(dr[1].ToString());

                if (numDays > 0)
                    contextMenuStrip1.Items.Add(dr[0].ToString() + ", باقي " + dr[1].ToString() + "يوم علي انتهاء معاد تجديد الرخصة ");
                else if (numDays == 0)
                    contextMenuStrip1.Items.Add(dr[0].ToString() + ", معاد تجديد الرخصة اليوم");
                else
                    contextMenuStrip1.Items.Add(dr[0].ToString() + ", انتهت الرخصة منذ " + (numDays * -1) + "يوم  ");

                x++;
            }
            dr.Close();

            query = "select cars.Car_Number, DATEDIFF(car_sparepart.Car_SpareDate,DATE(NOW())) as 'y' ,sparepart.SparePart_Name from cars inner join car_sparepart ON cars.Car_ID=car_sparepart.Car_ID INNER JOIN sparepart on sparepart.SparePart_ID=car_sparepart.SparePart_ID where (car_sparepart.Car_SpareDate-DATE(NOW()))<=7";
            com = new MySqlCommand(query, dbconnection);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                int numDays = Convert.ToInt16(dr[1].ToString());

                if (numDays > 0)
                    contextMenuStrip1.Items.Add(dr[0].ToString() + ", باقي " + dr[1].ToString() + "يوم علي انتهاء صلاحية " + dr[2].ToString());
                else if (numDays == 0)
                    contextMenuStrip1.Items.Add(dr[0].ToString() + ", معاد فحص" + dr[2].ToString() + " اليوم");
                else
                    contextMenuStrip1.Items.Add(dr[0].ToString() + " منذ " + (numDays * -1) + "يوم  " + dr[2].ToString() + ", انتهت صلاحية");

                x++;
            }
            dr.Close();

            if (x == 0)
                labNotify.Text = "";
            else
                labNotify.Text = x.ToString();

            for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
            {
                contextMenuStrip1.Items[i].Click += item_Click;
            }

        }
    }
}
