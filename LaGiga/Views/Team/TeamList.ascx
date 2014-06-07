<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.TeamIndexmodel)" %>
<%@ Import Namespace="LaGiga" %>
<h3>Teams</h3>
    <%= Ajax.ActionLink("<< Previous", "TeamIndex", New With {.TeamTypeID = Model.SelectedTeamType, .pageIndex = Model.PreviousPage}, New AjaxOptions With {.UpdateTargetId = "divTeamList"})%>

    <% For i = 1 To Model.PageCount%>
        <%= Ajax.ActionLink(i.ToString(), "TeamIndex", New With {.TeamTypeID = Model.SelectedTeamType, .pageIndex = i}, New AjaxOptions With {.UpdateTargetId = "divTeamList"})%>           
    <% Next%>

    <%= Ajax.ActionLink("Next >>", "TeamIndex", New With {.TeamTypeID = Model.SelectedTeamType, .pageIndex = Model.NextPage}, New AjaxOptions With {.UpdateTargetId = "divTeamList"})%>
    
    <br />
    <br />
    <table>
        <tr>
            <th>
                Edit Team
            </th>
            <th>
                TeamID
            </th>
            <th>
                Team Name
            </th>
            <th>
                Type of Team
            </th>
            <th>
                Team Relegated
            </th>
            <th>
                Delete
            </th>
        </tr>

    <% For Each item In Model.selectedTeams%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", New With {.id = item.TeamID})%>
            </td>
            <td>
                <%: item.TeamID %>
            </td>
            <td>
                <%: item.TeamName %>
            </td>
            <td>
                <%: [Enum].GetName(GetType(Helpers.TeamType),item.TeamTypeID) %>
            </td>
            <td>
                <%: IIf(item.InActive.HasValue andalso item.InActive.Value.Equals(True), "Relegated", "")%>
            </td>
            <td class="actions delete">
                <%= Ajax.ImageActionLink("../../Content/Delete.png", "Delete", "Delete", New With {.id = item.TeamID, .TeamTypeID = item.TeamTypeID, .pageIndex = 1}, New AjaxOptions With {.Confirm = "Delete team?", .HttpMethod = "Delete", .UpdateTargetId = "divTeamList"})%>
            </td>
        </tr>
    
    <% Next%>

    </table>