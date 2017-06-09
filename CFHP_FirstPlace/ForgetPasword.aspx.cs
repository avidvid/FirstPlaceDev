using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

namespace CFHP_FirstPlace
{
    public partial class ForgetPasword : System.Web.UI.Page
    {
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_DepartmentEmploees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", TextBoxUsername.Text);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string message = "";
                    message = "Dear " + dr["FullName"].ToString() + ",\n Your New intranet account Password is: " + dr["password"].ToString();
                    LabelReport.Text = SendMail(dr["Email"].ToString(), message, "[No Reply] CFHP Intranet-Web NewPassword");
                    LabelReport.CssClass = "validationOk";
                }
                else
                {
                    LabelReport.Text = "This user is not available in the system";
                    LabelReport.CssClass = "validationError";
                }
                dr.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public string SendMail(string Email, string Msg, string Subj)
        {
            try
            {
                MailMessage objEmail = new MailMessage();
                objEmail.From = new MailAddress("webmaster@cfhp.com");
                objEmail.To.Add(Email);
                objEmail.CC.Add("webmaster@cfhp.com");
                //objEmail.Bcc.Add("jflores@cfhp.com,H2@cfhp.com,anarimani@cfhp.com");
                objEmail.Subject = Subj;
                objEmail.Body = Msg;
                //Local Host
                SmtpClient SMTPServer = new SmtpClient("EX2010.CFHP.com", 25);
                SMTPServer.UseDefaultCredentials = false;
                SMTPServer.Send(objEmail);
                return "Your Email has been sent sucessfully-It may takes about 5 minutes plz be patient";
            }
            catch (System.Exception ex)
            {
                return "Send failure due to : <br />" + ex.Message;
            }
        }
    }
}