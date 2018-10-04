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
using System.Diagnostics;

namespace MainSystem
{
    class ReceptionSystem
    {
    }
    public partial class MainForm
    {
        MySqlConnection conn;
        public static XtraTabControl tabControlReception;

        public static Delegate_Movement DelegateMovementShow;
        public static GraphicActiveMonth_Report GraphicActiveMonthReport;
        public static DelegateAbsence_Report DelegateAbsenceReport;
        public static Commitment_Report CommitmentReport;
        public static GraphicDelegateExistence_Report GraphicDelegateExistenceReport;
        public static GraphicDelegateBusy_Report GraphicDelegateBusyReport;
        public static GraphicDelegateAvailable_Report GraphicDelegateAvailableReport;
        public static GraphicDelegateBreak_Report GraphicDelegateBreakReport;
        public static GraphicDelegateAbsence_Report GraphicDelegateAbsenceReport;
        public static DelegateBusyBill_Report DelegateBusyBillReport;
        public static DelegateBillTime_Report DelegateBillTimeReport;

        XtraTabPage tabPageDelegateMovement;
        Panel panelDelegateMovement;
        XtraTabPage tabPageGraphicActiveMonthReport;
        Panel panelGraphicActiveMonthReport;
        XtraTabPage tabPageDelegateAbsenceReport;
        Panel panelDelegateAbsenceReport;
        XtraTabPage tabPageCommitmentReport;
        Panel panelCommitmentReport;
        XtraTabPage tabPageGraphicDelegateExistenceReport;
        Panel panelGraphicDelegateExistenceReport;
        XtraTabPage tabPageGraphicDelegateBusyReport;
        Panel panelGraphicDelegateBusyReport;
        XtraTabPage tabPageGraphicDelegateAvailableReport;
        Panel panelGraphicDelegateAvailableReport;
        XtraTabPage tabPageGraphicDelegateBreakReport;
        Panel panelGraphicDelegateBreakReport;
        XtraTabPage tabPageGraphicDelegateAbsenceReport;
        Panel panelGraphicDelegateAbsenceReport;
        XtraTabPage tabPageDelegateBusyBillReport;
        Panel panelDelegateBusyBillReport;
        XtraTabPage tabPageDelegateBillTimeReport;
        Panel panelDelegateBillTimeReport;

     

        public void ReceptionSystem()
        {
            conn = new MySqlConnection(connection.connectionString);

            tabPageDelegateMovement = new XtraTabPage();
            panelDelegateMovement = new Panel();
            tabPageGraphicActiveMonthReport = new XtraTabPage();
            panelGraphicActiveMonthReport = new Panel();
            tabPageDelegateAbsenceReport = new XtraTabPage();
            panelDelegateAbsenceReport = new Panel();
            tabPageCommitmentReport = new XtraTabPage();
            panelCommitmentReport = new Panel();
            tabPageGraphicDelegateExistenceReport = new XtraTabPage();
            panelGraphicDelegateExistenceReport = new Panel();
            tabPageGraphicDelegateBusyReport = new XtraTabPage();
            panelGraphicDelegateBusyReport = new Panel();
            tabPageGraphicDelegateAvailableReport = new XtraTabPage();
            panelGraphicDelegateAvailableReport = new Panel();
            tabPageGraphicDelegateBreakReport = new XtraTabPage();
            panelGraphicDelegateBreakReport = new Panel();
            tabPageGraphicDelegateAbsenceReport = new XtraTabPage();
            panelGraphicDelegateAbsenceReport = new Panel();
            tabPageDelegateBusyBillReport = new XtraTabPage();
            panelDelegateBusyBillReport = new Panel();
            tabPageDelegateBillTimeReport = new XtraTabPage();
            panelDelegateBillTimeReport = new Panel();

            tabControlReception = xtraTabControl1;
        }

      
        private void xtraTabControlReception_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                if (xtraTabControlPointSale.SelectedTabPage == tabPageDelegateBusyBillReport)
                {
                    DelegateBusyBillReport.search();
                }
                //if (xtraTabControlBank.SelectedTabPage == tabPageBankReport)
                //{
                //    formShow.search();
                //}

