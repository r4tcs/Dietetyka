using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Dietetyka
{
    public partial class Statystyka : System.Web.UI.Page
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
            Calendar_Statystyka.SelectedDate = System.DateTime.Today;

            fillChart_allTime();

            con.Close();


        }

        private void fillChart_allTime()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id", con);
            DataSet ds = new DataSet();
            chart_sql.Fill(ds);
            Statystyka_Chart.DataSource = ds;

            Statystyka_Chart.Series["Kalorie"].YValueMembers = "Kalorie";
            Statystyka_Chart.Series["Kalorie"].Legend = "Kalorie";
            Statystyka_Chart.Series["Weglowodany"].YValueMembers = "Weglowodany";
            Statystyka_Chart.Series["Weglowodany"].Legend = "Kalorie";
            Statystyka_Chart.Series["Bialka"].YValueMembers = "Bialka";
            Statystyka_Chart.Series["Bialka"].Legend = "Kalorie";
            Statystyka_Chart.Series["Blonnik"].YValueMembers = "Blonnik";
            Statystyka_Chart.Series["Blonnik"].Legend = "Kalorie";
            Statystyka_Chart.Series["Sol"].YValueMembers = "Sol";
            Statystyka_Chart.Series["Sol"].Legend = "Kalorie";
            Statystyka_Chart.Series["Tluszcze"].YValueMembers = "Tluszcze";
            Statystyka_Chart.Series["Tluszcze"].Legend = "Kalorie";

            con.Close();

        }

        private void fillChart_selectedDay()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND m.data='" + Calendar_Statystyka.SelectedDate + "' ", new SqlConnection(constr));
            SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar_Statystyka.SelectedDate + "', 103)", new SqlConnection(constr));
            //SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)= CONVERT(date, '" + Calendar_Statystyka.SelectedDate.ToShortDateString() + "', 23)", new SqlConnection(constr));

            DataSet ds = new DataSet();
            chart_sql.Fill(ds);
            Statystyka_Chart.DataSource = ds;

            Statystyka_Chart.Series["Kalorie"].YValueMembers = "Kalorie";
            Statystyka_Chart.Series["Kalorie"].Legend = "Kalorie";
            Statystyka_Chart.Series["Weglowodany"].YValueMembers = "Weglowodany";
            Statystyka_Chart.Series["Weglowodany"].Legend = "Kalorie";
            Statystyka_Chart.Series["Bialka"].YValueMembers = "Bialka";
            Statystyka_Chart.Series["Bialka"].Legend = "Kalorie";
            Statystyka_Chart.Series["Blonnik"].YValueMembers = "Blonnik";
            Statystyka_Chart.Series["Blonnik"].Legend = "Kalorie";
            Statystyka_Chart.Series["Sol"].YValueMembers = "Sol";
            Statystyka_Chart.Series["Sol"].Legend = "Kalorie";
            Statystyka_Chart.Series["Tluszcze"].YValueMembers = "Tluszcze";
            Statystyka_Chart.Series["Tluszcze"].Legend = "Kalorie";

            con.Close();

        }

        private void fillChart_selectedWeek()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            
            //SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND m.data='" + Calendar_Statystyka.SelectedDate.Month + "' ", new SqlConnection(constr));
            SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar_Statystyka.SelectedDate + "', 103)", new SqlConnection(constr));
            //SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)= CONVERT(date, '" + Calendar_Statystyka.SelectedDate.ToShortDateString() + "', 23)", new SqlConnection(constr));

            DataSet ds = new DataSet();
            chart_sql.Fill(ds);
            Statystyka_Chart.DataSource = ds;

            Statystyka_Chart.Series["Kalorie"].YValueMembers = "Kalorie";
            Statystyka_Chart.Series["Kalorie"].Legend = "Kalorie";
            Statystyka_Chart.Series["Weglowodany"].YValueMembers = "Weglowodany";
            Statystyka_Chart.Series["Weglowodany"].Legend = "Kalorie";
            Statystyka_Chart.Series["Bialka"].YValueMembers = "Bialka";
            Statystyka_Chart.Series["Bialka"].Legend = "Kalorie";
            Statystyka_Chart.Series["Blonnik"].YValueMembers = "Blonnik";
            Statystyka_Chart.Series["Blonnik"].Legend = "Kalorie";
            Statystyka_Chart.Series["Sol"].YValueMembers = "Sol";
            Statystyka_Chart.Series["Sol"].Legend = "Kalorie";
            Statystyka_Chart.Series["Tluszcze"].YValueMembers = "Tluszcze";
            Statystyka_Chart.Series["Tluszcze"].Legend = "Kalorie";

            con.Close();

        }

        private void fillChart_selectedDates()
        {
          
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND m.data='" + Calendar_Statystyka.SelectedDate + "' ", new SqlConnection(constr));
            SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar_Statystyka.SelectedDate + "', 103)", new SqlConnection(constr));
            //SqlDataAdapter chart_sql = new SqlDataAdapter("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)= CONVERT(date, '" + Calendar_Statystyka.SelectedDate.ToShortDateString() + "', 23)", new SqlConnection(constr));

            DataSet ds = new DataSet();
            chart_sql.Fill(ds);
            Statystyka_Chart.DataSource = ds;

            Statystyka_Chart.Series["Kalorie"].YValueMembers = "Kalorie";
            Statystyka_Chart.Series["Kalorie"].Legend = "Kalorie";
            Statystyka_Chart.Series["Weglowodany"].YValueMembers = "Weglowodany";
            Statystyka_Chart.Series["Weglowodany"].Legend = "Kalorie";
            Statystyka_Chart.Series["Bialka"].YValueMembers = "Bialka";
            Statystyka_Chart.Series["Bialka"].Legend = "Kalorie";
            Statystyka_Chart.Series["Blonnik"].YValueMembers = "Blonnik";
            Statystyka_Chart.Series["Blonnik"].Legend = "Kalorie";
            Statystyka_Chart.Series["Sol"].YValueMembers = "Sol";
            Statystyka_Chart.Series["Sol"].Legend = "Kalorie";
            Statystyka_Chart.Series["Tluszcze"].YValueMembers = "Tluszcze";
            Statystyka_Chart.Series["Tluszcze"].Legend = "Kalorie";

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

        protected void ButtonKalendarz_Click(object sender, EventArgs e)
        {
            Response.Redirect("Kalendarz.aspx");
        }

        protected void ButtonStatystyka_Click(object sender, EventArgs e)
        {
            Response.Redirect("Statystyka.aspx");
        }

        protected void Statystyka_Chart_Load(object sender, EventArgs e)
        {
            fillChart_allTime();
        }

        protected void Calendar_Statystyka_SelectionChanged(object sender, EventArgs e)
        {
            //SqlCommand cmd = new SqlCommand("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND m.data='" + Calendar_Statystyka.SelectedDate + "' ", new SqlConnection(constr));
            SqlCommand cmd = new SqlCommand("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar_Statystyka.SelectedDate + "', 103)", new SqlConnection(constr));
            //SqlCommand cmd = new SqlCommand("SELECT SUM(ps.kalorie) Kalorie, SUM(ps.weglowodany) Weglowodany, SUM(ps.bialka) Bialka, SUM(ps.blonnik) Blonnik, SUM(ps.sol) Sol, SUM(ps.tluszcze) Tluszcze FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)= CONVERT(date, '" + Calendar_Statystyka.SelectedDate.ToShortDateString() + "', 23)", new SqlConnection(constr));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Repeater_Statystyka.DataSource = dt;
            Repeater_Statystyka.DataBind();
            fillChart_selectedDates();
        }






        protected void Calendar_Statystyka_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime eventDate;
            Calendar_Statystyka.SelectedDayStyle.BackColor = System.Drawing.Color.Gray;
            DataTable dt = GetDates();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    eventDate = Convert.ToDateTime(dt.Rows[i]["data"]);
                    if (e.Day.Date == eventDate)
                    {
                        e.Cell.BackColor = System.Drawing.Color.Red;
                    }

                }
            }
        }

        public DataTable GetDates()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(constr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT m.data FROM Menu m, Konto k WHERE m.id_klienta= k.Id AND k.login='" + Session["username"].ToString() + "'", con);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(dt);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }

        protected void bt_fillChart_allTime_Click(object sender, EventArgs e)
        {
            fillChart_allTime();
        }

        protected void bt_fillChart_week_Click(object sender, EventArgs e)
        {
            fillChart_selectedWeek();
        }

        protected void ButtonKlient_ShopList_Click(object sender, EventArgs e)
        {
            Response.Redirect("Klient_ShopList.aspx");
        }
    }
}