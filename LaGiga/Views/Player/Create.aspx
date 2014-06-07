<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Player)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add New Player</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>
    <% Using Ajax.BeginForm("Create", New AjaxOptions With {.UpdateTargetId = "results"})%>
        
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.Label("First Name") %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.FirstName) %>
                <%: Html.ValidationMessageFor(Function(model) model.FirstName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.Surname) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.Surname) %>
                <%: Html.ValidationMessageFor(Function(model) model.Surname) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("Position") %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("Positions", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.PositionID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("Club Team")%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("ClubTeams", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.TeamID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("National Team")%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("NationalTeams", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.NationalTeamID) %>
            </div>

            <p>
                <input type="submit" value="Create" />
            </p>

            <div id = "results">
                &nbsp;
            </div>

        </fieldset>

    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "PlayerIndex") %>
    </div>

</asp:Content>

