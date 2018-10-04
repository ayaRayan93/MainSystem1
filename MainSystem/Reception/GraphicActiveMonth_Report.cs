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
    public partial class GraphicActiveMonth_Report : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        Series series = new Series("Series1", ViewType.Pie);
        bool loaded = false;

        public GraphicActiveMonth_Report()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Graphic_Report_Load(object sender, EventArgs e)
        {
            try
            {
                //series.DataSource = CreateChartData();
                series.Label.TextPattern = "{A} : {V}";
                //chartControl1.Series.Add(series);

                series.ArgumentDataMember = "التاريخ";
                series.ValueDataMembers.AddRange(new string[] { "المدة" });

                // Adjust the position of series labels. 
                ((PieSeriesLabel)series.Label).Position = PieSeriesLabelPosition.TwoColumns;

                // Detect overlapping of series labels.
                ((PieSeriesLabel)series.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                //ChartTitle chartTitle1 = new ChartTitle();
                //chartTitle1.Text = "تقرير الاشهر الاكثر عمل";
                //chartTitle1.TextColor = Color.DarkBlue;
                //chartTitle1.Alignment = StringAlignment.Center;
                //chartControl1.Titles.Add(chartTitle1);
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
            string query = "SELECT DATE_FORMAT(attendance.Attendance_Date, '%Y-%m') as 'التاريخ', sum(attendance.Busy_Duration)/60 as 'المدة' FROM attendance where attendance.Absence_Date is NULL and Error=0 GROUP BY DATE_FORMAT(attendance.Attendance_Date, '%Y-%m') ORDER BY attendance.Attendance_Date Asc";
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
                string query = "SELECT DATE_FORMAT(attendance.Attendance_Date, '%Y-%m') as 'التاريخ', sum(attendance.Busy_Duration)/60 as 'المدة' FROM attendance where attendance.Absence_Date is NULL and Error=0 and DATE_FORMAT(Attendance_Date,'%Y-%m') between '" + this.dateTimePicker1.Value.ToString("yyyy-MM") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM") + "' GROUP BY DATE_FORMAT(attendance.Attendance_Date, '%Y-%m') ORDER BY attendance.Attendance_Date Asc";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                series.DataSource = dt;
                chartControl1.Series.Clear();
                chartControl1.Series.Add(series);

                if (!loaded)
                {
                    ChartTitle chartTitle1 = new ChartTitle();
                    chartTitle1.Text = "تقرير الاشهر الاكثر عمل";
                    chartTitle1.TextColor = Color.DarkBlue;
                    chartTitle1.Alignment = StringAlignment.Center;
                    chartControl1.Titles.Add(chartTitle1);
                    loaded = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}