using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace.UserHelpDesk
{
    public partial class AccessCopy : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || (Session["UserType"].ToString() != "help" && Session["UserType"].ToString() != "admin"))
                Response.Redirect("~/NoAccess.aspx");
            if (Request.QueryString["UserID"] != null && Request.QueryString["UserMirrorID"] != null)
                CopyAccess();
            if (Request.QueryString["UserID"] != null && Request.QueryString["ReportID"] != null)
                AddAccess();
        }

        public void CopyAccess()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_Report_Access_Copy", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NewEmployee", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
                cmd.Parameters.AddWithValue("@EmployeetoMirror", Convert.ToInt32(Request.QueryString["UserMirrorID"].ToString()));
                con.Open();
                int Count = cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserHelpDesk/AccessInfo.aspx?ok=" + Count + "&UserID=" + Request.QueryString["UserID"].ToString());
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
        public void AddAccess()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_Report_Access_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
                cmd.Parameters.AddWithValue("@ReportID", Convert.ToInt32(Request.QueryString["ReportID"].ToString()));
                con.Open();
                int Count = cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserHelpDesk/AccessInfo.aspx?ok=" + Count + "&UserID=" + Request.QueryString["UserID"].ToString());
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

    }
}