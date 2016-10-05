using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GameOnAPI
{
    public partial class login : System.Web.UI.Page
    {
        WebService1 wsGameAPI = new WebService1();
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            string userid = Request.QueryString["username"];
            string password = Request.QueryString["password"];
            switch (type)
            {
                case "login" :
                    string username = wsGameAPI.Login(userid, password);
                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        string json = Helper.GenerateJSON(wsGameAPI.GetUserByUserName(username));
                        Response.Write(json);
                    }
                    break;
                case "register" :
                    string useremail = Request.QueryString["email"];
                    string userphone = Request.QueryString["phone"];
                    // master commit test
                    break;
            }
        }
    }
}