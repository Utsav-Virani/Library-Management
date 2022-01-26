using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Library_Manage_database
{
    class dboper
    {
        public string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\study\C#\Library_Manage_database\LibraryManage.mdf;Integrated Security=True";
        public void insert(string q)
        {
            SqlConnection con = new SqlConnection(s);
            SqlCommand cmd = new SqlCommand(q, con);
            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                con.Close();
            }
        }
        public void fetch(string q,ref DataTable dt)
        {
            SqlDataAdapter da = new SqlDataAdapter(q, s);
            da.Fill(dt);
        }
    }
}       
