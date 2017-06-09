using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace.UserStaff
{
    public partial class ContentInfo : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedOn"] != null)
            {
                if (Session["UserType"] == null || (Session["UserType"].ToString() != "staff" && Session["UserType"].ToString() != "admin"))
                    Response.Redirect("~/NoAccess.aspx");
                GetDepartments();
                GetMedia();
                if (TextBody.Text == "")
                    TextBody.Text = "Type Here";
                if (Request.QueryString["ContentID"] != null && !IsPostBack)
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
            string id = Request.QueryString["ContentID"].ToString();
            DropDownInfo.Enabled = false;
            try
            {
                SqlCommand cmd = new SqlCommand("First_GetActiveContents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PK_Content", Convert.ToInt32(Request.QueryString["ContentID"].ToString()));
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    TextBody.Text = dr["ContentSummary"].ToString().Trim();
                    TextTitle.Text = dr["ContentHeader"].ToString().Trim();
                    DropDownDepartment.SelectedValue = dr["FK_Department"].ToString().Trim();
                    DropDownMedia.SelectedValue = dr["FK_ContentType"].ToString().Trim();
                }
                dr.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
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
        public void GetMedia()
        {
            SqlCommand cmd = new SqlCommand("dbo.First_ContentType_Select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                int temp = 0;
                con.Open();
                if (DropDownMedia.SelectedValue != "")
                    temp = Convert.ToInt32(DropDownMedia.SelectedValue.ToString());
                da.Fill(ds);
                DropDownMedia.DataSource = ds.Tables[0];
                DropDownMedia.DataValueField = "PK_ContentType";
                DropDownMedia.DataTextField = "ContentMedia";
                DropDownMedia.DataBind();
                da.Dispose();
                ds.Dispose();
                con.Close();
                if (temp != 0)
                    DropDownMedia.SelectedValue = temp.ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ContentID"] == null)
                AddNewContent();
            else
                UpdateContent(1);
        }
        public void UpdateContent(int DoUpdate)
        {
            try
            {
                string BodyString = "";
                SqlCommand cmd = new SqlCommand("dbo.First_Content_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PK_Content", Convert.ToInt32(Request.QueryString["ContentID"].ToString()));
                cmd.Parameters.AddWithValue("@AddedBy", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@FK_Department", Convert.ToInt32(DropDownDepartment.SelectedValue));
                cmd.Parameters.AddWithValue("@ContentActive", DoUpdate);
                cmd.Parameters.AddWithValue("@FK_ContentType", Convert.ToInt32(DropDownMedia.SelectedValue));
                cmd.Parameters.AddWithValue("@ContentHeader", TextTitle.Text);
                BodyString += TextBody.Text.Replace("&lt;", "<").Replace("&gt;", ">");
                cmd.Parameters.AddWithValue("@ContentSummary", BodyString);
                cmd.Parameters.AddWithValue("@ContentText", BodyString);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close(); 
                Response.Redirect("~/UserStaff/ContentList.aspx");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                LabelError.Text = "ERROR: " + ex.Message;
            }
        }
        public void AddNewContent()
        {
            try
            {
                string BodyString = "";
                string LinkString = "";
                string fileName = "";
                SqlCommand cmd = new SqlCommand("dbo.First_Content_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContentAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@AddedBy",Session["username"].ToString());
                cmd.Parameters.AddWithValue("@FK_Department", Convert.ToInt32(DropDownDepartment.SelectedValue));
                cmd.Parameters.AddWithValue("@ContentActive", 1);
                cmd.Parameters.AddWithValue("@FK_ContentType", Convert.ToInt32(DropDownMedia.SelectedValue));
                cmd.Parameters.AddWithValue("@ContentHeader", TextTitle.Text);
                BodyString += TextBody.Text.Replace("&lt;", "<").Replace("&gt;", ">");
                if (DropDownInfo.SelectedValue != "0")
                {
                    cmd.Parameters.AddWithValue("@ContentLink", 1);
                    if (DropDownInfo.SelectedValue == "1") // ADD LINK
                    {
                        LinkString = TextBoxLink1.Text;
                        cmd.Parameters.AddWithValue("@ContentURL", TextBoxLink1.Text);
                        BodyString += "<BR /><BR /><BR /> Click <A href=\"" + LinkString + "\">here</A> for " + TextBoxLinkName1.Text;
                        if (TextBoxLink2.Text!="")
                        {
                            LinkString = TextBoxLink2.Text;
                            BodyString += "<BR /><BR /><BR /> Click <A href=\"" + LinkString + "\">here</A> for " + TextBoxLinkName2.Text;
                        }
                        if (TextBoxLink3.Text != "")
                        {
                            LinkString = TextBoxLink3.Text;
                            BodyString += "<BR /><BR /><BR /> Click <A href=\"" + LinkString + "\">here</A> for " + TextBoxLinkName3.Text;
                        }
                    }
                    if (DropDownInfo.SelectedValue == "2") // ADD FILE
                    {
                        if (FileUpload1.HasFile)
                        {
                            string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                            if ( (fileExtension == ".gif") ||(fileExtension == ".png") ||(fileExtension == ".jpeg") ||(fileExtension == ".jpg")  )
                            {//Images
                                fileName = TextTitle.Text.Replace(',',' ') + "_" + FileUpload1.FileName;
                                fileName = fileName.Replace(" ", "_");
                                cmd.Parameters.AddWithValue("@ContentURL", fileName);
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/Photos/" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\Photos\" + fileName;
                                FileUpload1.PostedFile.SaveAs(fileName);
                                BodyString = "<BR /><div style=\"text-align: center;\"> <img src=\"" + LinkString + "\" ALT=\"" + TextBoxFileName1.Text + "\" height=\"300\" width=\"300\"  /> </div><BR /><BR />" + BodyString;
                            }
                            else
                            {
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload1.FileName;
                                fileName = fileName.Replace(" ", "_");
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\" + fileName;
                                FileUpload1.PostedFile.SaveAs(fileName);
                                cmd.Parameters.AddWithValue("@ContentURL", LinkString);
                                BodyString += "<BR /><BR /><BR /> Click <A href=\"" + LinkString + "\">here</A> for " + TextBoxFileName1.Text;
                            }
                        }
                        if (FileUpload2.HasFile)
                        {
                            string fileExtension = System.IO.Path.GetExtension(FileUpload2.FileName).ToLower();
                            if ((fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".jpeg") || (fileExtension == ".jpg"))
                            {//Images
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload2.FileName;
                                fileName = fileName.Replace(" ", "_");
                                //cmd.Parameters.AddWithValue("@ContentURL", fileName);
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/Photos/" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\Photos\" + fileName;
                                FileUpload2.PostedFile.SaveAs(fileName);
                                BodyString = "<BR /><div style=\"text-align: center;\"> <img src=\"" + LinkString + "\" ALT=\"" + TextBoxFileName2.Text + "\" height=\"300\" width=\"300\"  /> </div><BR /><BR />" + BodyString;
                            }
                            else
                            {
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload2.FileName;
                                fileName = fileName.Replace(" ", "_");
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\" + fileName;
                                FileUpload2.PostedFile.SaveAs(fileName);
                                //cmd.Parameters.AddWithValue("@ContentURL", LinkString);
                                BodyString += "<BR /><BR /><BR /> Click <A href=\"" + LinkString + "\">here</A> for " + TextBoxFileName2.Text;
                            }
                        }
                        if (FileUpload3.HasFile)
                        {
                            string fileExtension = System.IO.Path.GetExtension(FileUpload3.FileName).ToLower();
                            if ((fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".jpeg") || (fileExtension == ".jpg"))
                            {//Images
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload3.FileName;
                                fileName = fileName.Replace(" ", "_");
                                //cmd.Parameters.AddWithValue("@ContentURL", fileName);
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/Photos\" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\Photos\" + fileName;
                                FileUpload3.PostedFile.SaveAs(fileName);
                                BodyString = "<BR /><div style=\"text-align: center;\"> <img src=\"" + LinkString + "\" ALT=\"" + TextBoxFileName3.Text + "\" height=\"300\" width=\"300\"  /> </div><BR /><BR />" + BodyString;
                            }
                            else
                            {
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload3.FileName;
                                fileName = fileName.Replace(" ", "_");
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\" + fileName;
                                FileUpload3.PostedFile.SaveAs(fileName);
                                //cmd.Parameters.AddWithValue("@ContentURL", LinkString);
                                BodyString += "<BR /><BR /><BR /> Click <A href=\"" + LinkString + "\">here</A> for " + TextBoxFileName3.Text;
                            }
                        }
                        if (FileUpload4.HasFile)
                        {
                            string fileExtension = System.IO.Path.GetExtension(FileUpload4.FileName).ToLower();
                            if ((fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".jpeg") || (fileExtension == ".jpg"))
                            {//Images
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload4.FileName;
                                fileName = fileName.Replace(" ", "_");
                                //cmd.Parameters.AddWithValue("@ContentURL", fileName);
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/Photos\" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\Photos\" + fileName;
                                FileUpload4.PostedFile.SaveAs(fileName);
                                BodyString = "<BR /><div style=\"text-align: center;\"> <img src=\"" + LinkString + "\" ALT=\"" + TextBoxFileName4.Text + "\" height=\"300\" width=\"300\"  /> </div><BR /><BR />" + BodyString;
                            }
                            else
                            {
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload4.FileName;
                                fileName = fileName.Replace(" ", "_");
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\" + fileName;
                                FileUpload4.PostedFile.SaveAs(fileName);
                                //cmd.Parameters.AddWithValue("@ContentURL", LinkString);
                                BodyString += "<BR /><BR /><BR /> Click <A href=\"" + LinkString + "\">here</A> for " + TextBoxFileName4.Text;
                            }
                        }
                        if (FileUpload5.HasFile)
                        {
                            string fileExtension = System.IO.Path.GetExtension(FileUpload5.FileName).ToLower();
                            if ((fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".jpeg") || (fileExtension == ".jpg"))
                            {//Images
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload5.FileName;
                                fileName = fileName.Replace(" ", "_");
                                //cmd.Parameters.AddWithValue("@ContentURL", fileName);
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/Photos\" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\Photos\" + fileName;
                                FileUpload5.PostedFile.SaveAs(fileName);
                                BodyString = "<BR /><div style=\"text-align: center;\"> <img src=\"" + LinkString + "\" ALT=\"" + TextBoxFileName5.Text + "\" height=\"300\" width=\"300\"  /> </div><BR /><BR />" + BodyString;
                            }
                            else
                            {
                                fileName = TextTitle.Text.Replace(',', ' ') + "_" + FileUpload5.FileName;
                                fileName = fileName.Replace(" ", "_");
                                LinkString = @"http://firstplacedev/EmployeeResource/Contents/" + fileName;
                                fileName = @"\\cfhpfirstplace\EmployeeResource\Contents\" + fileName;
                                FileUpload5.PostedFile.SaveAs(fileName);
                                //cmd.Parameters.AddWithValue("@ContentURL", LinkString);
                                BodyString += "<BR /><BR /><BR /> Click <A href=\"" + LinkString + "\">here</A> for " + TextBoxFileName5.Text;
                            }
                        }
                    }
                }
                cmd.Parameters.AddWithValue("@ContentSummary", BodyString);
                cmd.Parameters.AddWithValue("@ContentText", BodyString);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserStaff/ContentList.aspx");
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
            //TextBoxLink1.Visible = false;
            //FileUpload1.Visible = false;
            //RequiredFieldValidatorURL.Enabled = false;
            //RequiredFieldValidatorFile.Enabled = false;
            if (DropDownInfo.SelectedValue == "1")
            {
                RequiredFieldValidatorURL.Enabled = true;
                RequiredFieldValidatorFile.Enabled = false;
                LabelInfo.Text = "URLs (Links)";
                TextBoxLink1.Visible = true;
                LabelLinkName1.Visible = true;
                TextBoxLinkName1.Visible = true;
                TextBoxLink2.Visible = true;
                LabelLinkName2.Visible = true;
                TextBoxLinkName2.Visible = true;
                TextBoxLink3.Visible = true;
                LabelLinkName3.Visible = true;
                TextBoxLinkName3.Visible = true;
                FileUpload1.Visible = false;
                LabelFileName1.Visible = false;
                TextBoxFileName1.Visible = false;
                FileUpload2.Visible = false;
                LabelFileName2.Visible = false;
                TextBoxFileName2.Visible = false;
                FileUpload3.Visible = false;
                LabelFileName3.Visible = false;
                TextBoxFileName3.Visible = false;
                FileUpload4.Visible = false;
                LabelFileName4.Visible = false;
                TextBoxFileName4.Visible = false;
                FileUpload5.Visible = false;
                LabelFileName5.Visible = false;
                TextBoxFileName5.Visible = false;
            }
            if (DropDownInfo.SelectedValue == "2")
            {
                RequiredFieldValidatorURL.Enabled = false;
                RequiredFieldValidatorFile.Enabled = true;
                LabelInfo.Text = "Files";
                TextBoxLink1.Visible = false;
                LabelLinkName1.Visible = false;
                TextBoxLinkName1.Visible = false;
                TextBoxLink2.Visible = false;
                LabelLinkName2.Visible = false;
                TextBoxLinkName2.Visible = false;
                TextBoxLink3.Visible = false;
                LabelLinkName3.Visible = false;
                TextBoxLinkName3.Visible = false;
                FileUpload1.Visible = true;
                LabelFileName1.Visible = true;
                TextBoxFileName1.Visible = true;
                FileUpload2.Visible = true;
                LabelFileName2.Visible = true;
                TextBoxFileName2.Visible = true;
                FileUpload3.Visible = true;
                LabelFileName3.Visible = true;
                TextBoxFileName3.Visible = true;
                FileUpload4.Visible = true;
                LabelFileName4.Visible = true;
                TextBoxFileName4.Visible = true;
                FileUpload5.Visible = true;
                LabelFileName5.Visible = true;
                TextBoxFileName5.Visible = true;
            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserStaff/ContentList.aspx");
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            UpdateContent(0);
            Response.Redirect("~/UserStaff/ContentList.aspx");
        }
    }
}