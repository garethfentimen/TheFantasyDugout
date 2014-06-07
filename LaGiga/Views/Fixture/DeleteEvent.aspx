<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Event)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DeleteEvent
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>DeleteEvent</h2>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="displayEventlabel">EventID</div>
        <div class="displayEventfield"><%: Model.EventID%></div>
        <% Html.HiddenFor(Function(model) model.EventID)%>
        <br />
        <div class="displayEventlabel">Event Type</div>
        <div class="displayEventfield"><%: Model.EventType.EventName%></div>
        <% Html.HiddenFor(Function(model) model.EventTypeID)%>
        <br />
        <div class="displayEventlabel">Player</div>
        <div class="displayEventfield"><%: Model.Player.FirstName & " " & Model.Player.Surname%></div>
        <br />
        <div class="displayEventlabel">FixtureID</div>
        <div class="displayEventfield"><%: model.FixtureID %></div>
        <br />
        <div class="displayEventlabel">From Minute</div>
        <div class="displayEventfield"><%: Model.FromMinute%></div>
        <br />
        <div class="displayEventlabel">To Minute</div>
        <div class="displayEventfield"><%: Model.ToMinute%></div>
        <br />
        <div class="displayEventlabel">Points</div>
        <div class="displayEventfield"><%: Model.Points %></div>
        
    </fieldset>
    <% Using Html.BeginForm(New With {.EventID = Model.EventID})%>
        <p>
            <input type="submit" value="DeleteEvent" /> |
            <%: Html.ActionLink("Back to List", "FixtureIndex") %>
        </p>
    <% End Using %>

</asp:Content>

