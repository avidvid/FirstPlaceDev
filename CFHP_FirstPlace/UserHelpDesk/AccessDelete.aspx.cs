using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace.UserHelpDesk
{
    public partial class AccessDelete : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || (Session["UserType"].ToString() != "help" && Session["UserType"].ToString() != "admin"))
                Response.Redirect("~/NoAccess.aspx");
            if (Request.QueryString["UserID"] != null)
                DeleteAccess();
        }
        public void DeleteAccess()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_Report_Access_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
                if (Request.QueryString["ReportID"] != null)
                    cmd.Parameters.AddWithValue("@ReportID", Convert.ToInt32(Request.QueryString["ReportID"].ToString()));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserHelpDesk/AccessInfo.aspx?ok=1&UserID=" + Request.QueryString["UserID"].ToString());
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}