                //else if (xtraTabControlBank.SelectedTabPage == Bank_Report.MainTabPagePrintingBank)
                //{
                //    if (loadedPrintBank)
                //    {
                //        Bank_Report.bankPrint.display();
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Debugger.IsAttached)
                {
                    Application.Exit();
                }
                else
                {
                    Application.Exit();
                }
                //Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemControl_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageDelegateMovement");
                if (xtraTabPage == null)
                {
                    tabPageDelegateMovement.Name = "tabPageDelegateMovement";
                    tabPageDelegateMovement.Text = "عرض حركة المندوبين";
                    panelDelegateMovement.Name = "panelDelegateMovement";
                    panelDelegateMovement.Dock = DockStyle.Fill;

                    DelegateMovementShow = new Delegate_Movement();
                    DelegateMovementShow.Size = new Size(1109, 660);
                    DelegateMovementShow.TopLevel = false;
                    DelegateMovementShow.FormBorderStyle = FormBorderStyle.None;
                    DelegateMovementShow.Dock = DockStyle.Fill;
                }
                panelDelegateMovement.Controls.Clear();
                panelDelegateMovement.Controls.Add(DelegateMovementShow);
                tabPageDelegateMovement.Controls.Add(panelDelegateMovement);
                xtraTabControlPointSale.TabPages.Add(tabPageDelegateMovement);
                DelegateMovementShow.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageDelegateMovement;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        private void navBarItemGraphic_Report_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageGraphicActiveMonthReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicActiveMonthReport.Name = "tabPageGraphicActiveMonthReport";
                    tabPageGraphicActiveMonthReport.Text = "تقرير بيانى";
                    panelGraphicActiveMonthReport.Name = "panelGraphicActiveMonthReport";
                    panelGraphicActiveMonthReport.Dock = DockStyle.Fill;

