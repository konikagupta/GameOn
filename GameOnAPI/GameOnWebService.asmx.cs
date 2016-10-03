using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace GameOnAPI
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private string strConnString = ConfigurationManager.ConnectionStrings["GameOnDBConString"].ConnectionString;
            

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int add(int a, int b)
        {
            return a + b;
        }

        [WebMethod]
        public string Login(string username, string password)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand("sp_user_find_by_username_password", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_username", username);
            cmd.Parameters.AddWithValue("@user_password", password);
            DataTable dt = GetData(cmd);
            string loggedInUser = "";
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    loggedInUser = dr["UserId"].ToString();
                }
            }
            return loggedInUser;
       }

        [WebMethod]
        public User GetUserByUserName(string username)
        {
            User objUser = new User();
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand("sp_user_get_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", username);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    objUser.UserID = dr["UserId"].ToString();
                    objUser.UserName = dr["UserName"].ToString();
                    objUser.UserPassword = dr["UserPassword"].ToString();
                    objUser.UserEmail = dr["UserEmail"].ToString();
                    objUser.UserPhone = dr["UserPhone"].ToString();
                }
            }
            return objUser;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(strConnString);
            return con;
        }

        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            try
            {
                cmd.Connection = con;
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
    }
}
