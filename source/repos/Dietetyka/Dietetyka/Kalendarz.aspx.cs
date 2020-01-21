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
    public partial class Kalendarz : System.Web.UI.Page
    {
        BazaDataContext baza = new BazaDataContext();
        string constr = ConfigurationManager.ConnectionStrings["BazaConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Home_Page.aspx");
            }
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("SELECT d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d, Dania_Menu dm, Menu m, Konto k WHERE d.Id=dm.Id_dania AND dm.Id_menu=m.id AND k.Id = m.id_klienta AND k.login='" + Session["username"].ToString() + "' AND m.data='" + Calendar.SelectedDate + "' ORDER BY 2", new SqlConnection(constr));
                //SqlCommand cmd = new SqlCommand("SELECT d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d, Dania_Menu dm, Menu m, Konto k WHERE d.Id=dm.Id_dania AND dm.Id_menu=m.id AND k.Id = m.id_klienta AND k.login='" + Session["username"].ToString() + "' AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar.SelectedDate + "', 103) ORDER BY 2", new SqlConnection(constr));
                //SqlCommand cmd = new SqlCommand("SELECT d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d, Dania_Menu dm, Menu m, Konto k WHERE d.Id=dm.Id_dania AND dm.Id_menu=m.id AND k.Id = m.id_klienta AND k.login='" + Session["username"].ToString() + "' AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar.SelectedDate.ToShortDateString() + "', 23) ORDER BY 2", new SqlConnection(constr));
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                RepeaterKalendarz.DataSource = dt;
                RepeaterKalendarz.DataBind();

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

        protected void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            Label_Data.Text = "Twoje Menu na " + Calendar.SelectedDate.ToShortDateString();
            SqlCommand cmd = new SqlCommand("SELECT d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d, Dania_Menu dm, Menu m, Konto k WHERE d.Id=dm.Id_dania AND dm.Id_menu=m.id AND k.Id = m.id_klienta AND k.login='" + Session["username"].ToString() + "' AND m.data='" + Calendar.SelectedDate + "' ORDER BY 2", new SqlConnection(constr));
            //SqlCommand cmd = new SqlCommand("SELECT d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d, Dania_Menu dm, Menu m, Konto k WHERE d.Id=dm.Id_dania AND dm.Id_menu=m.id AND k.Id = m.id_klienta AND k.login='" + Session["username"].ToString() + "' AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar.SelectedDate + "', 103) ORDER BY 2", new SqlConnection(constr));
            //SqlCommand cmd = new SqlCommand("SELECT d.Id, d.nazwa, d.kategoria, d.przepis FROM Danie d, Dania_Menu dm, Menu m, Konto k WHERE d.Id=dm.Id_dania AND dm.Id_menu=m.id AND k.Id = m.id_klienta AND k.login='" + Session["username"].ToString() + "' AND CONVERT(date, m.data, 103)=CONVERT(date, '" + Calendar.SelectedDate.ToShortDateString() + "', 23) ORDER BY 2", new SqlConnection(constr));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            RepeaterKalendarz.DataSource = dt;
            RepeaterKalendarz.DataBind();
            KalendarzListDiv.Visible = true;
            SkladnikiListDiv.Visible = false;
        }


        protected void ButtonPokazSkladniki_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Int32 id = Convert.ToInt32(b.Attributes["DanieID"]);
            SqlCommand cmd = new SqlCommand("SELECT ps.Id, ps.nazwa, s.ilosc FROM Produkt_spozywczy ps, Skladnik s, Danie d WHERE d.Id=s.Id_dania AND ps.Id=s.Id_produktu AND s.Id_dania =" + id + " ", new SqlConnection(constr));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            RepeaterSkladniki.DataSource = dt;
            RepeaterSkladniki.DataBind();
            SkladnikiListDiv.Visible = true;
        }

        protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime eventDate;
            Calendar.SelectedDayStyle.BackColor = System.Drawing.Color.Gray;
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