<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.TeamIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Manage Teams
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="TeamsLeftMenu">
    <h2>Team Maintenance</h2>
    <%: Html.ActionLink("Create New", "Create")%>
    <br />
    <br />

    <% For Each item In Model.TeamTypes %>
        <%= Html.selected(item, Model.SelectedTeamType)%>
        <% Dim name As String = item.GetName(gettype(Helpers.TeamType), item)%>
        <% Dim enumVal As Integer = item %>       
        <%= Ajax.ButtonHelper(name + " teams", "TeamIndex", Left(name, 4) + "TeamsBtn", New With {.TeamTypeID = enumVal}, New AjaxOptions With {.UpdateTargetId = "divTeamList"})%>
        <br />
    <% Next %>
        <%= Ajax.ButtonHelper("All Teams", "TeamIndex", "AllTeamsBtn", New With {.TeamTypeID = 0}, New AjaxOptions With {.UpdateTargetId = "divTeamList"})%>
    <br />
    <br />
    <% Using (Ajax.BeginForm("TeamIndex", New AjaxOptions With {.UpdateTargetId = "divTeamList"}))%>
    <% =html.TextBox("SearchValue", Nothing, New With {.size = 12})%>
        <input type = "submit", value = "Search" />
    <% End Using%>
     <br />
     <br />
        <%: Html.ActionLink("Back to Admin Menu","AdminIndex", "Home")%>

    </div>

    <div id="divTeamList">
        <% Html.RenderPartial("TeamList", Model)%>
    </div>

    <div class="divTeamList-bottom">&nbsp;</div>

    

</asp:Content>

