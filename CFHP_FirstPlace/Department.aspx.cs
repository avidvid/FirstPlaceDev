using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace.Departments
{
    public partial class Department : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            GetEmployees();
            GetNews();
        }

        public void GetEmployees()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_DepartmentEmploees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepID", Request.QueryString["Dept"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    Repeater_Emploee.DataSource = ds.Tables[0];
                    Repeater_Emploee.DataBind();
                    LableDepName.Text = "About " + ds.Tables[0].Rows[0]["DepartmentName"].ToString().Trim();
                    LableDepContent.Text = ds.Tables[0].Rows[0]["DepartmentDescription"].ToString().Trim();
                }
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void GetNews()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_GetActiveContents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FK_Department", Request.QueryString["Dept"].ToString());
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