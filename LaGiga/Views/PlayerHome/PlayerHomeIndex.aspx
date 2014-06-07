<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(of LaGiga.HomeIndexModel)" %>
<%@ Import Namespace="LaGiga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Player Home
</asp:Content>

<asp:Content ID="tempHeader" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <%= Html.RegCss("Site.css")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" >

     $(document).ready(function () {

         if ($("#Admin").val() == "true") {
             $("#Adminbtn").show();
         }

         var IE7 = false
         if (($.browser.msie) && ($.browser.version <= 7)) {
             IE7 = true
         }
         var PlayerPickerbtn = $("#PlayerPickerbtn");
         var PlayerPickerContainer = $("#PlayerPickerContainer");
         var MainContent = $("#main");

         PlayerPickerbtn.click(function () {

             //$("#PlayerPickerContainer").empty().html('<img src="content/82.gif" />');
             $.get("PlayerHome/GetPlayerPicker", {}, function (data) {
                 PlayerPickerContainer.html(data);
             });

             $("#ResultsContainer").hide();
             $("#OppositionContainer").hide();
             $("#SeasonContainer").hide();
             $("#TheListContainer").hide();
             $("#NouCamp").animate({ opacity: 0.1,
                 alpha: (opacity = 15)
             }, 250);

             PlayerPickerContainer.animate({ opacity: 1.00,
                 alpha: (opacity = 100)
             }, 250, function () {
                 if (IE7) {
                     PlayerPickerContainer.show();
                 }
                 else {
                     PlayerPickerContainer.show(125);
                 }
             });
         });

         PlayerPickerbtn.dblclick(function () {
             $("#NouCamp").animate({ opacity: 1,
                 alpha: (opacity = 100)
             }, 250);

             PlayerPickerContainer.animate({ opacity: 0,
                 alpha: (opacity = 0)
             }, 250, function () {
                 PlayerPickerContainer.hide();
             });
         });

         var WeekResultsbtn = $("#WeekResultsbtn");
         var ResultsContainer = $("#ResultsContainer");

         WeekResultsbtn.click(function () {
             $.get("PlayerHome/GetResultTable", {}, function (data) {
                 ResultsContainer.html(data);
             });

             PlayerPickerContainer.hide();
             $("#OppositionContainer").hide();
             $("#SeasonContainer").hide();
             $("#TheListContainer").hide();
             $("#NouCamp").animate({ opacity: 0.1,
                 alpha: (opacity = 15)
             }, 250, function () {
                 $("#NouCamp").show();
             });
             ResultsContainer.animate({ opacity: 1.00,
                 alpha: (opacity = 100)
             }, 250, function () {
                 if (IE7) {
                     ResultsContainer.show();
                 }
                 else {
                     ResultsContainer.show(125);
                 }
             });
         });

         WeekResultsbtn.dblclick(function () {
             $("#NouCamp").animate({ opacity: 1.00,
                 alpha: (opacity = 100)
             }, 250);
             ResultsContainer.animate({ opacity: 0.00,
                 alpha: (opacity = 0)
             }, 250, function () {
                 ResultsContainer.hide();
             });
         });


         var Oppositionbtn = $("#Oppositionbtn");
         var OppositionContainer = $("#OppositionContainer")

         Oppositionbtn.click(function () {
             $.get("PlayerHome/GetOpposition", {}, function (data) {
                 OppositionContainer.html(data);
             });

             PlayerPickerContainer.hide();
             ResultsContainer.hide();
             $("#SeasonContainer").hide();
             $("#TheListContainer").hide();
             $("#NouCamp").animate({ opacity: 0.1,
                 alpha: (opacity = 15)
             }, 250);

             OppositionContainer.animate({ opacity: 1.00,
                 alpha: (opacity = 100)
             }, 250, function () {
                 if (IE7) {
                     OppositionContainer.show();
                 }
                 else {
                     OppositionContainer.show(125);
                 }
             });
         });

         Oppositionbtn.dblclick(function () {
             $("#NouCamp").animate({ opacity: 1,
                 alpha: (opacity = 100)
             }, 250, function () {
                 OppositionContainer.hide();
             });
         });

         var Seasonbtn = $("#Seasonbtn");
         var SeasonContainer = $("#SeasonContainer")

         Seasonbtn.click(function () {
             $.get("PlayerHome/GetSeasonResults", {}, function (data) {
                 $("#SeasonContainer").html(data);
             });

             $("#PlayerPickerContainer").hide();
             $("#OppositionContainer").hide();
             $("#ResultsContainer").hide();
             $("#TheListContainer").hide();
             $("#NouCamp").animate({ opacity: 0.1,
                 alpha: (opacity = 15)
             }, 250);

             SeasonContainer.animate({ opacity: 1.00,
                 alpha: (opacity = 100)
             }, 250, function () {
                 if (IE7) {
                     SeasonContainer.show();
                 }
                 else {
                     SeasonContainer.show(125);
                 }
             });
         });

         Seasonbtn.dblclick(function () {
             $("#NouCamp").animate({ opacity: 1,
                 alpha: (opacity = 100)
             }, 250, function () {
                 SeasonContainer.hide();
             });
         });

         var TheListbtn = $("#TheListbtn");

         TheListbtn.click(function () {

             $("#TheListContainer").empty().html('<img src="content/82.gif" />');

             $.get("PlayerHome/GetTheList", {}, function (data) {
                 $("#TheListContainer").html(data);
             });

             $("#PlayerPickerContainer").hide();
             OppositionContainer.hide();
             $("#ResultsContainer").hide();
             $("#SeasonContainer").hide();
             $("#NouCamp").animate({ opacity: 0.1,
                 alpha: (opacity = 15)
             }, 250, function () {
                 $("#TheListContainer").show();
             });

             $("#TheListContainer").animate({ opacity: 1.00,
                 alpha: (opacity = 100)
             }, 250, function () {
                 $("#TheListContainer").show();
             });
         });

         TheListbtn.dblclick(function () {
             $("#TheListContainer").animate({ opacity: 1.00,
                 alpha: (opacity = 100)
             }, 250, function () {
                 $("#TheListContainer").hide();
             });
             $("#NouCamp").animate({ opacity: 1,
                 alpha: (opacity = 100)
             }, 250, function () {
                 $("#NouCamp").show();
             });
         });

         var Statsbtn = $("#Statsbtn");

         Statsbtn.click(function () {
             var $StatsContainer = $("#StatisticsContainer")
             $.get("PlayerHome/GetStatistics", {}, function (data) {
                 $StatsContainer.html(data);
                 $StatsContainer.show();
             });
         });

         var Closebtn = $("#Closebtn");

         Closebtn.click(function () {
             $("#PlayerPickerContainer").hide();
             $("#OppositionContainer").hide();
             $("#ResultsContainer").hide();
             $("#SeasonContainer").hide();
             $("#NouCamp").css("opacity", "1");
             $("#NouCamp").css("alpha", "(opacity = 100)");
         });

     });

  </script>

