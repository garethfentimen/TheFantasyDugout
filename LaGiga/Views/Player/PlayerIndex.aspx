<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.PlayerIndexModel)" %>
<%@ Import Namespace="LaGiga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PlayerIndex
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    

    <div id="PlayersLeftMenu">
    <h2>Player Maintenance</h2>
    <p>
        <%: Html.ActionLink("Add New Player", "Create")%>
    </p>

    <%= Ajax.ButtonHelper("All Players", "PlayerIndex", "AllPlayersBtn", New With {.PositionID = 0}, New AjaxOptions With {.UpdateTargetId = "divPlayerList"})%>
        <br />
        <br />
         <%: Html.Label("Filter by position:")%>
         <br />
    <% For Each item In Model.positionTypes%>
        <%= Html.selected(item.value, Model.SelectedPosition)%>
        <%= Ajax.ButtonHelper(item.key, "PlayerIndex", item.key + "PlayersBtn", New With {.PositionID = item.value}, New AjaxOptions With {.UpdateTargetId = "divPlayerList"})%>
        <br />
    <% Next %>
    
        <br />
        <% Using (Ajax.BeginForm("PlayerIndex", New AjaxOptions With {.UpdateTargetId = "divPlayerList"}))%>
            <%: Html.Label("Search by surname:")%>
            <%: html.TextBox("SearchValue", Nothing, New With {.size = 12})%>
        <br />
            <%: Html.Label("Search by team:")%>
            <%: Html.DropDownList("TeamID", Model.ClubTeams)%>
            <input type = "submit", value = "Search" />
        <% End Using%>
        <br />
        <%: Html.ActionLink("Back to Admin Menu","AdminIndex", "Home")%>
        
    </div>

    <div id="divPlayerList">
        <% Html.RenderPartial("PlayerList", Model)%>
    </div>
    
</asp:Content>

