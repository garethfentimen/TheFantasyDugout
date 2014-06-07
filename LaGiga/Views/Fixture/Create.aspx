<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Fixture)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>
    <fieldset>
            <legend>Fixture</legend>

    <% Using Ajax.BeginForm("CreateFixture", New AjaxOptions With {.UpdateTargetId = "results"})%>
        <%: Html.ValidationSummary(True) %>

            <%: Html.HiddenFor(Function(model) model.FixtureID)%>

            <div class="WeekIDLabel">
                <%: Html.Label("Week/Round")%>
            </div>

            <div class="WeekTypeField">
                <%: Html.DropDownList("WeekTypesDD", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.WeekID) %>
            </div>

            <div class="HomeTeamIDlabel">
                <%: Html.Label("Home Team")%>
            </div>

            <div class="HomeTeamIDfield">
                <%: Html.DropDownList("HomeTeamTypesDD", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.HomeTeamID)%>
            </div>

            <div class="HomeScoreLabel">
                <%: Html.Label("Home Score")%>
            </div>

            <div class="HomeScoreField">
                <%: Html.TextBoxFor(Function(model) model.HomeScore)%>
                <%: Html.ValidationMessageFor(Function(model) model.HomeScore)%>
            </div>

            <div class = "VsField">
                <p>Vs</p>
            </div>
            
            <div class="AwayScoreLabel">
                <%: Html.Label("Away Score")%>
            </div>

            <div class="AwayScoreField">
                <%: Html.TextBoxFor(Function(model) model.AwayScore)%>
                <%: Html.ValidationMessageFor(Function(model) model.AwayScore)%>
            </div>

            <div class="AwayTeamIDlabel">
                <%: Html.Label("Away Team")%>
            </div>
            
            <div class="AwayTeamIDfield">
                <%: Html.DropDownList("AwayTeamTypesDD", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.AwayTeamID)%>
            </div>
            
            <div class="DivSubmitFixture">
                <input type="submit" value="Create Fixture" />
            </div>

            <div id = "results">
                &nbsp;
            </div>

    <% End Using %>

    </fieldset>

        <h2><%: ViewData("Message") %></h2>

    <div>
        <%: Html.ActionLink("Back to List", "FixtureIndex") %>
    </div>

</asp:Content>

