﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kalendarz.aspx.cs" Inherits="Dietetyka.Kalendarz" %>

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
				<li class="nav-item dropdown no-arrow mx-1" runat="server">
					<asp:Button ID="ButtonLogout" runat="server" Text="Wyloguj" OnClick="ButtonLogout_Click" />
				</li>
			</ul>

		</nav>

		<div id="wrapper">

			<!-- Sidebar -->
    <ul class="sidebar navbar-nav">
      <li class="nav-item">
        <asp:Button ID="ButtonKalendarz" runat="server" Text="Kalendarz" OnClick="ButtonKalendarz_Click" />
      </li>
      <li class="nav-item"> 
          <asp:Button ID="ButtonKomentarz" runat="server" Text="Komentarz" OnClick="ButtonKomentarz_Click" />
      </li>
      <li class="nav-item">
        <asp:Button ID="ButtonStatystyka" runat="server" Text="Statystyka" OnClick="ButtonStatystyka_Click" />
      </li>
    </ul>

			<div id="content-wrapper">

				<div class="container-fluid">

					<!-- Page Content -->
					<h1>Witaj, <asp:Label ID="LabelName" runat="server" Text="LabelName"></asp:Label></h1>
                    <h2>Twoja Dieta</h2>
					<hr>
                    <asp:Calendar ID="Calendar" runat="server" OnSelectionChanged="Calendar_SelectionChanged"></asp:Calendar>
                    
                    <div id="KalendarzListDiv" runat="server" visible="true">
					
					<table border="1">
						<tr>
							<asp:Repeater ID="RepeaterKalendarz" runat="server">
								<HeaderTemplate>
									<td><b>Nazwa</b></td>
									<td><b>Kategoria</b></td>
								</HeaderTemplate>
								<ItemTemplate>
									<tr>
										<td><%# Eval("nazwa") %></td>
										<td><%# Eval("kategoria") %></td>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</tr>
					</table>
				</div>
                    



				</div>
			</div>

		</div>

	</form>
</body>

</html>