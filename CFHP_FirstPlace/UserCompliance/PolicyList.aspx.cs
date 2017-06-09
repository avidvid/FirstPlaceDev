using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;

namespace CFHP_FirstPlace.UserCompliance
{
    public partial class PolicyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || (Session["UserType"].ToString() != "staff" && Session["UserType"].ToString() != "comp" && Session["UserType"].ToString() != "admin"))
                Response.Redirect("~/NoAccess.aspx");
        }
    }
}