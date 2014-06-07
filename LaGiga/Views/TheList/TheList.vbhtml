@Imports LaGiga.TheCompleteList
@ModelType LaGiga.TheCompleteList.TheListIndexModel

@Code
	ViewBag.Title = "TheList"
	Layout = "~/Views/Shared/_FrontEndLayout.vbhtml"
End Code

@section FDLogo
    <div id="divTheFantasyDugoutTitle">
		<img alt="The Fantasy Dugout" src="../../Content/thefantasydugout.jpg" title="The Fantasy Dugout"/>
	</div>
End Section
	
@section LeftContent	
		<fieldset>
			@Html.LabelFor(Function(model) model.CompetitionId)
			@Html.DropDownListFor(Function(Model) Model.CompetitionId, Model.CompetitionList)
		
			@Html.LabelFor(Function(o) Model.SearchText)
			@html.TextBoxFor(Function(o) Model.SearchText)

			@Html.LabelFor(Function(o) Model.TeamID)
			@Html.DropDownListFor(Function(o) Model.TeamID, Model.ClubTeams)

			@Html.LabelFor(Function(o) Model.PositionID)
			@Html.DropDownListFor(Function(o) Model.PositionID, Model.Positions)
            
            <div class="buttonContainer">
                <a id="resetTheList" href="#" class="button small">Reset</a>
				<a id="searchTheList" href="#" class="button small">Search</a>
			</div>
	</fieldset>
End Section

<div id="AllPlayerList">
	
	@Html.Partial("TheListPlayers", Model)
	  
</div>

<div id="PlayerEvents">
	&nbsp
</div>
		  
<script type="text/javascript">

	$(document).ready(function () {

	    $("#resetTheList").on("click", function (e) {

	        $.ajax({
	            type: "POST",
	            url: "TheList/GetTheList",
	            data: {
	                CompetitionId: 5,
	                PositionId: 0,
	                TeamId: -1
	            }
	        }).done(function (data) {
	            $("#TeamId").val(-1);
	            $("#PositionId").val(0);
	            $("#CompetitionId").val(5);
	            $("#SearchText").val("");
	            $("#AllPlayerList").html(data).attr("title", "Season Information");
	        }).fail(function (jqXHR, textStatus) {
	            console.error("Request failed: " + textStatus);
	        });

	    });

	    $("#searchTheList").on("click", function (e) {

			$.ajax({
			    type: "POST",
			    url: "TheList/GetTheList",
				data: {
				    CompetitionId: $("#CompetitionId").val(),
				    PositionId: $("#PositionId").val(),
				    TeamId: $("#TeamId").val(),
				    SearchText: $("#SearchText").val()
				}
			}).done(function (data) {
			    $("#AllPlayerList").html(data);
			}).fail(function (jqXHR, textStatus) {
				console.error("Request failed: " + textStatus);
			});

		});
	});

	$(document).on("click", ".pagination li a", function () {

	    $.ajax({
	        url: "TheList/GetTheList",
	        type: "POST",
	        data: {
	            CompetitionId: $("#CompetitionId").val(),
	            PositionId: $("#PositionId").val(),
	            TeamId: $("#TeamId").val(),
	            SearchText: $("#SearchText").val(),
	            PageIndex: this.id.split("_ ")[1]
	        },
	        dataType: "html"
	    }).success(function (data) {
	        $("#AllPlayerList").html(data);
	    }).fail(function (jqxhr) {
	        console.error("Request failed: " + jqxhr);
	    });

	});

</script>