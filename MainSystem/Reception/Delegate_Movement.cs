using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace MainSystem
{
    public partial class Delegate_Movement : Form
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection2;
        MySqlConnection dbconnection3;
        MySqlConnection dbconnection4;
        MySqlConnection dbconnection5;
        MySqlDataReader dr;
        public static SoundPlayer snd;
        int EmpBranchId = 0;
        int delId = 0;
        string delName = "";

        bool flag = false;
        bool loaded = false;

        RepositoryItemButtonEdit repositoryItemButtonEdit0;
        RepositoryItemButtonEdit repositoryItemButtonEdit1_1;
        RepositoryItemButtonEdit repositoryItemButtonEdit2;
        RepositoryItemButtonEdit repositoryItemButtonEdit3;
        RepositoryItemButtonEdit repositoryItemButtonEdit4;

        //List<Delegate> lstDelegate = new List<Delegate>();

        public static WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

        Timer timer = new Timer();

        public Delegate_Movement()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            dbconnection4 = new MySqlConnection(connection.connectionString);
            dbconnection5 = new MySqlConnection(connection.connectionString);

            repositoryItemButtonEdit0 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit1_1 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit2 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit3 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit4 = new RepositoryItemButtonEdit();

            snd = new SoundPlayer();

            //Calculate the time of the actual work of the delegates
            timer.Interval = 1000 * 60;
            timer.Tick += new EventHandler(CalculateWorkingTime);
            timer.Start();

            //Registration of delegates latecomers absence
            var DailyTime = "11:00:00";
            var timeParts = DailyTime.Split(new char[1] { ':' });

            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day,
                       int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            TimeSpan ts;
            if (date > dateNow)
                ts = date - dateNow;
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }

            //waits certan time and run the code
            Task.Delay(ts).ContinueWith((x) => RecordAbsenceMethod());
        }

        private void DelegateAttend_Load(object sender, EventArgs e)
        {
            try
            {
                repositoryItemButtonEdit0.ContextImage = Properties.Resources.icons8_Delete_Shield_16;
                repositoryItemButtonEdit1_1.ContextImage = Properties.Resources.icons8_Protect_16;
                repositoryItemButtonEdit2.ContextImage = Properties.Resources.icons8_Do_Not_Disturb_16;
                repositoryItemButtonEdit3.ContextImage = Properties.Resources.icons8_Query_16;
                repositoryItemButtonEdit4.ContextImage = Properties.Resources.icons8_Warning_Shield_16;

                LoadGridData();
                loadStatus();

                //if(DateTime.Now.Hour >= 11 && DateTime.Now.Minute >= 0 && DateTime.Now.Second >= 0)
                //{
                //    flag = true;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                if (loaded)
                {
                    string query = "";
                    if (e.Action == CollectionChangeAction.Add)
                    {
                        if (Convert.ToInt16(gridView1.GetFocusedRowCellValue(colStatus)) == 4)
                        {
                            GridView view = sender as GridView;
                            view.SelectionChanged -= gridView1_SelectionChanged;
                            gridView1.UnselectRow(gridView1.FocusedRowHandle);
                            view.SelectionChanged += gridView1_SelectionChanged;
                            MessageBox.Show("عفوا لا يمكن تنفيذ هذا الطلب");
                        }
                        else if (!flag)
                        {
                            query = "insert into attendance(Delegate_ID,Name,Attendance_Date) values(" + gridView1.GetRowCellValue(e.ControllerRow, colDelegateID).ToString() + ",'" + gridView1.GetRowCellValue(e.ControllerRow, colDelegate).ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            gridView1.SetFocusedRowCellValue(colStatus, 1);
                            gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                            gridView1.SetFocusedRowCellValue(colAttend, DateTime.Now.ToString("HH:mm:ss"));
                            //repositoryItemButtonEdit1.Appearance.Image = Properties.Resources.icons8_Protect_16;
                            MySqlCommand command = new MySqlCommand(query, dbconnection);
                            dbconnection.Open();
                            command.ExecuteNonQuery();
                            gridView1.FocusedRowHandle += 1;
                            gridView1.FocusedRowHandle -= 1;
                        }
                        else
                        {
                            GridView view = sender as GridView;
                            view.SelectionChanged -= gridView1_SelectionChanged;
                            gridView1.UnselectRow(gridView1.GetSelectedRows()[0]);
                            view.SelectionChanged += gridView1_SelectionChanged;
                            //gridView1.SetFocusedRowCellValue(colStatus, 4);
                            MessageBox.Show("عفوا لقد مر الوقت المسموح به");
                        }
                    }
                    else if (e.Action == CollectionChangeAction.Remove)
                    {
                        query = "update attendance set Departure_Date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , Status='انصراف' where Delegate_ID=" + gridView1.GetRowCellValue(e.ControllerRow, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        gridView1.SetFocusedRowCellValue(colStatus, 4);
                        gridView1.SetFocusedRowCellValue(colDeparture, DateTime.Now.ToString("HH:mm:ss"));
                        gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                        //repositoryItemButtonEdit1.Appearance.Image = Properties.Resources.icons8_Warning_Shield_16;
                        MySqlCommand command = new MySqlCommand(query, dbconnection);
                        dbconnection.Open();
                        command.ExecuteNonQuery();

                        string qq1 = "select DATE_FORMAT(attendance.Departure_Date,'%Y-%m-%d') as 'التاريخ' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                        MySqlCommand comm1 = new MySqlCommand(qq1, dbconnection);
                        string DepartureDate = comm1.ExecuteScalar().ToString();

                        string qq2 = "select DATE_FORMAT(attendance.Attendance_Date,'%Y-%m-%d') as 'التاريخ' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                        MySqlCommand comm2 = new MySqlCommand(qq2, dbconnection);
                        string AttendanceDate = comm2.ExecuteScalar().ToString();

                        string qq3 = "";
                        if (DepartureDate == AttendanceDate)
                        {
                            string qq4 = "select SUBTIME(cast(attendance.Departure_Date as time),cast(attendance.Attendance_Date as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm4 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim1 = TimeSpan.Parse(comm4.ExecuteScalar().ToString());

                            qq3 = "update attendance set Attendance_Duration = '" + tim1 + "' where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                            MySqlCommand comm3 = new MySqlCommand(qq3, dbconnection);
                            comm3.ExecuteNonQuery();
                        }
                        else
                        {
                            string qq4 = "select SUBTIME(cast('23:59:59' as time),cast(attendance.Attendance_Date as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm4 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim1 = TimeSpan.Parse(comm4.ExecuteScalar().ToString());

                            qq4 = "select SUBTIME(cast(attendance.Departure_Date as time),cast('00:00:00' as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm5 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim2 = TimeSpan.Parse(comm5.ExecuteScalar().ToString());

                            qq3 = "update attendance set Attendance_Duration = '" + (tim1.Add(tim2)) + "' where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                            MySqlCommand comm3 = new MySqlCommand(qq3, dbconnection);
                            comm3.ExecuteNonQuery();
                        }

                        dbconnection.Close();

                        gridView1.SelectionChanged -= gridView1_SelectionChanged;
                        gridView1.UnselectRow(gridView1.FocusedRowHandle);
                        gridView1.SelectionChanged += gridView1_SelectionChanged;
                        gridView1.FocusedRowHandle += 1;
                        gridView1.FocusedRowHandle -= 1;

                        radNewBill.Visible = false;
                        radOldBill.Visible = false;
                        txtBill.Visible = false;
                        labBill.Visible = false;
                        txtBill.Text = "";
                        txtBill.Enabled = false;
                        btnStart.Visible = false;
                    }
                    dbconnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbconnection.Close();
            }
        }

        private void repositoryItemLookUpEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            gridView1.PostEditor();
        }

        private void repositoryItemLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.IsRowSelected(gridView1.FocusedRowHandle))
                {
                    LookUpEdit edit = sender as LookUpEdit;
                    if (Convert.ToInt32(edit.EditValue) == 2)
                    {
                        dbconnection.Open();
                        string qq = "update attendance set Status='مشغول' where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                        comm.ExecuteNonQuery();
                        dbconnection.Close();

                        radNewBill.Visible = true;
                        radOldBill.Visible = true;
                        radOldBill.Checked = true;
                        labBill.Visible = true;
                        txtBill.Visible = true;
                        txtBill.Text = "";
                        txtBill.Enabled = true;
                        btnStart.Visible = true;
                    }
                    else if (Convert.ToInt32(edit.EditValue) == 1)
                    {
                        dbconnection.Open();
                        string qq = "update attendance set Status='متاح' where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                        comm.ExecuteNonQuery();

                        qq = "delete from dash_delegate_bill where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString();
                        comm = new MySqlCommand(qq, dbconnection);
                        comm.ExecuteNonQuery();
                        dbconnection.Close();

                        radNewBill.Visible = false;
                        radOldBill.Visible = false;
                        txtBill.Visible = false;
                        labBill.Visible = false;
                        txtBill.Text = "";
                        txtBill.Enabled = false;
                        btnStart.Visible = false;
                    }
                    else if (Convert.ToInt32(edit.EditValue) == 3)
                    {
                        dbconnection.Open();
                        string qq = "update attendance set Status='استراحة' where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                        comm.ExecuteNonQuery();

                        qq = "delete from dash_delegate_bill where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString();
                        comm = new MySqlCommand(qq, dbconnection);
                        comm.ExecuteNonQuery();
                        dbconnection.Close();

                        radNewBill.Visible = false;
                        radOldBill.Visible = false;
                        txtBill.Visible = false;
                        labBill.Visible = false;
                        txtBill.Text = "";
                        txtBill.Enabled = false;
                        btnStart.Visible = false;
                    }
                    else if (Convert.ToInt32(edit.EditValue) == 4)
                    {
                        dbconnection.Open();
                        string qq = "update attendance set Departure_Date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , Status='انصراف' where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                        comm.ExecuteNonQuery();
                        gridView1.SetFocusedRowCellValue(colDeparture, DateTime.Now.ToString("HH:mm:ss"));

                        qq = "delete from dash_delegate_bill where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString();
                        comm = new MySqlCommand(qq, dbconnection);
                        comm.ExecuteNonQuery();

                        string qq1 = "select DATE_FORMAT(attendance.Departure_Date,'%Y-%m-%d') as 'التاريخ' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                        MySqlCommand comm1 = new MySqlCommand(qq1, dbconnection);
                        string DepartureDate = comm1.ExecuteScalar().ToString();

                        string qq2 = "select DATE_FORMAT(attendance.Attendance_Date,'%Y-%m-%d') as 'التاريخ' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                        MySqlCommand comm2 = new MySqlCommand(qq2, dbconnection);
                        string AttendanceDate = comm2.ExecuteScalar().ToString();

                        string qq3 = "";
                        if (DepartureDate == AttendanceDate)
                        {
                            string qq4 = "select SUBTIME(cast(attendance.Departure_Date as time),cast(attendance.Attendance_Date as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm4 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim1 = TimeSpan.Parse(comm4.ExecuteScalar().ToString());

                            qq3 = "update attendance set Attendance_Duration = '" + tim1 + "' where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL order by Attendance_ID desc limit 1";
                            MySqlCommand comm3 = new MySqlCommand(qq3, dbconnection);
                            comm3.ExecuteNonQuery();
                        }
                        else
                        {
                            string qq4 = "select SUBTIME(cast('23:59:59' as time),cast(attendance.Attendance_Date as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm4 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim1 = TimeSpan.Parse(comm4.ExecuteScalar().ToString());

                            qq4 = "select SUBTIME(cast(attendance.Departure_Date as time),cast('00:00:00' as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm5 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim2 = TimeSpan.Parse(comm5.ExecuteScalar().ToString());

                            qq3 = "update attendance set Attendance_Duration = '" + (tim1.Add(tim2)) + "' where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colDelegateID).ToString() + " and attendance.Absence_Date is NULL order by Attendance_ID desc limit 1";
                            MySqlCommand comm3 = new MySqlCommand(qq3, dbconnection);
                            comm3.ExecuteNonQuery();
                        }

                        dbconnection.Close();

                        gridView1.SelectionChanged -= gridView1_SelectionChanged;
                        gridView1.UnselectRow(gridView1.FocusedRowHandle);
                        gridView1.SelectionChanged += gridView1_SelectionChanged;

                        radNewBill.Visible = false;
                        radOldBill.Visible = false;
                        txtBill.Visible = false;
                        labBill.Visible = false;
                        txtBill.Text = "";
                        txtBill.Enabled = false;
                        btnStart.Visible = false;
                    }
                    gridView1.FocusedRowHandle += 1;
                    gridView1.FocusedRowHandle -= 1;
                }
                else
                {
                    gridView1.SetFocusedRowCellValue(colStatus, -1);
                    gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    //repositoryItemButtonEdit1.Appearance.Image = Properties.Resources.icons8_Delete_Shield_16;
                    gridView1.FocusedRowHandle += 1;
                    gridView1.FocusedRowHandle -= 1;
                    MessageBox.Show("هذا المندوب غير حاضر");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            try
            {
                Stream str = Properties.Resources.sound;
                snd = new SoundPlayer(str);
                snd.LoadAsync();
                snd.LoadCompleted += new AsyncCompletedEventHandler(snd_LoadCompleted);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                delId = Convert.ToInt16(gridView1.GetRowCellValue(e.RowHandle, colDelegateID).ToString());
                delName = gridView1.GetRowCellValue(e.RowHandle, colDelegate).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (loaded)
            {
                try
                {
                    if (gridView1.FocusedColumn.FieldName == "StatusID" && gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, colStatus).ToString() == "انصراف")
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void radNewBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                int id = 0;
                string query = "select Bill_Number from dash where Branch_ID=" + EmpBranchId + " order by Dash_ID desc limit 1";
                MySqlCommand cmd = new MySqlCommand(query, dbconnection);
                if (cmd.ExecuteScalar() != null)
                {
                    id = Convert.ToInt16(cmd.ExecuteScalar());
                }
                id = id + 1;
                //txtBill.Visible = true;
                //labBill.Visible = true;
                //btnConfirm.Visible = true;
                txtBill.Enabled = false;
                txtBill.Text = id.ToString();
                //txtBill.TextAlign = ContentAlignment.MiddleCenter;

                query = "insert into dash (Delegate_ID,Delegate_Name,Bill_Number,Branch_ID,Bill_Date) values (@Delegate_ID,@Delegate_Name,@Bill_Number,@Branch_ID,@Bill_Date)";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16).Value = delId;
                com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar).Value = delName;
                com.Parameters.Add("@Bill_Number", MySqlDbType.Int16).Value = id;
                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16).Value = EmpBranchId;
                com.Parameters.Add("@Bill_Date", MySqlDbType.DateTime).Value = DateTime.Now;
                com.ExecuteNonQuery();

                string q = "select Dash_ID from dash where Branch_ID=" + EmpBranchId + " and Bill_Number=" + id + " order by Dash_ID desc limit 1";
                MySqlCommand command = new MySqlCommand(q, dbconnection);
                int dashId = Convert.ToInt16(command.ExecuteScalar().ToString());

                UserControl.ItemRecord("dash", "اضافة", dashId, DateTime.Now, null, dbconnection);
                dbconnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radOldBill_Click(object sender, EventArgs e)
        {
            try
            {
                //labBill.Visible = false;
                //txtBill.Visible = false;
                txtBill.Text = "";
                txtBill.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBill.Text != "")
                {
                    int billNum = -1;
                    if (int.TryParse(txtBill.Text, out billNum))
                    {
                        //string query = "SELECT dash.Bill_Time FROM dash where dash.Branch_ID=" + EmpBranchId + "  and dash.Bill_Number=" + billNum;
                        //MySqlCommand com = new MySqlCommand(query, dbconnection);
                        //double BillTime = Convert.ToInt16(com.ExecuteScalar().ToString());

                        string query = "insert into dash_delegate_bill(Delegate_ID,Delegate_Name,Bill_Number,Branch_ID) values(" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + ",'" + gridView1.GetFocusedRowCellValue(colDelegate).ToString() + "'," + billNum + "," + EmpBranchId + ")";
                        MySqlCommand command = new MySqlCommand(query, dbconnection);
                        dbconnection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("تم");
                    }
                    else
                    {
                        MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال رقم الفاتورة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (loaded)
                {
                    if (gridView1.GetFocusedRowCellValue(colStatus).ToString() == "2")
                    {
                        radNewBill.Visible = true;
                        radOldBill.Visible = true;
                        radOldBill.Checked = true;
                        txtBill.Visible = true;
                        labBill.Visible = true;
                        txtBill.Text = "";
                        txtBill.Enabled = true;
                        btnStart.Visible = true;
                    }
                    else
                    {
                        radNewBill.Visible = false;
                        radOldBill.Visible = false;
                        radOldBill.Checked = false;
                        radNewBill.Checked = false;
                        txtBill.Visible = false;
                        labBill.Visible = false;
                        txtBill.Text = "";
                        txtBill.Enabled = false;
                        btnStart.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            try
            {
                GridView gridView = sender as GridView;
                if (e.Column != gridView.Columns[3])
                    return;

                int status = Convert.ToInt16(gridView.GetRowCellValue(e.RowHandle, colStatus).ToString());
                if (status == 1)
                {
                    e.RepositoryItem = repositoryItemButtonEdit1_1;
                }
                else if (status == 2)
                {
                    e.RepositoryItem = repositoryItemButtonEdit2;
                }
                else if (status == 3)
                {
                    e.RepositoryItem = repositoryItemButtonEdit3;
                }
                else if (status == 4)
                {
                    e.RepositoryItem = repositoryItemButtonEdit4;
                }
                if (status == -1)
                {
                    e.RepositoryItem = repositoryItemButtonEdit0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                loadStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection5.Close();
        }

        //functions
        void snd_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (snd.IsLoadCompleted)
            {
                snd.PlaySync();
                snd.Stop();
                snd.Stream.Close();
                snd.Stream.Dispose();
                //snd = new SoundPlayer();
            }
        }

        private void LoadGridData()
        {
            EmpBranchId = UserControl.UserBranch(dbconnection);
            //and delegate.Error=0
            dbconnection.Open();
            MySqlCommand adapter = new MySqlCommand("SELECT delegate.Delegate_ID,delegate.Delegate_Name FROM delegate where delegate.Branch_ID=" + EmpBranchId + "", dbconnection);
            dr = adapter.ExecuteReader();

            BindingList<GridData> lista = new BindingList<GridData>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lista.Add(new GridData() { DelegateId = Convert.ToInt16(dr["Delegate_ID"].ToString()), DelegateName = dr["Delegate_Name"].ToString(), StatusID = "-1" });
                }
                dr.Close();
            }
            gridControl1.DataSource = lista;
            dbconnection.Close();

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            List<ComboData> lista_combo = new List<ComboData>();
            lista_combo.Add(new ComboData() { StatusId = 1, Status = "متاح" });
            lista_combo.Add(new ComboData() { StatusId = 2, Status = "مشغول" });
            lista_combo.Add(new ComboData() { StatusId = 3, Status = "استراحة" });
            lista_combo.Add(new ComboData() { StatusId = 4, Status = "انصراف" });

            repositoryItemLookUpEdit1.DataSource = lista_combo;
            repositoryItemLookUpEdit1.DisplayMember = "Status";
            repositoryItemLookUpEdit1.ValueMember = "StatusId";

            repositoryItemLookUpEdit1.Columns.Clear();
            repositoryItemLookUpEdit1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo() { Caption = "الحالة", FieldName = "Status" });
            repositoryItemLookUpEdit1.EditValueChanged += repositoryItemLookUpEdit1_EditValueChanged;
            //repositoryItemLookUpEdit1.Closed += repositoryItemLookUpEdit1_Closed;
            
            gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
        }

        void loadStatus()
        {
            dbconnection.Open();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                TimeSpan dt = new TimeSpan();
                TimeSpan atime = new TimeSpan();
                string query = "SELECT attendance.Status FROM attendance where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " and date(attendance.Attendance_Date)='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'";
                MySqlCommand command = new MySqlCommand(query, dbconnection);
                if (command.ExecuteScalar() != null)
                {
                    string status = command.ExecuteScalar().ToString();
                    if (status == "متاح")
                    {
                        gridView1.SelectRow(i);
                        gridView1.SetRowCellValue(i, colStatus, 1);
                        gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    }
                    else if (status == "مشغول")
                    {
                        gridView1.SelectRow(i);
                        gridView1.SetRowCellValue(i, colStatus, 2);
                        gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    }
                    else if (status == "استراحة")
                    {
                        gridView1.SelectRow(i);
                        gridView1.SetRowCellValue(i, colStatus, 3);
                        gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    }
                    else if (status == "انصراف")
                    {
                        gridView1.UnselectRow(i);
                        gridView1.SetRowCellValue(i, colStatus, 4);
                        gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    }

                    dbconnection5.Open();
                    MySqlCommand adapter2 = new MySqlCommand("SELECT cast(Attendance_Date as time) as 'Attendance_Time',cast(Departure_Date as time) as 'Departure_Time' FROM attendance where attendance.Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " and DATE_FORMAT(attendance.Attendance_Date,'%Y-%m-%d') ='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'", dbconnection5);
                    MySqlDataReader dr2 = adapter2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            if (TimeSpan.TryParse(dr2["Departure_Time"].ToString(), out dt))
                            { }

                            gridView1.SetRowCellValue(i, colAttend, TimeSpan.Parse(dr2["Attendance_Time"].ToString()));
                            gridView1.SetRowCellValue(i, colDeparture, dt);
                            //lista.Add(new GridData() { DelegateId = Convert.ToInt16(dr["Delegate_ID"].ToString()), DelegateName = dr["Delegate_Name"].ToString(), StatusID = "-1", AttendId = TimeSpan.Parse(dr2["Attendance_Time"].ToString()), DepartureId = dt });
                        }
                        dr2.Close();
                    }
                    dbconnection5.Close();
                }
                else
                {
                    gridView1.UnselectRow(i);
                    gridView1.SetRowCellValue(i, colStatus, -1);
                    gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    gridView1.SetRowCellValue(i, colAttend, atime);
                    gridView1.SetRowCellValue(i, colDeparture, dt);
                }
            }
            dbconnection.Close();
            loaded = true;
        }

        public void CalculateWorkingTime(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.GetRowCellDisplayText(i, colStatus).ToString() == "مشغول")
                    {
                        string query = "SELECT distinct Busy_Duration FROM attendance where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        MySqlCommand command = new MySqlCommand(query, dbconnection2);
                        dbconnection2.Open();
                        double reader = Convert.ToDouble(command.ExecuteScalar());

                        query = "update Attendance set Busy_Duration=" + (reader + 1).ToString() + " where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        command = new MySqlCommand(query, dbconnection2);
                        command.ExecuteNonQuery();

                        dbconnection3.Open();
                        query = "SELECT Bill_Number FROM dash_delegate_bill where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString();
                        MySqlCommand adapter = new MySqlCommand(query, dbconnection3);
                        MySqlDataReader dr1 = adapter.ExecuteReader();
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                string q = "SELECT Bill_Time FROM dash where Branch_ID=" + EmpBranchId + " and Bill_Number=" + dr1["Bill_Number"].ToString();
                                MySqlCommand c = new MySqlCommand(q, dbconnection2);
                                double BillTime = Convert.ToDouble(c.ExecuteScalar().ToString());

                                BillTime = BillTime + 1;

                                //update time in dash
                                q = "update dash set Bill_Time = " + BillTime + "  where Branch_ID=" + EmpBranchId + " and Bill_Number=" + dr1["Bill_Number"].ToString();
                                c = new MySqlCommand(q, dbconnection2);
                                c.ExecuteNonQuery();
                            }
                            dr1.Close();
                        }
                        dbconnection2.Close();
                        dbconnection3.Close();
                    }

                    else if (gridView1.GetRowCellDisplayText(i, colStatus).ToString() == "متاح")
                    {
                        string query = "SELECT distinct Available_Duration FROM attendance where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        MySqlCommand command = new MySqlCommand(query, dbconnection2);
                        dbconnection2.Open();
                        double reader = Convert.ToDouble(command.ExecuteScalar());

                        query = "update Attendance set Available_Duration=" + (reader + 1).ToString() + " where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        command = new MySqlCommand(query, dbconnection2);
                        command.ExecuteNonQuery();
                        dbconnection2.Close();
                    }

                    else if (gridView1.GetRowCellDisplayText(i, colStatus).ToString() == "استراحة")
                    {
                        string query = "SELECT distinct Break_Duration FROM attendance where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        MySqlCommand command = new MySqlCommand(query, dbconnection2);
                        dbconnection2.Open();
                        double reader = Convert.ToDouble(command.ExecuteScalar());

                        query = "update Attendance set Break_Duration=" + (reader + 1).ToString() + " where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                        command = new MySqlCommand(query, dbconnection2);
                        command.ExecuteNonQuery();
                        dbconnection2.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection2.Close();
            dbconnection3.Close();
        }

        void RecordAbsenceMethod()
        {
            try
            {
                flag = true;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.IsRowSelected(i))
                    {
                        dbconnection4.Open();
                        string query = "insert into attendance(Delegate_ID,Name,Absence_Date) values(" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + ",'" + gridView1.GetRowCellValue(i, colDelegate).ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        MySqlCommand command = new MySqlCommand(query, dbconnection4);
                        command.ExecuteNonQuery();
                        dbconnection4.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbconnection4.Close();
            }
        }
    }

    public class ComboData
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
    }

    public class GridData
    {
        public int DelegateId { get; set; }
        public string DelegateName { get; set; }
        public string StatusID { get; set; }
        public TimeSpan AttendId { get; set; }
        public TimeSpan DepartureId { get; set; }
    }
}
