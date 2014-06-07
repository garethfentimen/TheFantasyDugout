<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.EventType)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>

    <% Using (Ajax.BeginForm("Save", New AjaxOptions With {.UpdateTargetId = "results"}))%>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <%: Html.HiddenFor(Function(model) model.EventTypeID)%>

            <div class="editor-label">
                <%: Html.Label("Event Name (i.e. Goal, Assist etc...")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.EventName) %>
                <%: Html.ValidationMessageFor(Function(model) model.EventName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.Points) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.Points) %>
                <%: Html.ValidationMessageFor(Function(model) model.Points) %>
            </div>

            <div class="editor-label">
                <%: Html.Label("Position") %>
            </div>

            <div class="editor-field">
                <%: Html.DropDownListFor(Function(model) model.PositionID, Model.PositionTypeValues)%>
                <%: Html.ValidationMessageFor(Function(model) model.PositionID)%>
            </div>

            <div class="editor-label">
                <%: Html.Label("Master Event?")%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBox("Master", isChecked:=Model.Master)%>
                <%: Html.ValidationMessageFor(Function(model) model.master)%>
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
        <%: Html.ActionLink("Back to List", "EventTypeIndex") %>
    </div>

</asp:Content>

