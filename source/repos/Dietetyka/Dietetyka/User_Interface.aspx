<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Interface.aspx.cs" Inherits="Dietetyka.User_Interface" %>

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
        <asp:Button ID="ButtonStatystyka" runat="server" Text="Statystyka" OnClick="ButtonStatystyka_Click" />
      </li>
        <li class="nav-item">
        <asp:Button ID="ButtonKlient_ShopList" runat="server" Text="Shop List" OnClick="ButtonKlient_ShopList_Click" />
      </li>
    </ul>

			<div id="content-wrapper">

				<div class="container-fluid">

					<!-- Page Content -->
					<h1>Witaj, <asp:Label ID="LabelName" runat="server" Text="LabelName"></asp:Label></h1>
					<hr>

                    <h2>Dear user, Welcome to your own Diet Manager Page. Please Check your Calendar to start following your diet. Also try to check our new feature of statistics, where you can 
                        see your progress!
                    </h2>
                    <hr />

				</div>
			</div>

		</div>

	</form>
</body>

</html>
