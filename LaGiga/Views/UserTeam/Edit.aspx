<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.UserTeamEditIndexModel)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>

    <%  Using (Ajax.BeginForm("Save",
               New AjaxOptions With {.UpdateTargetId = "results"}))%>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-field">
                <%: Html.HiddenFor(Function(model) model.UserTeam.UserTeamID)%>
                <%: Html.HiddenFor(Function(model) model.UserTeam.UserId)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.UserTeam.Name)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.UserTeam.Name)%>
                <%: Html.ValidationMessageFor(Function(model)  model.UserTeam.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("User Group") %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(Function(model)  model.UserTeam.UserGroupID, model.UserTeam.UserGroupValues)%>
                <%: Html.ValidationMessageFor(Function(model)  model.UserTeam.UserGroupID) %>
            </div>

            <div class="editor-label">
                <%: Html.Label("Reset Password?")%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(Function(model)  model.Password)%>
                <%: Html.ValidationMessageFor(Function(model) model.Password)%>
            </div>
            
            <span>**note** the fantasy dugout uses hashed passwords - this is a one way function for the best security - the password cannot be set to a given string because there is no way of extracting the password once hashed</span>
            <br />
            <p>
                <input type="submit" value="Save" />
            </p>

        </fieldset>

        <div id = "results">
        </div>

    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "UserTeamIndex") %>
    </div>

</asp:Content>

