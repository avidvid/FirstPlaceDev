using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace.UserClient
{
    public partial class LicenseView : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.Type.Contains("InternetExplorer") || Request.Browser.Type.ToUpper().Contains("IE"))
            {
                LableMessage.Visible = false;
                ButtonSubmit.Enabled = true;
            }
            if (Request.QueryString["LicenseID"] != null)
            {
                if (Session["LoggedOn"] != null)
                {
                    SqlDataSourceLicense.SelectParameters["User"].DefaultValue = Session["PK_User"].ToString();
                    SqlDataSourceLicense.SelectParameters["Department"].DefaultValue = Session["DepartmentId"].ToString();
                    SqlDataSourceLicense.SelectParameters["PK_License"].DefaultValue = Request.QueryString["LicenseID"].ToString();
                    LabelName.Text =  "(" + Session["firstname"].ToString() + " " + Session["lastname"].ToString() + ")"; 
                }
            }
            else
            {
                Response.Redirect("~/UserClient/LicenseList.aspx");
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_Insert_license", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FK_License", Convert.ToInt32(Request.QueryString["LicenseID"].ToString()));
                cmd.Parameters.AddWithValue("@FK_User", Convert.ToInt32(Session["PK_User"].ToString()));
                cmd.Parameters.AddWithValue("@EmployeeID", TextBoxEmployeeID.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserClient/LicenseList.aspx");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void DetailsViewLicense_Load(object sender, EventArgs e)
        {
            try
            {
                if (DetailsViewLicense.Rows[0].Cells.Count == 1)
                    Response.Redirect("~/UserClient/LicenseList.aspx");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}