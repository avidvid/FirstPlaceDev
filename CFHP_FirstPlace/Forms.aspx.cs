using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CFHP_FirstPlace
{
    public partial class Forms : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Show_Forms()
        {
            string html = "<table>";
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_GetActiveForms", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                string TypeForm = "";
                while (dr.Read())
                {
                    if (TypeForm != dr["FormTypeName"].ToString()) //Parent Menu
                    {
                        if (html != "")
                            html += @" </tr>";
                        TypeForm = dr["FormTypeName"].ToString();
                        html += @"<tr> <th>"+dr["FormTypeName"].ToString() +"</th>  </tr>"; 
                        html += @"<tr>";
                    }
                    if (dr["FormLink"].ToString() != "")
                        html += @"<td><a href='" + dr["FormLink"].ToString() + "'>" + dr["FormName"].ToString() + "</a></td>";
                    else
                        html += @"<td><a href='" + dr["FormFileName"].ToString() + "'>" + dr["FormName"].ToString() + "</a></td>";
                }
                html += @" </tr></table>";
                dr.Dispose();
                con.Close();
                Response.Write(html);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}