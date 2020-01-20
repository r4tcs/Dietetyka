using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
        static int KlientID = 0;
        static int dietetykID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Home_Page.aspx");
            }
            if (!IsPostBack)
            {
                SetInitialRow();
                ustawRepeaterKlientow();
                ustawKategoriaDanDropdownList();
            }

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand sql = new SqlCommand("SELECT CONCAT(imie, ' ', nazwisko) FROM Konto WHERE login='" + Session["username"].ToString() + "'", con);
            sql.CommandType = CommandType.Text;
            LabelName.Text = sql.ExecuteScalar() as string;
            foreach(Konto k in baza.Kontos)
            {
                if(Session["username"].ToString() == k.login)
                {
                    dietetykID = k.Id;
                }
            }
            con.Close();
        }
        protected void ustawRepeaterKlientow()
        {
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
                p.jednostka = "100g";
                p.kalorie = Math.Round(float.Parse(TextBoxKalorie.Text.Replace(',', '.'), CultureInfo.InvariantCulture),2);
                p.weglowodany = Math.Round(float.Parse(TextBoxWeglowodany.Text.Replace(',', '.'), CultureInfo.InvariantCulture), 2);
                p.bialka = Math.Round(float.Parse(TextBoxBialka.Text.Replace(',', '.'), CultureInfo.InvariantCulture), 2);
                p.blonnik = Math.Round(float.Parse(TextBoxBlonnik.Text.Replace(',', '.'), CultureInfo.InvariantCulture), 2);
                p.sol = Math.Round(float.Parse(TextBoxSol.Text.Replace(',', '.'), CultureInfo.InvariantCulture), 2);
                p.tluszcze = Math.Round(float.Parse(TextBoxTluszcze.Text.Replace(',', '.'), CultureInfo.InvariantCulture), 2);
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
                    SqlCommand sql = new SqlCommand("SELECT Id FROM Produkt_spozywczy WHERE nazwa=N'" + box1.SelectedItem.Text + "'", con);
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
            DishListDayClient.Visible = false;
            AddDishDayClient.Visible = false;
            edycjaProduktow.Visible = false;

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
            editCaloryPlan.Visible = true;
			DishListDayClient.Visible = true;
			Button b = sender as Button;
            Int32 ClientId = Convert.ToInt32(b.Attributes["ClientID"]);
            KlientID = ClientId;
            foreach(Konto k in baza.Kontos)
            {
                if(k.Id == KlientID && k.rodzaj == 'K')
                {
                    foreach(Plan_zywienia pz in baza.Plan_zywienias)
                    {
                        if (KlientID == pz.Id_konta)
                        {
                            caloryTextbox.Text = Convert.ToString(pz.kalorie);
                            weglowodanyTextbox.Text = Convert.ToString(pz.weglowodany);
                            bialkaTextBox.Text = Convert.ToString(pz.bialka);
                            blonnikTextBox.Text = Convert.ToString(pz.blonnik);
                            solTextBox.Text = Convert.ToString(pz.sol);
                            tluszczeTextBox.Text = Convert.ToString(pz.tluszcze);
                        }
                    }
                }
            }
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
            odswiezZliczanie();
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
            odswiezZliczanie();
            DropDownListDish.Items.Clear();
            LabelPrzepis.Text = "";
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand sql = new SqlCommand("SELECT Id, nazwa FROM Danie WHERE kategoria=N'" + DropDownListCategory.SelectedItem.Text + "' ORDER BY nazwa", con);
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

        protected void editCaloryPlanButton_Click(object sender, EventArgs e)
        {
			if (caloryTextbox.Enabled == true)
			{
				bool czyPlanIstnieje = false;
				foreach (Konto k in baza.Kontos)
				{
					if (k.Id == KlientID)
					{
						foreach (Plan_zywienia plz in baza.Plan_zywienias)
						{
							if (KlientID == plz.Id_konta)
							{
								plz.kalorie = Convert.ToInt32(caloryTextbox.Text);
								plz.weglowodany = Convert.ToInt32(weglowodanyTextbox.Text);
								plz.bialka = Convert.ToInt32(bialkaTextBox.Text);
								plz.blonnik = Convert.ToInt32(blonnikTextBox.Text);
								plz.sol = Convert.ToInt32(solTextBox.Text);
								plz.tluszcze = Convert.ToInt32(tluszczeTextBox.Text);
								plz.Id_konta = k.Id;
								czyPlanIstnieje = true;
								break;
							}
						}
						if (czyPlanIstnieje == false)
						{
							Plan_zywienia pz = new Plan_zywienia();
							pz.kalorie = Convert.ToInt32(caloryTextbox.Text);
							pz.weglowodany = Convert.ToInt32(weglowodanyTextbox.Text);
							pz.bialka = Convert.ToInt32(bialkaTextBox.Text);
							pz.blonnik = Convert.ToInt32(blonnikTextBox.Text);
							pz.sol = Convert.ToInt32(solTextBox.Text);
							pz.tluszcze = Convert.ToInt32(tluszczeTextBox.Text);
							pz.Id_konta = k.Id;
							baza.Plan_zywienias.InsertOnSubmit(pz);
							break;
						}
					}
				}
				baza.SubmitChanges();
				odswiezZliczanie();
				caloryTextbox.Enabled = false;
				weglowodanyTextbox.Enabled = false;
				bialkaTextBox.Enabled = false;
				blonnikTextBox.Enabled = false;
				solTextBox.Enabled = false;
				tluszczeTextBox.Enabled = false;
				editCaloryPlanButton.Text = "Edytuj";
			}
			else
			{
				editCaloryPlanButton.Text = "Zapisz";
				caloryTextbox.Enabled = true;
				weglowodanyTextbox.Enabled = true;
				bialkaTextBox.Enabled = true;
				blonnikTextBox.Enabled = true;
				solTextBox.Enabled = true;
				tluszczeTextBox.Enabled = true;
			}
        }


        protected void Calendar_Render(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.CompareTo(DateTime.Today) < 0)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Red;
            }
            else e.Cell.ForeColor = System.Drawing.Color.Green;

		}

		protected void Calendar_SelectionChanged(object sender, EventArgs e)
		{
			SqlConnection con = new SqlConnection(constr);
			con.Open();
			LabelDay.Text = Calendar.SelectedDate.ToString("dd MMMM yyyy");
			DishListDayClient.Visible = true;
			AddDishDayClient.Visible = true;
			dayDiet.Visible = true;
            SqlCommand cmd = new SqlCommand("SELECT dm.id AS dania_menuID, d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d JOIN Dania_Menu dm ON d.Id = dm.Id_dania JOIN Menu m ON dm.Id_menu = m.id WHERE CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar.SelectedDate + "', 103) AND m.id_klienta=" + KlientID, new SqlConnection(constr));
            //SqlCommand cmd = new SqlCommand("SELECT dm.id AS dania_menuID, d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d JOIN Dania_Menu dm ON d.Id = dm.Id_dania JOIN Menu m ON dm.Id_menu = m.id WHERE CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar.SelectedDate.ToShortDateString() + "', 23) AND m.id_klienta=" + KlientID, new SqlConnection(constr));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			sda.Fill(dt);
			DishListDayClientRepeater.DataSource = dt;
			DishListDayClientRepeater.DataBind();

            //ZLICZANIE
            odswiezZliczanie();
            //cmd = new SqlCommand(
            //"SELECT SUM(ps.kalorie*s.ilosc/100) Kalorie, SUM(ps.weglowodany*s.ilosc/100) Weglowodany, SUM(ps.bialka*s.ilosc/100) Bialka, SUM(ps.blonnik*s.ilosc/100) Blonnik, SUM(ps.sol*s.ilosc/100) Sol, SUM(ps.tluszcze*s.ilosc/100) Tluszcze " +
            //"FROM Produkt_spozywczy ps JOIN Skladnik s ON ps.Id = s.Id_produktu JOIN Danie d ON d.Id = s.Id_dania JOIN Dania_Menu dm ON d.Id = dm.Id_dania JOIN Menu m ON dm.Id_menu = m.id " +
            //"WHERE CONVERT(date, m.data, 103) = CONVERT(date, '" + Calendar.SelectedDate.ToShortDateString() + "', 23) AND m.id_klienta=" + KlientID, con);
            con.Close();
		}

		protected void ustawKategoriaDanDropdownList()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand sql = new SqlCommand("SELECT Id, kategoria FROM Danie ORDER BY kategoria", con);
            sql.CommandType = CommandType.Text;
            kategoriaDanDropDownList.DataSource = sql.ExecuteReader();
            kategoriaDanDropDownList.DataTextField = "kategoria";
            kategoriaDanDropDownList.DataValueField = "Id";
            kategoriaDanDropDownList.DataBind();
            kategoriaDanDropDownList.Items.Insert(0, new ListItem("-- WYBIERZ KATEGORIE DANIA --", "0"));
        }

        protected void kategoriaDanDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            daniaDropDownList.Items.Clear();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand sql = new SqlCommand("SELECT Id, nazwa FROM Danie WHERE kategoria = '" + kategoriaDanDropDownList.SelectedItem.Text + "' ORDER BY nazwa", con);
            sql.CommandType = CommandType.Text;
            daniaDropDownList.DataSource = sql.ExecuteReader();
            daniaDropDownList.DataTextField = "nazwa";
            daniaDropDownList.DataValueField = "Id";
            daniaDropDownList.DataBind();
            daniaDropDownList.Items.Insert(0, new ListItem("-- WYBIERZ DANIE --", "0"));
        }

        protected void dodajDanie_Click(object sender, EventArgs e)
        {
            bool flaga = false;
            foreach(Menu m in baza.Menus)
            {
                if(m.data == Calendar.SelectedDate && m.id_klienta == KlientID)
                {
                    flaga = true;
                }
            }
            if (flaga)
            {
                Dania_Menu dm = new Dania_Menu();
                foreach (Menu me in baza.Menus)
                {
                    if (me.data == Calendar.SelectedDate && me.id_klienta == KlientID && me.id_dietetyka == dietetykID)
                    {
                        dm.Id_menu = me.id;
                    }
                }
                foreach (Danie d in baza.Danies)
                {
                    if (d.nazwa == daniaDropDownList.SelectedItem.Text)
                    {
                        dm.Id_dania = d.Id;
                    }
                }
                baza.Dania_Menus.InsertOnSubmit(dm);
                baza.SubmitChanges();
            }
            else
            {
                Menu m = new Menu();
                m.data = Calendar.SelectedDate;
                m.id_klienta = KlientID;
                m.id_dietetyka = dietetykID;
                baza.Menus.InsertOnSubmit(m);
                baza.SubmitChanges();
                Dania_Menu dm = new Dania_Menu();
                foreach (Menu me in baza.Menus)
                {
                    if (me.data == Calendar.SelectedDate && me.id_klienta == KlientID && me.id_dietetyka == dietetykID)
                    {
                        dm.Id_menu = me.id;
                    }
                }
                foreach (Danie d in baza.Danies)
                {
                    if(d.nazwa == daniaDropDownList.SelectedItem.Text)
                    {
                        dm.Id_dania = d.Id;
                    }
                }
                baza.Dania_Menus.InsertOnSubmit(dm);
                baza.SubmitChanges();
            }
            odswiezMenu();
            odswiezZliczanie();
        }
        protected void usunDanie_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Int32 danie_menuID = Convert.ToInt32(b.Attributes["danie_menuID"]);
            foreach(Dania_Menu dm in baza.Dania_Menus)
            {
                if(dm.id == danie_menuID)
                {
                    baza.Dania_Menus.DeleteOnSubmit(dm);
                }
            }
            baza.SubmitChanges();
            odswiezMenu();
            odswiezZliczanie();
        }

        protected void odswiezMenu()
        {
            SqlCommand cmd = new SqlCommand("SELECT dm.id AS dania_menuID, d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d JOIN Dania_Menu dm ON d.Id = dm.Id_dania JOIN Menu m ON dm.Id_menu = m.id WHERE CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar.SelectedDate + "', 103) AND m.id_klienta=" + KlientID, new SqlConnection(constr));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DishListDayClientRepeater.DataSource = dt;
            DishListDayClientRepeater.DataBind();
        }

        protected void odswiezZliczanie()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand(
            "SELECT SUM(ps.kalorie*s.ilosc/100) Kalorie, SUM(ps.weglowodany*s.ilosc/100) Weglowodany, SUM(ps.bialka*s.ilosc/100) Bialka, SUM(ps.blonnik*s.ilosc/100) Blonnik, SUM(ps.sol*s.ilosc/100) Sol, SUM(ps.tluszcze*s.ilosc/100) Tluszcze " +
            "FROM Produkt_spozywczy ps JOIN Skladnik s ON ps.Id = s.Id_produktu JOIN Danie d ON d.Id = s.Id_dania JOIN Dania_Menu dm ON d.Id = dm.Id_dania JOIN Menu m ON dm.Id_menu = m.id " +
            "WHERE CONVERT(date, m.data, 103) = CONVERT(date, '" + Calendar.SelectedDate + "', 103) AND m.id_klienta=" + KlientID, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
			TableRow r = new TableRow();
			TableCell c = new TableCell();
			c.Text = "";
			r.Cells.Add(c);
			c = new TableCell();
			c.Text = "Aktualne";
			r.Cells.Add(c);
			c = new TableCell();
			c.Text = "Zapotrzebowanie";
			r.Cells.Add(c);
			c = new TableCell();
			c.Text = "Jednostka";
			r.Cells.Add(c);
			TableZliczoneWartosci.Rows.Add(r);
			while (reader.Read())
            {
                for (int i = 0; i < 6; i++)
                {
                    if (reader.IsDBNull(i))
                    {
                        //pozostaleKalorie.Text = "";
                        //pozostaleWeglowodany.Text = "";
                        //pozostaleBialka.Text = "";
                       // pozostalyBlonnik.Text = "";
                        //pozostalaSol.Text = "";
                       // pozostaleTluszcze.Text = "";
                        return;
                    }
                        
                    TableRow r1 = new TableRow();
                    TableCell c1 = new TableCell();
					c1.Text = reader.GetName(i);
                    r1.Cells.Add(c1);
                    c1 = new TableCell();
					c1.Text = reader.GetDouble(i).ToString();
					r1.Cells.Add(c1);
					c1 = new TableCell();
					if (i == 0)
                    {
                        //double sumaKalorii = Convert.ToDouble(c1.Text);
                        double kalorie = Convert.ToDouble(caloryTextbox.Text);
                        //pozostaleKalorie.Text = Convert.ToString(kalorie - sumaKalorii);
						c1.Text = kalorie.ToString();
					}
                    if(i == 1)
                    {
                        //double sumaWeglowodanow = Convert.ToDouble(c1.Text);
                        double weglowodany = Convert.ToDouble(weglowodanyTextbox.Text);
						//pozostaleWeglowodany.Text = Convert.ToString(weglowodany - sumaWeglowodanow);
						c1.Text = weglowodany.ToString();
					}
                    if(i == 2)
                    {
                       // double sumaBialek = Convert.ToDouble(c1.Text);
                        double bialka = Convert.ToDouble(bialkaTextBox.Text);
						//pozostaleBialka.Text = Convert.ToString(bialka - sumaBialek);
						c1.Text = bialka.ToString();
					}
                    if(i == 3)
                    {
                        //double sumaBlonnika = Convert.ToDouble(c1.Text);
                        double blonnik = Convert.ToDouble(blonnikTextBox.Text);
						//pozostalyBlonnik.Text = Convert.ToString(blonnik - sumaBlonnika);
						c1.Text = blonnik.ToString();
					}
                    if(i == 4)
                    {
                        //double sumaSoli = Convert.ToDouble(c1.Text);
                        double sol = Convert.ToDouble(solTextBox.Text);
						// pozostalaSol.Text = Convert.ToString(sol - sumaSoli);
						c1.Text = sol.ToString();
					}
                    if(i == 5)
                    {
                        //double sumaTluszcze = Convert.ToDouble(c1.Text);
                        double tluszcze = Convert.ToDouble(tluszczeTextBox.Text);
						//pozostaleTluszcze.Text = Convert.ToString(tluszcze - sumaTluszcze);
						c1.Text = tluszcze.ToString();
					}
					r1.Cells.Add(c1);
					
					if (reader.GetDouble(i)/Convert.ToDouble(c1.Text) < 0.9 || reader.GetDouble(i) / Convert.ToDouble(c1.Text) > 1.1)
						r1.BackColor = System.Drawing.Color.Red;
					else
						r1.BackColor = System.Drawing.Color.Green;
					c1 = new TableCell();
					if (reader.GetName(i) != "Kalorie")
						c1.Text = "g";
					else
						c1.Text = "kcal";
					r1.Cells.Add(c1);
					TableZliczoneWartosci.Rows.Add(r1);
				}
            }
            con.Close();
        }
    }
}