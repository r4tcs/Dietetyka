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
			text-align: left;
			margin: 10px 25px;
			display: inline;
		}

		.center {
			float: left;
			text-align: left;
			width: 170px;
			margin: 50px 140px;
			display: inline;
		}

		.right {
			float: right;
			text-align: left;
			width: 650px;
			margin: 50px 150px;
			display: inline;
		}

		.lista {
			display: inline;
			margin-left: 300px;
		}

		.edytuj {
			float: none;
			display: inline;
		}

		.wyloguj {
			float: right;
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
					<asp:Button ID="ButtonAddProduct" runat="server" Text="Dodaj produkt" OnClick="ButtonAddProduct_Click" />
				</li>
				<li class="nav-item dropdown no-arrow mx-1" runat="server">
					<asp:Button ID="ButtonProductList" runat="server" Text="Lista produktów" OnClick="ButtonProductList_Click" />
				</li>
				<li class="nav-item dropdown no-arrow mx-1" runat="server">
					<asp:Button ID="ButtonCreateDish" runat="server" Text="Stwórz danie" OnClick="ButtonCreateDish_Click" />
				</li>
				<li class="nav-item dropdown no-arrow mx-1" runat="server">
					<asp:Button ID="ButtonDishList" runat="server" Text="Lista dań" OnClick="ButtonDishList_Click" />
				</li>
				<li class="nav-item dropdown no-arrow mx-1" runat="server">
					<asp:Button ID="ButtonCreateMenu" runat="server" Text="Utwórz menu" OnClick="ButtonCreateMenu_Click" />
				</li>
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
					<h1>Witaj,
						<asp:Label ID="LabelName" runat="server" Text="LabelName"></asp:Label></h1>
					<hr />
				</div>
				<div class="left" id="addProductDiv" runat="server" visible="false">
					<h2>Dodawanie produktów</h2>
					<div class="form-group">
						Nazwa
							<br />
						<asp:TextBox ID="TextBoxNazwa" runat="server"></asp:TextBox><br />
						<asp:RegularExpressionValidator
							ID="NazwaWalidator" runat="server" ErrorMessage="Podaj poprawną nazwę"
							ControlToValidate="textboxNazwa" ValidationExpression="[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ'.\s]{1,50}"
							ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"></asp:RegularExpressionValidator>
					</div>

					<div class="form-group">
						Jednostka
							<br />
						<asp:DropDownList ID="DropDownListJednostka" runat="server" Width="175px">
							<asp:ListItem>100g</asp:ListItem>
							<asp:ListItem>szt</asp:ListItem>
						</asp:DropDownList><br />
					</div>

					<div class="form-group">
						Kalorie
							<br />
						<asp:TextBox ID="TextBoxKalorie" runat="server"></asp:TextBox>kcal<br />
						<asp:RangeValidator ID="RangeValidatorKalorie" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxKalorie" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Węglowodany
							<br />
						<asp:TextBox ID="TextBoxWeglowodany" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidatorWeglowodany" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxWeglowodany" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Białka
							<br />
						<asp:TextBox ID="TextBoxBialka" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidatorBialka" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxBialka" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Błonnik
							<br />
						<asp:TextBox ID="TextBoxBlonnik" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidatorBlonnik" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxBlonnik" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Sól
							<br />
						<asp:TextBox ID="TextBoxSol" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidatorSol" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxSol" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Tłuszcze
							<br />
						<asp:TextBox ID="TextBoxTluszcze" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidatorTluszcze" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxTluszcze" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>
					<asp:Button ID="addProduct" runat="server" Width="150px" Text="Dodaj produkt" ValidationGroup="addProduct" OnClick="addProduct_Click" />
				</div>


				<div id="createDishDiv" class="left" visible="false" runat="server">
					<h2>Stwórz danie</h2>
					<div class="form-group">
						Nazwa
						<br />
						<asp:TextBox ID="TextBoxNazwaDania" runat="server" Width="400"></asp:TextBox><br />
						<asp:RegularExpressionValidator
							ID="RegularExpressionValidator1" runat="server" ErrorMessage="Podaj poprawną nazwę"
							ControlToValidate="TextBoxNazwaDania" ValidationExpression="[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ'.\s]{1,50}"
							ForeColor="Red" Display="Dynamic" ValidationGroup="addDanie"></asp:RegularExpressionValidator>
					</div>
					<asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxNazwaDania" ErrorMessage="Podaj nazwę" ForeColor="Red" Display="Dynamic" ValidationGroup="addDanie"></asp:RequiredFieldValidator>
					<div class="form-group">
						Kategoria
						<br />
						<asp:DropDownList ID="KategoriaDropDownList" runat="server" Width="400">
							<asp:ListItem Selected="True" Value="Danie Gorące"> Danie gorące </asp:ListItem>
							<asp:ListItem Value="Danie Zimne"> Danie zimne </asp:ListItem>
							<asp:ListItem Value="Przekąska"> Przekąska </asp:ListItem>
							<asp:ListItem Value="Deser"> Deser </asp:ListItem>
							<asp:ListItem Value="Napój"> Napój </asp:ListItem>
						</asp:DropDownList>
						<br />
					</div>
					<div class="form-group">
						Przepis
						<br />
						<asp:TextBox ID="textboxPrzepis" TextMode="MultiLine" Rows="15" Width="400px" runat="server"></asp:TextBox><br />
						<asp:RequiredFieldValidator runat="server" ControlToValidate="textboxPrzepis" ErrorMessage="Podaj przepis" ForeColor="Red" Display="Dynamic" ValidationGroup="addDanie"></asp:RequiredFieldValidator>
					</div>
					<div>
						<asp:GridView ID="Gridview1" HeaderStyle-HorizontalAlign="Center" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="400" RowStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
							<Columns>
								<asp:TemplateField HeaderText="Składnik">
									<ItemTemplate>
										<asp:DropDownList ID="DropDownListIngredient" AppendDataBoundItems="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="nazwa" DataValueField="Id">
											<asp:ListItem Text="--WYBIERZ SKŁADNIK--" />
										</asp:DropDownList>
										<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BazaConnectionString %>" SelectCommand="SELECT [Id], [nazwa] FROM [Produkt_spozywczy] ORDER BY [nazwa]"></asp:SqlDataSource>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Ilość (w gramach)">
									<ItemTemplate>
										<asp:TextBox ID="TextBoxWeight" runat="server" TextMode="Number"></asp:TextBox>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center" />
									<FooterTemplate>
										<asp:Button ID="ButtonAdd" runat="server" Text="Dodaj kolejny" OnClick="ButtonAdd_Click" />
									</FooterTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</div>
					<br />
					<div class="form-group">
						<asp:Button runat="server" ID="addDanie" OnClick="addDanie_Click" Text="Dodaj danie" ValidationGroup="addDanie" />
					</div>
				</div>

				<div class="left" id="ProductListDiv" runat="server" visible="false">
					<h2>Lista dodanych produktów</h2>
					<table border="1">
						<tr>
							<asp:Repeater ID="RepeaterProduktow" runat="server">
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
										<td>
											<asp:Button ProduktID='<%# Eval("Id")  %>' runat="server" ID="EditProduct" Text="Edytuj" OnClick="EditProduct_Click" /></td>
										<td>
											<asp:Button ProduktID='<%# Eval("Id")  %>' runat="server" ID="DeleteProduct" Text="Usuń" OnClick="DeleteProduct_Click" /></td>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</tr>
					</table>
				</div>
				<div class="left" id="edycjaProduktow" runat="server" visible="false">
					<div class="form-group">
						Nazwa
							<br />
						<asp:TextBox ID="TextBoxNazwa2" runat="server"></asp:TextBox><br />
						<asp:RegularExpressionValidator
							ID="RegularExpressionValidator2" runat="server" ErrorMessage="Podaj poprawną nazwę"
							ControlToValidate="textboxNazwa" ValidationExpression="[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ'.\s]{1,50}"
							ForeColor="Red" Display="Dynamic" ValidationGroup="addProduct"></asp:RegularExpressionValidator>
					</div>

					<div class="form-group">
						Jednostka
							<br />
						<asp:DropDownList ID="DropDownListJednostka2" runat="server" Width="175px">
							<asp:ListItem>100g</asp:ListItem>
							<asp:ListItem>szt</asp:ListItem>
						</asp:DropDownList><br />
					</div>

					<div class="form-group">
						Kalorie
							<br />
						<asp:TextBox ID="TextBoxKalorie2" runat="server"></asp:TextBox>kcal<br />
						<asp:RangeValidator ID="RangeValidator1" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="editProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxKalorie" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Węglowodany
							<br />
						<asp:TextBox ID="TextBoxWeglowodany2" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidator2" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="editProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxWeglowodany" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Białka
							<br />
						<asp:TextBox ID="TextBoxBialka2" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidator3" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="editProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxBialka" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Błonnik
							<br />
						<asp:TextBox ID="TextBoxBlonnik2" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidator4" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="editProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxBlonnik" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Sól
							<br />
						<asp:TextBox ID="TextBoxSol2" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidator5" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="editProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxSol" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>

					<div class="form-group">
						Tłuszcze
							<br />
						<asp:TextBox ID="TextBoxTluszcze2" runat="server"></asp:TextBox>g<br />
						<asp:RangeValidator ID="RangeValidator6" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="editProduct"
							MinimumValue="0.01" MaximumValue="100" CultureInvariantValues="true" Type="Currency" ControlToValidate="TextBoxTluszcze" ErrorMessage="Podaj wartość w przedziale <0,01, 100>"></asp:RangeValidator>
					</div>
					<div class="form-group">
						<asp:Button ID="editProduct" runat="server" OnClick="EditProduct2_Click" Text="Zatwierdź zmiany" />
					</div>
				</div>
				<div class="left" runat="server" id="createMenu" visible="false">
					<h2>Wybierz klienta: </h2>
					<table border="1">
						<tr>
							<asp:Repeater ID="RepeaterKlientow" runat="server">
								<HeaderTemplate>
									<td><b>Imie</b></td>
									<td><b>Nazwisko</b></td>
									<td><b>Login</b></td>
									<td><b>Telefon</b></td>
								</HeaderTemplate>
								<ItemTemplate>
									<tr>
										<td><%# Eval("imie") %> </td>
										<td><%# Eval("nazwisko") %> </td>
										<td><%# Eval("login") %>    </td>
										<td><%# Eval("telefon") %>  </td>
										<td>
											<asp:Button ProduktID='<%# Eval("Id")  %>' runat="server" ID="chooseClient" Text="Wybierz" OnClick="chooseClient_Click" /></td>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</tr>
					</table>
				</div>
				<div class="left" runat="server" id="DishListDiv" visible="false">
                    <h2>Dodane przepisy</h2>
					<b>Kategoria: </b><br />
					<asp:DropDownList ID="DropDownListCategory" AutoPostBack="true" AppendDataBoundItems="true" runat="server" DataSourceID="SqlDataSource3" DataTextField="kategoria" DataValueField="kategoria" OnSelectedIndexChanged="DropDownListCategory_SelectedIndexChanged">
						<asp:ListItem Selected="True" Text="--WYBIERZ KATEGORIĘ--"> </asp:ListItem>
					</asp:DropDownList>
					<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:BazaConnectionString %>" SelectCommand="SELECT DISTINCT [kategoria] FROM [Danie]"></asp:SqlDataSource>
					<br /><b>Danie: </b><br />
					<asp:DropDownList ID="DropDownListDish" AutoPostBack="true" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="DropDownListDish_SelectedIndexChanged"></asp:DropDownList>
					<br /><b>Przepis: </b><br /><asp:Label ID="LabelPrzepis" runat="server" Text=""></asp:Label>
					<br /><b>Składniki: </b><br /><asp:Table ID="TableProducts" runat="server" BorderStyle="Solid" GridLines="Both"></asp:Table>
				</div>
			</div>
		</div>
	</form>
</body>

</html>
