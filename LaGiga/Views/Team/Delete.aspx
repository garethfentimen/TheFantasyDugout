<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Team)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>
    <%  Using (Ajax.BeginForm("Delete",
                 New AjaxOptions With {.UpdateTargetId = "results"}))%>
      <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">
             <%: Html.LabelFor(Function(model) model.TeamID)%>
        </div>
        <div class="display-field">
                <%: Html.TextBoxFor(Function(model) model.TeamID)%>
        </div>
        
        <div class="display-label">
             <%: Html.LabelFor(Function(model) model.TeamName)%>
        </div>
        <div class="display-field">
                <%: Html.TextBoxFor(Function(model) model.TeamName)%>
        </div>
        
        <div class="editor-label">
             <%: Html.Label("Team Type")%>
        </div>
        <div class="editor-field">
             <%: Html.DropDownListFor(Function(model) model.TeamTypeID, Model.TeamTypeValues)%>
             <%: Html.ValidationMessageFor(Function(model) model.TeamTypeID)%>
        </div>
        
        <div class="editor-label">
                <%: Html.Label("Team Relegated")%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBox("InActive", isChecked:=Model.InActive.HasValue AndAlso Model.InActive.Value.Equals(True))%>
                <%: Html.ValidationMessageFor(Function(model) model.InActive) %>
            </div>

        <p>
            <input type="submit" value="Delete" /> |
            <%: Html.ActionLink("Back to List", "TeamIndex") %>
        </p>

        <div id = "results">
                &nbsp;
            </div>
            </fieldset>
    <% End Using %>

</asp:Content>

