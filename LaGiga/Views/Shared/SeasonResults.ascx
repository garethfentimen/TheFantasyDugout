<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.SeasonResultIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<h3>Season Results</h3>


<br />
<br />

<%  If Model.SeasonResult.Count > 0 Then %>

<table>
    <tr>
            <th>
                Team
            </th>
            <th>
                Won
            </th>
            <th>
                Drawn
            </th>
            <th>
                Lost
            </th>
            <th>
                Points Scored
            </th>
            <th>
                Overall Points
            </th>
        </tr>

       <% For Each item In Model.SeasonResult%>
    
        <tr>
            <td>
                <%: item.UserTeam.Name%>
            </td>
            <td>
                <%: item.Won%>
            </td>
            <td>
                <%: item.Drawn%>
            </td>
            <td>
                <%: item.Lost%>
            </td>
            <td>
                <%: item.GoalsScored%>
            </td>
            <td>
                <%: item.Points%>
            </td>
        </tr>
    
    <% Next%>
</table>

<%  Else%>
    
    <p> No Season results are available yet. </p>

<%  End If%>