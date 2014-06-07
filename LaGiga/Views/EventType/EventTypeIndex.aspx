<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.EventTypeIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Manage EventTypes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="EventTypeLeftMenu">
    <h2>Event Type Maintenance</h2>

    <p>
        <%: Html.ActionLink("Create New", "Create")%>
    </p>
    
    
        <%: Html.ActionLink("Back to Admin Menu","AdminIndex", "Home")%>
    </div>

    <div id="divEventTypeList">
      <% Html.RenderPartial("EventTypeList", Model)%>
    </div>

</asp:Content>

