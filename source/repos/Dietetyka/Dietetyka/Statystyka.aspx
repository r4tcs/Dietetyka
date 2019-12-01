<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statystyka.aspx.cs" Inherits="Dietetyka.Statystyka" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
                    <asp:Chart ID="ChartAllTime" runat="server">
                        <Series>
                            <asp:Series Name="Statystyka">
                                <Points>
                                <asp:DataPoint AxisLabel="Kalorie" Yvalues="20"/>
                                <asp:DataPoint AxisLabel="Bialek" Yvalues="20"/>
                                <asp:DataPoint AxisLabel="Sol" Yvalues="20"/>
                                <asp:DataPoint AxisLabel="Tluszcz" Yvalues="20"/>
                                </Points>
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>



					<hr>
				</div>
			</div>

		</div>

	</form>
</body>

</html>
