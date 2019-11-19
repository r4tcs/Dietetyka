<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Home_Page.aspx.cs" Inherits="Dietetyka.Home_Page" %>

<!DOCTYPE html>
<html lang="en">

<head>
	<title>Home Page</title>
	<link href="css/css.css" rel="stylesheet">
	<link href="css/css.min.css" rel="stylesheet" type="text/css">
	<style type="text/css">
		.auto-style1 {
			width: 400px;
		}
	</style>
</head>

<body id="page-top">
	<form id="form1" runat="server">
		<nav class="navbar navbar-expand navbar-dark bg-dark static-top">

			<a class="navbar-brand mr-1" href="Home_Page.aspx">Diet Manager</a>
			<!-- Navbar -->
			<ul class="navbar-nav ml-auto ml-md-0">
				<li class="nav-item dropdown no-arrow mx-1" runat="server">
					<asp:Button ID="Registration_Log_In_button" runat="server" Text="Zarejestruj się!" OnClick="Registration_Log_In_button_Click"></asp:Button>
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
					<h1>Logowanie</h1>
					<hr>
					<div class="left">
						<div class="form-group">
							<label for="imie" class="auto-style1">Nazwa użytkownika:</label><br />
							<asp:TextBox ID="logNazwa" runat="server" Width="400px"></asp:TextBox><br />
							<asp:RequiredFieldValidator ID="RequiredLogLogin" ControlToValidate="logNazwa" Display="Dynamic"
								runat="server" ForeColor="Red" ErrorMessage="Podaj login!" ValidationGroup="login"></asp:RequiredFieldValidator>
						</div>
						<div class="form-group">
							<label for="haslo">Hasło:</label><br />
							<input id="logHaslo" runat="server" type="password" class="auto-style1" /><br />
							<asp:RequiredFieldValidator ID="RequiredLogPassword" ControlToValidate="logHaslo" Display="Dynamic"
								runat="server" ForeColor="Red" ErrorMessage="Podaj hasło!" ValidationGroup="login"></asp:RequiredFieldValidator>
						</div>
						<div class="zaloguj">
							<asp:Button ID="zaloguj" runat="server" Width="100px" Text="Zaloguj" OnClick="zaloguj_Click" ValidationGroup="login"></asp:Button><br />
						</div>
					</div>
				</div>

			</div>

		</div>

	</form>
</body>

</html>
