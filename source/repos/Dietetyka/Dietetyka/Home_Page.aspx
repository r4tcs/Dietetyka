<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Home_Page.aspx.cs" Inherits="Dietetyka.Home_Page" %>
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

    <a class="navbar-brand mr-1" href="index.html">Diet Manager</a>
    <!-- Navbar -->
    <ul class="navbar-nav ml-auto ml-md-0">
      <li class="nav-item dropdown no-arrow mx-1" runat="server">
      <asp:Button ID="Registration_Log_In_button" runat="server" Text="Registration \ Log In" OnClick="Registration_Log_In_button_Click" > </asp:Button><br />
      </li>
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
        <h1>Just a Web Form so u can se tha layout of the Form</h1>
        <hr>

      </div>

    </div>

  </div>

        </form>
</body>

</html>
