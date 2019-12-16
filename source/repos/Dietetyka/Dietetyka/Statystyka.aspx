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
                    <asp:Button ID="ButtonStatystyka" runat="server" Text="Statystyka" OnClick="ButtonStatystyka_Click" />
                </li>
            </ul>

            <div id="content-wrapper">

                <div class="container-fluid">

                    <!-- Page Content -->
                    <h1>Witaj,
                        <asp:Label ID="LabelName" runat="server" Text="LabelName"></asp:Label></h1>
                    <table>
                        <tr>
                            <td>Twoja Statystyka za caly czas korzystania programem!
                                <br />
                                <asp:Chart ID="Statystyka_Chart" runat="server" OnLoad="Statystyka_Chart_Load">
                                    <Series>
                                        <asp:Series Name="Kalorie" Legend="Kalorie"></asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Legend="Kalorie" Name="Weglowodany">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Legend="Kalorie" Name="Bialka">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Legend="Kalorie" Name="Blonnik">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Legend="Kalorie" Name="Sol">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Legend="Kalorie" Name="Tluszcze">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend Name="Kalorie">
                                        </asp:Legend>
                                    </Legends>
                                </asp:Chart>
                            </td>


                            <td>
                                <asp:Label ID="Label_Data" runat="server" Text="Wybierz date zeby zobaczyc statystyke "></asp:Label>
                                <br />
                                <asp:Calendar ID="Calendar_Statystyka" runat="server" OnDayRender="Calendar_Statystyka_DayRender" OnSelectionChanged="Calendar_Statystyka_SelectionChanged"></asp:Calendar>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="1">
                                    <tr>
                                        <asp:Repeater ID="Repeater_Statystyka" runat="server">
                                            <HeaderTemplate>
                                                <td><b>Kalorie</b></td>
                                                <td><b>Weglowodany</b></td>
                                                <td><b>Bialka</b></td>
                                                <td><b>Blonnik</b></td>
                                                <td><b>Sol</b></td>
                                                <td><b>Tluszcze</b></td>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Kalorie") %></td>
                                                    <td><%# Eval("Weglowodany") %></td>
                                                    <td><%# Eval("Bialka") %></td>
                                                    <td><%# Eval("Blonnik") %></td>
                                                    <td><%# Eval("Sol") %></td>
                                                    <td><%# Eval("Tluszcze") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <hr>
                </div>
            </div>
        </div>
    </form>
</body>

</html>
