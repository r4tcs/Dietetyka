<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Interface.aspx.cs" Inherits="Dietetyka.Admin_Interface" %>
<!DOCTYPE html>
<html lang="en">

<head>
  <title>Home Page</title>
  <link href="css/css.css" rel="stylesheet">
   <link href="css/css.min.css" rel="stylesheet" type="text/css">
</head>

<body id="page-top">
    <form id="form1" runat="server">
  <nav class="navbar navbar-expand navbar-dark bg-dark static-top">

    <a class="navbar-brand mr-1" href="Home_Page.aspx">Diet Manager</a>
    <!-- Navbar -->
    <ul class="navbar-nav ml-auto ml-md-0">
      <li class="nav-item dropdown no-arrow mx-1" runat="server">
      <asp:Button ID="Options_button" runat="server" Text="Options" OnClick="Options_button_Click" />
      </li>
      <li class="nav-item dropdown no-arrow mx-1" runat="server">
      <asp:Button ID="Messages_button" runat="server" Text="Messages" OnClick="Messages_button_Click" />
      </li>
    </ul>

  </nav>

  <div id="wrapper">

    <ul class="sidebar navbar-nav">
      <li class="nav-item">
          <span>Side Bar</span>
      </li>
    </ul>

    <div id="content-wrapper">

      <div class="container-fluid">

        <!-- Page Content -->
        <h1>Admin Interface Web Page</h1>
        <hr>
          <div class="form-group">
				<label for="rejestracja"><b>Rejestracja dietetyka</b></label><br />
			</div>
			<div class="form-group">
				<label for="imie">Imię:</label><br />
				<asp:TextBox ID="textboxImie" runat="server" Width="400px"></asp:TextBox><br />
				<asp:RegularExpressionValidator
					ID="ImieWalidator" runat="server" ErrorMessage="Podaj poprawne imię"
					ControlToValidate="textboxImie" ValidationExpression="[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ'.\s]{1,50}"
					ForeColor="Red" Display="Dynamic" ValidationGroup="register"></asp:RegularExpressionValidator>
				<asp:RequiredFieldValidator ID="RequiredImie" ControlToValidate="textboxImie" Display="Dynamic"
					runat="server" ForeColor="Red" ErrorMessage="Podaj imię!" ValidationGroup="register"></asp:RequiredFieldValidator>
			</div>
			<div class="form-group">
				<label for="nazwisko">Nazwisko:</label><br />
				<asp:TextBox ID="textboxNazwisko" runat="server" Width="400px"></asp:TextBox><br />
				<asp:RegularExpressionValidator
					ID="NazwiskoWalidator" runat="server" ErrorMessage="Podaj poprawne nazwisko"
					ControlToValidate="textboxNazwisko" ValidationExpression="[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ'.\s]{1,50}"
					ForeColor="Red" Display="Dynamic" ValidationGroup="register"></asp:RegularExpressionValidator>
					<asp:RequiredFieldValidator ID="RequiredNazwisko" ControlToValidate="textboxNazwisko" Display="Dynamic"
						runat="server" ForeColor="Red" ErrorMessage="Podaj nazwisko!" ValidationGroup="register"></asp:RequiredFieldValidator>
			</div>
			<div class="form-group">
				<label for="login">Nazwa użytkownika:</label><br />
				<asp:TextBox ID="textboxLogin" runat="server" Width="400px"></asp:TextBox><br />
				<asp:RequiredFieldValidator ID="RequiredLogin" ControlToValidate="textboxLogin" Display="Dynamic"
						runat="server" ForeColor="Red" ErrorMessage="Podaj login!" ValidationGroup="register"></asp:RequiredFieldValidator>
			</div>
			<div class="form-group">
				<label for="haslo">Hasło:</label><br />
				<input id="textboxHaslo" runat="server" type="password" class="passwordStyle" /><br />
				<asp:RequiredFieldValidator ID="RequiredPassword" ControlToValidate="textboxHaslo" Display="Dynamic"
						runat="server" ForeColor="Red" ErrorMessage="Podaj hasło!" ValidationGroup="register"></asp:RequiredFieldValidator>
			</div>
			<div class="form-group">
				<label for="haslo">Powtórz hasło:</label><br />
				<input id="textboxHaslo2" runat="server" type="password" class="passwordStyle" /><br />
				<asp:RequiredFieldValidator ID="RequiredPassword2" ControlToValidate="textboxHaslo2" Display="Dynamic"
						runat="server" ForeColor="Red" ErrorMessage="Podaj hasło!" ValidationGroup="register"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" ErrorMessage="Podane hasła muszą być takie same!" 
					ValidationGroup="register" ControlToValidate="textboxHaslo" 
					Operator="Equal" ControlToCompare="textboxHaslo2" Display="Dynamic"></asp:CompareValidator>
			</div>

			<div class="form-group">
				<label for="telefon">Numer telefonu:</label><br />
				<asp:TextBox ID="textboxTelefon" runat="server" Width="400px"></asp:TextBox><br />
				<asp:RegularExpressionValidator
					ID="TelefonValidator" runat="server" ErrorMessage="Podaj poprawny numer"
					ControlToValidate="textboxTelefon" ValidationExpression="[0-9]{9}"
					ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
					<asp:RequiredFieldValidator ID="RequiredTelefon" ControlToValidate="textboxTelefon" Display="Dynamic"
						runat="server" ForeColor="Red" ErrorMessage="Podaj numer telefonu!" ValidationGroup="register"></asp:RequiredFieldValidator>
			</div>
			<div class="utworzKonto">
				<asp:Button ID="utworzKonto" runat="server" Width="150px" Text="Utwórz konto" OnClick="utworzKonto_Click" ValidationGroup="register"></asp:Button><br />
			</div>
      </div>

    </div>

  </div>

        </form>
</body>

</html>
