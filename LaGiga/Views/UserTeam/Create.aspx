<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.UserTeam)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>
    <%  Using (Ajax.BeginForm("Create",
               New AjaxOptions With {.UpdateTargetId = "results"}))%>

        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-field">
                <%: Html.HiddenFor(Function(model) model.UserTeamID)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.UserId) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.UserId) %>
                <%: Html.ValidationMessageFor(Function(model) model.UserId)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.Name) %>
                <%: Html.ValidationMessageFor(Function(model) model.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("User Group") %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("UserGroupDD", "**Please choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.UserGroupID) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
            <div id = "results">
            </div>
        </fieldset>

    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "UserTeamIndex")%>
    </div>

</asp:Content>

