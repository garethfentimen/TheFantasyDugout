<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.EventType)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>
    <% Using Ajax.BeginForm("Create", New AjaxOptions With {.UpdateTargetId = "results"})%>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <%: Html.HiddenFor(Function(model) model.EventTypeID) %>

            <div class="editor-label">
                <%: Html.Label("Event Name")%>
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
                <%: Html.LabelFor(Function(model) model.PositionID)%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("PositionTypes", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.PositionID)%>
            </div>

            <div class="editor-label">
                <%: Html.Label("Is this a master Event? (one per event type)")%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBox("Master", False)%>
                <%: Html.ValidationMessageFor(Function(model) model.master)%>
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
        <%: Html.ActionLink("Back to List", "EventTypeIndex") %>
    </div>

</asp:Content>

