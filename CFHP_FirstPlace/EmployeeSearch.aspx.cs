using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace CFHP_FirstPlace
{
    public partial class EmployeeSearch : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            GetDepartments();
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
    }
}