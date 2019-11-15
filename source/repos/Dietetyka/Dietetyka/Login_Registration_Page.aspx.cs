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
    public partial class _Login_Regestration_Page : Page
    {
		BazaDataContext baza = new BazaDataContext();
		string constr = ConfigurationManager.ConnectionStrings["BazaConnectionString"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
        {
        }

		protected void utworzKonto_Click(object sender, EventArgs e)
		{
			//hash password
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
			var pbkdf2 = new Rfc2898DeriveBytes(textboxHaslo.Value, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);
			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);
			string savedPasswordHash = Convert.ToBase64String(hashBytes);

			try
			{
				SqlConnection con = new SqlConnection(constr);
				con.Open();
				Konto k = new Konto();
				k.imie = textboxImie.Text;
				k.nazwisko = textboxNazwisko.Text;
				k.rodzaj = "K";
				k.login = textboxLogin.Text;
				k.haslo = savedPasswordHash;
				k.telefon = textboxTelefon.Text;
				baza.Kontos.InsertOnSubmit(k);
				baza.SubmitChanges();
				con.Close();
            }
			catch(Exception)
			{
				Response.Write("<script>alert('Wystąpił nieoczekiwany błąd. Spróbuj ponownie później');</script>");
			}
            Response.Write("<script>alert('Pomyślnie utworzono konto');</script>");
            Response.Redirect("Successfull_Registration.aspx");
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
			Response.Write("<script>alert('Pomyślnie zalogowano');</script>");
            Response.Redirect("User_Interface.aspx");
        }
	}
}