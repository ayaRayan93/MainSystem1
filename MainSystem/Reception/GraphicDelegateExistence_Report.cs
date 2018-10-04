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
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraCharts;
using MySql.Data.MySqlClient;
using DevExpress.XtraCharts.Printing;

namespace MainSystem
{
    public partial class GraphicDelegateExistence_Report : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        Series series = new Series("Series1", ViewType.Bar);
        bool loaded = false;

        public GraphicDelegateExistence_Report()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void GraphicDelegate_Report_Load(object sender, EventArgs e)
        {
            try
            {
                /*DataTable dt = CreateChartData();*/
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    series.ValueScaleType = ScaleType.Numerical;
                //    series.Points.Add(new SeriesPoint(dt.Rows[i]["المندوب"],dt.Rows[i]["عدد الساعات"], dt.Rows[i]["المدة"]));
                //}
                /*series.DataSource = dt;
                chartControl1.Series.Add(series);*/
                series.Label.TextPattern = "{A} : {V}";

                //chartControl1.SeriesDataMember = "المدة";
                series.ArgumentDataMember = "المندوب";
                series.ValueDataMembers.AddRange(new string[] { "عدد الساعات" });
                //chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                //((XYDiagram)chartControl1.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*private DataTable CreateChartData()
        {
            string query = "select Name as 'المندوب',SEC_TO_TIME(SUM(time_to_sec(attendance.Attendance_Duration))) as 'المدة', Cast(DATE_FORMAT(Cast(SEC_TO_TIME(SUM(time_to_sec(attendance.Attendance_Duration))) as datetime), '%H') as unsigned) as 'عدد الساعات' from attendance where Delegate_ID is not null and attendance.Absence_Date is NULL and Error=0 group by Delegate_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                chartControl1.OptionsPrint.SizeMode = PrintSizeMode.Stretch;
                chartControl1.OptionsPrint.ImageFormat = DevExpress.XtraCharts.Printing.PrintImageFormat.Bitmap;
                chartControl1.ShowRibbonPrintPreview();
                //if (!chartControl1.IsPrintingAvailable)
                //{
                //    chartControl1.Print();
                //}
                //chartControl1.ShowPrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select Name as 'المندوب',SEC_TO_TIME(SUM(time_to_sec(attendance.Attendance_Duration))) as 'المدة', Cast(DATE_FORMAT(Cast(SEC_TO_TIME(SUM(time_to_sec(attendance.Attendance_Duration))) as datetime), '%H') as unsigned) as 'عدد الساعات' from attendance where Delegate_ID is not null and attendance.Absence_Date is NULL and Error=0 and DATE_FORMAT(Attendance_Date,'%Y-%m-%d') between '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' group by Delegate_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                series.DataSource = dt;
                chartControl1.Series.Clear();
                chartControl1.Series.Add(series);

                if(!loaded)
                {
                    ChartTitle chartTitle1 = new ChartTitle();
                    chartTitle1.Text = "تقرير المناديب الاكثر عمل";
                    chartTitle1.TextColor = Color.DarkBlue;
                    chartTitle1.Alignment = StringAlignment.Center;
                    chartControl1.Titles.Add(chartTitle1);
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}