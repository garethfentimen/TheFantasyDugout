<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.UserPlayerIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	UserPlayerIndex
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <%--<%:  %>--%>

    <div class="UserPlayerLeftMenu">
    <h2>User Player Maintenance</h2>


   <%: Html.ActionLink("Draft Player for Users Team", "CreateUserPlayer", "UserPlayer")%>
    <br />
    <br />
        <p> 
            show competition:
        </p>
    
    <% For Each item In Model.CompetitionTypes%>
        <%= Ajax.ButtonHelper(item.Name, "UserPlayerIndex", item.Name + "UserPlayerCompetitionBtn", New With {.UserTeamID = Model.UserTeam.UserTeamID, .CompetitionID = item.CompetitionID, .PageIndex = Model.NextPage -1, .searchValue = "" }, New AjaxOptions With {.UpdateTargetId = "UserPlayerList"})%>
        <br />
    <% Next %>
    <br />
    <br />
    <%: Html.ActionLink("Back to User Team list","UserTeamIndex", "UserTeam")%>
    <br />
    <br />
    <%: Html.ActionLink("Back to Admin Menu","AdminIndex", "Home")%>
    
    </div>
    <div id = "UserPlayerList">
       <% Html.RenderPartial("UserPlayerList", Model)%>
    </div>

</asp:Content>

