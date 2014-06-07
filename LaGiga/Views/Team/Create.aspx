<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Team)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>
              
    <% Using (Ajax.BeginForm("Create", 
                 New AjaxOptions With {.UpdateTargetId = "results"}))%>
            
            <%: Html.ValidationSummary(True) %>
            <fieldset>
            <legend>Fields</legend>

            <div class="editor-label">
                <%: Html.Label("Team Name")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.TeamName) %>
                <%: Html.ValidationMessageFor(Function(model) model.TeamName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("Type of Team")%>
            </div>
            <div class="editor-field">
                <%= Html.DropDownList("TeamTypes", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.TeamTypeID)%>
            </div>

            <div class="editor-label">
                <%: Html.Label("Team Relegated")%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBox("InActive", false) %>
                <%: Html.ValidationMessageFor(Function(model) model.InActive) %>
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
        <%: Html.ActionLink("Back to List", "TeamIndex")%>
    </div>

</asp:Content>

