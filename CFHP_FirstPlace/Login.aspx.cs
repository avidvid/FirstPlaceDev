using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

//Because of company policy me and Juan Flores Decided to use unsecure login for this internal website 11/25/2014

namespace CFHP_FirstPlace
{
    public partial class Login : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connStr);
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox tboxU = (TextBox)this.Login1.FindControl("UserName");
            TextBox tboxP = (TextBox)this.Login1.FindControl("Password");
            if (tboxU != null)
            {
                if (IsPostBack)
                    tboxP.Focus();
                else
                    tboxU.Focus();
            } 
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_UserLoginValidate", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", Login1.UserName.ToString());
                cmd.Parameters.AddWithValue("@password", Login1.Password.ToString());
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //Add security here 
                    Session["username"] = dr["UserName"].ToString().ToLower().Trim();
                    Session["firstname"] = dr["firstname"].ToString().ToLower().Trim();
                    Session["lastname"] = dr["lastname"].ToString().ToLower().Trim();
                    Session["userid"] = dr["userid"].ToString().Trim();
                    Session["PK_User"] = dr["PK_User"].ToString().Trim();
                    Session["UserType"] = dr["UserType"].ToString().Trim(); 
                    Session["DepartmentId"] = dr["DepartmentId"].ToString().Trim();
                    Session["LoggedOn"] = (bool)Convert.ToBoolean(dr["available"].ToString().Trim());
                    if(Convert.ToInt32(dr["PassDiff"].ToString())>61)
                        Response.Redirect("~/UserClient/ChangePassword.aspx");
                    if (Request.Cookies["UrlSettings"] != null)
                    {
                        string test =
                            Server.HtmlEncode(Request.Cookies["UrlSettings"]["Url"]);
                        Response.Redirect(Server.HtmlEncode(Request.Cookies["UrlSettings"]["Url"]));
                    }
                    Response.Redirect("~/UserClient/MyProfile.aspx");
                }
                dr.Dispose();
                connection.Close();
            }
            catch (System.Exception ex)
            {
                Response.Write(ex.Message);
                Response.Redirect("~/UserClient/MyProfile.aspx");
            }
        }
    }
}