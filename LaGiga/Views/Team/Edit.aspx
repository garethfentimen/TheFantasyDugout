<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Team)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        $('#TeamID').attr('disabled', 'disabled');
    });
</script>

    <h2>Edit</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>

    <% Using (Ajax.BeginForm("Save", 
                 New AjaxOptions With {.UpdateTargetId = "results"}))%>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.TeamID) %>
            </div>
            <div class="editor-field">
                <%: Model.TeamID%>
                <%: Html.HiddenFor(Function(model) model.TeamID)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.TeamName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.TeamName) %>
                <%: Html.ValidationMessageFor(Function(model) model.TeamName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("Type of Team")%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(Function(model) model.TeamTypeID, Model.TeamTypeValues)%>
                <%: Html.ValidationMessageFor(Function(model) model.TeamTypeID)%>
            </div>

            <div class="editor-label">
                <%: Html.Label("Team Relegated (Meaning all players registered to this team are ignored by the list)")%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBox("InActive", isChecked:=Model.InActive.HasValue AndAlso Model.InActive.Value.Equals(True))%>
                <%: Html.ValidationMessageFor(Function(model) model.InActive) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>

            <div id = "results">
                &nbsp;
            </div>
        </fieldset>

    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "TeamIndex") %>
    </div>

</asp:Content>

