using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace MainSystem
{
    class UserControl
    {
        public static int userID;
        public static string userName;
        public static int userType;

        public static void ItemRecord(string tableName, string status, int recordID, DateTime date, string reason, MySqlConnection conn)
        {
            string query = "insert into //UserControl (//UserControl_UserID,//UserControl_TableName,//UserControl_Status,//UserControl_RecordID,//UserControl_Date,//UserControl_Reason)values (@//UserControl_UserID,@//UserControl_TableName,@//UserControl_Status,@//UserControl_RecordID,@//UserControl_Date,@//UserControl_Reason)";
            MySqlCommand com = new MySqlCommand(query, conn);
            com.Parameters.Add("@//UserControl_UserID", MySqlDbType.Int16);
            com.Parameters["@//UserControl_UserID"].Value = userID;
            com.Parameters.Add("@//UserControl_TableName", MySqlDbType.VarChar, 255);
            com.Parameters["@//UserControl_TableName"].Value = tableName;
            com.Parameters.Add("@//UserControl_Status", MySqlDbType.VarChar, 255);
            com.Parameters["@//UserControl_Status"].Value = status;
            com.Parameters.Add("@//UserControl_RecordID", MySqlDbType.Int16);
            com.Parameters["@//UserControl_RecordID"].Value = recordID;
            com.Parameters.Add("@//UserControl_Date", MySqlDbType.DateTime);
            com.Parameters["@//UserControl_Date"].Value = date;
            com.Parameters.Add("@//UserControl_Reason", MySqlDbType.VarChar, 255);
            com.Parameters["@//UserControl_Reason"].Value = reason;
            com.ExecuteNonQuery();
        }

        public static int UserBranch(MySqlConnection conn)
        {
            int EmpBranchID = 0;
            if (userType != 0)
            {
                string query = "SELECT employee.Branch_ID FROM employee INNER JOIN users ON users.Employee_ID = employee.Employee_ID where users.User_ID=" + userID;
                MySqlCommand com = new MySqlCommand(query, conn);
                conn.Open();
                EmpBranchID = Convert.ToInt16(com.ExecuteScalar().ToString());
                conn.Close();
            }
            else
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                EmpBranchID = Convert.ToInt16(System.IO.File.ReadAllText(path));
            }

            return EmpBranchID;
        }

        public static int DelegateBranch(MySqlConnection conn)
        {
            int DelegateBranchID = 0;
            if (userType != 0)
            {
                string query = "SELECT delegate.Branch_ID FROM delegate INNER JOIN users ON users.Employee_ID = delegate.Delegate_ID where users.User_ID=" + userID;
                MySqlCommand com = new MySqlCommand(query, conn);
                conn.Open();
                DelegateBranchID = Convert.ToInt16(com.ExecuteScalar().ToString());
                conn.Close();
            }
            else
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                DelegateBranchID = Convert.ToInt16(System.IO.File.ReadAllText(path));
            }

            return DelegateBranchID;
        }

        public static int LoginDelegate(MySqlConnection conn)
        {
            conn.Open();
            string query = "SELECT users.Employee_ID FROM users INNER JOIN delegate ON users.Employee_ID = delegate.Delegate_ID where users.User_ID=" + userID;
            MySqlCommand com = new MySqlCommand(query, conn);
            int LoginDelegateID = Convert.ToInt16(com.ExecuteScalar().ToString());
            conn.Close();
            return LoginDelegateID;
        }
    }
}
