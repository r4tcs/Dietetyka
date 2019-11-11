<%@ Page Title="Login Regestration Page" Language="C#" AutoEventWireup="true" CodeBehind="Login_Regestration_Page.aspx.cs" Inherits="Dietetyka._Login_Regestration_Page" %>

<!DOCTYPE html>

<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
<link rel="Stylesheet" href="css/master.css" type="text/css" />
<link href="css/css.css" rel="stylesheet">
<link href="css/css.min.css" rel="stylesheet" type="text/css">
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
		margin: 0px 300px;
	}

	.utworzKonto {
		margin: 0px 250px;
	}

	.passwordStyle {
		width: 400px;
	}
</style>

<html xmlns="http://www.w3.org/1999/xhtml">

<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
<head runat="server">
	<title>Log In and Regestration</title>
</head>
<body id="page-top">
    <form id="form2" runat="server">
  <nav class="navbar navbar-expand navbar-dark bg-dark static-top">

    <a class="navbar-brand mr-1" href="Home_Page.aspx">Diet Manager</a>
    <!-- Navbar -->
    <ul class="navbar-nav ml-auto ml-md-0">
      <li class="nav-item dropdown no-arrow mx-1" runat="server">
      <asp:Button ID="Registration_Log_In_button" runat="server" Text="Registration \ Log In"  > </asp:Button><br />
      </li>
      <li class="nav-item dropdown no-arrow mx-1" runat="server">
      <asp:Button ID="Options_button" runat="server" Text="Options"  />
      </li>
      <li class="nav-item dropdown no-arrow mx-1" runat="server">
      <asp:Button ID="Messages_button" runat="server" Text="Messages"  />
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

        <div class="left">
			<div class="form-group">
				<label for="logowanie">Logowanie</label><br />
			</div>
			<div class="form-group">
				<label for="imie">Nazwa użytkownika:</label><br />
				<asp:TextBox ID="logNazwa" runat="server" Width="400px"></asp:TextBox><br />
				<asp:RequiredFieldValidator ID="RequiredLogLogin" ControlToValidate="logNazwa" Display="Dynamic"
						runat="server" ForeColor="Red" ErrorMessage="Podaj login!" ValidationGroup="login"></asp:RequiredFieldValidator>
			</div>
			<div class="form-group">
				<label for="haslo">Hasło:</label><br />
				<input id="logHaslo" runat="server" Width="400px" type="password" class="passwordStyle"/>
				<asp:RequiredFieldValidator ID="RequiredLogPassword" ControlToValidate="logHaslo" Display="Dynamic"
						runat="server" ForeColor="Red" ErrorMessage="Podaj hasło!" ValidationGroup="login"></asp:RequiredFieldValidator>
			</div>
			<div class="zaloguj">
				<asp:Button ID="zaloguj" runat="server" Width="100px" Text="Zaloguj" OnClick="zaloguj_Click" ValidationGroup="login" ></asp:Button><br />
			</div>
		</div>


		<div class="right">
			<div class="form-group">
				<label for="rejestracja">Nie masz konta ? Zarejestruj się !</label><br />
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

  </div>

        </form>
</body>

</html>
