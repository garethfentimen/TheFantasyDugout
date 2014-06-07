<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(of LaGiga.FixtureEditModel)" %>
<%@ Import Namespace="LaGiga" %>

<table>
        <tr>
            <th></th>
            <th>
                EventID
            </th>
            <th>
                Event Type
            </th>
            <th>
                Player
            </th>
            <th>
                From Minute*
            </th>
            <th>
                To Minute
            </th>
            <th></th>
        </tr>

    <% For Each item In Model.Events%>
    
        <tr>

            <td>
                <%: Html.ActionLink("Edit", "EventEdit", New With {.id = item.EventID})%>
            </td>
            <th>
                <%: item.EventID%>
            </th>
            <td>
                <%: item.EventType.EventName%>
            </td>
            <td>
               <%: item.Player.FirstName & " " & item.Player.Surname%>
            </td>
            <td>
                <%: item.FromMinute%>
            </td>
            <td>
                <%: item.ToMinute%>
            </td>
            <td>
                <%: Html.ActionLink("Delete", "DeleteEvent", New With {.id = item.EventID})%>
            </td>

        </tr>
    
    <% Next%>

    </table>

