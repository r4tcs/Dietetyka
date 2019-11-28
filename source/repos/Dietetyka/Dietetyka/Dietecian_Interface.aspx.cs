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
	public partial class Dietecian_Interface : System.Web.UI.Page
	{
		BazaDataContext baza = new BazaDataContext();
		string constr = ConfigurationManager.ConnectionStrings["BazaConnectionString"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["username"] == null)
			{
				Response.Redirect("Home_Page.aspx");
			}
            if(!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("SELECT Id, nazwa, kalorie, weglowodany, bialka, tluszcze, blonnik, sol FROM Produkt_spozywczy", new SqlConnection(constr));
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                RepeaterProdoktow.DataSource = dt;
                RepeaterProdoktow.DataBind();
            }
			
			SqlConnection con = new SqlConnection(constr);
			con.Open();
			SqlCommand sql = new SqlCommand("SELECT CONCAT(imie, ' ', nazwisko) FROM Konto WHERE login='" + Session["username"].ToString() + "'", con);
			sql.CommandType = CommandType.Text;
			LabelName.Text = sql.ExecuteScalar() as string;
			con.Close();
		}

		protected void addProduct_Click(object sender, EventArgs e)
		{
			try
			{
				SqlConnection con = new SqlConnection(constr);
				con.Open();
				Produkt_spozywczy p = new Produkt_spozywczy();
				p.nazwa = TextBoxNazwa.Text;
				p.jednostka = DropDownListJednostka.SelectedValue; 
				p.kalorie = float.Parse(TextBoxKalorie.Text);
				p.weglowodany = float.Parse(TextBoxWeglowodany.Text);
				p.bialka = float.Parse(TextBoxBialka.Text);
				p.blonnik = float.Parse(TextBoxBlonnik.Text);
				p.sol = float.Parse(TextBoxSol.Text);
				p.tluszcze = float.Parse(TextBoxTluszcze.Text);
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

        protected void addDanie_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                Danie d = new Danie();
                d.nazwa = TextBoxNazwaDania.Text;
                d.kategoria = KategoriaDropDownList.SelectedValue;
                d.przepis = textboxPrzepis.Text;
                baza.Danies.InsertOnSubmit(d);
                baza.SubmitChanges();
                con.Close();
                Response.Write("<script>alert('Pomyślnie dodano danie');</script>");
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

		protected void ButtonAddProduct_Click(object sender, EventArgs e)
		{
			addProductDiv.Visible = true;
			createDishDiv.Visible = false;
			ProductListDiv.Visible = false;
		}

		protected void ButtonCreateDish_Click(object sender, EventArgs e)
		{
			createDishDiv.Visible = true;
			addProductDiv.Visible = false;
			ProductListDiv.Visible = false;
		}

		protected void ButtonProductList_Click(object sender, EventArgs e)
		{
			ProductListDiv.Visible = true;
			addProductDiv.Visible = false;
			createDishDiv.Visible = false;
		}

		protected void DropDownListIngredient_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}