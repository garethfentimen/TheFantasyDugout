<%@ Page Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(of LaGiga.HomeIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Administrators Home Page
</asp:Content>

<asp:Content ID="Headcss" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <%= Html.RegCss("Site.css")%>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <p class="WelcomeRole">
        Welcome to LaGiga, Administrator.
    </p>
    
    <div class = "AdminLeftMenu">
    <p id= "AdminMenuHeader" class="MenuHeaders">Administrator Menu</p>
    <p id= "WeeklyMainHeader" class="MenuHeaders">Weekly Maintenance</p>
    <%: Html.ActionLink("Manage Fixtures", "FixtureIndex", "Fixture")%>
    <br />
    <br />
    <%: Html.ActionLink("Manage Weeks", "WeekIndex", "Week")%>
    <br />
    <br />
    <p id= "GenMainHeader" class="MenuHeaders">General Maintenance</p>
    <%: Html.ActionLink("Manage User Teams and Draft Players", "UserTeamIndex", "UserTeam")%>
    <br />
    <br />
    <%: Html.ActionLink("Manage Players", "PlayerIndex", "Player")%>
    <br />
    <br />
    <%: Html.ActionLink("Manage club/national Teams", "TeamIndex", "Team")%>
    <br />
    <br />
    <%: Html.ActionLink("Manage Event types and awards", "EventTypeIndex", "EventType")%>
    <br />
    <br />
    <%: Html.ActionLink("Manage Competitions", "Index", "Competition")%>
    <br />
    <br />
    <br />
    <%: Html.ActionLink("Home", "", "")%>
    </div>
    <img class = "StadiumPhoto1" src="../../Content/StadiumPhoto1.jpg" alt="Nelson Mandela Bay Stadium"/> </>
    
</asp:Content>
