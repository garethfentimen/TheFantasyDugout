<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Week)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Week
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

    <script type="text/javascript" >

        $(document).ready(function () {
            $("#FromDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#ToDate").datepicker({ dateFormat: 'dd/mm/yy' });
        });

    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>
     <% Using (Ajax.BeginForm("Create", 
                 New AjaxOptions With {.UpdateTargetId = "results"}))%>
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.Label("Week Name/Description")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.WeekName)%>
                <%: Html.ValidationMessageFor(Function(model) model.WeekName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(Function(model) model.CompetitionID)%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("CompetitionTypes", "**Please Choose**")%>
                <%: Html.ValidationMessageFor(Function(model) model.CompetitionID) %>
            </div>

             <%: Html.HiddenFor(Function(model) model.CurrentWeek)%>

            <div class="editor-label">
                <%: Html.Label("Week Number for the competition")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.WeekNo)%>
                <%: Html.ValidationMessageFor(Function(model) model.WeekNo)%>
            </div>

            <div class="editor-label">
                <%: Html.Label("Start Date (if left blank defaults to today)")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.FromDate)%>
                <%: Html.ValidationMessageFor(Function(model) model.FromDate)%>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
            <div id = "results">
                &nbsp;
            </div>
        </fieldset>

    <% End Using %>

    <div>
        <%: Html.ActionLink("Back to List", "WeekIndex") %>
    </div>

</asp:Content>

