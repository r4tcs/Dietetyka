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
		static int ProduktID = 0;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["username"] == null)
			{
				Response.Redirect("Home_Page.aspx");
			}
			if (!IsPostBack)
			{
				SetInitialRow();
				SqlCommand cmd = new SqlCommand("SELECT Id, nazwa, kalorie, weglowodany, bialka, tluszcze, blonnik, sol FROM Produkt_spozywczy", new SqlConnection(constr));
				SqlDataAdapter sda = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				sda.Fill(dt);
				RepeaterProduktow.DataSource = dt;
				RepeaterProduktow.DataBind();
				cmd = new SqlCommand("Select Id, imie, nazwisko, login, telefon FROM Konto WHERE rodzaj='K'", new SqlConnection(constr));
				sda = new SqlDataAdapter(cmd);
				dt = new DataTable();
				sda.Fill(dt);
				RepeaterKlientow.DataSource = dt;
				RepeaterKlientow.DataBind();
			}

			SqlConnection con = new SqlConnection(constr);
			con.Open();
			SqlCommand sql = new SqlCommand("SELECT CONCAT(imie, ' ', nazwisko) FROM Konto WHERE login='" + Session["username"].ToString() + "'", con);
			sql.CommandType = CommandType.Text;
			LabelName.Text = sql.ExecuteScalar() as string;
			con.Close();
		}

		private void SetInitialRow()
		{
			DataTable dt = new DataTable();
			DataRow dr = null;
			dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
			dt.Columns.Add(new DataColumn("Column1", typeof(string)));
			dt.Columns.Add(new DataColumn("Column2", typeof(string)));
			dt.Columns.Add(new DataColumn("Column3", typeof(string)));
			dr = dt.NewRow();
			dr["RowNumber"] = 1;
			dr["Column1"] = string.Empty;
			dr["Column2"] = string.Empty;
			dr["Column3"] = string.Empty;
			dt.Rows.Add(dr);
			ViewState["CurrentTable"] = dt;
			Gridview1.DataSource = dt;
			Gridview1.DataBind();
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
				Response.Redirect("Dietecian_Interface.aspx");
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
				int danieID = d.Id;

				int rowIndex = 0;
				DataTable dt = (DataTable)ViewState["CurrentTable"];
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					Skladnik s = new Skladnik();
					s.Id_dania = danieID;
					DropDownList box1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[0].FindControl("DropDownListIngredient");
					SqlCommand sql = new SqlCommand("SELECT Id FROM Produkt_spozywczy WHERE nazwa='" + box1.SelectedItem.Text + "'", con);
					sql.CommandType = CommandType.Text;
					s.Id_produktu = Convert.ToInt32(sql.ExecuteScalar());
					TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBoxWeight");
					s.ilosc = Convert.ToInt32(box2.Text);

					baza.Skladniks.InsertOnSubmit(s);
					baza.SubmitChanges();
					rowIndex++;
				}
				con.Close();
				Response.Write("<script>alert('Pomyślnie dodano danie');</script>");
			}
			catch (FormatException ex)
			{
				Console.Write(ex.Message);
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
			EnableOnlyOneDiv(addProductDiv);
		}

		protected void ButtonCreateDish_Click(object sender, EventArgs e)
		{
			SetInitialRow();
			EnableOnlyOneDiv(createDishDiv);
		}

		protected void ButtonProductList_Click(object sender, EventArgs e)
		{
			EnableOnlyOneDiv(ProductListDiv);
		}

		protected void ButtonDishList_Click(object sender, EventArgs e)
		{
			EnableOnlyOneDiv(DishListDiv);
		}

		protected void ButtonCreateMenu_Click(object sender, EventArgs e)
		{
			EnableOnlyOneDiv(createMenu);
		}

		void EnableOnlyOneDiv(System.Web.UI.HtmlControls.HtmlContainerControl div)
		{
			addProductDiv.Visible = false;
			createDishDiv.Visible = false;
			ProductListDiv.Visible = false;
			createMenu.Visible = false;
			DishListDiv.Visible = false;
			DivCreateDiet.Visible = false;

			div.Visible = true;
		}

		protected void ButtonAdd_Click(object sender, EventArgs e)
		{
			int rowIndex = 0;
			if (ViewState["CurrentTable"] != null)
			{
				DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
				DataRow drCurrentRow = null;
				if (dtCurrentTable.Rows.Count > 0)
				{
					for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
					{
						DropDownList box1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[0].FindControl("DropDownListIngredient");
						TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBoxWeight");
						drCurrentRow = dtCurrentTable.NewRow();
						drCurrentRow["RowNumber"] = i + 1;
						dtCurrentTable.Rows[i - 1]["Column1"] = box1.SelectedItem.Text;
						dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
						rowIndex++;
					}
					dtCurrentTable.Rows.Add(drCurrentRow);
					ViewState["CurrentTable"] = dtCurrentTable;
					Gridview1.DataSource = dtCurrentTable;
					Gridview1.DataBind();
				}
			}
			else
			{
				Response.Write("ViewState is null");
			}

			SetPreviousData();
		}

		private void SetPreviousData()
		{
			int rowIndex = 0;
			if (ViewState["CurrentTable"] != null)
			{
				DataTable dt = (DataTable)ViewState["CurrentTable"];
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						DropDownList box1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[0].FindControl("DropDownListIngredient");
						TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBoxWeight");
						box1.SelectedItem.Text = dt.Rows[i]["Column1"].ToString();
						box2.Text = dt.Rows[i]["Column2"].ToString();
						rowIndex++;
					}
				}
			}
		}
		protected void DeleteProduct_Click(object sender, EventArgs e)
		{
			BazaDataContext baza = new BazaDataContext();
			Button b = sender as Button;
			Int32 id = Convert.ToInt32(b.Attributes["ProduktID"]);
			foreach (Produkt_spozywczy p in baza.Produkt_spozywczies)
			{
				if (p.Id == id)
				{
					baza.Produkt_spozywczies.DeleteOnSubmit(p);
					break;
				}
			}
			baza.SubmitChanges();
			SqlCommand cmd = new SqlCommand("SELECT Id, nazwa, kalorie, weglowodany, bialka, tluszcze, blonnik, sol FROM Produkt_spozywczy", new SqlConnection(constr));
			SqlDataAdapter sda = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			sda.Fill(dt);
			RepeaterProduktow.DataSource = dt;
			RepeaterProduktow.DataBind();
		}
		protected void EditProduct_Click(object sender, EventArgs e)
		{
			edycjaProduktow.Visible = true;
			BazaDataContext baza = new BazaDataContext();
			Button b = sender as Button;
			Int32 id = Convert.ToInt32(b.Attributes["ProduktID"]);
			foreach (Produkt_spozywczy pr in baza.Produkt_spozywczies)
			{
				if (pr.Id == id)
				{
					ProduktID = id;
					TextBoxNazwa2.Text = pr.nazwa;
					DropDownListJednostka2.SelectedValue = pr.jednostka;
					TextBoxKalorie2.Text = Convert.ToString(pr.kalorie);
					TextBoxWeglowodany2.Text = Convert.ToString(pr.weglowodany);
					TextBoxBialka2.Text = Convert.ToString(pr.bialka);
					TextBoxBlonnik2.Text = Convert.ToString(pr.blonnik);
					TextBoxSol2.Text = Convert.ToString(pr.sol);
					TextBoxTluszcze2.Text = Convert.ToString(pr.tluszcze);
				}
			}
		}
		protected void EditProduct2_Click(object sender, EventArgs e)
		{
			foreach (Produkt_spozywczy p in baza.Produkt_spozywczies)
			{
				if (p.Id == ProduktID)
				{
					p.nazwa = TextBoxNazwa2.Text;
					p.jednostka = DropDownListJednostka2.SelectedValue;
					p.kalorie = float.Parse(TextBoxKalorie2.Text);
					p.weglowodany = float.Parse(TextBoxWeglowodany2.Text);
					p.bialka = float.Parse(TextBoxBialka2.Text);
					p.blonnik = float.Parse(TextBoxBlonnik2.Text);
					p.sol = float.Parse(TextBoxSol2.Text);
					p.tluszcze = float.Parse(TextBoxTluszcze2.Text);
					break;
				}
			}
			baza.SubmitChanges();
			SqlCommand cmd = new SqlCommand("SELECT Id, nazwa, kalorie, weglowodany, bialka, tluszcze, blonnik, sol FROM Produkt_spozywczy", new SqlConnection(constr));
			SqlDataAdapter sda = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			sda.Fill(dt);
			RepeaterProduktow.DataSource = dt;
			RepeaterProduktow.DataBind();
			edycjaProduktow.Visible = false;
		}

		protected void chooseClient_Click(object sender, EventArgs e)
		{
			createMenu.Visible = false;
			DivCreateDiet.Visible = true;
			Button b = sender as Button;
			Int32 ClientId = Convert.ToInt32(b.Attributes["ClientID"]);
			try
			{
				SqlConnection con = new SqlConnection(constr);
				con.Open();
				SqlCommand sql = new SqlCommand("SELECT CONCAT(imie, ' ', nazwisko) FROM Konto WHERE Id=" + ClientId.ToString(), con);
				sql.CommandType = CommandType.Text;
				LabelClient.Text = sql.ExecuteScalar() as string;
				con.Close();
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
			}
		}

		protected void DropDownListDish_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DropDownListDish.SelectedIndex == 0)
			{
				LabelPrzepis.Text = "";
			}

			try
			{
				SqlConnection con = new SqlConnection(constr);
				con.Open();
				SqlCommand sql = new SqlCommand("SELECT przepis FROM Danie WHERE Id='" + DropDownListDish.SelectedItem.Value + "'", con);
				sql.CommandType = CommandType.Text;
				LabelPrzepis.Text = sql.ExecuteScalar() as string;
				sql = new SqlCommand("SELECT p.nazwa, s.ilosc FROM Skladnik s JOIN Produkt_spozywczy p ON (s.Id_produktu=p.Id) WHERE s.Id_dania=" + DropDownListDish.SelectedItem.Value, con);
				sql.CommandType = CommandType.Text;

				TableRow r = new TableRow();
				TableCell c = new TableCell();
				c.Text = "Produkt";
				r.Cells.Add(c);
				c = new TableCell();
				c.Text = "Waga (w gramach)";
				r.Cells.Add(c);
				TableProducts.Rows.Add(r);

				SqlDataReader reader = sql.ExecuteReader();
				while (reader.Read())
				{
					TableRow r1 = new TableRow();
					TableCell c1 = new TableCell();
					c1.Text = reader.GetString(0);
					r1.Cells.Add(c1);
					c1 = new TableCell();
					c1.Text = reader.GetInt32(1).ToString();
					r1.Cells.Add(c1);
					TableProducts.Rows.Add(r1);
				}
				con.Close();
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
			}

		}

		protected void DropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownListDish.Items.Clear();
			LabelPrzepis.Text = "";
			try
			{
				SqlConnection con = new SqlConnection(constr);
				con.Open();
				SqlCommand sql = new SqlCommand("SELECT Id, nazwa FROM Danie WHERE kategoria='" + DropDownListCategory.SelectedItem.Text + "' ORDER BY nazwa", con);
				sql.CommandType = CommandType.Text;
				DropDownListDish.DataSource = sql.ExecuteReader();
				DropDownListDish.DataTextField = "nazwa";
				DropDownListDish.DataValueField = "Id";
				DropDownListDish.DataBind();
				DropDownListDish.Items.Insert(0, new ListItem("--WYBIERZ DANIE--", "0"));
				con.Close();
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
			}
		}

		protected void Calendar_Render(object sender, DayRenderEventArgs e)
		{
			if (e.Day.Date.CompareTo(DateTime.Today) <= 0)
			{
				e.Day.IsSelectable = false;
				e.Cell.ForeColor = System.Drawing.Color.Red;
			}
			else e.Cell.ForeColor = System.Drawing.Color.Green;
		}
	}
}