<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.EventTypeIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

    <%= Ajax.ActionLink("<< Previous", "EventTypeIndex", New With {.PageIndex = Model.PreviousPage}, New AjaxOptions With {.UpdateTargetId = "divEventTypeList"})%>

    <% For i = 1 To Model.PageCount%>
        <%= Ajax.ActionLink(i.ToString(), "EventTypeIndex", New With {.PageIndex = i}, New AjaxOptions With {.UpdateTargetId = "divEventTypeList"})%>           
    <% Next%>

    <%= Ajax.ActionLink("Next >>", "EventTypeIndex", New With {.PageIndex = Model.NextPage}, New AjaxOptions With {.UpdateTargetId = "divEventTypeList"})%>

<table>
        <tr>
            <th>
                Edit
            </th>
            <th>
                ID (Ignore)
            </th>
            <th>
                Event Name
            </th>
            <th>
                Points Awarded
            </th>
            <th>
                Position
            </th>
            <th>
                Master Event
            </th>
            <th>
                Delete
            </th>
        </tr>

    <% For Each item In Model.AllEventTypes%>
    
        <tr>
            <td>
                <a href="<%= Url.Action("Edit", "EventType", New With {.id = item.EventTypeID}) %>"><img src="../../Content/Edit.png" alt="Edit" title="Edit Event Type" /></a>
            </td>
            <td>
                <%: item.EventTypeID%>
            </td>
            <td>
                <%: item.EventName%>
            </td>
            <td>
                <%: item.Points%>
            </td>
            <td>
                <%: [enum].GetName(GetType(LaGiga.Helpers.Position), item.PositionID) %>
            </td>
            <td>
                <%: item.Master%>
            </td>
            <td class="actions delete">
                <%= Ajax.ImageActionLink("../../Content/Delete.png", "Delete", "Delete", New With {.id = item.EventTypeID, .PageIndex = 1}, New AjaxOptions With {.Confirm = "Are you sure that you wish to delete this event type?", .HttpMethod = "Delete", .UpdateTargetId = "divEventTypeList"})%>
            </td>
        </tr>
    
    <% Next%>

    </table>

