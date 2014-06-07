<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.WeekIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

    Current Week is set to:  WeekID: <%= IIf(IsNothing(Model.CurrentWeek), "None Set", Model.CurrentWeek.WeekID)%> - <%= IIf(IsNothing(Model.CurrentWeek), "None Set", Model.CurrentWeek.WeekName)%>
    <br />
    <%= Ajax.ActionLink("<< Previous", "WeekIndex", New With {.pageIndex = Model.PreviousPage}, New AjaxOptions With {.UpdateTargetId = "divWeekList"})%>

    <% For i = 1 To Model.PageCount%>
        <%= Ajax.ActionLink(i.ToString(), "WeekIndex", New With {.pageIndex = i}, New AjaxOptions With {.UpdateTargetId = "divWeekList"})%>           
    <% Next%>

    <%= Ajax.ActionLink("Next >>", "WeekIndex", New With {.pageIndex = Model.NextPage}, New AjaxOptions With {.UpdateTargetId = "divWeekList"})%>

    <table>
        <tr>
            <th></th>
            <th>
                WeekNo
            </th>
            <th>
                Week Name i.e. PRM Week 1
            </th>
            <th>
                Competition
            </th>
            <th>
                From Date
            </th>
            <th>
                To Date
            </th>
            <th>
                Current
            </th>
            <th>
                Delete
            </th>
        </tr>

    <% For Each item In Model.selectedWeeks%>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", New With {.id = item.WeekID}, New AjaxOptions With {.UpdateTargetId = "divWeekList"})%>
            </td>
            <td>
                <%: item.WeekNo%>
            </td>
            <td>
                <%: item.WeekName %>
            </td>
            <td>
                <%: item.Competition.Name %>
            </td>
            <td>
                <%: item.FromDate %>
            </td>
            <td>
                <%: item.ToDate %>
            </td>
            <td>
                <%: IIf(item.CurrentWeek, "yes", "") %>
            </td>
            <td class="actions delete week">
                <%= Ajax.ImageActionLink("../../Content/Delete.png", "Delete", "Delete", New With {.id = item.WeekID, .PageIndex = 1}, New AjaxOptions With {.Confirm = "Delete team?", .HttpMethod = "Delete", .UpdateTargetId = "divWeekList"})%>
            </td>
        </tr>
    
    <% Next%>

    </table>
