<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.PickPlayerIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<script type="text/javascript">

    $(document).ready(function () {

        if ($("#sDisabled").val() == "Disable") {
            $("#SubmitTeamSelection").addClass("divButtonHidden");
            $("#results").html("<p class=\"error\">Unfortunately you cannot change your team at this time.</span>");
            $("#PickInstText").hide();
        };

        if ($("#sDisabled").val() == "Enable") {
            $("#SubmitTeamSelection").removeClass("divButtonHidden");
            $("#results").html("");
        };

        $("#SubmitTeamSelection").click(function () {
            $("#SubmitTeamSelection").addClass("divButtonHidden");
            $("#PlayerPickerTable").hide();
            $("#PickInstText").hide();
            $('#SubmitTeam').trigger('click');
        });

    });
</script>

    <h3>Pick Players for <%: Model.Week.WeekName%></h3>

    <div id="results"></div>

    <p>This Weeks Fixtures</p>

    <div id="TWFixtures">

        <table id="FixturesTable">
            <tr>
                <th>
                    Team
                </th>
                <th>
            
                </th>
                <th>
                    Team
                </th>
            </tr>

            <% For Each oFixture In Model.Fixtures%>
               <tr>
                    <td>
                        <%: oFixture.UserTeam.Name%>
                    </td>
                    <td>
                        Vs
                    </td>
                    <td>
                        <%: oFixture.UserTeam1.Name%>
                    </td>
            
               </tr>
            <% Next oFixture%>
        </table>

    </div>

    <div id="PickInstText">
        <h4>Pick your team</h4>
        <p>To play in a 4-4-2, 4-3-3 or 5-3-2 formation</p>
    </div>

    <%  Using Ajax.BeginForm("CreateWeekUserPlayers", New AjaxOptions With {.UpdateTargetId = "results"})%>
    <%: Html.ValidationSummary(True) %>

        <table id="PlayerPickerTable">

            <tr>
                <th>
                    PlayerID
                </th>
                <th>
                    Position
                </th>
                <th>
                    Player Name
                </th>
                <th>
                    Pick status
                </th>
            </tr>

            <% For Each PlayerPickerForm In Model.FormPlayers%>
               <tr>
                    <%: Html.Hidden("PlayerIDs", PlayerPickerForm.weekUserPlayer.PlayerID)%>
                    <td>
                        <%: PlayerPickerForm.weekUserPlayer.PlayerID%>
                    </td>
                    <td>
                        <%: [enum].GetName(GetType(LaGiga.Helpers.Position), PlayerPickerForm.weekUserPlayer.Player.PositionID)%>
                    </td>
                    <td>
                        <%: PlayerPickerForm.weekUserPlayer.Player.FirstName + " " + PlayerPickerForm.weekUserPlayer.Player.Surname%>
                    </td>
                    <td>
                        <%: Html.DropDownList("StatusIDs", PlayerPickerForm.status)%>
                    </td>
               </tr>
            <% Next %>
            
        </table>
            
        <div id="SubmitTeamContainer">
            <div class="divButton">
                <a id="SubmitTeamSelection" href="#">Submit Team Selection</a>
            </div>
            <input type="submit" id="SubmitTeam" value="Submit Team Selection" style="visibility: hidden" />
        </div>

    <% end using %>

    <%: Html.Hidden("sDisabled", Model.sDisabled)%>
    <%: Html.ValidationMessage("WeekUserPlayer", ViewData("Validationmsg"))%>