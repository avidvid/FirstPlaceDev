using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CFHP_FirstPlace.UserClient
{
    public partial class LicenseList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedOn"] != null)
            {
                SqlDataSourceLicenses.SelectParameters["User"].DefaultValue = Session["PK_User"].ToString();
                SqlDataSourceLicenses.SelectParameters["Department"].DefaultValue = Session["DepartmentId"].ToString();
            }
        }
    }
}