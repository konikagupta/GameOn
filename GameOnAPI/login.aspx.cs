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
                        Response.Write(Helper.GenerateJSON(wsGameAPI.GetUserByUserName(username)));
                    }
                    break;
                case "register" :
                    string useremail = Request.QueryString["email"];
                    string userphone = Request.QueryString["phone"];
                    break;
            }
        }

        private void GenerateJSON(object obj)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(User));
            ser.WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            Response.Write(sr.ReadToEnd());
        }
    }
}