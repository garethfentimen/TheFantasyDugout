<%@ Page Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>About this site</h3>
        <%= ViewData("About")%>
        <img id="copyright" alt="copyright" src="../../Content/copyrightsymbol.jpg" /> 2010 G.Fentimen
</asp:Content>
