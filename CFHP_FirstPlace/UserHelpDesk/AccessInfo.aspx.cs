using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CFHP_FirstPlace.UserHelpDesk
{
    public partial class AccessInfo : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || (Session["UserType"].ToString() != "help" && Session["UserType"].ToString() != "admin"))
                Response.Redirect("~/NoAccess.aspx");
            TextBoxAccess.Focus();
            RegisterDefaultButton(TextBoxAccess, ButtonSearch2);
            RegisterDefaultButton(TextBoxName, ButtonSearch);
            RegisterDefaultButton(DropDownDepartment, ButtonSearch);
            GetDepartments();
            HyperLinkDelete.NavigateUrl = "~/UserHelpDesk/AccessDelete.aspx?UserID=" + Request.QueryString["UserID"];
            if (Request.QueryString["UserID"] != null)
            {
                SetPage();
                SetName();
            }
            if (Request.QueryString["ok"] != null)
                LabelOk.Text += "Your action was successful\n " + Request.QueryString["ok"].ToString() + " Accesses Updated";
            LabelResultAccess.Text = "";
            LabelResultEmployee.Text = "";
            if (DropDownDepartment.SelectedValue != "0" || TextBoxName.Text != "")
                GetEmployees();
            if (TextBoxAccess.Text != "")
                GetAccess();
        }
        private void SetPage()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_Report_Access_User", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();
                con.Close();
            }
            catch (System.Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void SetName()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_DepartmentEmploees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    LabelName.Text = dr["FullName"].ToString().Trim();
                dr.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
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
                LabelResultEmployee.Text = Count + " employee(s) for your search (" + TextBoxAccess.Text + ")";
                if (Count == 0)
                    LabelResultEmployee.CssClass = "ErrorMsg";
                else
                    LabelResultEmployee.CssClass = "OkMsg";
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
        public void GetAccess()
        {
            SqlCommand cmd = new SqlCommand("First_Report_Access_ByName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
            cmd.Parameters.AddWithValue("@Name", TextBoxAccess.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                int Count = da.Fill(ds);
                GridView3.DataSource = ds.Tables[0];
                GridView3.DataBind();
                con.Close();
                LabelResultAccess.Text = Count + " Access(es) for your search (" + TextBoxAccess.Text + ")";
                if (Count == 0)
                    LabelResultAccess.CssClass = "ErrorMsg";
                else
                    LabelResultAccess.CssClass = "OkMsg";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
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

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserHelpDesk/UserSearch.aspx");
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            LabelResultAccess.Text = "";
            TextBoxAccess.Text = "";
            GridView3.Visible = false;
        }
        protected void ButtonSearch2_Click(object sender, EventArgs e)
        {
            GridView1.Visible = false;
            LabelResultEmployee.Text = "";
            TextBoxName.Text = "";
            DropDownDepartment.SelectedValue = "0";
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