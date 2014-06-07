<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.FixtureEditModel)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewFixture
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>View Fixture Events</h3>

    <div id="divEventList">
        <% Html.RenderPartial("ViewFixtureEvents", Model)%>
    </div>

    <br />

    <div>
        <%: Html.ActionLink("Back to Fixture Screen", "FixtureIndex") %>
    </div>

</asp:Content>

