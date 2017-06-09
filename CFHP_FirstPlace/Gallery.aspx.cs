using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CFHP_FirstPlace
{
    public partial class Gallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
        }
        protected String DynamicHTML()
        {
            string html = "";
            for (int i = 1; i < 120; i++)
            {
                html += "<div data-p='144.50' style='display: none;'>";
                html += "<img data-u='image' src='/EmployeeResource/PhotoGallery/20YearCeleb/20YearCeleb (" + i.ToString() + ").jpg' />";
                html += "<img data-u='thumb' src='/EmployeeResource/PhotoGallery/20YearCeleb/20YearCeleb-Thumb (" + i.ToString() + ").jpg' />";
                html += "</div>";   
            }
            return html;
        }
        
    }
}