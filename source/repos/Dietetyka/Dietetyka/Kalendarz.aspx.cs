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
        string confirmedDate = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Home_Page.aspx");
            }
            if(!IsPostBack)
            {
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

        protected void ButtonKomentarz_Click(object sender, EventArgs e)
        {
            Response.Redirect("Komentarz.aspx");
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
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT id, data, nazwa FROM Menu WHERE data='" + Calendar.SelectedDate +"' ORDER BY data", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            RepeaterKalendarz.DataSource = dt;
            RepeaterKalendarz.DataBind();
        }
    }
}