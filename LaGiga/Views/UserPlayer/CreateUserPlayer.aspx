<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.UserPlayerIndexModel)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CreateUserPlayer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <script type="text/javascript">

        $(document).ready(function () {
            $("#ClubTeamID").change(function () {

                $.get("UserPlayer/GetSecondList", { id: $("#ClubTeamID").val() }, function (data) {
                    $("#SecondListContainer").html(data);
                });

            });
        });

    </script>

    <h2>Create User Player</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>

     <%  Using Ajax.BeginForm("CreateUserPlayer", New AjaxOptions With {.UpdateTargetId = "results"})%>
        <%: Html.ValidationSummary(True) %>
             <fieldset>
            <legend>Fields</legend>

            <div class="editor-label">
                <%: Html.Label("Select Team")%>
            </div>
            
            <%: Html.DropDownList("ClubTeamID", Model.ClubTeams)%>
            <br />
            <br />
            <%: Html.Label("Select Player")%>
            <br />
            <div id="SecondListContainer">
            &nbsp;<br />
            </div>


            <br />

            <%: Html.HiddenFor(Function(model) model.UserTeamID, Model.UserTeamID)%>
            <%: Html.HiddenFor(Function(model) model.UserGroupID, Model.UserGroupID) %>
            <%: Html.HiddenFor(Function(model) model.CompetitionID, Model.CompetitionID) %>

            <p>
                <input type="submit" value="Add Player" />
            </p>

            <div id = "results">
            </div>
            </fieldset>
        <% End Using %>

    <div>
        <%: Html.ActionLink("Back User Team list", "UserTeamIndex", "UserTeam") %>
    </div>

</asp:Content>

