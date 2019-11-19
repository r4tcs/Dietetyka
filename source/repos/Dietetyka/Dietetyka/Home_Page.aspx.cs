using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dietetyka
{
    public partial class Home_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Registration_Log_In_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login_Registration_Page.aspx");
        }

        protected void Admin_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_Interface.aspx");
        }

		protected void DietecianButton_Click(object sender, EventArgs e)
		{
			Response.Redirect("Dietecian_Interface.aspx");
		}
	}
}