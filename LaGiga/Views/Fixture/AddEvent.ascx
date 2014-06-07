<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.FixtureEditModel)" %>

<div class="AddEventForm">

<% Dim formId As string = Guid.NewGuid().ToString%>

<%  Using Html.BeginForm("CreateEvent", "Fixture")%>
        <%: Html.ValidationSummary(True) %>

            <%: Html.HiddenFor(Function(model) model.ThisEvent.EventID)%>
            
            <%: Html.Hidden("FormId", formId)%>

            <div id="EventTypeID<%: formId %>" class="AddEventType">
                <%: Html.Label("Event Type*") %>
                <%: Html.DropDownList("EventTypeID", Model.EventTypeValues, "**Please Choose**", New Object() {})%>
                <%: Html.ValidationMessageFor(Function(model) model.ThisEvent.EventTypeID)%>
            </div>
            
            <br />

            <div id="PlayerID<%: formId %>" class="AddEventPlayerID" >
                <%: Html.Label("Player*")%>
            <% If ViewData("Home") then %>
                <%: Html.DropDownList("PlayerID", Model.HomePlayerTypeValues, "**Please Choose**", New Object() {})%>
                <%: Html.ValidationMessageFor(Function(model) model.ThisEvent.PlayerID)%>
            <% Else %>
                <%: Html.DropDownList("PlayerID", Model.AwayPlayerTypeValues, "**Please Choose**", New Object() {})%>
                <%: Html.ValidationMessageFor(Function(model) model.ThisEvent.PlayerID)%>
            <% End if %>
            </div>

            <br />

            <div id="FromMinute<%: formId %>" class="AddEventFromMinute">
                <%: Html.Label("From Minute*")%>
                <%: Html.TextBox("FromMinute")%>
                <%: Html.ValidationMessageFor(Function(model) model.ThisEvent.FromMinute)%>
            </div>

            <div id="ToMinute<%: formId %>" class="AddEventToMinute">
                <%: Html.Label("To Minute")%>
                <%: Html.TextBox("ToMinute")%>
                <%: Html.ValidationMessageFor(Function(model) model.ThisEvent.ToMinute)%>
            </div>

            <%: Html.HiddenFor(Function(model) model.Fixture.FixtureID)%>

            <div class="CreateEventBtn">
                <input id="<%= ViewData("formId") %>" class="saveEvent" type="submit" value="Create" />
            </div>

    <% End Using %>

    </div>