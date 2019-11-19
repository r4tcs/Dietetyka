﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dietecian_Interface.aspx.cs" Inherits="Dietetyka.Dietecian_Interface" %>

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
					<h1>Dodawanie produktów</h1>
					
		<div class="form-group">
			<div class="form-group">Nazwa <br /><asp:TextBox ID="TextBoxNazwa" runat="server"></asp:TextBox><br />
			<asp:RegularExpressionValidator
					ID="NazwaWalidator" runat="server" ErrorMessage="Podaj poprawną nazwę"
					ControlToValidate="textboxNazwa" ValidationExpression="[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ'.\s]{1,50}"
					ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"></asp:RegularExpressionValidator>
			</div>

			<div class="form-group">Jednostka <br /><asp:DropDownList ID="DropDownListJednostka" runat="server" Width="175px">
				<asp:ListItem>100g</asp:ListItem>
				<asp:ListItem>szt</asp:ListItem>
			</asp:DropDownList><br /></div>
			
			<div class="form-group">Kalorie <br /><asp:TextBox ID="TextBoxKalorie" runat="server"></asp:TextBox>kcal<br />
				<asp:RangeValidator ID="RangeValidatorKalorie" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct" 
					MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxKalorie" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
			</div>

			<div class="form-group">Węglowodany <br /><asp:TextBox ID="TextBoxWeglowodany" runat="server"></asp:TextBox>g<br />
				<asp:RangeValidator ID="RangeValidatorWeglowodany" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct" 
					MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxWeglowodany" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
			</div>

			<div class="form-group">Białka <br /><asp:TextBox ID="TextBoxBialka" runat="server"></asp:TextBox>g<br />
				<asp:RangeValidator ID="RangeValidatorBialka" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct" 
					MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxBialka" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
			</div>

			<div class="form-group">Błonnik <br /><asp:TextBox ID="TextBoxBlonnik" runat="server"></asp:TextBox>g<br />
				<asp:RangeValidator ID="RangeValidatorBlonnik" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct" 
					MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxBlonnik" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
			</div>

			<div class="form-group">Sól <br /><asp:TextBox ID="TextBoxSol" runat="server"></asp:TextBox>g<br />
				<asp:RangeValidator ID="RangeValidatorSol" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct" 
					MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxSol" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
			</div>

			<div class="form-group">Tłuszcze <br /><asp:TextBox ID="TextBoxTluszcze" runat="server"></asp:TextBox>g<br />
				<asp:RangeValidator ID="RangeValidatorTluszcze" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct" 
					MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxTluszcze" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
			</div>
			<asp:Button ID="addProduct" runat="server" Width="150px" Text="Dodaj produkt" ValidationGroup="addProduct" OnClick="addProduct_Click"/>
		</div>
				</div>

			</div>

		</div>

	</form>
</body>

</html>
