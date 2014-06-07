<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage(Of LaGiga.Week)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
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

    <h2>Edit</h2>

    <%-- The following line works around an ASP.NET compiler warning --%>
    <%: ""%>

    <% Using (Ajax.BeginForm("Save", New AjaxOptions With {.UpdateTargetId = "results"}))%>
        
        <%: Html.ValidationSummary(True) %>
        <fieldset>
            <legend>Fields</legend>
            
                <%: Html.HiddenFor(Function(model) model.WeekID) %>
            
            <div class="editor-label">
                <%: Html.Label("Week Description") %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.WeekName) %>
                <%: Html.ValidationMessageFor(Function(model) model.WeekName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("Competition") %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(Function(model) model.CompetitionID, Model.CompetitionTypeValues)%>
                <%: Html.ValidationMessageFor(Function(model) model.CompetitionID) %>
            </div>

            <div class="editor-label">
                <%: Html.Label("Is this the Current Week?")%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(Function(model) model.CurrentWeek)%>
                <%: Html.ValidationMessageFor(Function(model) model.CurrentWeek)%>
            </div>
            
            <div class="editor-label">
                <%: Html.Label("Week Number for the competition")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.WeekNo)%>
                <%: Html.ValidationMessageFor(Function(model) model.WeekNo)%>
            </div>

            <div class="editor-label">
                <%: Html.Label("Start Date")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.FromDate)%>
                <%: Html.ValidationMessageFor(Function(model) model.FromDate)%>
            </div>

            <div class="editor-label">
                <%: Html.Label("End Date")%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(Function(model) model.ToDate)%>
                <%: Html.ValidationMessageFor(Function(model) model.ToDate)%>
            </div>

            <p>
                <input type="submit" value="Save" />
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

