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
    public partial class Klient_ShopList : System.Web.UI.Page
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
            Calendar_ShopList.SelectedDate = System.DateTime.Today;


            con.Close();
        }

        protected void Calendar_ShopList_SelectionChanged(object sender, EventArgs e)
        {
            //SqlCommand cmd = new SqlCommand("SELECT ps.nazwa Nazwa, s.ilosc Ilosc, ps.jednostkamiary JednostkaMiary FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND m.data='" + Calendar_ShopList.SelectedDate + "' ", new SqlConnection(constr));
            //SqlCommand cmd = new SqlCommand("SELECT ps.nazwa Nazwa, s.ilosc Ilosc, ps.jednostkamiary JednostkaMiary FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar_ShopList.SelectedDate + "', 103)", new SqlConnection(constr));
            SqlCommand cmd = new SqlCommand("SELECT ps.nazwa Nazwa, s.ilosc Ilosc, ps.jednostkamiary JednostkaMiary FROM Produkt_spozywczy ps, Skladnik s, Danie d, Dania_Menu dm, Menu m, Konto k WHERE k.login='" + Session["username"].ToString() + "' AND m.id = dm.Id_menu AND d.Id = dm.Id_dania AND d.Id = s.Id_dania AND s.Id_produktu = ps.Id AND CONVERT(date, m.data, 103)= CONVERT(date, '" + Calendar_ShopList.SelectedDate.ToShortDateString() + "', 23)", new SqlConnection(constr));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Repeater_ShopList.DataSource = dt;
            Repeater_ShopList.DataBind();
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

        protected void Calendar_Statystyka_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime eventDate;
            Calendar_ShopList.SelectedDayStyle.BackColor = System.Drawing.Color.Gray;
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

        protected void ButtonKlient_ShopList_Click(object sender, EventArgs e)
        {
            Response.Redirect("Klient_ShopList.aspx");
        }
    }
}