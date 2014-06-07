<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(of LaGiga.RealWeekFixturesIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<script type="text/javascript">

    $(document).on("mouseover", ".fixtureHover", function () {
        $("#" + this.id + "_fixturePreview").show();
    });

    $(document).on("mouseout", ".fixtureHover", function () {
        $("#" + this.id + "_fixturePreview").hide();
    });

</script>

<div class="panel radius">
    Fixtures for Week <%: Model.WeekNo %> ( <%: Model.WeekDate %> )
</div>

<table>
    <tr>
        <th>
            
        </th>
        <th>
            
        </th>
        
        <th>
           
        </th>
        <th>
            
        </th>
    </tr>

    <% For Each item In Model.Fixtures%>
    
        <tr id="<%: item.Team1.TeamID%>" class="fixtureHover">
            <td>
                <%: item.Team1.TeamName%>
            </td>
            <td>
                <%: item.HomeScore%> 
            </td>
            <td>
                -
            </td>
            <td>
                <%: item.AwayScore%>
            </td>
            <td style="float:right; text-align:right;">
                <%: item.Team.TeamName%>
                <div id="<%: item.Team1.TeamID%>_fixturePreview" style="display:none;">
                    <a target="_blank" href="http://www.google.co.uk/search?q=<%: item.Team1.TeamName%> vs <%: item.Team.TeamName%>" >Info</a>
                </div>
            </td>

        </tr>
    
    <% Next%>

</table>
