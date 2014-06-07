<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Event)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>
    <% Using Html.BeginForm()%>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.EventID) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.EventID) %>
                <%: Html.ValidationMessageFor(Function(model) model.EventID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.EventTypeID) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.EventTypeID) %>
                <%: Html.ValidationMessageFor(Function(model) model.EventTypeID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.PlayerID) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.PlayerID) %>
                <%: Html.ValidationMessageFor(Function(model) model.PlayerID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.FixtureID) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.FixtureID) %>
                <%: Html.ValidationMessageFor(Function(model) model.FixtureID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.FromMinute)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.FromMinute)%>
                <%: Html.ValidationMessageFor(Function(model) model.FromMinute) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

