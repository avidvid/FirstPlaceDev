using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CFHP_FirstPlace
{
    public partial class News : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            GetNews();
        }
        public void GetNews()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_GetActiveContents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Repeater_News.Visible = true;
                    Repeater_News.DataSource = ds.Tables[0];
                    Repeater_News.DataBind();
                }
                da.Dispose();
                ds.Dispose();
            }
            catch (System.Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


    }
}
