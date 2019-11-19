using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dietetyka
{
    public partial class User_Interface : System.Web.UI.Page
    {
		BazaDataContext baza = new BazaDataContext();
		string constr = ConfigurationManager.ConnectionStrings["BazaConnectionString"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
        {
			if (Session["username"] == null)
				Response.Redirect("Login_Registration_Page.aspx");

			SqlConnection con = new SqlConnection(constr);
			con.Open();
			SqlCommand sql = new SqlCommand("SELECT CONCAT(imie, ' ', nazwisko) FROM Konto WHERE login='" + Session["username"].ToString() + "'", con);
			sql.CommandType = CommandType.Text;
			LabelName.Text = sql.ExecuteScalar() as string;
			con.Close();
		}

        protected void Options_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login_Regestration_Page.aspx");
        }

        protected void Messages_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login_Regestration_Page.aspx");
        }

		protected void ButtonLogout_Click(object sender, EventArgs e)
		{
			Session.Clear();
			Response.Redirect("Home_Page.aspx");
		}
	}
}