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
    class HRSystem
    {
    }
    public partial class MainForm 
    {
      
        //employees
        private void navBarItem2_ItemClicked(object sender, EventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlHRContent.Visible)
                    xtraTabControlHRContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"بيان الموظفين");
                if (xtraTabPage == null)
                {
                    xtraTabControlHRContent.TabPages.Add("بيان الموظفين");
                    xtraTabPage = getTabPage(xtraTabControlHRContent,"بيان الموظفين");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlHRContent.SelectedTabPage = xtraTabPage;
                bindDisplayEmployeesForm(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //employee basic data
        private void navBarItemEmployee_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlHRContent.Visible)
                    xtraTabControlHRContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير البيانات الاساسية الموظفين");
                if (xtraTabPage == null)
                {
                    xtraTabControlHRContent.TabPages.Add("تقرير البيانات الاساسية الموظفين");
                    xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير البيانات الاساسية الموظفين");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlHRContent.SelectedTabPage = xtraTabPage;
                EmployeesBasicData objForm = new EmployeesBasicData(this);
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
        //employee names report
        private void navBarItem7_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlHRContent.Visible)
                    xtraTabControlHRContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير اسماء الموظفين");
                if (xtraTabPage == null)
                {
                    xtraTabControlHRContent.TabPages.Add("تقرير اسماء الموظفين");
                    xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير اسماء الموظفين");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlHRContent.SelectedTabPage = xtraTabPage;
                EmployeesName objForm = new EmployeesName(this);
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

        private void navBarItemEmployeeReports_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlHRContent.Visible)
                    xtraTabControlHRContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير عناوين وارقام هواتف الموظفين");
                if (xtraTabPage == null)
                {
                    xtraTabControlHRContent.TabPages.Add("تقرير عناوين وارقام هواتف الموظفين");
                    xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير عناوين وارقام هواتف الموظفين");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlHRContent.SelectedTabPage = xtraTabPage;
                EmployeeConnectionInfo objForm = new EmployeeConnectionInfo(this);
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

        private void navBarItem9_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlHRContent.Visible)
                    xtraTabControlHRContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير البيانات الشخصية للموظفين");
                if (xtraTabPage == null)
                {
                    xtraTabControlHRContent.TabPages.Add("تقرير البيانات الشخصية للموظفين");
                    xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير البيانات الشخصية للموظفين");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlHRContent.SelectedTabPage = xtraTabPage;
                EmployeePersonalInformation objForm = new EmployeePersonalInformation(this);
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
        //money processe 
        private void btnSalaryRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlHRContent.Visible)
                    xtraTabControlHRContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent," رواتب الموظفين");
                if (xtraTabPage == null)
                {
                    xtraTabControlHRContent.TabPages.Add(" رواتب الموظفين");
                    xtraTabPage = getTabPage(xtraTabControlHRContent," رواتب الموظفين");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlHRContent.SelectedTabPage = xtraTabPage;
                Salary objForm = new Salary(this);
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

        //Employee Main form functions
        public void bindDisplayEmployeesForm(XtraTabPage xtraTabPage)
        {
            Employees objForm = new Employees(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordEmployeesForm(Employees employees)
        {
            EmployeeRecord objForm = new EmployeeRecord(employees, xtraTabControlHRContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"أضافة موظف");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("أضافة موظف");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"أضافة موظف");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateEmployeesForm(DataRowView row, Employees employees)
        {
            EmployeeUpdate objForm = new EmployeeUpdate(row, employees, xtraTabControlHRContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تعديل موظف");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("تعديل موظف");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"تعديل موظف");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReportEmployeesForm(GridControl gridControl)
        {
            EmployeeReport objForm = new EmployeeReport(gridControl, "تقرير الموظفين");

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير الموظفين");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("تقرير الموظفين");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير الموظفين");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReport1EmployeesForm(GridControl gridControl)
        {
            EmployeeReport objForm = new EmployeeReport(gridControl, "تقرير البيانات الاساسية الموظفين");

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير البيانات الاساسية الموظفين");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("تقرير البيانات الاساسية الموظفين");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير البيانات الاساسية الموظفين");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReport2EmployeesForm(GridControl gridControl)
        {
            EmployeeReport objForm = new EmployeeReport(gridControl, "تقرير اسماء الموظفين");

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير اسماء الموظفين");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("تقرير اسماء الموظفين");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير اسماء الموظفين");
            }

            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReport3EmployeesForm(GridControl gridControl)
        {
            EmployeeReport objForm = new EmployeeReport(gridControl, "تقرير عناوين وارقام هواتف الموظفين");

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير عناوين وارقام هواتف الموظفين");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("تقرير عناوين وارقام هواتف الموظفين");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير عناوين وارقام هواتف الموظفين");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReport4EmployeesForm(GridControl gridControl)
        {
            EmployeeReport objForm = new EmployeeReport(gridControl, "تقرير البيانات الشخصية للموظفين");

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير البيانات الشخصية للموظفين");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("تقرير البيانات الشخصية للموظفين");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير البيانات الشخصية للموظفين");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //money processe 
        public void bindRecordSalariesForm(Salary salary)
        {
            SalaryRecord objForm = new SalaryRecord(salary, xtraTabControlHRContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"أضافة راتب موظف");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("أضافة راتب موظف");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"أضافة راتب موظف");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdateSalariesForm(DataRowView row, Salary salaries)
        {
            SalaryUpdate objForm = new SalaryUpdate(row, salaries, xtraTabControlHRContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تعديل راتب موظف");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("تعديل راتب موظف");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"تعديل راتب موظف");
            }

            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }

        public void bindReportSalariesForm(GridControl gridControl)
        {
            EmployeeReport objForm = new EmployeeReport(gridControl, "تقرير رواتب الموظفين");

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير رواتب الموظفين");
            if (xtraTabPage == null)
            {
                xtraTabControlHRContent.TabPages.Add("تقرير رواتب الموظفين");
                xtraTabPage = getTabPage(xtraTabControlHRContent,"تقرير رواتب الموظفين");
            }

            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlHRContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

       
        //functions
   


    }
}
