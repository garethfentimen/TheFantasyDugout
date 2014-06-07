<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.EventType)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>
<%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>

    <% Using Html.BeginForm() %>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.EventTypeID) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.EventTypeID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.EventName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.EventName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.Points) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.Points) %>
            </div>
            
            <p>
                <input type="submit" value="Delete" />
            </p>
        </fieldset>

    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "EventTypeIndex") %>
    </div>

</asp:Content>