                    GraphicActiveMonthReport = new GraphicActiveMonth_Report();
                    GraphicActiveMonthReport.Size = new Size(1109, 660);
                    GraphicActiveMonthReport.TopLevel = false;
                    GraphicActiveMonthReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicActiveMonthReport.Dock = DockStyle.Fill;
                }
                panelGraphicActiveMonthReport.Controls.Clear();
                panelGraphicActiveMonthReport.Controls.Add(GraphicActiveMonthReport);
                tabPageGraphicActiveMonthReport.Controls.Add(panelGraphicActiveMonthReport);
                xtraTabControlPointSale.TabPages.Add(tabPageGraphicActiveMonthReport);
                GraphicActiveMonthReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageGraphicActiveMonthReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateAbsence_Report_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageDelegateAbsenceReport");
                if (xtraTabPage == null)
                {
                    tabPageDelegateAbsenceReport.Name = "tabPageDelegateAbsenceReport";
                    tabPageDelegateAbsenceReport.Text = "تقرير غياب المندوبين";
                    panelDelegateAbsenceReport.Name = "panelDelegateAbsenceReport";
                    panelDelegateAbsenceReport.Dock = DockStyle.Fill;

                    DelegateAbsenceReport = new DelegateAbsence_Report();
                    DelegateAbsenceReport.Size = new Size(1109, 660);
                    DelegateAbsenceReport.TopLevel = false;
                    DelegateAbsenceReport.FormBorderStyle = FormBorderStyle.None;
                    DelegateAbsenceReport.Dock = DockStyle.Fill;
                }
                panelDelegateAbsenceReport.Controls.Clear();
                panelDelegateAbsenceReport.Controls.Add(DelegateAbsenceReport);
                tabPageDelegateAbsenceReport.Controls.Add(panelDelegateAbsenceReport);
                xtraTabControlPointSale.TabPages.Add(tabPageDelegateAbsenceReport);
                DelegateAbsenceReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageDelegateAbsenceReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemCommitment_Report_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageCommitmentReport");
                if (xtraTabPage == null)
                {
                    tabPageCommitmentReport.Name = "tabPageCommitmentReport";
                    tabPageCommitmentReport.Text = "تقرير الالتزام";
                    panelCommitmentReport.Name = "panelCommitmentReport";
                    panelCommitmentReport.Dock = DockStyle.Fill;

                    CommitmentReport = new Commitment_Report();
                    CommitmentReport.Size = new Size(1109, 660);
                    CommitmentReport.TopLevel = false;
                    CommitmentReport.FormBorderStyle = FormBorderStyle.None;
                    CommitmentReport.Dock = DockStyle.Fill;
                }
                panelCommitmentReport.Controls.Clear();
                panelCommitmentReport.Controls.Add(CommitmentReport);
                tabPageCommitmentReport.Controls.Add(panelCommitmentReport);
                xtraTabControlPointSale.TabPages.Add(tabPageCommitmentReport);
                CommitmentReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageCommitmentReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateExistence_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageGraphicDelegateExistenceReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateExistenceReport.Name = "tabPageGraphicDelegateExistenceReport";
                    tabPageGraphicDelegateExistenceReport.Text = "تقرير بيانى";
                    panelGraphicDelegateExistenceReport.Name = "panelGraphicDelegateExistenceReport";
                    panelGraphicDelegateExistenceReport.Dock = DockStyle.Fill;

                    GraphicDelegateExistenceReport = new GraphicDelegateExistence_Report();
                    GraphicDelegateExistenceReport.Size = new Size(1109, 660);
                    GraphicDelegateExistenceReport.TopLevel = false;
                    GraphicDelegateExistenceReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateExistenceReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateExistenceReport.Controls.Clear();
                panelGraphicDelegateExistenceReport.Controls.Add(GraphicDelegateExistenceReport);
                tabPageGraphicDelegateExistenceReport.Controls.Add(panelGraphicDelegateExistenceReport);
                xtraTabControlPointSale.TabPages.Add(tabPageGraphicDelegateExistenceReport);
                GraphicDelegateExistenceReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageGraphicDelegateExistenceReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBusy_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageGraphicDelegateBusyReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateBusyReport.Name = "tabPageGraphicDelegateBusyReport";
                    tabPageGraphicDelegateBusyReport.Text = "تقرير بيانى";
                    panelGraphicDelegateBusyReport.Name = "panelGraphicDelegateBusyReport";
                    panelGraphicDelegateBusyReport.Dock = DockStyle.Fill;

                    GraphicDelegateBusyReport = new GraphicDelegateBusy_Report();
                    GraphicDelegateBusyReport.Size = new Size(1109, 660);
                    GraphicDelegateBusyReport.TopLevel = false;
                    GraphicDelegateBusyReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateBusyReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateBusyReport.Controls.Clear();
                panelGraphicDelegateBusyReport.Controls.Add(GraphicDelegateBusyReport);
                tabPageGraphicDelegateBusyReport.Controls.Add(panelGraphicDelegateBusyReport);
                xtraTabControlPointSale.TabPages.Add(tabPageGraphicDelegateBusyReport);
                GraphicDelegateBusyReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageGraphicDelegateBusyReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateAvailable_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageGraphicDelegateAvailableReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateAvailableReport.Name = "tabPageGraphicDelegateAvailableReport";
                    tabPageGraphicDelegateAvailableReport.Text = "تقرير بيانى";
                    panelGraphicDelegateAvailableReport.Name = "panelGraphicDelegateAvailableReport";
                    panelGraphicDelegateAvailableReport.Dock = DockStyle.Fill;

                    GraphicDelegateAvailableReport = new GraphicDelegateAvailable_Report();
                    GraphicDelegateAvailableReport.Size = new Size(1109, 660);
                    GraphicDelegateAvailableReport.TopLevel = false;
                    GraphicDelegateAvailableReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateAvailableReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateAvailableReport.Controls.Clear();
                panelGraphicDelegateAvailableReport.Controls.Add(GraphicDelegateAvailableReport);
                tabPageGraphicDelegateAvailableReport.Controls.Add(panelGraphicDelegateAvailableReport);
                xtraTabControlPointSale.TabPages.Add(tabPageGraphicDelegateAvailableReport);
                GraphicDelegateAvailableReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageGraphicDelegateAvailableReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBreak_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageGraphicDelegateBreakReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateBreakReport.Name = "tabPageGraphicDelegateBreakReport";
                    tabPageGraphicDelegateBreakReport.Text = "تقرير بيانى";
                    panelGraphicDelegateBreakReport.Name = "panelGraphicDelegateBreakReport";
                    panelGraphicDelegateBreakReport.Dock = DockStyle.Fill;

                    GraphicDelegateBreakReport = new GraphicDelegateBreak_Report();
                    GraphicDelegateBreakReport.Size = new Size(1109, 660);
                    GraphicDelegateBreakReport.TopLevel = false;
                    GraphicDelegateBreakReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateBreakReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateBreakReport.Controls.Clear();
                panelGraphicDelegateBreakReport.Controls.Add(GraphicDelegateBreakReport);
                tabPageGraphicDelegateBreakReport.Controls.Add(panelGraphicDelegateBreakReport);
                xtraTabControlPointSale.TabPages.Add(tabPageGraphicDelegateBreakReport);
                GraphicDelegateBreakReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageGraphicDelegateBreakReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateAbsence_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageGraphicDelegateAbsenceReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateAbsenceReport.Name = "tabPageGraphicDelegateAbsenceReport";
                    tabPageGraphicDelegateAbsenceReport.Text = "تقرير بيانى";
                    panelGraphicDelegateAbsenceReport.Name = "panelGraphicDelegateAbsenceReport";
                    panelGraphicDelegateAbsenceReport.Dock = DockStyle.Fill;

                    GraphicDelegateAbsenceReport = new GraphicDelegateAbsence_Report();
                    GraphicDelegateAbsenceReport.Size = new Size(1109, 660);
                    GraphicDelegateAbsenceReport.TopLevel = false;
                    GraphicDelegateAbsenceReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateAbsenceReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateAbsenceReport.Controls.Clear();
                panelGraphicDelegateAbsenceReport.Controls.Add(GraphicDelegateAbsenceReport);
                tabPageGraphicDelegateAbsenceReport.Controls.Add(panelGraphicDelegateAbsenceReport);
                xtraTabControlPointSale.TabPages.Add(tabPageGraphicDelegateAbsenceReport);
                GraphicDelegateAbsenceReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageGraphicDelegateAbsenceReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBusyBill_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageDelegateBusyBillReport");
                if (xtraTabPage == null)
                {
                    tabPageDelegateBusyBillReport.Name = "tabPageDelegateBusyBillReport";
                    tabPageDelegateBusyBillReport.Text = "تقرير فواتير المناديب الحالية";
                    panelDelegateBusyBillReport.Name = "panelDelegateBusyBillReport";
                    panelDelegateBusyBillReport.Dock = DockStyle.Fill;

                    DelegateBusyBillReport = new DelegateBusyBill_Report();
                    DelegateBusyBillReport.Size = new Size(1109, 660);
                    DelegateBusyBillReport.TopLevel = false;
                    DelegateBusyBillReport.FormBorderStyle = FormBorderStyle.None;
                    DelegateBusyBillReport.Dock = DockStyle.Fill;
                }
                panelDelegateBusyBillReport.Controls.Clear();
                panelDelegateBusyBillReport.Controls.Add(DelegateBusyBillReport);
                tabPageDelegateBusyBillReport.Controls.Add(panelDelegateBusyBillReport);
                xtraTabControlPointSale.TabPages.Add(tabPageDelegateBusyBillReport);
                DelegateBusyBillReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageDelegateBusyBillReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBillTime_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControl1,"tabPageDelegateBillTimeReport");
                if (xtraTabPage == null)
                {
                    tabPageDelegateBillTimeReport.Name = "tabPageDelegateBillTimeReport";
                    tabPageDelegateBillTimeReport.Text = "تقرير استغراق المناديب للفواتير";
                    panelDelegateBillTimeReport.Name = "panelDelegateBillTimeReport";
                    panelDelegateBillTimeReport.Dock = DockStyle.Fill;

                    DelegateBillTimeReport = new DelegateBillTime_Report();
                    DelegateBillTimeReport.Size = new Size(1109, 660);
                    DelegateBillTimeReport.TopLevel = false;
                    DelegateBillTimeReport.FormBorderStyle = FormBorderStyle.None;
                    DelegateBillTimeReport.Dock = DockStyle.Fill;
                }
                panelDelegateBillTimeReport.Controls.Clear();
                panelDelegateBillTimeReport.Controls.Add(DelegateBillTimeReport);
                tabPageDelegateBillTimeReport.Controls.Add(panelDelegateBillTimeReport);
                xtraTabControlPointSale.TabPages.Add(tabPageDelegateBillTimeReport);
                DelegateBillTimeReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageDelegateBillTimeReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    
    }
}
