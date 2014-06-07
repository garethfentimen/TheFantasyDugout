@ModelType LaGiga.PlayerHomeEventIndexModel
@Code
    ViewBag.Title = Model.PlayerName
End Code

<script type="text/javascript">

    $(".ui-widget-overlay").on("click", function () {
        $(".ui-dialog-titlebar-close").trigger('click');
    });

    $(".ui-dialog").find(".ui-dialog-title").text($("#PlayerName").text())

    var options = {
        top: '10%',
        left: "15%"
        };
    var target = document.getElementById("PlayerBasicInfo");
    var spinner = new Spinner(options).spin(target);

    $.ajax({
            url: "TheList/GetPlayerBasicInfo",
            type: "GET",
            data: { playerId: $("#PlayerId").text() },
            dataType: "html"
        }).success(function (data) {
            $("#PlayerBasicInfo").html(data);
            spinner.stop();
        }).fail(function (jqxhr) {
            console.error("Request failed: " + jqxhr);
        });

</script>

<div id="PlayerName" style="display: none;">
    @Model.PlayerName
</div>

<div id="PlayerId" style="display: none;">
    @Model.PlayerID
</div>

<div id="PlayerBasicInfo" class="playerBasicInfo">
    &nbsp;
</div>
        
<div class="playerEventList">
    <table>
        <tr>
            <th>
                Week
            </th>
            <th>
                Minutes Played
            </th>
            <th>
                Fixture(s)
            </th>
            <th title="Goals">
                G
            </th>
            <th title="Assists">
                A
            </th>
            <th title="Clean Sheets">
                CS
            </th>
            <th title="Goals Conceded">
                GC
            </th>
            <th title="Yellow Cards">
                YC
            </th>
            <th title="Red Cards">
                RC
            </th>
            <th>
                Points
            </th>
        </tr>

    @code
        For Each item In Model.REMS
        
            If item.IsTransferREM Then
                @<tr>
                    <td>
                        Transferred from @item.FromTeam to @item.ToTeam for £@item.TransferFee million
                    </td>
                 </tr>
            Else
                @<tr>
                    <td>
                        @item.WeekName
                    </td>
                    <td>
                        @item.MinutesOnPitch
                    </td>
                    <td>
                        @item.Fixtures
                    </td>
                    <td>
                        @item.Goals
                    </td>
                    <td>
                        @item.Assists
                    </td>
                    <td>
                        @item.CleanSheets
                    </td>
                    <td>
                        @item.GoalsConceded
                    </td>
                    <td>
                        @item.YellowCards
                    </td>
                    <td>
                        @item.RedCards
                    </td>
                    <td>
                        @item.WeekPoints
                    </td>
                </tr>
            End If
        
        Next
    End Code
        

    </table>
</div>