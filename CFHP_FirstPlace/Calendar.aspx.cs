using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CFHP_FirstPlace.Events
{
    public partial class Calendar : System.Web.UI.Page
    {
        DateTime[] EventDates = new DateTime[1000];
        String[,] EventColors = new String[1000,2];
        static string connStr = ConfigurationManager.ConnectionStrings["CFHPFirstPlaceConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        protected void Page_Load(object sender, EventArgs e)
        {
            SetDates();
            if (Request.QueryString["Date"] != null)
            {
                GetEvents(Convert.ToDateTime(Request.QueryString["Date"]));
                Calendar1.TodaysDate = Convert.ToDateTime(Request.QueryString["Date"]);
            }
            else
                GetEvents(DateTime.Now);
        }
        public void SetDates()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("First_GetActiveEvents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {
                    EventDates[i] = (DateTime)dr["EventStartTimeOrg"];
                    EventColors[i, 0] = dr["EventColor"].ToString().Trim();
                    EventColors[i, 1] = dr["EventType"].ToString().Trim();
                    i++;
                }
                dr.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Response.Redirect("~/Calendar.aspx?Date=" + Calendar1.SelectedDate.Date.ToString());
        }


        public void GetEvents(DateTime ThisDay)
        {
            Repeater_Day.Visible = false;
            try
            {
                SqlCommand cmd = new SqlCommand("First_EventsOfDay", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eventDate", ThisDay);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                da.Fill(ds);
                Repeater_Events.DataSource = ds.Tables[0];
                Repeater_Events.DataBind();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    Repeater_Day.Visible = true;
                    Repeater_Day.DataSource = ds.Tables[1];
                    Repeater_Day.DataBind();
                    Label lblHeader = Repeater_Day.Controls[0].Controls[0].FindControl("LabelHeaderDay") as Label;
                    lblHeader.Text = "Event(s) of " + String.Format("{0:dddd, MMM d}", ThisDay); 
                }
                da.Dispose();
                ds.Dispose();
                con.Close();
            }
            catch (System.Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        {
            // Add custom text to cell in the Calendar control.
            int i = 0;
            foreach (DateTime Event in EventDates)
            {
                if (e.Day.Date == Event.Date)
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml(EventColors[i, 0]);
                    e.Cell.Controls.Add(new LiteralControl("<br />" + EventColors[i, 1]));
                    break;
                }
                i++;
                if (e.Day.Date == DateTime.Now.Date)
                {
                    e.Cell.Controls.Add(new LiteralControl("<br /> Today"));
                    e.Cell.ForeColor = System.Drawing.Color.Black;
                    e.Cell.BackColor = System.Drawing.Color.Lavender;
                    break;
                }
            }
        }
    }
}