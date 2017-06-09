using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace.UserStaff
{
    public partial class FormInfo : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedOn"] != null)
            {
                if (Session["UserType"] == null || (Session["UserType"].ToString() != "staff" && Session["UserType"].ToString() != "comp" && Session["UserType"].ToString() != "admin"))
                    Response.Redirect("~/NoAccess.aspx");
                GetFormTypes();
                if (Request.QueryString["FormID"] != null && !IsPostBack)
                {
                    SetPage();
                    ButtonDelete.Visible = true;
                }
            }
            else
                Response.Redirect("~/Login.aspx");
        }
        private void SetPage()
        {
            LabelTitle.Text = "Update post";
            HyperLinkFormView.Visible = true;
            string id = Request.QueryString["FormID"].ToString();
            try
            {
                SqlCommand cmd = new SqlCommand("First_GetActiveForms", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PK_Form", Convert.ToInt32(Request.QueryString["FormID"].ToString()));
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["FormIsLink"].ToString() == "True")
                        HyperLinkFormView.NavigateUrl = dr["FormLink"].ToString();
                    else
                        HyperLinkFormView.NavigateUrl = dr["FormFileName"].ToString();
                    HiddenFieldFile.Value = dr["FormFileName"].ToString();
                    HiddenFieldLink.Value = dr["FormLink"].ToString();
                    HiddenFieldIsLink.Value = dr["FormIsLink"].ToString();
                    TextTitle.Text = dr["FormName"].ToString().Trim();
                    DropDownFormType.SelectedValue = dr["FormTypeID"].ToString().Trim();
                }
                dr.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void GetFormTypes()
        {
            SqlCommand cmd = new SqlCommand("First_FormType_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                int temp = 0;
                con.Open();
                if (DropDownFormType.SelectedValue != "")
                    temp = Convert.ToInt32(DropDownFormType.SelectedValue.ToString());
                da.Fill(ds);
                DropDownFormType.DataSource = ds.Tables[0];
                DropDownFormType.DataValueField = "PK_FormType";
                DropDownFormType.DataTextField = "FormType";
                DropDownFormType.DataBind();
                da.Dispose();
                ds.Dispose();
                con.Close();
                if (temp != 0)
                    DropDownFormType.SelectedValue = temp.ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FormID"] == null)
                AddNewForm();
            else
                UpdateForm(1);
        }
        public void UpdateForm(int DoUpdate)
        {
            try
            {
                string LinkString = "";
                string fileName = "";
                SqlCommand cmd = new SqlCommand("dbo.First_Form_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormActive", DoUpdate);
                cmd.Parameters.AddWithValue("@PK_Form", Convert.ToInt32(Request.QueryString["FormID"].ToString()));
                cmd.Parameters.AddWithValue("@FormType", Convert.ToInt32(DropDownFormType.SelectedValue));
                cmd.Parameters.AddWithValue("@FormName", TextTitle.Text);
                if (DropDownInfo.SelectedValue == "0") // old 
                {
                    LinkString = TextBoxLink1.Text;
                    cmd.Parameters.AddWithValue("@FormIsLink", HiddenFieldIsLink.Value.ToString());
                    cmd.Parameters.AddWithValue("@FormURL", HiddenFieldLink.Value.ToString());
                    cmd.Parameters.AddWithValue("@FormFileName", HiddenFieldFile.Value.ToString());
                } 
                if (DropDownInfo.SelectedValue == "1") // ADD LINK
                {
                    LinkString = TextBoxLink1.Text;
                    cmd.Parameters.AddWithValue("@FormIsLink", 1);
                    cmd.Parameters.AddWithValue("@FormURL", TextBoxLink1.Text);
                    cmd.Parameters.AddWithValue("@FormFileName", "");
                }
                if (DropDownInfo.SelectedValue == "2") // ADD FILE
                    if (FileUpload1.HasFile)
                    {
                        string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                        fileName = TextTitle.Text + "_" + FileUpload1.FileName;
                        fileName = fileName.Replace(" ", "_");
                        LinkString = @"http://firstplacedev/EmployeeResource/Forms/" + fileName;
                        fileName = @"\\cfhpfirstplace\EmployeeResource\Forms\" + fileName;
                        FileUpload1.PostedFile.SaveAs(fileName);
                        cmd.Parameters.AddWithValue("@FormIsLink", 0);
                        cmd.Parameters.AddWithValue("@FormURL", "");
                        cmd.Parameters.AddWithValue("@FormFileName", LinkString);
                    }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close(); 
                Response.Redirect("~/UserStaff/FormList.aspx");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                LabelError.Text = "ERROR: " + ex.Message;
            }
        }
        public void AddNewForm()
        {
            try
            {
                string LinkString = "";
                string fileName = "";
                SqlCommand cmd = new SqlCommand("dbo.First_Form_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormType", Convert.ToInt32(DropDownFormType.SelectedValue));
                cmd.Parameters.AddWithValue("@FormName", TextTitle.Text);
                if (DropDownInfo.SelectedValue == "1") // ADD LINK
                {
                    LinkString = TextBoxLink1.Text;
                    cmd.Parameters.AddWithValue("@FormIsLink", 1);
                    cmd.Parameters.AddWithValue("@FormURL", TextBoxLink1.Text);
                    cmd.Parameters.AddWithValue("@FormFileName", "");
                }
                if (DropDownInfo.SelectedValue == "2") // ADD FILE
                    if (FileUpload1.HasFile)
                    {
                        string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                        fileName = TextTitle.Text + "_" + FileUpload1.FileName;
                        fileName = fileName.Replace(" ", "_");
                        LinkString = @"http://firstplacedev/EmployeeResource/Forms/" + fileName;
                        fileName = @"\\cfhpfirstplace\EmployeeResource\Forms\" + fileName;
                        FileUpload1.PostedFile.SaveAs(fileName);
                        cmd.Parameters.AddWithValue("@FormIsLink", 0);
                        cmd.Parameters.AddWithValue("@FormURL", "");
                        cmd.Parameters.AddWithValue("@FormFileName", LinkString);
                    }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserStaff/FormList.aspx");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                LabelError.Text = "ERROR: " + ex.Message;
            }
        }
        protected void DropDownInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelInfo.Text = "";
            if (DropDownInfo.SelectedValue == "1")
            {
                RequiredFieldValidatorURL.Enabled = true;
                RequiredFieldValidatorFile.Enabled = false;
                LabelInfo.Text = "URL (Link)";
                TextBoxLink1.Visible = true;
                FileUpload1.Visible = false;
            }
            if (DropDownInfo.SelectedValue == "2")
            {
                RequiredFieldValidatorURL.Enabled = false;
                RequiredFieldValidatorFile.Enabled = true;
                LabelInfo.Text = "File";
                TextBoxLink1.Visible = false;
                FileUpload1.Visible = true;
            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserStaff/FormList.aspx");
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            UpdateForm(0);
            Response.Redirect("~/UserStaff/FormList.aspx");
        }
    }
}