using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

namespace CFHP_FirstPlace.UserHelpDesk
{
    public partial class UserEdit : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || (Session["UserType"].ToString() != "help" && Session["UserType"].ToString() != "admin"))
                Response.Redirect("~/NoAccess.aspx");
            GetDepartments();
            GetSubDepartments();
            GetAuthorization();
            GetTitle();
            RegisterDefaultButton(this, ButtonSubmit);
            if (Request.QueryString["UserID"] != null && !IsPostBack)
                SetPage();
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
                DropDownDepartment.Items.Insert(0, "");
                da.Dispose();
                ds.Dispose();
                con.Close();
                if (temp != 0)
                    DropDownDepartment.SelectedValue = temp.ToString();
            }
            catch (Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit Dep");
                Response.Write("ERROR Dep: " + ex.Message);
            }
        }

        public void GetSubDepartments()
        {
            SqlCommand cmd = new SqlCommand("dbo.First_DepartmentSub_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                int temp = 0;
                con.Open();
                if (DropDownSubDepartment.SelectedValue != "")
                    temp = Convert.ToInt32(DropDownSubDepartment.SelectedValue.ToString());
                da.Fill(ds);
                DropDownSubDepartment.DataSource = ds.Tables[0];
                DropDownSubDepartment.DataValueField = "DepartmentSubID";
                DropDownSubDepartment.DataTextField = "DepartmentSubName";
                DropDownSubDepartment.DataBind();
                DropDownSubDepartment.SelectedValue = temp.ToString();
                da.Dispose();
                ds.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit SubDep");
                Response.Write("ERROR SubDep: " + ex.Message);
            }
        }
        public void GetAuthorization()
        {
            SqlCommand cmd = new SqlCommand("First_Authorization_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                int temp = 0;
                con.Open();
                if (DropDownAuthorization.SelectedValue != "")
                    temp = Convert.ToInt32(DropDownAuthorization.SelectedValue.ToString());
                da.Fill(ds);
                DropDownAuthorization.DataSource = ds.Tables[0];
                DropDownAuthorization.DataValueField = "AuthorizationID";
                DropDownAuthorization.DataTextField = "AuthorizationLevel";
                DropDownAuthorization.DataBind();
                DropDownAuthorization.SelectedValue = temp.ToString();
                da.Dispose();
                ds.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit Auth");
                Response.Write("ERROR Auth: " + ex.Message);
            }
        }
        public void GetTitle()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_Title_List", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ArrayList Title = new ArrayList();
                while (dr.Read())
                {
                    Title.Add(dr["Title"].ToString().Trim());
                }
                dr.Dispose();
                con.Close();
                hdnTitle.Value = string.Join(";", Title.ToArray());
            }
            catch (System.Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit Title");
                Response.Write("ERROR Title: " + ex.Message);
            }
        }
        private void SetPage()
        {
            LabelTitle.Text = "Update user information";
            string id = Request.QueryString["UserID"].ToString();
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_DepartmentEmploees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                CheckBoxSendMail.Visible = false;
                if (dr.Read())
                {
                    TextFName.Text = dr["FirstName"].ToString().Trim();
                    TextLName.Text = dr["LastName"].ToString().Trim();
                    DropDownDepartment.SelectedValue = dr["DepartmentID"].ToString().Trim();
                    DropDownSubDepartment.SelectedValue = dr["SubDepartmentID"].ToString().Trim();
                    TextTitle.Text = dr["Title"].ToString().Trim();
                    CheckBoxSenior.Checked = Convert.ToBoolean(dr["SeniorStaff"].ToString());
                    TextUsername.Text = dr["UserName"].ToString().Trim();
                    TextPassword.Text = dr["password"].ToString().Trim();
                    TextExt.Text = dr["ExtOrg"].ToString().Trim();
                    TextEmail.Text = dr["EmailOrg"].ToString().Trim();
                    TextHomePhone.Text = dr["HomePhone"].ToString().Trim();
                    TextMobilePhone.Text = dr["MobilePhone"].ToString().Trim();
                    DropDownAuthorization.SelectedValue = dr["AuthorizationID"].ToString().Trim();
                    TextPager.Text = dr["Pager"].ToString().Trim();
                    TextBirthDate.Text = dr["DOB2"].ToString().Trim();
                    CheckBoxBirthDay.Checked = Convert.ToBoolean(dr["BirthdayPublish"].ToString());
                    CheckBoxActive.Checked = Convert.ToBoolean(dr["Available"].ToString());
                }
                dr.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit SET");
                Response.Write("ERROR SET: " + ex.Message);
            }
        }
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["UserID"] == null)
                AddNewUser();
            else
                UpdateUser();
        }
        public void UpdateUser()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_User_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", TextUsername.Text);
                cmd.Parameters.AddWithValue("@DepartmentID", Convert.ToInt32(DropDownDepartment.SelectedValue));
                cmd.Parameters.AddWithValue("@SubDepartmentID", Convert.ToInt32(DropDownSubDepartment.SelectedValue));
                cmd.Parameters.AddWithValue("@FirstName", TextFName.Text);
                cmd.Parameters.AddWithValue("@LastName", TextLName.Text);
                if (TextExt.Text != "")
                    cmd.Parameters.AddWithValue("@Ext", Convert.ToInt32(TextExt.Text));
                cmd.Parameters.AddWithValue("@Title", TextTitle.Text);
                cmd.Parameters.AddWithValue("@HomePhone", TextHomePhone.Text);
                cmd.Parameters.AddWithValue("@MobilePhone", TextMobilePhone.Text);
                cmd.Parameters.AddWithValue("@Pager", TextPager.Text);
                cmd.Parameters.AddWithValue("@password", TextPassword.Text);
                if (TextBirthDate.Text != "")
                {
                    cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(TextBirthDate.Text));
                    cmd.Parameters.AddWithValue("@BirthdayPublish", Convert.ToInt32(CheckBoxBirthDay.Checked));
                }


                cmd.Parameters.AddWithValue("@Available", Convert.ToInt32(CheckBoxActive.Checked));
                cmd.Parameters.AddWithValue("@Email", TextEmail.Text);
                cmd.Parameters.AddWithValue("@AuthorizationID", Convert.ToInt32(DropDownAuthorization.SelectedValue));
                cmd.Parameters.AddWithValue("@SeniorStaff", Convert.ToInt32(CheckBoxSenior.Checked));
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["UserID"].ToString()));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/UserHelpDesk/UserSearch.aspx?ok=1&Name=" + TextFName.Text);
            }
            catch (Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit UPDATE");
                Response.Write("ERROR UPDATE: " + ex.Message);
                LabelError.Text = "ERROR: " + ex.Message;
            }
        }
        public void AddNewUser()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_User_Add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", TextUsername.Text);
                cmd.Parameters.AddWithValue("@DepartmentID", Convert.ToInt32(DropDownDepartment.SelectedValue));
                cmd.Parameters.AddWithValue("@SubDepartmentID", Convert.ToInt32(DropDownSubDepartment.SelectedValue));
                cmd.Parameters.AddWithValue("@FirstName", TextFName.Text);
                cmd.Parameters.AddWithValue("@LastName", TextLName.Text);
                if (TextExt.Text != "")
                    cmd.Parameters.AddWithValue("@Ext", Convert.ToInt32(TextExt.Text));
                cmd.Parameters.AddWithValue("@Title", TextTitle.Text);
                cmd.Parameters.AddWithValue("@HomePhone", TextHomePhone.Text);
                cmd.Parameters.AddWithValue("@MobilePhone", TextMobilePhone.Text);
                cmd.Parameters.AddWithValue("@Pager", TextPager.Text);
                cmd.Parameters.AddWithValue("@password", TextPassword.Text);
                if (TextBirthDate.Text != "")
                {
                    cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(TextBirthDate.Text));
                    cmd.Parameters.AddWithValue("@BirthdayPublish", Convert.ToInt32(CheckBoxBirthDay.Checked));
                }
                cmd.Parameters.AddWithValue("@Available", Convert.ToInt32(CheckBoxActive.Checked));
                cmd.Parameters.AddWithValue("@Email", TextEmail.Text);
                cmd.Parameters.AddWithValue("@AuthorizationID", Convert.ToInt32(DropDownAuthorization.SelectedValue));
                cmd.Parameters.AddWithValue("@SeniorStaff", Convert.ToInt32(CheckBoxSenior.Checked));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                if (TextEmail.Text != "" && CheckBoxSendMail.Checked)
                {
                    string message = "";
                    if (TextPassword.Text == TextUsername.Text)
                        message = "Dear " + TextFName.Text + " " + TextLName.Text + ",\n Your intranet account has been created. You’ll use this account to access the web report or CFHP web applications. \n Your username and password have been set to: " + TextUsername.Text;
                    else
                        message = "Dear " + TextFName.Text + " " + TextLName.Text + ",\n Your intranet account has been created. You’ll use this account to access the web report or CFHP web applications. \n Your username and password have been set to: " + TextUsername.Text + " - " + TextPassword.Text;
                    LabelTitle.Text = SendMail(TextEmail.Text, message, "[No Reply] CFHP Intranet-Web Report Account Created");
                }
                Response.Redirect("~/UserHelpDesk/UserSearch.aspx?ok=1&Name=" + TextFName.Text);
            }
            catch (Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit ADD");
                Response.Write("ERROR ADD: " + ex.Message);
                LabelError.Text = "ERROR: " + ex.Message;
            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserHelpDesk/UserSearch.aspx");
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
        public string SendMail(string Email, string Msg, string Subj)
        {
            try
            {
                Email += "@cfhp.com";
                MailMessage objEmail = new MailMessage();
                objEmail.From = new MailAddress("webmaster@cfhp.com");
                objEmail.To.Add(Email);
                objEmail.CC.Add("webmaster@cfhp.com");
                objEmail.Bcc.Add(GetAdminEmails());
                //objEmail.Bcc.Add("jflores@cfhp.com,JConlu@cfhp.com,anarimani@cfhp.com"); =GetAdminEmails() 12-20-2016
                objEmail.Subject = Subj;
                objEmail.Body = Msg;
                //Local Host
                SmtpClient SMTPServer = new SmtpClient("EX2010.CFHP.com", 25);
                SMTPServer.UseDefaultCredentials = false;
                SMTPServer.Send(objEmail);
                return "Your Email has been sent sucessfully - Thank You";
            }
            catch (System.Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit email");
                return "Send failure due to : <br />" + ex.Message;
            }
        }
        public string SendError(string Msg, string Subj)
        {
            try
            {
                MailMessage objEmail = new MailMessage();
                objEmail.From = new MailAddress("webmaster@cfhp.com");
                objEmail.To.Add("anarimani@cfhp.com");
                objEmail.Subject = Subj;
                objEmail.Body = Msg;
                //Local Host
                SmtpClient SMTPServer = new SmtpClient("EX2010.CFHP.com", 25);
                SMTPServer.UseDefaultCredentials = false;
                SMTPServer.Send(objEmail);
                return "Your Email has been sent sucessfully - Thank You";
            }
            catch (System.Exception ex)
            {
                return "Send failure due to : <br />" + ex.Message;
            }
        }
        public string GetAdminEmails()
        {
            string EmailList = "";
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.First_UserGetAdminEmails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmailList += dr["Email"].ToString().ToLower().Trim() + ",";
                }
                EmailList += "webmaster@cfhp.com";
                dr.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
            {
                SendError(ex.Message, "[Error] UserEdit admin email");
                Response.Write(ex.Message);
            }
            return EmailList;
        }
    }
}