using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace.UserStaff
{
    public partial class EventInfo : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || (Session["UserType"].ToString() != "staff" && Session["UserType"].ToString() != "admin"))
                Response.Redirect("~/NoAccess.aspx");
            GetEventType();
            if (TextBody.Text == "")
                TextBody.Text = "Type Here";
            if (Request.QueryString["EventID"] != null && !IsPostBack)
            {
                SetPage();
                ButtonDelete.Visible = true;
            }
        }
        private void SetPage()
        {
            LabelTitle.Text = "Update Event";
            string id = Request.QueryString["EventID"].ToString();
            try
            {
                SqlCommand cmd = new SqlCommand("First_GetActiveEvents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PK_Event", Convert.ToInt32(Request.QueryString["EventID"].ToString()));
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ImageEvent.Visible = true;
                    TextBody.Text = dr["EventText"].ToString().Trim();
                    TextTitle.Text = dr["EventHeader"].ToString().Trim();
                    TextBoxStartDate.Text = dr["EventStartTime"].ToString().Trim();
                    TextBoxEndDate.Text = dr["EventEndTime"].ToString().Trim();
                    DropDownEventType.SelectedValue = dr["FK_EventType"].ToString().Trim();
                    TextBoxLink.Text = dr["EventLink"].ToString().Trim();
                    TextBoxLinkName.Text = dr["EventLinkName"].ToString().Trim();
                    PictureNameOld.Value = dr["EventPicture"].ToString();
                    TextBoxLocation.Text = dr["EventLocation"].ToString().Trim();
                    ImageEvent.ImageUrl = @"~\Images\Event\" + dr["EventPicture"].ToString();
                }
                dr.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void GetEventType()
        {
            SqlCommand cmd = new SqlCommand("First_Event_type_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                int temp = 0;
                con.Open();
                if (DropDownEventType.SelectedValue != "")
                    temp = Convert.ToInt32(DropDownEventType.SelectedValue.ToString());
                da.Fill(ds);
                DropDownEventType.DataSource = ds.Tables[0];
                DropDownEventType.DataValueField = "PK_EventType";
                DropDownEventType.DataTextField = "EventType";
                DropDownEventType.DataBind();
                da.Dispose();
                ds.Dispose();
                con.Close();
                if (temp != 0)
                    DropDownEventType.SelectedValue = temp.ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EventID"] == null)
                AddNewContent();
            else
                UpdateContent(1);
        }
        public void UpdateContent(int DoUpdate)
        {
            try
            {
                string BodyString = "";
                SqlCommand cmd = new SqlCommand("First_Event_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PK_Event", Convert.ToInt32(Request.QueryString["EventID"].ToString()));
                cmd.Parameters.AddWithValue("@EditBy", "avid");//Session["username"].ToString());
                cmd.Parameters.AddWithValue("@FK_EventType", Convert.ToInt32(DropDownEventType.SelectedValue));
                cmd.Parameters.AddWithValue("@EventActive", DoUpdate);
                cmd.Parameters.AddWithValue("@EventHeader", TextTitle.Text);
                BodyString += TextBody.Text.Replace("&lt;", "<").Replace("&gt;", ">");
                cmd.Parameters.AddWithValue("@EventText", BodyString);
                cmd.Parameters.AddWithValue("@EventLink", TextBoxLink.Text);
                cmd.Parameters.AddWithValue("@EventLinkName", TextBoxLinkName.Text);
                cmd.Parameters.AddWithValue("@EventLocation", TextBoxLocation.Text);
                cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(TextBoxStartDate.Text));
                cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(TextBoxEndDate.Text));
                if (FileUploadPicture.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(FileUploadPicture.FileName).ToLower();
                    if ((fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".jpeg") || (fileExtension == ".jpg"))
                    {
                        string fileName = TextTitle.Text + fileExtension;
                        cmd.Parameters.AddWithValue("@EventPicture", fileName);
                        fileName = @"\\cfhpfirstplace\firstplacedev\Images\Event\" + fileName;
                        FileUploadPicture.PostedFile.SaveAs(fileName);
                    }
                    else
                    {
                        LabelError.Text = "ERROR: File's Format is not correct";
                        return;
                    }
                }
                else
                    cmd.Parameters.AddWithValue("@EventPicture", PictureNameOld.Value);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close(); 
                Response.Redirect("~/UserStaff/EventList.aspx");
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
                SqlCommand cmd = new SqlCommand("dbo.First_Event_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AddedBy", "avid");//Session["username"].ToString());
                cmd.Parameters.AddWithValue("@FK_EventType", Convert.ToInt32(DropDownEventType.SelectedValue));
                cmd.Parameters.AddWithValue("@EventHeader", TextTitle.Text);
                cmd.Parameters.AddWithValue("@EndDate", TextBoxEndDate.Text);
                cmd.Parameters.AddWithValue("@StartDate", TextBoxStartDate.Text);
                BodyString += TextBody.Text.Replace("&lt;", "<").Replace("&gt;", ">");
                cmd.Parameters.AddWithValue("@EventText", BodyString);
                cmd.Parameters.AddWithValue("@EventLink", TextBoxLink.Text);
                cmd.Parameters.AddWithValue("@EventLinkName", TextBoxLinkName.Text);
                cmd.Parameters.AddWithValue("@EventLocation", TextBoxLocation.Text);
                if (FileUploadPicture.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(FileUploadPicture.FileName).ToLower();
                    if ((fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".jpeg") || (fileExtension == ".jpg"))
                    {
                        string fileName = TextTitle.Text + fileExtension;
                        cmd.Parameters.AddWithValue("@EventPicture", fileName);
                        fileName = @"\\cfhpfirstplace\firstplacedev\Images\Event\" + fileName;
                        FileUploadPicture.PostedFile.SaveAs(fileName);
                    }
                    else
                    {
                        LabelError.Text = "ERROR: File's Format is not correct";
                        return;
                    }
                }
                else
                    cmd.Parameters.AddWithValue("@EventPicture", "blank.jpg");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserStaff/EventList.aspx");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                LabelError.Text = "ERROR: " + ex.Message;
            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserStaff/EventList.aspx");
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            UpdateContent(0);
            Response.Redirect("~/UserStaff/EventList.aspx");
        }
    }
}