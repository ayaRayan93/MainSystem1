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
    class StoreSystem
    {
    }
    public partial class MainForm 
    {
       
        //stores
        private void btnStoreRecord_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض المخازن");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عرض المخازن");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض المخازن");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                bindDisplayStoresForm(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //ProductItems
        private void btnProductItems_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;


                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"عناصر البند");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عناصر البند");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"عناصر البند");
                }
                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                ProductItems objForm = new ProductItems();

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
        //Products
        private void btnProducts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;



                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض البنود");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عرض البنود");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض البنود");
                }
                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                Products objForm = new Products(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.displayProducts();
                objForm.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Ataqm
        //Ataqm CURD
        private void btnAtaqm_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض الأطقم");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عرض الأطقم");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض الأطقم");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                Ataqm objForm = new Ataqm(this);

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
        //Ataqm Tagame3
        private void navBarItemAtaqmTagame3_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تسجيل كميات الاطقم");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسجيل كميات الاطقم");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"تسجيل كميات الاطقم");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                AtaqmStorage objForm = new AtaqmStorage(this);

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


        #endregion

        //storage
        private void navBarItemstorage_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent," كميات البنود");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add(" كميات البنود");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent," كميات البنود");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                Storage objForm = new Storage(this);

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

        /// <summary>
        private void xtraTabControlStoresContent_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
                XtraTabPage xtraTabPage = (XtraTabPage)arg.Page;
                if (xtraTabPage.ImageOptions.Image != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to Close this page without save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        xtraTabControlStoresContent.TabPages.Remove(arg.Page as XtraTabPage);
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                else
                {
                    xtraTabControlStoresContent.TabPages.Remove(arg.Page as XtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        //functions
        //Stores
        public void bindDisplayStoresForm(XtraTabPage xtraTabPage)
        {
            Stores objForm = new Stores(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordStoresForm(Stores stores)
        {
            Store_Record objForm = new Store_Record(stores, xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة مخزن");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("أضافة مخزن");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة مخزن");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindUpdateStoresForm(DataRowView selRow, Stores stores)
        {
            int id = Convert.ToInt16(selRow[0].ToString());

            Store_Update objForm = new Store_Update(id, stores, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل مخزن");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل مخزن");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل مخزن");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindReportStoresForm(GridControl gridControl)
        {
            Store_Report objForm = new Store_Report(gridControl);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير المخازن");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير المخازن");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير المخازن");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindStorePlacesForm(DataRowView storeRow)
        {
            DataRowView storeRow1 = storeRow;
            StorePlaces objForm = new StorePlaces(storeRow1);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"أماكن التخزين");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("أماكن التخزين");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"أماكن التخزين");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //Products
        public void bindDisplayProductsForm(XtraTabPage xtraTabPage)
        {
            Products objForm = new Products(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordProductForm(Products products)
        {
            Product_Record objForm = new Product_Record(products, xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة بند");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("أضافة بند");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة بند");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindUpdateProductForm(DataRowView prodRow, Products products)
        {
            Product_Update objForm = new Product_Update(prodRow, products, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل بند");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل بند");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل بند");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindReportProductForm(GridControl gridControl)
        {
            Product_Report objForm = new Product_Report(gridControl, "تقرير البنود");
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير البنود");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير البنود");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //Sets
        public void bindDisplaySetsForm(XtraTabPage xtraTabPage)
        {
            Ataqm objForm = new Ataqm(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordSetForm(Ataqm ataqm)
        {
            SetRecord objForm = new SetRecord(ataqm, xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة طقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("أضافة طقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة طقم");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindUpdateSetForm(DataRowView prodRow, Ataqm ataqm)
        {
            SetUpdate objForm = new SetUpdate(prodRow, ataqm, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل طقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل طقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل طقم");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindReportSetForm(GridControl gridControl)
        {
            SetReport objForm = new SetReport(gridControl);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير أطقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير أطقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير أطقم");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //sets Storage
        public void bindDisplaySetsStorageForm(XtraTabPage xtraTabPage)
        {
            AtaqmStorage objForm = new AtaqmStorage(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindTagame3SetForm(AtaqmStorage ataqm)
        {
            SetTagame3 objForm = new SetTagame3(ataqm);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تجميع طقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تجميع طقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تجميع طقم");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindFakSetForm(AtaqmStorage ataqm)
        {
            SetFak objForm = new SetFak(ataqm);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"فك طقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("فك طقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"فك طقم");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindReportStorageSetForm(GridControl gridControl)
        {
            SetReport objForm = new SetReport(gridControl);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير كميات الأطقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير كميات الأطقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير كميات الأطقم");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //storag
        public void bindRecordStorageForm(Storage storage)
        {
            initialCodeStorage objForm = new initialCodeStorage(storage, xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تسجيل كميات البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تسجيل كميات البنود");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تسجيل كميات البنود");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindReportStorageForm(GridControl gridControl)
        {
            Product_Report objForm = new Product_Report(gridControl, "تقرير كميات البنود");
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير كميات البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير كميات البنود");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير كميات البنود");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateStorageForm(List<DataRowView> rows, Storage storage)
        {
            UpdateCodeStorage objForm = new UpdateCodeStorage(rows, storage, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل  كمية بند");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل  كمية بند");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل  كمية بند");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }


      
        private void StoreMainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (SetUpdate.tipImage != null)
                {
                    SetUpdate.tipImage.Close();
                    SetUpdate.tipImage = null;
                }
                if (SetRecord.tipImage != null)
                {
                    SetRecord.tipImage.Close();
                    SetRecord.tipImage = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
