using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CFHP_FirstPlace
{
    public partial class Policy : System.Web.UI.Page
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
                SqlCommand cmd = new SqlCommand("dbo.First_GetActivePolicy", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (Convert.ToBoolean(dr["Header"])) //Header
                        html += @"<tr> <th></th><th>" + dr["Subject"].ToString() + " (" + dr["Classification"].ToString() + ") </th>  </tr>";
                    else
                    {
                        html += @"<tr> <td>" + dr["Classification"].ToString() + "</td>";
                        if ("http" == dr["FileName"].ToString().Substring(0,4))
                            html += @"<td><a href='" + dr["FileName"].ToString() + "'>" + dr["Subject"].ToString() + "</a></td> </tr>";
                        else
                            html += @"<td><a href='\\cfhpfirstplace\EmployeeResource\Policy\" + dr["FileName"].ToString() + "'>" + dr["Subject"].ToString() + "</a></td> </tr>";
                    }
                }
                html += @"</table>";
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