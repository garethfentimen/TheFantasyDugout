<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.WeekIndexModel)" %>
<%@ Import Namespace="LaGiga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Manage Weeks
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" >

    $(document).ready(function () {

        $("#EndWeekbtn").mouseover(function () {
            $("#EndWeekbtn").css("background-color", "Orange");
            $("#EndWeekbtn").css("cursor", "pointer");
        });

        $("#EndWeekbtn").mouseout(function () {
            $("#EndWeekbtn").css("background-color", "#e8eef4");
        });

        $("#EndWeekbtn").click(function () {
            $("#EndWeekbtn").css("background-color", "#e8eef4");
            $("#EndWeekbtn").css("cursor", "none");
            $("#EndWeekbtn").css("visibility", "hidden");
        });

    });


    function reloadTable() {

        $.post("All", { pageIndex: 1 }, function (data) {
            $("#divWeekList").html(data);
        });

    }


</script>

    <h2>Manage Weeks</h2>

    <p>
        <%: Html.ActionLink("Create New", "Create")%>
    </p>
    
    <%= Ajax.ButtonHelper("All Weeks", "WeekIndex", "AllTeamsBtn", New With {.pageIndex = 1}, New AjaxOptions With {.UpdateTargetId = "divWeekList"})%>

    <div id="divWeekList">
        <% Html.RenderPartial("WeekList", Model)%>
    </div>

    <br />
    <br />
    <%: Html.ActionLink("Back to Admin Menu","AdminIndex", "Home")%>

    <% Using (Ajax.BeginForm("SetResultTables", New AjaxOptions With {.UpdateTargetId = "setTablesResults", .OnSuccess = "reloadTable()"}))%>

    <%: Html.Hidden("WeekID", Model.CurrentWeek.WeekID) %>

    <div id ="EndWeekBorder">
    This button is to be used once all fixtures have been completed, on clicking this button the weekly results will be calculated, the season results will be updated and the current week will move forward.
    <input type="submit" id="EndWeekbtn" class="PlayerMenuBtn" value="End <%: Model.CurrentWeek.WeekName %>" />
    
        <div id = "setTablesResults" >
            &nbsp;
        </div>
    </div>

    <% End Using%>

    <%--<% Using (Ajax.BeginForm("PopulateHistoricalListData", New AjaxOptions With {.UpdateTargetId = "results2"}))%>

        <input type="submit" id="PopulateHistoricalListData" class="PlayerMenuBtn" value="Populate Historical List Data" />

        <div id="results2" style="left: 65%; position:absolute; top:150px;" >
            &nbsp;
        </div>

    <% End Using%>--%>

</asp:Content>