<h3 align="center">
    <%: ViewData("UserName")%>'s dugout
</h3>
    
<div id="Adminbtn">
    <%: Html.Hidden("Admin", ViewData("Admin"))%>
    <%: Html.ActionLink("Admin Pages (admin only)", "AdminIndex", "Home")%>
</div>

<div class="PlayerHomeLeftMenu">
    <ul id="PHLMenu">
        <li id="Closebtn" class="PlayerMenuBtn">
            <a href="#">Close Menu screen</a>
        </li>
        <li id="PlayerPickerbtn" class="PlayerMenuBtn">
            <a href="#">Pick players for this week</a>
        </li>
        <li id="WeekResultsbtn" class="PlayerMenuBtn">
            <a href="#">This week's results</a>
        </li>
        <li id="Oppositionbtn" class="PlayerMenuBtn">
            <a href="#">Check The Opposition</a>
        </li>
        <li id="Seasonbtn" class="PlayerMenuBtn">
            <a href="#">Season Results</a>
        </li>
        <p class="SmallText"> Tip: double click on any menu item to close the current screen</p>
        <%: Html.ActionLink("Home", "", "")%>
    </ul>
</div>

    <div id ="NouCamp" class = "NouCampPhoto">
        <img id="NouCampPhoto" src="../../Content/Stadium3.jpg" alt="The Nou Camp"/>
    </div>

    <div id="ResultsContainer" class="PlayerPartials">
       &nbsp;
    </div>

    <div id="PlayerPickerContainer" class="PlayerPartials">
       &nbsp;
    </div>

    <div id="OppositionContainer" class="PlayerPartials">
       &nbsp;
    </div>

    <div id="SeasonContainer" class="PlayerPartials">
       &nbsp;
    </div>

</asp:Content>
