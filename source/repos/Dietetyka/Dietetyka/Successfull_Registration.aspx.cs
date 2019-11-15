using System;
using System.Diagnostics;

namespace Dietetyka
{
    public partial class SuccessfullRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("REFRESH", "2;URL=Login_Registration_Page.aspx");
        }
    }
}

        