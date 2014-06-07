<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Event)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EventEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EventEdit</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>

    <% Using (Ajax.BeginForm("SaveEvent", New AjaxOptions With {.UpdateTargetId = "results1"}))%>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <%: Html.HiddenFor(Function(model) model.EventID)%>
            
            <div class="editor-label">
                <%: Html.Label("Event Type*")%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(Function(model) model.EventTypeID, Model.EventTypeValues)%>
                <%: Html.ValidationMessageFor(Function(model) model.EventTypeID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("Player*")%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(Function(model) model.PlayerID, Model.PlayerDDValues)%>
                <%: Html.ValidationMessageFor(Function(model) model.PlayerID) %>
            </div>

            <div class="editor-label">
                <%: Html.Label("From Minute or Event Minute*")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.FromMinute)%>
                <%: Html.ValidationMessageFor(Function(model) model.FromMinute)%>
            </div>

            <div class="editor-label">
                <%: Html.Label("To Minute - for appearance/substitution data")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.ToMinute)%>
                <%: Html.ValidationMessageFor(Function(model) model.ToMinute)%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.Points)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.Points)%>
                <%: Html.ValidationMessageFor(Function(model) model.Points)%>
            </div>

            <%: Html.HiddenFor(Function(model) model.FixtureID) %>
            
            <p>
                <input type="submit" value="Save" />
            </p>

            <div id = "results1">
                &nbsp;
            </div>

        </fieldset>

        
    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "FixtureIndex") %>
    </div>

</asp:Content>

