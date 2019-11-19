using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dietetyka
{
	public partial class Dietecian_Interface : System.Web.UI.Page
	{
		BazaDataContext baza = new BazaDataContext();
		string constr = ConfigurationManager.ConnectionStrings["BazaConnectionString"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void addProduct_Click(object sender, EventArgs e)
		{
			try
			{
				SqlConnection con = new SqlConnection(constr);
				con.Open();
				Produkt_spozywczy p = new Produkt_spozywczy();
				p.nazwa = TextBoxNazwa.Text;
				//p.jednostka = DropDownListJednostka.SelectedValue;
				//p.kalorie = float.Parse(TextBoxKalorie.Text);
				//p.weglowodany = float.Parse(TextBoxWeglowodany.Text);
				//p.bialka = float.Parse(TextBoxBialka.Text);
				//p.blonnik = float.Parse(TextBoxBlonnik.Text);
				//p.sol = float.Parse(TextBoxSol.Text);
				//p.tluszcze = float.Parse(TextBoxTluszcze.Text);
				baza.Produkt_spozywczies.InsertOnSubmit(p);
				baza.SubmitChanges();
				con.Close();
				Response.Write("<script>alert('Pomyślnie dodano produkt');</script>");
				Response.Redirect("Admin_Interface.aspx");
			}
			catch (Exception)
			{
				Response.Write("<script>alert('Wystąpił nieoczekiwany błąd. Spróbuj ponownie później');</script>");
			}
		}
	}
}