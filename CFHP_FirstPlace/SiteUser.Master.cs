using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CFHP_FirstPlace
{
    public partial class SiteUser : System.Web.UI.MasterPage
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
            CheckAccess();
            //test
            //ContentPlaceHolderMenuStaff.Visible = true;
            //ContentPlaceHolderMenuAdmin.Visible = true;
            //ContentPlaceHolderMenuHelp.Visible = true;
        }
        public bool IsLoggedIn()
        {
            if (Session["LoggedOn"] == null || Session["username"] == null || Session["firstname"] == null || Session["lastname"] == null || Session["userid"] == null || Session["departmentid"] == null || Session["UserType"] == null)
                Response.Redirect("~/Login.aspx");
            if (!(Boolean)Session["LoggedOn"])
                Response.Redirect("~/Login.aspx");
            return true;
        }

        public bool CheckAccess()
        {
            if (Session["UserType"] != null)
            {
                if (Session["UserType"].ToString() == "admin")
                {
                    ContentPlaceHolderMenuStaff.Visible = true;
                    ContentPlaceHolderMenuAdmin.Visible = true;
                    ContentPlaceHolderMenuHelp.Visible = true;
                    ContentPlaceHolderCompliance.Visible = true;
                }
                if (Session["UserType"].ToString() == "staff")
                {
                    ContentPlaceHolderMenuStaff.Visible = true;
                    ContentPlaceHolderCompliance.Visible = true;
                }
                if (Session["UserType"].ToString() == "help")
                    ContentPlaceHolderMenuHelp.Visible = true;
                if (Session["UserType"].ToString() == "comp")
                    ContentPlaceHolderCompliance.Visible = true;
            }
            else
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


        public void Show_Menu()
        {
            string html = "";
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_GetActiveMenus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                string TopMenu = "";
                while (dr.Read())
                {
                    if (TopMenu != dr["MenuName"].ToString()) //Parent Menu
                    {
                        TopMenu = dr["MenuName"].ToString();
                        if (html != "")
                            html += @"</ul></li>";
                        if (dr["MenuLink"].ToString() != "")
                        {

                            html += @"<li><a href='" + dr["MenuLink"].ToString() + "'>" + dr["MenuName"].ToString() + "</a><ul>";
                        }
                        else
                            html += @"<li>" + dr["MenuName"].ToString() + "<ul>";
                    }
                    if (dr["ItemName"].ToString() != "")
                    {
                        html += @"<li>";
                        html += @"<a href='" + dr["ItemLink"].ToString() + "'>" + dr["ItemName"].ToString() + "</a>";
                        html += @"</li>";
                    }
                }
                dr.Dispose();
                con.Close();
                Response.Write(html);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void CalendarSide_SelectionChanged(object sender, EventArgs e)
        {
            Response.Redirect("~/Calendar.aspx?Date=" + CalendarSide.SelectedDate.Date.ToString());
        }
    }
}
