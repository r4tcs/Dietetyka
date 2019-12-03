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
            if(!IsPostBack)
            {
				SetInitialRow();
				SqlCommand cmd = new SqlCommand("SELECT Id, nazwa, kalorie, weglowodany, bialka, tluszcze, blonnik, sol FROM Produkt_spozywczy", new SqlConnection(constr));
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                RepeaterProduktow.DataSource = dt;
                RepeaterProduktow.DataBind();
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
					s.Id_produktu = Convert.ToInt32(box1.SelectedItem.Value);
					TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBoxWeight");
					s.ilosc = Convert.ToInt32(box2.Text);
					baza.Skladniks.InsertOnSubmit(s);
					baza.SubmitChanges();
					rowIndex++;
				}
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
			SetInitialRow();
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
    }
}