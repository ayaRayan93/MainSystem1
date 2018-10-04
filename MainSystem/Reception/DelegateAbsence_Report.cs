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
    public partial class DelegateAbsence_Report : Form
    {
        MySqlConnection conn;
        bool loaded = false;
        public DelegateAbsence_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);

            comDelegate.AutoCompleteMode = AutoCompleteMode.Suggest;
            comDelegate.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void DelegateAbsence_Report_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from delegate where Branch_ID=" + UserControl.UserBranch(conn) + " and Error = 0";
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";
                conn.Close();
                loaded = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
                if (selRows.Length > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    Form prompt = new Form()
                    {
                        Width = 500,
                        Height = 220,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "",
                        StartPosition = FormStartPosition.CenterScreen,
                        MaximizeBox = false,
                        MinimizeBox = false
                    };
                    Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب الحذف؟" };
                    TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 385, Multiline = true, Height = 80, RightToLeft = RightToLeft };
                    Button confirmation = new Button() { Text = "تأكيد", Left = 200, Width = 100, Top = 140, DialogResult = DialogResult.OK };
                    prompt.Controls.Add(textBox);
                    prompt.Controls.Add(confirmation);
                    prompt.Controls.Add(textLabel);
                    prompt.AcceptButton = confirmation;
                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        if (textBox.Text != "")
                        {
                            conn.Open();
                            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                            {
                                DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[i]));
                                string Query = "update attendance set Error=1 where Attendance_ID=" + Convert.ToInt16(selRow[0].ToString());
                                MySqlCommand MyCommand = new MySqlCommand(Query, conn);
                                MyCommand.ExecuteNonQuery();

                                UserControl.ItemRecord("attendance", "حذف", Convert.ToInt16(selRow[0].ToString()), DateTime.Now, textBox.Text, conn);
                            }
                            search();
                        }
                        else
                        {
                            MessageBox.Show("يجب كتابة السبب");
                        }
                    }
                    else
                    { }
                }
                else
                {
                    MessageBox.Show("يجب ان تختار عنصر للحذف");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        public void search()
        {
            try
            {
                string query = "";
                if (comDelegate.Text != "")
                {
                    query = "select Attendance_ID as 'التسلسل', Name as 'المندوب', Absence_Date as 'تاريخ الغياب' from attendance where DATE_FORMAT(Absence_Date,'%Y-%m-%d') between '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and Delegate_ID=" + comDelegate.SelectedValue.ToString() + " and Absence_Date is not null and Error=0";
                }
                else
                {
                    query = "select Attendance_ID as 'التسلسل', Name as 'المندوب', Absence_Date as 'تاريخ الغياب' from attendance INNER JOIN delegate ON attendance.Delegate_ID = delegate.Delegate_ID where DATE_FORMAT(Absence_Date,'%Y-%m-%d') between '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and delegate.Branch_ID=" + UserControl.UserBranch(conn) + " and Absence_Date is not null and attendance.Error=0";
                }

                conn.Open();
                MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
                DataSet dset = new DataSet();
                adpt.Fill(dset);
                gridControl1.DataSource = dset.Tables[0];
                gridView1.Columns[0].Visible = false;

                txtSum.Visible = true;
                label4.Visible = true;
                txtSum.Text = "";
                txtSum.Text = (gridView1.RowCount).ToString();
                //btnDelete.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        public void search2()
        {
            try
            {
                conn.Open();

                string query = "select Attendance_ID as 'التسلسل', Name as 'المندوب', Absence_Date as 'تاريخ الغياب' from attendance where Delegate_ID=" + comDelegate.SelectedValue.ToString() + " and Absence_Date is not null and Error=0";

                MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
                DataSet dset = new DataSet();
                adpt.Fill(dset);
                gridControl1.DataSource = dset.Tables[0];
                gridView1.Columns[0].Visible = false;

                txtSum.Visible = true;
                label4.Visible = true;
                txtSum.Text = "";
                txtSum.Text = (gridView1.RowCount).ToString();
                //btnDelete.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void comDelegate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                search2();
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlPointSale.TabPages.Count; i++)
                if (MainForm.tabControlPointSale.TabPages[i].Name == text)
                {
                    return MainForm.tabControlPointSale.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }
    }
}
