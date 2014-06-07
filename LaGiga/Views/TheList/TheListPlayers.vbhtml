@Imports LaGiga
@ModelType LaGiga.TheCompleteList.TheListIndexModel
    
@Code
    ViewBag.Title = "The List"
End Code

<div class="panel radius" style="width: 738px;">
    <ul class="pagination">
    
    @Code
        If Model.CurrentPage > 10 Then
            @<li><a href="#" id="topage_ 1">1</a></li>
            @<li class="unavailable"><a href="#">&hellip;</a></li>
        End If
            
        If Model.CurrentPage > 1 Then
            @<li class="arrow"><a id="topage_ @Model.PreviousPage" href="#">&laquo;</a></li>    
        Else
            @<li class="arrow unavailable"><a href="#">&laquo;</a></li>
        End If
        
        For i = Model.TenPagesLowLimit To Model.TenPagesHighLimit
            If Model.CurrentPage = i Then
                @<li class="current"><a id="currentpage_ @i" href="#">@i</a></li>
            Else
                @<li><a id="topage_ @i" href="#">@i</a></li>
            End If
        Next
        
        
        If Model.CurrentPage < Model.PageCount Then
            @<li class="arrow"><a id="topage_ @Model.NextPage" href="#">&raquo;</a></li>
        Else
            @<li class="arrow unavailable"><a href="#">&raquo;</a></li>
        End If
        
        If Model.CurrentPage <> Model.PageCount Then
            @<li class="unavailable"><a href="#">&hellip;</a></li>
            @<li><a href="#" id="topage_ @Model.PageCount">@Model.PageCount</a></li>
        End If
    End Code
          
    </ul>
</div>

<table>
    <tr>
        <th>
            Name
        </th>
        <th>
            Surname
        </th>
        <th>
            Position
        </th>
        <th>
            Team
        </th>
        <th>
            Last Weeks Points
        </th>
        <th>
            Total points
        </th>
        @If Model.IsAuthenticatedUser Then
            @<th>
                Owned By
            </th>
        End If
        <th>
            History
        </th>
    </tr>

    @For Each item In Model.PlayerList
        @<tr>
            <td>
                @item.FirstName
            </td>
            <td>
                @item.Surname
            </td>
            <td>
                @item.Position
            </td>
            <td>
                @item.Team
            </td>
            <td>
                @item.LastWeeksPoints
            </td>
            <td>
                @item.TotalPoints
            </td>
            @If Model.IsAuthenticatedUser Then
                @<td>
                    @item.UserTeamName
                </td>
            end if
            <td>
                <a href="#" id="@item.PlayerID.ToString" class="showPlayerEvents"><img src="/Content/EventDetail.png" alt="See week on week breakdown" /></a>
            </td>
        </tr>
    Next

</table>

<script type="text/javascript">  

    $(".showPlayerEvents").on("click", function () {

        $.ajax({
            url: "TheList/GetEvents",
            type: "GET",
            data: {
                playerId: this.id,
                competitionId: $("#CompetitionId").val()
            },
            dataType: "html"
        }).success(function (data) {

            var height = window.screen.height - (window.screen.height / 2.2);
            var width = (window.screen.width / 1.8);

            if (width < 500) {
                width = window.screen.width - 10;
            }

            if (height < 400) {
                height = window.screen.height - 10;
                width = window.screen.width - 10;
            }

            $("#PlayerEvents").dialog({
                height: height,
                width: width,
                modal: true,
                position: "left",
                show: "fadein",
                hide: "fade",
                buttons: {
                    Close: function () {
                        $(this).dialog("close");
                    }
                }
            }).html(data);
        }).fail(function (jqxhr) {
            console.error("Request failed: " + jqxhr);
        });

    });

</script>