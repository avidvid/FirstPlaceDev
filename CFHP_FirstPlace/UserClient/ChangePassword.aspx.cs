using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections;

namespace CFHP_FirstPlace.UserClient
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelOk.Visible = false;
            LabelError.Visible = false;
            if (Request.QueryString["Error"] != null)
            {

                switch (Request.QueryString["Error"] )
                {
                    case "2":// Same Pass
                        LabelError.Text = "Current password doesn't match your old password";
                        break;
                    default :// Wrong pass
                        LabelError.Text =  "Password change failed. Please re-enter your correct current password and try again.";
                        break;
                }
                LabelError.Visible = true;
            }
            if (Request.QueryString["Ok"] != null)
            {
                LabelOk.Text = "Your password has been changed!";
                LabelOk.Visible = true;
                //If forced to change password
                //if (Request.Cookies["UrlSettings"] != null)
                //{
                //    string test =
                //        Server.HtmlEncode(Request.Cookies["UrlSettings"]["Url"]);
                //    Response.Redirect(Server.HtmlEncode(Request.Cookies["UrlSettings"]["Url"]));
                //}
            }
        }
        protected void ChangePassword1_ChangingPassword(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
        {
            if (ChangePassword1.NewPassword == ChangePassword1.CurrentPassword)
                Response.Redirect("~/UserClient/ChangePassword.aspx?Error=1");
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.First_Password_Update", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@password", ChangePassword1.NewPassword.ToString());
                    cmd.Parameters.AddWithValue("@OldPassword", ChangePassword1.CurrentPassword.ToString());
                    SqlParameter param = new SqlParameter("@ReturnVal", SqlDbType.Int);
                    param.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(param);
                    int returnvalue = 0;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //Get the return value
                    int.TryParse(cmd.Parameters["@ReturnVal"].Value.ToString(), out returnvalue);
                    con.Close();
                    switch (returnvalue)
                    {
                        case 1:// correct
                            Response.Redirect("~/UserClient/ChangePassword.aspx?Ok=1");
                            break;
                        case 2:// Same Pass
                            Response.Redirect("~/UserClient/ChangePassword.aspx?Error=2");
                            break;
                        case 3:// Wrong pass
                            Response.Redirect("~/UserClient/ChangePassword.aspx?Error=3");
                            break;
                    }
                    Response.Redirect("~/UserClient/ChangePassword.aspx?Error=3");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

    }
}