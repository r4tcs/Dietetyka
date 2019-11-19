using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dietetyka
{
    public partial class Home_Page : System.Web.UI.Page
    {
		BazaDataContext baza = new BazaDataContext();
		string constr = ConfigurationManager.ConnectionStrings["BazaConnectionString"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Registration_Log_In_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login_Registration_Page.aspx");
        }

		protected void zaloguj_Click(object sender, EventArgs e)
		{
			SqlConnection con = new SqlConnection(constr);
			con.Open();
			SqlCommand sql = new SqlCommand("SELECT haslo FROM Konto WHERE login='" + logNazwa.Text + "'", con);
			sql.CommandType = CommandType.Text;
			string savedPasswordHash = sql.ExecuteScalar() as string;
			con.Close();
			if (savedPasswordHash is null)
			{
				Response.Write("<script>alert('Niepoprawny login');</script>");
				return;
			}


			byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
			/* Get the salt */
			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);
			/* Compute the hash on the password the user entered */
			var pbkdf2 = new Rfc2898DeriveBytes(logHaslo.Value, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);
			/* Compare the results */
			for (int i = 0; i < 20; i++)
				if (hashBytes[i + 16] != hash[i])
				{
					Response.Write("<script>alert('Niepoprawne hasło');</script>");
					return;
				}

			con.Open();
			sql = new SqlCommand("SELECT rodzaj FROM Konto WHERE login='" + logNazwa.Text + "'", con);
			sql.CommandType = CommandType.Text;
			string type = sql.ExecuteScalar() as string;
			con.Close();

			if (type == "A")
			{
				Response.Write("<script>alert('Pomyślnie zalogowano');</script>");
				Session["username"] = logNazwa.Text;
				Response.Redirect("Admin_Interface.aspx");
			}
			else if (type == "D")
			{
				Response.Write("<script>alert('Pomyślnie zalogowano');</script>");
				Session["username"] = logNazwa.Text;
				Response.Redirect("Dietecian_Interface.aspx");
			}
			else if (type == "K")
			{
				Response.Write("<script>alert('Pomyślnie zalogowano');</script>");
				Session["username"] = logNazwa.Text;
				Response.Redirect("User_Interface.aspx");
			}
			else
			{
				Response.Redirect("404.aspx");
			}
		}
	}
}