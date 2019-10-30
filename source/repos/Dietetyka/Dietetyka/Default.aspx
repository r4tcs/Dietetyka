<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Dietetyka._Default" %>

<!DOCTYPE html>

<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
<link rel="Stylesheet" href="css/master.css" type="text/css" />

<style type="text/css">

.left {
  float: left;
  width: 125px;
  text-align: left;
  margin: 50px 200px;
  display: inline;
}

.right {
  float: right;
  text-align: left;
  margin: 50px 100px;
  display: inline;
}

.zaloguj {
    margin: 0px 400px;
}

.utworzKonto
{
    margin: 0px 350px;
}

</style>

<html xmlns="http://www.w3.org/1999/xhtml">

<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
<head runat="server">
    <title>Logowanie</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="left">
            <div class="form-group">
                <label for="logowanie">Logowanie</label><br />
            </div>
            <div class="form-group">
                <label for="imie">Nazwa użytkownika:</label><br />
                <asp:TextBox ID="logNazwa" runat="server" Width="500px"></asp:TextBox><br />
            </div>
            <div class="form-group">
                <label for="haslo">Hasło:</label><br />
                <asp:TextBox ID="logHaslo" runat="server" Width="500px"></asp:TextBox><br />
            </div>
            <div class="zaloguj">
                <asp:Button ID="zaloguj" runat="server" Width="100px" Text="Zaloguj"></asp:Button><br />
            </div>
        </div>

        
        <div class="right">
            <div class="form-group">
                <label for="rejestracja">Nie masz konta ? Zarejestruj się !</label><br />
            </div>
            <div class="form-group">
                <label for="imie">Imię:</label><br />
                <asp:TextBox ID="textboxImie" runat="server" Width="500px"></asp:TextBox><br />
                <asp:RegularExpressionValidator 
                    ID="ImieWalidator" runat="server" ErrorMessage="Podaj poprawne imię" 
                    ControlToValidate="textboxImie" ValidationExpression="[a-zA-Z'.\s]{1,50}" 
                    ForeColor="#CC3300" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label for="nazwisko">Nazwisko:</label><br />
                <asp:TextBox ID="textboxNazwisko" runat="server" Width="500px"></asp:TextBox><br />
                <asp:RegularExpressionValidator 
                    ID="NazwiskoWalidator" runat="server" ErrorMessage="Podaj poprawne nazwisko" 
                    ControlToValidate="textboxNazwisko" ValidationExpression="[a-zA-Z'.\s]{1,50}" 
                    ForeColor="#CC3300" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label for="rodzaj">Rodzaj:</label><br />
                <asp:dropdownlist ID="listaRodzaj" runat="server">
                    <asp:ListItem Value="Dietetyk"> Dietetyk </asp:ListItem>
                    <asp:ListItem Value="Klient"> Klient </asp:ListItem>
                </asp:dropdownlist>
            </div>
            <div class="form-group">
                <label for="login2">Nazwa użytkownika:</label><br />
                <asp:TextBox ID="textboxLogin2" runat="server" Width="500px"></asp:TextBox><br />
             </div>
             <div class="form-group">
                <label for="haslo2">Hasło:</label><br />
                <asp:TextBox ID="textboxHaslo2" runat="server" Width="500px"></asp:TextBox><br />
                 </div>

         <div class="form-group">
                <label for="telefon">Numer telefonu:</label><br />
                <asp:TextBox ID="textboxTelefon" runat="server" Width="500px"></asp:TextBox><br />
                <asp:RegularExpressionValidator 
                    ID="TelefonValidator" runat="server" ErrorMessage="Podaj poprawny numer" 
                    ControlToValidate="textboxTelefon" ValidationExpression="[0-9]{9}" 
                    ForeColor="#CC3300" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
            <div class="utworzKonto">
                <asp:Button ID="utworzKonto" runat="server" Width="150px" Text="Utwórz konto"></asp:Button><br />
            </div>
                 
        </div>
    </form>
        
</body>
</html>
