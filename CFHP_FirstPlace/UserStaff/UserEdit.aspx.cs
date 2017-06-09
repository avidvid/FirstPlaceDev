using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;

namespace CFHP_FirstPlace.UserStaff
{
    public partial class UserEdit : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || (Session["UserType"].ToString() != "staff" && Session["UserType"].ToString() != "admin"))
                Response.Redirect("~/NoAccess.aspx");
            if (Request.QueryString["UserID"] != null && !IsPostBack)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("First_DepartmentEmploees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        //Add security here 
                        FirstName.Text = dr["FullName"].ToString();
                        PictureName.Value = dr["DepartmentName"].ToString().Substring(0, 5) + "_" + dr["FirstName"].ToString().Substring(0, 3) + "_" + dr["LastName"].ToString().Substring(0, 3) + dr["UserId"].ToString();
                        PictureName.Value = PictureName.Value.Replace(",", "").Replace("\'", "").Replace("/", "").Replace("\\", "").Replace(" ", "").Trim();
                        PictureNameOld.Value = dr["Picture"].ToString();
                        BirthdayPublish.Checked = Convert.ToBoolean(dr["BirthdayPublish"].ToString());
                        BirthDate.Text = dr["DOB2"].ToString();
                        ImageEmp.ImageUrl = @"~\EmployeeResource\Employees\" + dr["Picture"].ToString();
                    }
                    dr.Dispose();
                    con.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserStaff/UserSearch.aspx");
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_User_Update_Staff", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
                if(BirthDate.Text!="")
                    cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(BirthDate.Text));
                else
                    cmd.Parameters.AddWithValue("@DOB", null);
                if (FileUpload1.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                    if ((fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".jpeg") || (fileExtension == ".jpg"))
                    {
                        string fileName = PictureName.Value + fileExtension;
                        cmd.Parameters.AddWithValue("@Picture", fileName); 
                        string filePath = @"\\cfhpfirstplace\EmployeeResource\Employees\" + fileName;
                        FileUpload1.PostedFile.SaveAs(filePath);
                        // Copy the file for old system
                        //string filePath2 = @"\\cfhpfirstplace\EmployeeResource\Employees\" + fileName;
                        //File.Copy(filePath, filePath2);
                    }
                    else
                    {
                        LabelError.Text = "ERROR: File's Format is not correct";
                        return;
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Picture", PictureNameOld.Value);
                }
                cmd.Parameters.AddWithValue("@BirthdayPublish", BirthdayPublish.Checked);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserStaff/UserSearch.aspx?ok=1");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                LabelError.Text = "ERROR: " + ex.Message + " User : " + System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            }
        }

    }
}