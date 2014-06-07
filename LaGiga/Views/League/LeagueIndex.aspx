<%@ Page Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(of LaGiga.LeagueIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	The Fantasy Dugout
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
<link type="text/css" href="../../Scripts/jquerytheme/jquery-ui-1.8.11.custom.css" rel="stylesheet" />
    
    <script type="text/javascript">
        $(document).ready(function () {

            $("#aSeasonResult").click(function () {
                $.get("Home/GetSeasonResults/", {}, function (data) {

                    $("#SeasonView").dialog({
                        minHeight: 350,
                        width: 480,
                        bgiframe: true,
                        autoOpen: true,
                        draggable: true,
                        modal: true,
                        closeOnEscape: true,
                        resizable: false,
                        title: 'Season Results'
                    });

                    $("#SeasonView").html(data);

                    $("#SeasonView").show();
                    $(".ui-dialog").show();
                    $(".ui-widget-overlay").show();
                    overlaycheck();
                });
            });


        });

        function overlaycheck() {
            $(".ui-widget-overlay").click(function () {
                $("#SeasonView").hide();
                $(".ui-dialog").hide();
                $(".ui-widget-overlay").hide();
            });
        }
    
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div id="leftContent">
        <% Html.RenderPartial("RealWeekFixtures", Model.RealWeekFixtures) %>
    </div>

    <div id="leftBorder">
    </div>

    <div id="content">
        <h2 class="HomePageHeader">Welcome To The Fantasy Dugout</h2>
        <%= ViewData("Content") %>
    </div>

    <div id="SeasonView">
        &nbsp
    </div>

</asp:Content>
