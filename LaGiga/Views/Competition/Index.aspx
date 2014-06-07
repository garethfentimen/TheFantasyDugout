<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of IEnumerable (Of LaGiga.Competition))" %>
<%@ Import Namespace="LaGiga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Competitions</h2>

    <p>
        <%: Html.ActionLink("Create New", "Create")%>
    </p>
    
    <div id="divCompetitionList">
        <% Html.RenderPartial("CompetitionList", Model)%>
    </div>

    <br />
    <br />
    <br />

    <%: Html.ActionLink("Back to Admin Menu","AdminIndex", "Home")%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderMainContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

