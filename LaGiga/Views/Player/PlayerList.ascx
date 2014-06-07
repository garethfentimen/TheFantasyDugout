<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.PlayerIndexmodel)" %>
<%@ Import Namespace="LaGiga" %>

    <%= Ajax.ActionLink("<< Previous", "PlayerIndex", New With {.PositionID = Model.SelectedPosition, .TeamID = Model.selectedTeam, .pageIndex = Model.PreviousPage}, New AjaxOptions With {.UpdateTargetId = "divPlayerList"})%>

    <% For i = 1 To Model.PageCount%>
        <%= Ajax.ActionLink(i.ToString(), "PlayerIndex", New With {.PositionID = Model.SelectedPosition, .TeamID = Model.selectedTeam, .pageIndex = i}, New AjaxOptions With {.UpdateTargetId = "divPlayerList"})%>           
    <% Next%>

    <%= Ajax.ActionLink("Next >>", "PlayerIndex", New With {.PositionID = Model.SelectedPosition, .TeamID = Model.selectedTeam, .pageIndex = Model.NextPage}, New AjaxOptions With {.UpdateTargetId = "divPlayerList"})%>

<table>
        <tr>
            <th></th>
            <th>
                PlayerID
            </th>
            <th>
                First Name
            </th>
            <th>
                Surname
            </th>
            <th>
                Position
            </th>
            <th>
                Club Team
            </th>
            <th>
                National Team
            </th>
            <th>
                Delete
            </th>
        </tr>

    <% For Each item In Model.selectedPlayers%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", New With {.id = item.PlayerID})%> 
            </td>
            <td>
                <%: item.PlayerID %>
            </td>
            <td>
                <%: item.FirstName %>
            </td>
            <td>
                <%: item.Surname %>
            </td>
            <td>
                <%: [enum].GetName(GetType(LaGiga.Helpers.Position), Item.PositionID)%>
            </td>
            <td>
                <%: item.Team.TeamName%>
            </td>
            <td>
                <%: item.Team1.TeamName%>
            </td>
            <td class="actions delete">
                <%= Ajax.ImageActionLink("../../Content/Delete.png", "Delete", "Delete", New With {.PlayerID = item.PlayerID, .PositionID = item.PositionID, .pageIndex = 1}, New AjaxOptions With {.Confirm = "Are you sure that you wish to delete this player?", .HttpMethod = "Delete", .UpdateTargetId = "divPlayerList"})%>
            </td>
        </tr>
    
    <% Next%>

    </table>

