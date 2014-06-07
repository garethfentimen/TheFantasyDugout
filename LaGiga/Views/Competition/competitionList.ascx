<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of IEnumerable (Of LaGiga.Competition))" %>
<%@ Import Namespace="LaGiga" %>

<table>
        <tr>
            <th></th>
            <th>
                Name
            </th>
            <th>
                Current Competition
            </th>
            <th>
                From Date
            </th>
            <th>
                To Date
            </th>
            <th>
                Team Type
            </th>
            <th>
                Squad Size
            </th>
            <th>
                Delete
            </th>
        </tr>

    <% For Each item In Model%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", New With {.id = item.CompetitionID})%>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.CurrentCompetition %>
            </td>
            <td>
                <%: item.FromDate %>
            </td>
            <td>
                <%: item.ToDate %>
            </td>
            <td>
                <%: item.TeamTypeID %>
            </td>
            <td>
                <%: item.SquadSize %>
            </td>
            <td class="actions delete week">
                <%= Ajax.ImageActionLink("../../Content/Delete.png", "Delete", "Delete", New With {.id = item.CompetitionID}, New AjaxOptions With {.Confirm = "Delete competition?", .HttpMethod = "Delete", .UpdateTargetId = "divCompetitionList"})%>
            </td>
        </tr>
    
    <% Next%>

</table>
