using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace CFHP_FirstPlace.UserHelpDesk
{
    public partial class UserSearch : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || (Session["UserType"].ToString() != "help" && Session["UserType"].ToString() != "admin"))
                Response.Redirect("~/NoAccess.aspx");
            GetDepartments();
            RegisterDefaultButton(this, ButtonSearch);
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            GetEmployees();
        }

        public void GetDepartments()
        {
            SqlCommand cmd = new SqlCommand("First_Department_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                int temp = 0;
                con.Open();
                if (DropDownDepartment.SelectedValue != "")
                    temp = Convert.ToInt32(DropDownDepartment.SelectedValue.ToString());
                da.Fill(ds);
                DropDownDepartment.DataSource = ds.Tables[0];
                DropDownDepartment.DataValueField = "DepartmentIDOld";
                DropDownDepartment.DataTextField = "DepartmentName";
                DropDownDepartment.DataBind();
                da.Dispose();
                ds.Dispose();
                con.Close();
                if (temp != 0)
                    DropDownDepartment.SelectedValue = temp.ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void GetEmployees()
        {
            SqlCommand cmd = new SqlCommand("First_EmployeeSearch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", TextBoxName.Text);
            if (DropDownDepartment.SelectedValue == "")
                cmd.Parameters.AddWithValue("@DepartmentID", null);
            else
                cmd.Parameters.AddWithValue("@DepartmentID", Convert.ToInt32(DropDownDepartment.SelectedValue));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                int Count = da.Fill(ds);
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                con.Close();
                LabelResult.Text = Count + " Employee(s) for your search (" + TextBoxName.Text + "  " + DropDownDepartment.SelectedItem + ")";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void RegisterDefaultButton(System.Web.UI.Control ControlWithFocus, System.Web.UI.Control ControlToClick)
        {

            PostBackOptions p = new PostBackOptions(ControlToClick);
            p.PerformValidation = true;
            if (ControlToClick is Button)
                p.ValidationGroup = ((Button)ControlToClick).ValidationGroup;
            else if (ControlToClick is ImageButton)
                p.ValidationGroup = ((ImageButton)ControlToClick).ValidationGroup;
            else if (ControlToClick is LinkButton)
                p.ValidationGroup = ((LinkButton)ControlToClick).ValidationGroup;
            p.RequiresJavaScriptProtocol = false;
            AttributeCollection a = null;
            if (ControlWithFocus is HtmlControl)
                a = ((System.Web.UI.HtmlControls.HtmlControl)ControlWithFocus).Attributes;
            else if (ControlWithFocus is WebControl)
                a = ((System.Web.UI.WebControls.WebControl)ControlWithFocus).Attributes;
            if (a != null)
                a["onKeyDown"] = string.Format("if (event.keyCode == 13) {{{0}}}", ControlToClick.Page.ClientScript.GetPostBackEventReference(p));
        }

    }
}