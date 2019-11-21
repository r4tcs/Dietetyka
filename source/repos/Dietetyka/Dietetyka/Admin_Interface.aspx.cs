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
    public partial class Admin_Interface : System.Web.UI.Page
    {
        BazaDataContext baza = new BazaDataContext();
        string constr = ConfigurationManager.ConnectionStrings["BazaConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
			if (Session["username"] == null)
			{
				Response.Redirect("Home_Page.aspx");
			}
				
			SqlConnection con = new SqlConnection(constr);
			con.Open();
			SqlCommand sql = new SqlCommand("SELECT CONCAT(imie, ' ', nazwisko) FROM Konto WHERE login='" + Session["username"].ToString() + "'", con);
			sql.CommandType = CommandType.Text;
			LabelName.Text = sql.ExecuteScalar() as string;
			con.Close();
		}

        protected void Options_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home_Page.aspx");
        }

        protected void Messages_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home_Page.aspx");
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
                k.rodzaj = Convert.ToChar("D");
                k.login = textboxLogin.Text;
                k.haslo = savedPasswordHash;
                k.telefon = textboxTelefon.Text;
                baza.Kontos.InsertOnSubmit(k);
                baza.SubmitChanges();
                con.Close();
                Response.Write("<script>alert('Pomyślnie utworzono konto');</script>");
                Response.Redirect("Admin_Interface.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Wystąpił nieoczekiwany błąd. Spróbuj ponownie później');</script>");
            }
        }

		protected void ButtonLogout_Click(object sender, EventArgs e)
		{
			Session.Clear();
			Response.Redirect("Home_Page.aspx");
		}
	}
}