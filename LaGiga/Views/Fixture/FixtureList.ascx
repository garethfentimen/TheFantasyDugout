<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(of LaGiga.FixtureIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<div class="PagingControls">

    <%= Ajax.ActionLink("<< Previous", "FixtureIndex", New With {.CompetitionID = Model.selectedCompetition, .pageIndex = Model.PreviousPage}, New AjaxOptions With {.UpdateTargetId = "divFixtureList"})%>

    <% For i = 1 To Model.PageCount%>
        <%= Ajax.ActionLink(i.ToString(), "FixtureIndex", New With {.CompetitionID = Model.selectedCompetition, .pageIndex = i}, New AjaxOptions With {.UpdateTargetId = "divFixtureList"})%>           
    <% Next%>

    <%= Ajax.ActionLink("Next >>", "FixtureIndex", New With {.CompetitionID = Model.selectedCompetition, .pageIndex = Model.NextPage}, New AjaxOptions With {.UpdateTargetId = "divFixtureList"})%>

</div>

<table>
        <tr>
            <th>
                Events
            </th>
            <th>
                Fixture
            </th>
            <th>
                FixtureID
            </th>
            <th>
                Competition
            </th>
            <th>
                Week
            </th>
            <th>
                Home Team
            </th>
            <th>
                Home Score
            </th>
            <th>
                Away Team
            </th>
            <th>
                Away Score
            </th>
            <th>
                Delete
            </th>
        </tr>

    <% For Each item In Model.selectedFixtures%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Create/Edit", "Edit", New With {.id = item.FixtureID})%>
            </td>
            <td>
                <%: Html.ActionLink("View Events", "ViewFixture", New With {.id = item.FixtureID})%>
            </td>
            <td>
                <%: item.FixtureID %>
            </td>
            <td>
               <%: item.Week.Competition.Name %>
            </td>
            <td>
                <%: item.Week.WeekName %>
            </td>
            <td>
                <%: item.Team1.TeamName%>
            </td>
            <td>
                <%: item.HomeScore%>
            </td>
            <td>
                <%: item.AwayScore%>
            </td>
            <td>
                <%: item.Team.TeamName%>
            </td>
            <td>
                <%= Ajax.ImageActionLink("../../Content/Delete.png", "Delete", "Delete", New With {.FixtureID = item.FixtureID, .CompetitionID = item.Week.CompetitionID, .pageIndex = 1}, New AjaxOptions With {.Confirm = "Are you sure that you wish to delete this fixture?", .HttpMethod = "Delete", .UpdateTargetId = "divFixtureList"})%>
            </td>
        </tr>
    
    <% Next%>

    </table>