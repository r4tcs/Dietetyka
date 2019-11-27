<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dietecian_Interface.aspx.cs" Inherits="Dietetyka.Dietecian_Interface" %>

<!DOCTYPE html>
<html lang="en">

<head>
	<title>Home Page</title>
	<link href="css/css.css" rel="stylesheet">
	<link href="css/css.min.css" rel="stylesheet" type="text/css">
    <style type="text/css">

    .left {
		float: left;
		width: 200px;
		text-align: left;
		margin: 50px 50px;
		display: inline;
	}
	.center {
		float: left;
		text-align: left;
        width: 170px;
		margin: 50px 140px;
		display: inline;
	}
    .right{
        float: right;
        text-align: left;
        width: 650px;
        margin: 50px 150px;
        display: inline;
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
					<asp:Button ID="ButtonLogout" runat="server" Text="Wyloguj" OnClick="ButtonLogout_Click" />
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

				<div class="fluid-container">

					<!-- Page Content -->
					<h1>Witaj, <asp:Label ID="LabelName" runat="server" Text="LabelName"></asp:Label></h1>
					
		<div class="left">
            <h2>Dodawanie produktów</h2>
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

                <div class="center"><h2>Stwórz danie</h2>
                    <div class="form-group">Nazwa <br />
                        <asp:TextBox ID="TextBoxNazwaDania" runat="server"></asp:TextBox><br/>
                        <asp:RegularExpressionValidator
					    ID="RegularExpressionValidator1" runat="server" ErrorMessage="Podaj poprawną nazwę"
					    ControlToValidate="TextBoxNazwaDania" ValidationExpression="[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ'.\s]{1,50}"
					    ForeColor="Red" Display="Dynamic" ValidationGroup="addDanie"></asp:RegularExpressionValidator></div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxNazwaDania" ErrorMessage="Podaj nazwę" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <div class=form-group>Kategoria <br />
                        <asp:DropDownList ID="KategoriaDropDownList" runat="server">
                        <asp:ListItem Selected="True" Value="Danie Gorące"> Danie gorące </asp:ListItem>
                        <asp:ListItem Value="Danie Zimne"> Danie zimne </asp:ListItem>
                        <asp:ListItem Value="Przekąska"> Przekąska </asp:ListItem>
                        <asp:ListItem Value="Deser"> Deser </asp:ListItem>
                        <asp:ListItem Value="Napój"> Napój </asp:ListItem>
                        </asp:DropDownList> <br />
                    </div>
                    <div class="form-group">Przepis <br />
                        <asp:TextBox ID="textboxPrzepis" textmode="MultiLine" Rows=15 width="300px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="textboxPrzepis" ErrorMessage="Podaj przepis" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-goup">
                        <asp:Button runat="server" ID="addDanie" OnClick="addDanie_Click" Text="Dodaj danie"/>
                    </div>
                </div>
                
                <div class="right"><br />
                    <h2>Lista dodanych produktów</h2>
                    <table border="1">
                        <tr>
                            <asp:Repeater ID="RepeaterProdoktow" runat="server">
                                <HeaderTemplate>
                                    <td><b>Nazwa</b></td>
                                    <td><b>Kalorie</b></td>
                                    <td><b>Węglowodany</b></td>
                                    <td><b>Białka</b></td>
                                    <td><b>Tłuszcze</b></td>
                                    <td><b>Błonnik</b></td>
                                    <td><b>Sól</b></td>
                                </HeaderTemplate>
                            <ItemTemplate>
                            <tr>
                                <td><%# Eval("nazwa") %></td>
                                <td><%# Eval("kalorie") %> kcal</td>
                                <td><%# Eval("weglowodany") %> g</td>
                                <td><%# Eval("bialka") %> g</td>
                                <td><%# Eval("tluszcze") %> g</td>
                                <td><%# Eval("blonnik") %> g</td>
                                <td><%# Eval("sol") %> g</td>
                             </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </table>
                </div>
			</div>
		</div>
	</form>
</body>

</html>
