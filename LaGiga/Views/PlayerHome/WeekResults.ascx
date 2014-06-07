<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of LaGiga.PlayerHomeIndexModel)" %>
<%@ Import Namespace="LaGiga" %>
<h3><%: Model.Weekname%> Results for <%: Model.UserName%></h3> 

    <table id="OppositionTotalScore" class="TotalScore">
            <th>
                Overall Total
            </th>
            <td>
              <%: Model.TotalScore%>
            </td>
    </table>

    <table>
    <tr>
            <th>
                Player
            </th>
            <th>
                Team
            </th>
            <th>
                App(s)
            </th>
            <th>
                Goals
            </th>
            <th>
                Assists
            </th>
            <th>
                Clean Sheet
            </th>
            <th>
                Goals Conceded
            </th>
            <th>
                Yellow Card
            </th>
            <th>
                Red Card
            </th>
            <th>
                Score
            </th>
        </tr>

       <%  If Model.Results Is Nothing Then%>
       <%  Else%>
       <% For Each item In Model.Results%>
    
        <tr>
            <td>
                <%: item.PlayerName%>
            </td>
            <td>
                <%: item.TeamName%>
            </td>
            <td>
                <%: item.Appearances%>
            </td>
            <td>
                <%: item.Goals%>
            </td>
            <td>
                <%: item.Assists%>
            </td>
            <td>
                <%: item.CleanSheets%>
            </td>
            <td>
                <%: item.GoalsConceded%>
            </td>
            <td>
                <%: item.Yellowcards%>
            </td>
            <td>
                <%: item.RedCards%>
            </td>
            <td>
                <%: item.Score%>
            </td>
           
        </tr>
    
    <% Next%>
    <% end if %>
    </table>