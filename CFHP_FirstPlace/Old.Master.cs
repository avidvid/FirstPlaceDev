using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CFHP_FirstPlace
{
    public partial class Old : System.Web.UI.MasterPage
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookie = new HttpCookie("UrlSettings");
            myCookie["Url"] = Request.Url.ToString();
            myCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(myCookie);
            IsLoggedIn();
        }
        public bool IsLoggedIn()
        {
            if (Session["LoggedOn"] == null || Session["username"] == null || Session["firstname"] == null || Session["lastname"] == null || Session["userid"] == null || Session["departmentid"] == null)
                Response.Redirect("~/Login.aspx");
            if (!(Boolean)Session["LoggedOn"])
                Response.Redirect("~/Login.aspx");
            return true;
        }

        public void HaveAccess(int ReportID)
        {
            bool check = false;
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.procUserReport_List", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr["ReportID"].ToString()) == ReportID)
                    {
                        check = true;
                        break;
                    }
                }
                dr.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
            {
                Response.Write(ex.Message);
            }
            if (!check)
                Response.Redirect("~/NoAccess.aspx");
        }
    }
}