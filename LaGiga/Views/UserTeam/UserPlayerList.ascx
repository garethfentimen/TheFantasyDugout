<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(of UserPlayerIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<h3><%: Model.UserTeam.Name%>'s Players</h3>
<p>Players: <%: Model.RecordCount %></p>

<table>
        <tr>
            <th>
                User Player ID
            </th>
            <th>
                User Team
            </th>
            <th>
                Position
            </th>
            <th>
                Player Name
            </th>
            <th>
                User Group
            </th>
            <th>
                Remove from team
            </th>
        </tr>

    <% For Each item In Model.selectedUserPlayers%>
    
        <tr>
            <td>
                <%: item.UserPlayerID %>
            </td>
            <td>

            </td>
            <td>
                <%: [enum].GetName(GetType(LaGiga.Helpers.Position), item.Player.PositionID) %>
            </td>
            <td>
                <%: item.Player.FirstName & " " & item.Player.Surname%>
            </td>
            <td>
                <%: item.UserGroup.Name%>
            </td>
            <td class="actions delete">
                <%= Ajax.ImageActionLink("../../Content/Delete.png", "Remove", "Delete", New With {.id = item.UserPlayerID, .competitionID = Model.CompetitionID, .typeID = 2, .pageIndex = IIf(Model.PreviousPage = Nothing, 1, Model.PreviousPage - 1)}, New AjaxOptions With {.Confirm = "Remove Player from Team?", .HttpMethod = "Delete", .UpdateTargetId = "UserPlayerList"})%>
            </td>
        </tr>
    
    <% Next%>

    </table>