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
using MySql.Data.MySqlClient;

namespace MainSystem
{
    public partial class ItemDetails : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        string code = "";
        string type = "";

        public ItemDetails(string Code, string Type)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            code = Code;
            type = Type;
        }

        private void ItemDetails_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                DataTable dt = null;
                if (type == "طقم")
                {
                    //AND size.Factory_ID = factory.Factory_ID AND groupo.Factory_ID = factory.Factory_ID  AND product.Group_ID = groupo.Group_ID  AND factory.Type_ID = type.Type_ID AND color.Type_ID = type.Type_ID
                    query = "SELECT set_details.Set_ID as 'التسلسل',data.Code as 'الكود',product.Product_Name as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',data.Description as 'البيان',set_details.Quantity as 'العدد',sellprice.Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM set_details INNER JOIN data ON set_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Set_ID = set_details.Set_ID where set_details.Set_ID=" + code + " and sellprice.Price_Type='لستة' group by set_details.Set_ID";
                    MySqlDataAdapter da1 = new MySqlDataAdapter(query, dbconnection);
                    dt = new DataTable();
                    da1.Fill(dt);

                    query = "SELECT set_details.Set_ID as 'التسلسل',data.Code as 'الكود',product.Product_Name as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',data.Description as 'البيان',set_details.Quantity as 'العدد',sellprice.Sell_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM set_details INNER JOIN data ON set_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Set_ID = set_details.Set_ID where set_details.Set_ID=" + code + " and sellprice.Price_Type='قطعى' group by set_details.Set_ID";
                    MySqlDataAdapter da2 = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    dt.Merge(dt2);
                }
                else if(type == "عرض")
                {
                    query = "SELECT offer_details.Offer_ID as 'التسلسل',data.Code as 'الكود',product.Product_Name as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',data.Description as 'البيان',offer_details.Quantity as 'العدد' FROM offer_details INNER JOIN data ON offer_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  where offer_details.Offer_ID=" + code;
                    MySqlDataAdapter da1 = new MySqlDataAdapter(query, dbconnection);
                    dt = new DataTable();
                    da1.Fill(dt);
                }
                
                gridControl1.DataSource = dt;

                gridView1.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridView1.Columns[1].Width = 200;
                gridView1.Columns[0].Visible = false;
                for (int i = 2; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}