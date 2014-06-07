<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.FixtureEditModel)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<script type="text/javascript" >

    var g_playerEventId;

    $(document).ready(function () {


        $("#HomeEventsBtn").toggle(function () {
            $("#HomeEventForm").hide("slow");
            $("#CreateEvent").hide("slow");
        }, function () {
            $("#HomeEventForm").show("faster");
            $("#CreateEvent").show("slow");
        });


        $("#AwayEventsBtn").toggle(function () {
            $("#AwayEventForm").hide("slow");
            $("#AwayCreateEvent1").hide("slow");
        }, function () {
            $("#AwayEventForm").show("faster");
            $("#AwayCreateEvent1").show("slow");
        });

        $("#saveFixture").click(function () {

            $("#saveFixtureForm").submit(function (result) {

                setTimeout(function () {
                    $("#divUserFeedback").hide("slow");
                }, 3000);

            });

        });

        $(".AddEventForm form").live("submit", function (event) {
            event.preventDefault();
            var form = $(this);

            WriteEventVidiPrint(form, form.context.FormId.value);
        });

    });

    function WriteEventVidiPrint(form, formId) {

        var playerEventId = $("#PlayerID" + formId + " select").val() + "_" + $("#EventTypeID" + formId + " select option:selected").val();
        if (!g_playerEventId || g_playerEventId !== playerEventId) {

            // only do this if this is not a direct duplicate

            $.ajax({
                url: form.attr('action'),
                type: "POST",
                data: form.serialize(),
                success: function (data) {
                    //$.validator.unobtrusive.parse("form");
                    $("#addEventResult").html(data);
                    $("#addEventResult").show();
                    HideAfterDelay();

                    var playerName = $("#PlayerID" + formId + " select option:selected").text();
                    var playerPosition = playerName.substr(playerName.length - 2, playerName.length);
                    playerName = playerName.substr(0, playerName.length - 2);
                    var commaIndex = playerName.indexOf(", ");
                    playerName = playerName.substr(commaIndex + 2, playerName.length) + playerName.substr(0, commaIndex) + " " + playerPosition;

                    var detail = "*** Event: " + $("#EventTypeID" + formId + " select option:selected").text() + " for " + playerName
                                + " from " + $("#FromMinute" + formId + " input").val() + " to " + $("#ToMinute" + formId + " input").val() + " ... </br>";

                    $("#EventDetails").html(detail + $("#EventDetails").html());

                    // set global player Id and Event Type ID to avoid duplicates appearing due to using class (used on both forms) as click handler
                    g_playerEventId = $("#PlayerID" + formId + " select").val() + "_" + $("#EventTypeID" + formId + " select option:selected").val();

                },
                error: function (jqXhr, textStatus, errorThrown) {
                    $("#addEventResult").html("<b>Event was not added - something went wrong - Error '" + jqXhr.status + "' (textStatus: '" + textStatus + "', errorThrown: '" + errorThrown + "')");
                    $("#addEventResult").show();
                    HideAfterDelay();
                }
            });

        }
        else {
            $("#addEventResult").html("<b>Event was not added - Duplicate event with same event type and player selected.</b>");
            $("#addEventResult").show();
            HideAfterDelay();
        }

    }

    function HideAfterDelay() {
        setTimeout(function () {
            $("#addEventResult").hide("slow");
        }, 3000);
    }

  </script>

    <h2>Edit</h2>

    <div id="FixturesForm">

        <%-- The following line works around an ASP.NET compiler warning --%>
        <%: ""%>
        <fieldset>
        <legend>Edit Fixture Score</legend>
    
        <% Using (Ajax.BeginForm("Save", "Fixture", New AjaxOptions With {.UpdateTargetId = "results"}, New With { .id="saveFixtureForm" } ))%>
                 <%: Html.ValidationSummary(True) %>
            
            <div class="HiddenFixtureID">
                <%: Html.HiddenFor(Function(model) model.Fixture.FixtureID)%>
            </div>
            
            <div id="FixtureWeekID">
                <%: Html.Label("Week:")%>
                <%: Html.DropDownListFor(Function(model) model.Fixture.WeekID, Model.Fixture.WeekTypeValues, New with { .disabled = "disabled" } )%>
                <%: Html.ValidationMessageFor(Function(model) model.Fixture.WeekID)%>
            </div>
            
            <div id="FixtureTeamID">
                <%: Html.Label("Home Team")%>
                <%: Html.DropDownListFor(Function(model) model.Fixture.HomeTeamID, Model.Fixture.HomeTeamTypeValues, New with { .disabled = "disabled" } )%>
                <%: Html.ValidationMessageFor(Function(model) model.Fixture.HomeTeamID)%>
            </div>

            <div id="FixtureHomeScore">
                <%: Html.TextBoxFor(Function(model) model.Fixture.HomeScore)%>
                <%: Html.ValidationMessageFor(Function(model) model.Fixture.HomeScore)%>
            </div>

            <span>Vs</span>
            
            <div id="FixtureAwayScore">

                <%: Html.TextBoxFor(Function(model) model.Fixture.AwayScore)%>
                <%: Html.ValidationMessageFor(Function(model) model.Fixture.AwayScore)%>
            </div>

            <div id="FixtureAwayTeamID">
                <%: Html.DropDownListFor(Function(model) model.Fixture.AwayTeamID, Model.Fixture.AwayTeamTypeValues, New With {.disabled = "disabled"})%>
                <%: Html.ValidationMessageFor(Function(model) model.Fixture.AwayTeamID)%>
                <%: Html.Label("Away Team")%>
            </div>
            
            <div id="FixtureSubmit">
                <input id="saveFixture" type="submit" value="Save Fixture" />
            </div>

            <div id="results">
                &nbsp;
            </div>
            
        <% End Using %>
        </fieldset>
    </div>
                 
            <p>
                <input type="button" id="HomeEventsBtn" value="Home events->>"/>
                <input type="button" id="AwayEventsBtn" value="Away events->>"/>
            </p>
               
            <div id="HomeEventForm">
                <fieldset>
                    <legend>Add event for the Home Team</legend>

                    <% ViewData("Home") = New System.Web.Mvc.ViewDataDictionary %>
                    <% ViewData("Home").Add("Home", True) %>
                    <% Html.RenderPartial("AddEvent", Model, ViewData("Home"))%>

                </fieldset>
            </div>

            <div id="AwayEventForm">
                <fieldset>
                    <legend>Add event for the Away Team</legend>

                        <% ViewData("Home") = New System.Web.Mvc.ViewDataDictionary %>
                        <% ViewData("Home").Add("Home", False)%>
                        <% Html.RenderPartial("AddEvent", Model, ViewData("Home"))%>

                </fieldset>
            </div> 

    <div id="addEventResult">
        &nbsp;
    </div>

    <div id="vidiPrinter">

        <div id="divVidiTitle">Event Vidi Printer</div>
        <div id="EventDetails">
            
        </div>

    </div>

    <div class="divBackToBtn">
        <%: Html.ActionLink("Back to List", "FixtureIndex") %>
    </div>

</asp:Content>

