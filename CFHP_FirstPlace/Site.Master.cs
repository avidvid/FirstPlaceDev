using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI.WebControls;

namespace CFHP_FirstPlace
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
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
                string TopMenu="";
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
