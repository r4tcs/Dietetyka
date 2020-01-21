<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Klient_ShopList.aspx.cs" Inherits="Dietetyka.Klient_ShopList" %>

<style>
    #map {
        height: 500px;
        width: 500px;
    }
</style>

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
                    <asp:Button ID="ButtonKlient_ShopList" runat="server" Text="Klient_ShopList" OnClick="ButtonKlient_ShopList_Click" />
                </li>
            </ul>

            <div id="content-wrapper">

                <div class="container-fluid">

                    <!-- Page Content -->
                    <h1>Witaj,
                        <asp:Label ID="LabelName" runat="server" Text="LabelName"></asp:Label></h1>
                    <h2>Twoja Lista Zakupów</h2>
                    <hr>
                    <table>
                        <tr>
                            <td>
                                <h4>Wybierz datę żeby zobaczyć co musiś kupić dla swojej diety</h4>
                                <asp:Calendar ID="Calendar_ShopList" runat="server" OnDayRender="Calendar_Statystyka_DayRender" OnSelectionChanged="Calendar_ShopList_SelectionChanged"></asp:Calendar>

                            </td>
                            <td>
                                <div id="info_div"></div>
                                <h4>Najbliższy do ciebie sklep</h4>
                                <div id="map"></div>
                                <script>
                                    var stores = [
                                        {
                                            name: 'Biedronka',
                                            location: { lat: 51.778988, lng: 19.490180 },
                                            adress: 'Tamka 3/5, 91-403 Łódź'
                                        },
                                        {
                                            name: 'Żabka',
                                            location: { lat: 51.778017, lng: 19.492795 },
                                            adress: 'Pomorska 144, 91-404 Łódź'
                                        },
                                        {
                                            name: 'Sklep Spożywczo-warzywny',
                                            location: { lat: 51.773199, lng: 19.485953 },
                                            adress: '90-001 Łódź'
                                        }

                                    ];



                                    // Note: This example requires that you consent to location sharing when
                                    // prompted by your browser. If you see the error "The Geolocation service
                                    // failed.", it means you probably did not give permission for the browser to
                                    // locate you.
                                    var map, infoWindow;
                                    function initMap() {
                                        map = new google.maps.Map(document.getElementById('map'), {
                                            center: { lat: 51.757736, lng: 19.465479 },
                                            zoom: 6
                                        });
                                        infoWindow = new google.maps.InfoWindow;

                                        function markStore(storeInfo) {

                                            // Create a marker and set its position.
                                            var marker = new google.maps.Marker({
                                                map: map,
                                                position: storeInfo.location,
                                                title: storeInfo.name
                                            });

                                            // show store info when marker is clicked
                                            marker.addListener('click', function () {
                                                showStoreInfo(storeInfo);
                                            });
                                        }

                                        // show store info in text box
                                        function showStoreInfo(storeInfo) {
                                            var info_div = document.getElementById('info_div');
                                            info_div.innerHTML = 'Store name: '
                                                + storeInfo.name
                                                + '<br>Hours: ' + storeInfo.adress;
                                        }
                                        stores.forEach(function (store) {
                                            markStore(store);
                                        });

                                        // Try HTML5 geolocation.
                                        if (navigator.geolocation) {
                                            navigator.geolocation.getCurrentPosition(function (position) {
                                                var pos = {
                                                    lat: position.coords.latitude,
                                                    lng: position.coords.longitude
                                                };

                                                infoWindow.setPosition(pos);
                                                infoWindow.setContent('Location found.');
                                                infoWindow.open(map);
                                                map.setCenter(pos);
                                            }, function () {
                                                handleLocationError(true, infoWindow, map.getCenter());
                                            });
                                        } else {
                                            // Browser doesn't support Geolocation
                                            handleLocationError(false, infoWindow, map.getCenter());
                                        }
                                    }

                                    function handleLocationError(browserHasGeolocation, infoWindow, pos) {
                                        infoWindow.setPosition(pos);
                                        infoWindow.setContent(browserHasGeolocation ?
                                            'Error: The Geolocation service failed.' :
                                            'Error: Your browser doesn\'t support geolocation.');
                                        infoWindow.open(map);
                                    }
                                </script>
                                <script async defer
                                    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBwTNGW_nlw79W2GI34alcpdg9y2JvWij0&callback=initMap">
                                </script>
                                <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBwTNGW_nlw79W2GI34alcpdg9y2JvWij0&callback=initMap"
                                    async defer></script>
                                <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBwTNGW_nlw79W2GI34alcpdg9y2JvWij0&libraries=geometry,places">
                                </script>
                                <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBwTNGW_nlw79W2GI34alcpdg9y2JvWij0&language=en&region=PL">
                                </script>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="Repeater_ShopList" runat="server" Visible="true">
                                    <HeaderTemplate>
                                        <td><b>Nazwa</b></td>
                                        <td><b>Ilosc</b></td>
                                        <td><b>JednostkaMiary</b></td>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Nazwa") %></td>
                                            <td><%# Eval("Ilosc") %></td>
                                            <td><%# Eval("JednostkaMiary") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>

                        </tr>



                    </table>



                </div>
            </div>

        </div>

    </form>
</body>

</html>
