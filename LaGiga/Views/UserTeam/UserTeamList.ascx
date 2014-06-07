<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(of UserTeamIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<h3>User Teams</h3>

    <div class="CountsDisplay">
        Pages: <%= Model.PageCount%>
        Records: <%= Model.RecordCount%>
    </div>

<%= Ajax.ActionLink("<< Previous", "UserTeamIndex", New With {.pageIndex = Model.PreviousPage}, New AjaxOptions With {.UpdateTargetId = "divTeamList"})%>

    <% For i = 1 To Model.PageCount%>
        <%= Ajax.ActionLink(i.ToString(), "UserTeamIndex", New With {.pageIndex = i}, New AjaxOptions With {.UpdateTargetId = "divTeamList"})%>           
    <% Next%>

    <%= Ajax.ActionLink("Next >>", "UserTeamIndex", New With {.pageIndex = Model.NextPage}, New AjaxOptions With {.UpdateTargetId = "divTeamList"})%>

    <br />
<table>
        <tr>
            <th>
                User Team
            </th>
            <th>
                User Players
            </th>
            <th>
                UserTeamID
            </th>
            <th>
                UserAccountID
            </th>
            <th>
                Name
            </th>
            <th>
                User Group
            </th>
            <th>
                Delete
            </th>
        </tr>

    <% For Each item In Model.selectedUserTeams%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", New With {.id = item.UserTeamID})%>
            </td>
            <td>
                <%: Html.ActionLink("Players", "UserPlayerIndex", New With {.UserTeamID = item.UserTeamID})%> 
            </td>
            <td>
                <%: item.UserTeamID %>
            </td>
            <td>
                <%: item.UserId.tostring %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.UserGroup.Name%>
            </td>
            <td class="actions delete">
                <%= Ajax.ImageActionLink("../../Content/Delete.png", "Delete", "Delete", New With {.id = item.UserTeamID, .pageIndex = IIf(Model.PreviousPage = Nothing, 1, Model.PreviousPage - 1)}, New AjaxOptions With {.Confirm = "Delete UserTeam(not advised)?", .HttpMethod = "Delete", .UpdateTargetId = "UserTeamList"})%>
            </td>
        </tr>
    
    <% Next%>

    </table>