<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.FixtureIndexModel)" %>
<%@ Import Namespace="LaGiga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	FixtureIndex
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="FixturesLeftMenu">
    
    <h2>Fixture Maintenance</h2>

    <p>
        <%: Html.ActionLink("Create New", "Create")%>
    </p>

    <%= Ajax.ButtonHelper("All Fixtures", "FixtureIndex", "AllFixturesBtn", New With {.CompetitionID = 0, .pageIndex = Model.NextPage - 1}, New AjaxOptions With {.UpdateTargetId = "divFixtureList"})%>
        <br />
        <p> 
            filter by competition:
        </p>
    
    <% For Each item In Model.CompetitionTypes%>
        <%= Ajax.ButtonHelper(item.Name, "FixtureIndex", item.Name + "FixturesBtn", New With {.CompetitionID = item.CompetitionID, .pageIndex = Model.NextPage - 1}, New AjaxOptions With {.UpdateTargetId = "divFixtureList"})%>
        <br />
    <% Next %>
    
        <p> 
            Search by :
        </p>
    <% Using (Ajax.BeginForm("FixtureIndex", New AjaxOptions With {.UpdateTargetId = "divFixtureList"}))%>
    <% html.TextBox("SearchValue", Nothing, New With {.size = 12})%>
    <br />
        <input type = "submit", value = "Search" />
        
        <% End Using%>
        <br />
        <br />
        

       <%: Html.ActionLink("Back to Admin Menu","AdminIndex", "Home")%>

    </div>

    <div id="divFixtureList">
        <% Html.RenderPartial("FixtureList", Model)%>
    </div>

    
</asp:Content>

