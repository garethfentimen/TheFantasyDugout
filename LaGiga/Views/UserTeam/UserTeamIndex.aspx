<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.UserTeamIndexModel)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="UserTeamLeftMenu">
    <h2>User Teams Maintenance</h2>
    
    <p>
        <%: Html.ActionLink("Create New", "Create")%>
    </p>
    <br />
    <br />

    <%: Html.ActionLink("Back to Admin Menu","AdminIndex", "Home")%>
    </div>

    <div id="UserTeamList">
      <% Html.RenderPartial("UserTeamList", Model)%>
    </div>

</asp:Content>

