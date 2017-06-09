using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace.UserCompliance
{
    public partial class PolicyInfo : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedOn"] != null)
            {
                if (Session["UserType"] == null || (Session["UserType"].ToString() != "staff" && Session["UserType"].ToString() != "comp" && Session["UserType"].ToString() != "admin"))
                    Response.Redirect("~/NoAccess.aspx");
                GetPolicyCategory();
                if (Request.QueryString["PolicyID"] != null && !IsPostBack)
                    SetPage();
            }
            else
                Response.Redirect("~/Login.aspx");
        }
        private void SetPage()
        {
            LabelTitle.Text = "Update post";
            HyperLinkFormView.Visible = true;
            string id = Request.QueryString["PolicyID"].ToString();
            try
            {
                SqlCommand cmd = new SqlCommand("First_GetActivePolicy", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PK_Policy", Convert.ToInt32(Request.QueryString["PolicyID"].ToString()));
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    TextSubject.Text = dr["Subject"].ToString().Trim();
                    TextClassification.Text = dr["Classification"].ToString().Trim();
                    if(Convert.ToBoolean(dr["Header"].ToString()))
                        DropDownInfo.Visible=false;
                    else
                        if ("http" == dr["FileName"].ToString().Substring(0, 4))
                            HyperLinkFormView.NavigateUrl = dr["FileName"].ToString();
                        else
                            HyperLinkFormView.NavigateUrl = @"\\cfhpfirstplace\EmployeeResource\Policy\" + dr["FileName"].ToString();
                    HiddenFieldFile.Value = dr["FileName"].ToString();
                    DropDownCategory.SelectedValue = dr["Category"].ToString().Trim();
                    CheckBoxAvailable.Checked = Convert.ToBoolean(dr["Available"].ToString());
                    CheckBoxHeader.Checked = Convert.ToBoolean(dr["Header"].ToString());
                    CheckBoxRequiredReading.Checked = Convert.ToBoolean(dr["RequiredReading"].ToString());
                }
                dr.Dispose();
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

        public void GetPolicyCategory()
        {
            SqlCommand cmd = new SqlCommand("First_PolicyCategory_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                int temp = 0;
                con.Open();
                if (DropDownCategory.SelectedValue != "")
                    temp = Convert.ToInt32(DropDownCategory.SelectedValue.ToString());
                da.Fill(ds);
                DropDownCategory.DataSource = ds.Tables[0];
                DropDownCategory.DataValueField = "Category";
                DropDownCategory.DataTextField = "CategoryName";
                DropDownCategory.DataBind();
                da.Dispose();
                ds.Dispose();
                if (temp != 0)
                    DropDownCategory.SelectedValue = temp.ToString();
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
        public void UpdatePolicy()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_Policy_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PK_Policy", Convert.ToInt32(Request.QueryString["PolicyID"].ToString()));
                cmd.Parameters.AddWithValue("@Category", Convert.ToInt32(DropDownCategory.SelectedValue));
                cmd.Parameters.AddWithValue("@Subject", TextSubject.Text);
                cmd.Parameters.AddWithValue("@Classification", TextClassification.Text);
                cmd.Parameters.AddWithValue("@Available", Convert.ToInt32(CheckBoxAvailable.Checked));
                cmd.Parameters.AddWithValue("@RequiredReading", Convert.ToInt32(CheckBoxRequiredReading.Checked));
                cmd.Parameters.AddWithValue("@Header", Convert.ToInt32(CheckBoxHeader.Checked));
                switch (DropDownInfo.SelectedValue)
                {
                    case "1":// ADD LINK
                        cmd.Parameters.AddWithValue("@FileName", TextBoxLink1.Text);
                        break;
                    case "2":// ADD FILE
                        if (FileUpload1.HasFile)
                        {
                            string fileName = "";
                            string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                            fileName = TextClassification.Text + " " + TextSubject.Text;
                            fileName = fileName.Replace(" ", "_");
                            cmd.Parameters.AddWithValue("@FileName", fileName);
                            fileName = @"\\cfhpfirstplace\EmployeeResource\Policy\" + fileName;
                            FileUpload1.PostedFile.SaveAs(fileName);
                        }
                        break;
                    default:
                        cmd.Parameters.AddWithValue("@FileName", HiddenFieldFile.Value.ToString());
                        break;
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserCompliance/PolicyList.aspx");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                LabelError.Text = "ERROR: " + ex.Message;
            }
        }
        public void AddNewPolicy()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_Policy_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Category", Convert.ToInt32(DropDownCategory.SelectedValue));
                cmd.Parameters.AddWithValue("@Subject", TextSubject.Text);
                cmd.Parameters.AddWithValue("@Classification", TextClassification.Text);
                cmd.Parameters.AddWithValue("@Available", Convert.ToInt32(CheckBoxAvailable.Checked));
                cmd.Parameters.AddWithValue("@RequiredReading", Convert.ToInt32(CheckBoxRequiredReading.Checked));
                cmd.Parameters.AddWithValue("@Header", Convert.ToInt32(CheckBoxHeader.Checked));
                switch (DropDownInfo.SelectedValue)
                {
                    case "1":// ADD LINK
                        cmd.Parameters.AddWithValue("@FileName", TextBoxLink1.Text);
                        break;
                    case "2":// ADD FILE
                        if (FileUpload1.HasFile)
                        {
                            string fileName = "";
                            string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                            fileName = TextClassification.Text + " " + TextSubject.Text;
                            fileName = fileName.Replace(" ", "_");
                            cmd.Parameters.AddWithValue("@FileName", fileName);
                            fileName = @"\\cfhpfirstplace\EmployeeResource\Policy\" + fileName;
                            FileUpload1.PostedFile.SaveAs(fileName);
                        }
                        break;
                    default:
                        cmd.Parameters.AddWithValue("@FileName", "");
                        break;
                }
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("~/UserCompliance/PolicyList.aspx");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                LabelError.Text = "ERROR: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PolicyID"] == null)
                AddNewPolicy();
            else
                UpdatePolicy();
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserCompliance/PolicyList.aspx");
        }
    }
}